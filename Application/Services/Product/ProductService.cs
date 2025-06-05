

namespace Applications.Services
{
    public class ProductService : IproductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        public static int AllElementNumber { get; set; }
        private readonly IMemoryCache _memoryCache;
        private readonly IDiscountRepository _discountRepository;
        private string key = "Products";
        private readonly UserManager<User> _userManager;
        public ProductService(IProductRepository productRepository, IMapper mapper, ICategoryRepository categoryRepository, IDiscountRepository discountRepository, IMemoryCache memoryCache, UserManager<User> userManager)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _memoryCache = memoryCache;
            _discountRepository = discountRepository;
            _userManager = userManager;
        }
        public void Intialize()
        {
            AllElementNumber = _productRepository.EntityCount().Result;
        }

        public async Task<ResultView<CreateProductDTO>> CreateAsync(CreateProductDTO entity)
        {
            ResultView<CreateProductDTO> result = new();

            try
            {
                #region for price after discount
                decimal discountPrice = 0;
                if (entity.Discounts is not null && entity.Discounts.Count() > 0)
                {
                    foreach (int discountId in entity.Discounts)
                    {
                        var percintage = (await _discountRepository.GetOneAsync(discountId)).Percentage;
                        discountPrice += (percintage / 100) * entity.Price;
                    }
                }
                #endregion
                Product product = new()
                {
                    Name = entity.Name,
                    Name_EN = entity.Name_EN,
                    description = entity.Description,
                    description_EN = entity.Description_EN,
                    price = entity.Price,
                    Unit_Instock = entity.Unit_Instock,
                    IsActive = true,
                    image_url = string.Empty,
                    Code = (await _productRepository.MaxCode()) + 1,
                    priceAfterDiscount = entity.Price - discountPrice,
                    CreatedBy = entity.CreatedBy
                };
                if (entity.ImageData != null)
                {
                    var fileName = Guid.NewGuid() + "_" + entity.ImageData.FileName;

                    string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products");

                    Directory.CreateDirectory(uploadFolder);
                    string filePath = Path.Combine(uploadFolder, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await entity.ImageData.CopyToAsync(fileStream);
                    }

                    product.image_url = "/images/products/" + fileName;
                }

                var createdProduct = await _productRepository.CreateAsync(product);

                await _productRepository.SaveChangesAsync();

                //Adding discounts to the Prodct
                if (entity.Discounts is not null && entity.Discounts.Count() > 0)
                {

                    foreach (int dis in entity.Discounts)
                    {
                        await _productRepository.AddProductDiscountAsync(new Product_Discount { ProductId = createdProduct.Id, DiscountId = dis });
                    }
                }

                //Adding Sub Categoty To Product

                await _productRepository.AddProductCategoryAsync(new Product_Categoty { ProductId = createdProduct.Id, CategoryId = entity.SubCategoryId });

                AddtoCashMemoery(key, new List<ProductDTO> {
                        new ProductDTO(){Name=createdProduct.Name,Name_EN=createdProduct.Name_EN,
                            Price=createdProduct.price,Unit_Instock=createdProduct.Unit_Instock,
                            image_url=createdProduct.image_url ,Id=createdProduct.Id,Code=createdProduct.Code}});

                var returnProduct = _mapper.Map<CreateProductDTO>(createdProduct);

                result = new ResultView<CreateProductDTO>
                {
                    Entity = returnProduct,
                    IsSuccess = true,
                    Massage = "Product Created Successfully"
                };
                Intialize();
                return result;
            }
            catch (Exception ex)
            {
                result = new ResultView<CreateProductDTO>
                {
                    IsSuccess = false,
                    Massage = "Error Occurred: " + ex.Message
                };
                return result;
            }
        }
        public async Task<ResultView<AddImagesDTO>> AddImagesAsync(AddImagesDTO dto)
        {
            ResultView<AddImagesDTO> result = new();

            try
            {
                var product = await _productRepository.GetOneAsync(dto.Id);
                if (product == null)
                {
                    result.IsSuccess = false;
                    result.Massage = "Product not found.";
                    return result;
                }

                if (dto.Images != null && dto.Images.Count != 0)
                {
                    var productImages = new List<Product_Image>();

                    foreach (var imageFile in dto.Images)
                    {
                        var fileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;

                        string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products");

                        Directory.CreateDirectory(uploadFolder);

                        string filePath = Path.Combine(uploadFolder, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }

                        var productImage = new Product_Image
                        {
                            image_url = $"/images/products/{fileName}",
                            ProductId = product.Id,
                            Product = product
                        };

                        productImages.Add(productImage);
                    }

                    foreach (var image in productImages)
                    {
                        if(product.Images!= null)
                        product.Images.Add(image);
                    }
                    await _productRepository.SaveChangesAsync();

                    result.Entity = dto;
                    result.IsSuccess = true;
                    result.Massage = "Images added successfully.";
                }
                else
                {
                    result.IsSuccess = false;
                    result.Massage = "No images to upload.";
                }

                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Massage = "Error occurred: " + ex.Message;
                return result;
            }
        }

        public async Task<List<ProductDTO>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            var productDtos = await products.Select(p => new ProductDTO(p)).ToListAsync();
            return productDtos;
        }

        public async Task<IQueryable<Product>> GetSortedFilterAsync<TKey>(Expression<Func<Product, TKey>> orderBy, Expression<Func<Product, bool>> searchPredicate = null, bool ascending = true)
        {
            return await _productRepository.GetSortedFilterAsync(orderBy, searchPredicate, ascending);
        }
        // all product pagented with details
        public async Task<EntityPaginated<ProductDTO>> GetAllAsync(int PageNumber = 1, int Pagesize = 15)
        {
            if (AllElementNumber == 0)
            {
                Intialize();
            }
            var Allproducts = await _productRepository.GetAllAsync(PageNumber, Pagesize);
            var discounts = await _discountRepository.GetAllAsync();


            EntityPaginated<ProductDTO> paginatedProduct = new()
            {
                CurrentPage = PageNumber,
                ItemsPerPage = Pagesize,
                TotalPages = (int)Math.Ceiling((double)AllElementNumber / Pagesize),
                Data = (await Allproducts.ToListAsync()).Select(p => new ProductDTO(p)).ToList()

            };
            return paginatedProduct;
        }
        public async Task<ProductDTO> GetOneAsync(int id)
        {
            var product = await _productRepository.GetOneAsync(id);
            return new ProductDTO(product);
        }

        public async Task RemoveImageFromProductAsync(int productId, string imageUrl)
        {
            var product = await _productRepository.GetOneAsync(productId);
            if (product != null)
            {
                var imageToRemove = product.Images!=null? product.Images.FirstOrDefault(img => img.image_url == imageUrl):null;
                if (imageToRemove != null)
                {
                    string rootPath = Directory.GetCurrentDirectory();
                    string filePath = Path.Combine(rootPath, "wwwroot", imageToRemove.image_url.TrimStart('/'));

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    if(product.Images!=null) 
                    product.Images.Remove(imageToRemove);

                    await _productRepository.SaveChangesAsync();
                }
            }
        }


        public async Task<ResultView<string>> UpdateProductPriceAsync(UpdateProductPrice entity)
        {
            ResultView<string> result = new();

            try
            {
                var product = await _productRepository.GetOneAsync(entity.ProductId);

                if (product == null)
                {
                    result.IsSuccess = false;
                    result.Massage = "Product not found";
                    return result;
                }
                if (!entity.IsSubtract)
                {
                    product.Unit_Instock = (product.Unit_Instock + entity.quantity);
                }
                else
                {
                    if ((product.Unit_Instock - entity.quantity) >= 0)
                    {
                        product.Unit_Instock = (product.Unit_Instock - entity.quantity);
                    }
                }
                await _productRepository.UpdateAsync(product);
                result = new ResultView<string>
                {
                    Entity = string.Empty,
                    IsSuccess = true,
                    Massage = "Product Price updated successfully"
                };
                return result;
            }
            catch (Exception ex)
            {
                result = new ResultView<string>
                {
                    IsSuccess = false,
                    Massage = "Error Occurred: " + ex.Message,
                    Entity = string.Empty
                };
                return result;
            }
        }

        //==============================
        public async Task<ResultView<UpdateProductDTO>> UpdateAsync(UpdateProductDTO entity)
        {
            ResultView<UpdateProductDTO> result = new();
            try
            {
                var product = await _productRepository.GetOneAsync(entity.Id);
                if (product == null)
                {
                    result.IsSuccess = false;
                    result.Massage = "Product not found";
                    return result;
                }
                #region Image 
                if (entity.ImageD != null)
                {
                    if (!string.IsNullOrEmpty(product.image_url))
                    {
                        var oldFilePath = Path.Combine("wwwroot", product.image_url.TrimStart('/'));
                        if (File.Exists(oldFilePath))
                        {
                            File.Delete(oldFilePath);
                        }
                    }

                    var fileName = Guid.NewGuid() + Path.GetExtension(entity.ImageD.FileName);
                    var filePath = Path.Combine("wwwroot/images/products", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await entity.ImageD.CopyToAsync(stream);
                    }

                    product.image_url = $"/images/products/{fileName}";
                }
                #endregion

                #region Category
                if (entity.CategoryId > 0)
                {

                    var existingProductCategories = product.Product_Category!=null? product.Product_Category.ToList():new List<Product_Categoty>();
                    foreach (var existingCategory in existingProductCategories)
                    {
                        if (existingCategory.CategoryId != entity.CategoryId)
                        {
                            await _productRepository.RemoveProductCategory(existingCategory);
                        }
                    }

                    await _productRepository.AddProductCategoryAsync(
                        new Product_Categoty
                        {
                            ProductId = entity.Id,
                            CategoryId = entity.CategoryId
                        }
                    );
                }
                #endregion

                #region Discount
                decimal discountPrice = 0;
                var existingDiscounts = product.Discount!=null? product.Discount.ToList():new List<Product_Discount>();

                foreach (var existingDiscount in existingDiscounts)
                {
                    await _productRepository.RemoveProductDiscount(existingDiscount);
                }
                if (entity.Discounts != null)
                {
                    foreach (var discountId in entity.Discounts)
                    {
                        var newDiscount = new Product_Discount
                        {
                            ProductId = product.Id,
                            DiscountId = discountId
                        };
                        await _productRepository.AddProductDiscountAsync(newDiscount);
                        var percintage = (await _discountRepository.GetOneAsync(discountId)).Percentage;
                        discountPrice += (percintage / 100) * entity.Price ?? 0;
                    }
                }//1000
                #endregion
                #region Mapping
                product.price = entity.Price ?? 0m;
                product.Name = entity.Name!;
                product.Name_EN = entity.Name_EN!;
                product.description = entity.Description;
                product.description_EN = entity.Description_EN;
                product.Unit_Instock = entity.Unit_Instock ?? 0;
                product.Updated_at = DateTime.Now;
                product.priceAfterDiscount = product.price - discountPrice;
                product.Updated_By = entity.Updated_By;
                product.IsActive=entity.Is_Active;
                // product.Updated_By= _userManager.GetUserId(User);
                #endregion
                var prd = await _productRepository.UpdateAsync(product);
                await _productRepository.SaveChangesAsync();
                var updatedProductDto = new UpdateProductDTO(prd);
                result = new ResultView<UpdateProductDTO>
                {
                    Entity = updatedProductDto,
                    IsSuccess = true,
                    Massage = "Product updated successfully"
                };
                return result;
            }
            catch (Exception ex)
            {
                result = new ResultView<UpdateProductDTO>
                {
                    IsSuccess = false,
                    Massage = "Error Occurred: " + ex.Message
                };
                return result;
            }
        }
        // Fetch products by category
        public async Task<List<ProductDTO>> GetProductsByCategoryIdAsync(int categoryId)
        {
            var products = await _productRepository.GetProductsByCategoryIdAsync(categoryId).Result.ToListAsync();
            var productDtos = products.Select(product => new ProductDTO(product)).ToList();
            return productDtos;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var product = await _productRepository.GetOneAsync(id);
                if (product == null) return false;

                // Remove associated ProductSpecifications before deleting the product
                if (product.ProductSpecifications != null)
                    foreach (var specification in product.ProductSpecifications)
                    {
                        await _productRepository.DeleteSpecification(specification);
                    }
                if (!string.IsNullOrEmpty(product.image_url))
                {
                    var oldFilePath = Path.Combine("wwwroot", product.image_url.TrimStart('/'));
                    if (File.Exists(oldFilePath))
                    {
                        File.Delete(oldFilePath);
                    }
                }
                await _productRepository.DeleteAsync(product);
                await _productRepository.SaveChangesAsync();
                Intialize();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<ResultView<ProductDTO>> AddSubCategoriesAsync(int productId, List<int> subCategoryIds)
        {
            ResultView<ProductDTO> result = new();

            try
            {
                // Fetch the product by ID
                var product = await _productRepository.GetOneAsync(productId);
                if (product == null)
                {
                    result.IsSuccess = false;
                    result.Massage = "Product not found.";
                    return result;
                }
                var productWithCategory = product.Product_Category;
                List<Product_Categoty> existingProductCategories = new();
                if (productWithCategory != null)
                    existingProductCategories = productWithCategory.ToList();

                var parentCategoryIds = existingProductCategories
                    .Where(pc => subCategoryIds.Contains(pc.CategoryId))
                    .Select(pc => pc.Category!.ParentCategoryID)
                    .Distinct()
                    .ToList();

                foreach (var existingCategory in existingProductCategories)
                {
                    if (subCategoryIds.Contains(existingCategory.CategoryId) &&
                        !parentCategoryIds.Contains(existingCategory.CategoryId))
                    {
                        await _productRepository.RemoveProductCategory(existingCategory);
                    }
                }

                foreach (var subCategoryId in subCategoryIds)
                {
                    if (!existingProductCategories.Any(pc => pc.CategoryId == subCategoryId))
                    {
                        var productCategory = new Product_Categoty
                        {
                            ProductId = productId,
                            CategoryId = subCategoryId
                        };

                        await _productRepository.AddProductCategoryAsync(productCategory);
                    }
                }

                await _productRepository.SaveChangesAsync();

                result.Entity = _mapper.Map<ProductDTO>(product);
                result.IsSuccess = true;
                result.Massage = "Subcategories updated successfully.";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Massage = "Error occurred: " + ex.Message;
            }

            return result;
        }

        public async Task Createspecifications(ICollection<ProductSpecificationDTO> entity, int ProductId)
        {
            foreach (var spec in entity)
            {
                var productSpecification = new ProductSpecification
                {
                    SpecificationKeyId = spec.SpecificationKeyId,
                    Value = spec.Value ?? string.Empty,
                    ProductId = ProductId
                };
                await _productRepository.CreateSpecificationAsync(productSpecification);
            }
            await _productRepository.SaveChangesAsync();
        }

        public async Task<ResultView<List<ProductSpecificationDTO>>> SaveProductSpecificationsAsync(AddSpecsDTO model)
        {
            ResultView<List<ProductSpecificationDTO>> result = new();
            try
            {
                var product = await _productRepository.GetOneAsync(model.Id);
                if (product == null)
                {
                    result.IsSuccess = false;
                    result.Massage = "Product not found.";
                    return result;
                }
                List<ProductSpecification> specificationsToDelete = product.ProductSpecifications != null ? product.ProductSpecifications.ToList() : new List<ProductSpecification>();
                // var specificationsToDelete = product.ProductSpecifications.ToList();

                foreach (var spec in specificationsToDelete)
                {
                    await _productRepository.DeleteSpecification(spec);
                }

                //List<ProductSpecificationDTO> specificationDtos = new();
                foreach (var specDto in model.ProductSpecifications)
                {
                    var newSpecification = new ProductSpecification
                    {
                        ProductId = model.Id,
                        SpecificationKeyId = specDto.SpecificationKeyId,
                        Value = specDto.Value ?? string.Empty
                    };
                    await _productRepository.CreateSpecificationAsync(newSpecification);

                    //specificationDtos.Add(new ProductSpecificationDTO
                    //{
                    //    SpecificationKeyId = newSpecification.SpecificationKeyId,
                    //    Name = newSpecification.SpecificationKey?.Key ?? "Unknown Specification", // Handle null case
                    //    Value = newSpecification.Value
                    //});
                }

                await _productRepository.SaveChangesAsync();
                result.IsSuccess = true;
                result.Massage = "Specifications saved successfully.";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Massage = "Error: " + ex.Message;
            }

            return result;
        }


        //Add sub categories

        public async Task<Product> GetProductWithCategoriesAsync(int id)
        {
            return await _productRepository.GetOneAsync(id);
        }


        public async Task<List<SpecificationKey>> GetProductSpecificationsByIdAsync(int productId)
        {
            var product = await _productRepository.GetOneAsync(productId);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }
            if (product.Product_Category != null)
            {

                var CatID = product.Product_Category.Select(c => c.CategoryId);
                var Specs = await _categoryRepository.GetCategorySpecificationKeysByCategoryIdAsync(CatID.FirstOrDefault());
                return Specs.ToList();
            }
            else
            {
                return null;
            }




        }

        public async Task<Product> GetOneWithDetails(int id)
        {
            var prd = await _productRepository.GetOneAsync(id);

            return prd;
        }

        //for remote validation
        public async Task<bool> NameExistsAsync(string name)
        {
            return await _productRepository.ExistsAsync(name);
        }

        public async Task<bool> NameENExistsAsync(string nameEN)
        {
            return await _productRepository.ExistsAsync(nameEN, true);
        }



        public async Task<List<ProductDTO>> GetProductBySpecificationValues(List<string> specificationValues)
        {
            var productsQuery = await _productRepository.GetSortedFilterAsync(
                p => p.Name,
                p => specificationValues.Any(specValue => p.ProductSpecifications!=null&& p.ProductSpecifications.Any(ps => ps.Value == specValue))
            );

            var products = productsQuery.ToList();

            return products.Select(p => new ProductDTO(p)).ToList();
        }







        //====================================================
        //****************************************************
        // for cash memory
        public void AddToCache(string key, object value, TimeSpan? absoluteExpiration = null)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpiration
            };

            _memoryCache.Set(key, value, cacheEntryOptions);
        }

        public T GetFromCache<T>(string key)
        {
            _memoryCache.TryGetValue(key, out T value);
            return value;
        }
        public List<ProductDTO> AddtoCashMemoery(string key, List<ProductDTO> items)
        {
            var DataIncash = GetFromCache<List<ProductDTO>>(key);
            if (DataIncash != null && DataIncash.Count > 0)
            {
                foreach (var item in items)
                {
                    if (!DataIncash.Any(existingItem => existingItem.Id == item.Id))
                    {
                        DataIncash.Insert(0, item);
                    }
                }
                AddToCache(key, DataIncash, TimeSpan.FromMinutes(30));
                return DataIncash;
            }
            else
            {
                AddToCache(key, items, TimeSpan.FromMinutes(30));
                return items;
            }

        }
        public void RemoveFromCashMemoery(string key, ProductDTO item)
        {
            var DataIncash = GetFromCache<List<ProductDTO>>(key);
            if (DataIncash != null && DataIncash.Count > 0)
            {
                if (DataIncash.Where(existingItem => existingItem.Id == item.Id).ToList().Count == 1)
                {
                    var data = DataIncash.FirstOrDefault(p => p.Id == item.Id);
                    if (data != null)
                    {
                        DataIncash.Remove(data);
                        AddToCache(key, DataIncash, TimeSpan.FromMinutes(30));
                    }
                }
            }
        }
        public void RemoveFromCache(string key)
        {
            _memoryCache.Remove(key);
        }


    }



}
