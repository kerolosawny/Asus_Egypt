
namespace AsusDashbord.Controllers
{
    [Authorize(Roles = "Admin")]

    public class ProductController : Controller
    {
        private readonly IproductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ICategoryService _subCategoryService;
        private readonly ISpecificationKeyService _specificationKeyService;
        private readonly IDiscountService _discountService;
        private readonly IMapper _mapper;
        private string key = "Products";
        public ProductController(
            IproductService productService,
            ICategoryService categoryService,
            ICategoryService subCategoryService,
            ISpecificationKeyService specificationKeyService,
            IDiscountService discountService, IMapper mapper)
        {
            _productService = productService;
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
            _specificationKeyService = specificationKeyService;
            _discountService = discountService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int PageSize = 15)
        {
            if (ProductService.AllElementNumber == 0)
            {
                _productService.Intialize();
            }
            int TotalPage = (int)Math.Ceiling((double)ProductService.AllElementNumber / PageSize);
            var cachedData = _productService.GetFromCache<List<ProductDTO>>(key);
            if (pageNumber >= 1 && pageNumber <= TotalPage)
            {
                if (cachedData != null)
                {
                    var PagnetedCachMemory = cachedData.Skip((pageNumber - 1) * PageSize).Take(PageSize).ToList();
                    if (PagnetedCachMemory.Count == 0)
                    {
                        var UpdatedProductList = await _productService.GetAllAsync(pageNumber, PageSize);
                        _productService.AddtoCashMemoery(key, UpdatedProductList.Data);
                        return View(UpdatedProductList);
                    }
                    return View(new EntityPaginated<ProductDTO>() { Data = PagnetedCachMemory, CurrentPage = pageNumber, ItemsPerPage = PageSize, TotalPages = TotalPage });
                }
                var allProduct = await _productService.GetAllAsync(pageNumber, PageSize);

                _productService.AddToCache(key, allProduct.Data, TimeSpan.FromMinutes(30));
                cachedData = allProduct.Data;
            }
            return View(new EntityPaginated<ProductDTO>() { Data = cachedData, CurrentPage = pageNumber, ItemsPerPage = PageSize, TotalPages = TotalPage });
        }

        public async Task<IActionResult> PartialIndex(string id)
        {
            var cachedData = _productService.GetFromCache<List<ProductDTO>>(key);

            if (cachedData != null)
            {
                if (!string.IsNullOrEmpty(id))
                {
                    cachedData = cachedData.Where(p =>
                               p.Name.IndexOf(id, StringComparison.OrdinalIgnoreCase) >= 0 ||
                               p.Name_EN.IndexOf(id, StringComparison.OrdinalIgnoreCase) >= 0 ||
                               p.Price.ToString().Contains(id) ||
                               p.Code!.ToString()!.Contains(id) ||
                               p.Unit_Instock.ToString().Contains(id)).ToList();

                    if (cachedData.Count == 0)
                    {
                        var data = await _productService.GetSortedFilterAsync(p => p.Name_EN, p => p.Name.Contains(id) || p.Name_EN.Contains(id) || p.price.ToString().Contains(id) || p.Code!.ToString()!.Contains(id) || p.Unit_Instock.ToString().Contains(id));
                        //if no data back return empty list
                        if (data.ToList().Count == 0)
                            return PartialView(nameof(PartialIndex), new List<ProductDTO>());
                        var newData = _mapper.Map<List<ProductDTO>>(await data.ToListAsync());
                        return PartialView(newData);
                    }
                    else
                        return PartialView(cachedData);
                }
                cachedData = cachedData.Take(15).ToList();
            }
            return PartialView(cachedData);
        }

        public IActionResult Details(int id)
        {

            var cachedData = _productService.GetFromCache<List<ProductDTO>>(key);

            return  PartialView(cachedData.Where(p => p.Id == id).FirstOrDefault());
        }

        public async Task<IActionResult> Create()
        {         
                var categories = await _categoryService.GetAllAsync();
            ViewBag.Categories = categories.Where(c => c.ParentCategoryID != null);
            ViewBag.Discounts = (await _discountService.GetAllAsync()).Where(p=>p.IsActive);
            ViewBag.Specifications = await _specificationKeyService.GetAllSpecificationKeysAsync();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductDTO product)
        {
            if (ModelState.IsValid)
            {
                var StruserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (StruserId != null && int.TryParse(StruserId, out int userId))
                    product.CreatedBy = userId;
                    var result = await _productService.CreateAsync(product);
                if (result.IsSuccess)
                {

                    TempData["SuccessMessageCreate"] = "Product created successfully!";
                    return RedirectToAction(nameof(Index));
                }


            }

            var categories = await _categoryService.GetAllAsync();
            ViewBag.Categories = categories.Where(c => c.ParentCategoryID != null);

            var subCategories = await _subCategoryService.GetAllAsync();
            ViewBag.Discounts = await _discountService.GetAllAsync();
            ViewBag.Specifications = await _specificationKeyService.GetAllSpecificationKeysAsync();

            return View(product);
        }


        [HttpGet]
        public async Task<JsonResult> GetSubCategories(int id)
        {
            var subCategories = await _categoryService.GetSubCategoriesByCategoryIdAsync(id);
            return Json(subCategories);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetOneWithDetails(id);
            if (product == null)
            {
                return NotFound();
            }
            UpdateProductDTO productDTO =new UpdateProductDTO(product);
            
            productDTO.CategoryId = product.Product_Category!=null? product.Product_Category.Select(c => c.CategoryId).FirstOrDefault():0;
            productDTO.Image = product.image_url;
            productDTO.Discounts = product.Discount!=null? product.Discount.Select(d => d.DiscountId).ToList():new List<int> {0};

            var categories = await _categoryService.GetAllAsync();
            ViewBag.Categories = categories.Where(c => c.ParentCategoryID != null);

            var subCategories = await _subCategoryService.GetAllAsync();
            ViewBag.SubCategory = subCategories.Where(s => s.ParentCategoryID != null);

            ViewBag.Discounts = (await _discountService.GetAllAsync()).Where(p => p.IsActive);
            ViewBag.Specifications = await _specificationKeyService.GetAllSpecificationKeysAsync();

            return View(productDTO);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateProductDTO product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var StruserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (StruserId != null && int.TryParse(StruserId, out int userId))
                    product.Updated_By = userId;
                var result = await _productService.UpdateAsync(product);
                if (result.IsSuccess)
                {
                     _productService.RemoveFromCashMemoery(key, await _productService.GetOneAsync(product.Id));
                    _productService.AddtoCashMemoery(key, new List<ProductDTO> { new ProductDTO(result.Entity) });
                    TempData["SuccessMessageUpdate"] = "Product Updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                TempData["FailMessageUpdate"] = "Failed to Update Product !";
            }
            var categories = await _categoryService.GetAllAsync();
            ViewBag.Categories = categories.Where(c => c.ParentCategoryID != null); 
            ViewBag.Discounts = await _discountService.GetAllAsync();
            ViewBag.Specifications = await _specificationKeyService.GetAllSpecificationKeysAsync();

            return View(product);
        }
        public async Task<IActionResult> Delete(int Id)
        {
            _productService.RemoveFromCashMemoery(key, await _productService.GetOneAsync(Id));

            var del = await _productService.DeleteAsync(Id);
            if (del)
            {
                TempData["SuccessMessageDelete"] = "Product deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Error occurred while deleting the Product.";
            }
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> AddImages(int id)
        {
            var productDto = await _productService.GetOneAsync(id);

            if (productDto == null)
            {
                return NotFound();
            }
            var addImagesDto = new AddImagesDTO
            {
                Id = productDto.Id,
                ExistingImages = productDto.ImageURLs.ToList()
            };

            return View(addImagesDto);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteImage(string imageUrl, int productId)
        {
            await _productService.RemoveImageFromProductAsync(productId, imageUrl);
            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> AddImages(int id, AddImagesDTO model)
        {
            var product = await _productService.GetOneAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            if (model.Images != null && model.Images.Any())
            {
                var result = await _productService.AddImagesAsync(model);

                if (result.IsSuccess)
                {
                    TempData["SuccessMessage"] = "Images added successfully!";
                    return RedirectToAction("Edit", new { id = product.Id });
                }
                else
                {
                    ModelState.AddModelError("", result.Massage);
                }
            }
            else
            {
                ModelState.AddModelError("", "Please upload at least one image.");
            }

            return View(model);
        }


        public async Task<IActionResult> AddSubCategories(int id)
        {
            var product = await _productService.GetProductWithCategoriesAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var allCategories = await _categoryService.GetAllAsync();
            //////set update her to remember : product.Product_Category!=null &&
            var parentCategories = allCategories
                .Where(c => !c.ParentCategoryID.HasValue &&
                           product.Product_Category!=null && product.Product_Category.Any(pc => pc.CategoryId == c.Id))
                .ToList();

            List<CategoryDTO> subCategories = new List<CategoryDTO>();

            foreach (var parent in parentCategories)
            {
                var children = allCategories
                    .Where(c => c.ParentCategoryID == parent.Id)
                    .ToList();

                subCategories.AddRange(children);
            }

            return View(subCategories);
        }
        [HttpPost]
        public async Task<IActionResult> AddSubCategories(int id, List<int> subCategoryIds)
        {
            var result = await _productService.AddSubCategoriesAsync(id, subCategoryIds);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Massage);
                return View(result.Entity);
            }

            return RedirectToAction("Edit", new { id });
        }


        [HttpGet]
        public async Task<IActionResult> AddSpecifications(int id)
        {
            var specs = await _productService.GetProductSpecificationsByIdAsync(id);
            var model = new AddSpecsDTO();
            if (specs != null)
            {
                var specifications = new List<ProductSpecificationDTO>();
                foreach (var item in specs)
                {
                    specifications.Add(new ProductSpecificationDTO { Name = item.Key, SpecificationKeyId = item.Id,
                        Value=item.ProductSpecifications!=null? item.ProductSpecifications.Where(p=>p.SpecificationKeyId==item.Id)
                        .Select(p=>p.Value).FirstOrDefault():string.Empty });
                }
                model.ProductSpecifications = specifications;
                model.Id = id;
                return View(model);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddSpecifications(AddSpecsDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.SaveProductSpecificationsAsync(model);

                if (result.IsSuccess)
                {
                    return RedirectToAction("Edit", new { id = model.Id });
                }

                ModelState.AddModelError("", result.Massage);
            }

            return View(model);
        }

        public async Task<IActionResult> IsProductNameAvailable(string name)
        {
            var exists = await _productService.NameExistsAsync(name);
            return Json(exists ? $"Product with name '{name}' already exists." : (object)true);
        }
        [AcceptVerbs("Get", "Post")]

        public async Task<IActionResult> IsProductNameENAvailable(string nameEN)
        {
            var exists = await _productService.NameENExistsAsync(nameEN);
            return Json(exists ? $"Product with name '{nameEN}' already exists." : (object)true);
        }

        //public async Task<IActionResult> Delete(int id)
        //{
        //    var product = await _productService.GetOneAsync(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(product);
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var success = await _productService.DeleteAsync(id);
        //    if (success)
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View();
        //}

    }
}
