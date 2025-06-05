
namespace AsusDashbord.Controllers
{
    [Authorize(Roles = "Admin")]

    public class CategoryController : Controller
    {
        private readonly ICategoryService CategoryService;
        private readonly IMapper mapper;

        public CategoryController(ICategoryService _CategoryService, IMapper _mapper)
        {
            CategoryService = _CategoryService;
            mapper = _mapper;
        }

        public async Task<IActionResult> GetALLCategories()
        {
            var Categories = await CategoryService.GetAllAsync();
            foreach(var cat in Categories) 
            {
                if (cat.ParentCategoryID.HasValue)
                {
                    cat.ChildCategories = (await CategoryService.GetSubCategoriesByCategoryIdAsync(cat.ParentCategoryID.Value)).ToList();
                }

            }

            return View(Categories);
        }
        public IActionResult CreateCategory()
        {

            return PartialView(nameof(CreateCategory));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await CategoryService.CreateAsync(model);
                if (result.IsSuccess)
                {
                    TempData["SuccessMessageCreate"] = "Category created successfully!";
                    return RedirectToAction(nameof(GetALLCategories));
                }
               
            }

            return PartialView(nameof(CreateCategory), model);
        }

        public async Task<IActionResult> UpdateCategory(int id)
        {
            var ID = await CategoryService.GetByIdAsync(id);
            var cat = new UpdateCategoryDTO
            {
                Name = ID.Name,
                Name_EN = ID.Name_EN,
                Id = ID.Id,
                ParentCategoryId = ID.ParentCategoryID,
            };
 
            return PartialView(nameof(UpdateCategory), cat);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await CategoryService.UpdateAsync(model);
                if (result.IsSuccess)
                {
                    TempData["SuccessMessageUpdate"] = "Category Updated successfully!";
                    return RedirectToAction(nameof(GetALLCategories));
                }
            }

            return PartialView(nameof(UpdateCategory), model);
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            bool isDeleted = await CategoryService.DeleteAsync(id);
            if (isDeleted)
            {
                TempData["SuccessMessageDelete"] = "Category deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Can't delete this category it's has sub category related delete them before.";
            }

            return RedirectToAction(nameof(GetALLCategories));
        }

        public IActionResult CreateSubCategory(int id)
        {
            ViewBag.ParentCategoryId = id; 
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubCategory(CreateCategoryDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await CategoryService.CreateAsync(model);
                if (result.IsSuccess)
                {
                    TempData["SuccessMessageSub"] = "Subcategory added successfully!";

                    return RedirectToAction(nameof(GetALLCategories));
                }
            }
            return PartialView(nameof(CreateSubCategory), model);
        }

        public async Task<IActionResult> UpdateSubCategory(int id)
        {
            var subCategory = await CategoryService.GetByIdAsync(id);
            var cat = new UpdateCategoryDTO
            {
                Name = subCategory.Name,
                Name_EN = subCategory.Name_EN,
                Id = subCategory.Id,
                ParentCategoryId = subCategory.ParentCategoryID,
            };
            ViewBag.Category=(await CategoryService.GetAllAsync()).Where(p=>p.ParentCategoryID is null);
            return PartialView(nameof(UpdateSubCategory), cat);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSubCategory(UpdateCategoryDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await CategoryService.UpdateAsync(model);
                if (result.IsSuccess)
                {
                    TempData["SuccessMessageUpdate"] = "Category Updated successfully!";
                    return RedirectToAction(nameof(GetALLCategories));
                }
            }
            return PartialView(nameof(UpdateSubCategory), model);
        }

        public async Task<IActionResult> DeleteSubCategory(int id)
        {
            bool isDeleted = await CategoryService.DeleteAsync(id);
            if (isDeleted)
            {
                TempData["SuccessMessageDelete"] = "Category deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Error Ouccerd on deleteing sub category.";
            }
            return RedirectToAction(nameof(GetALLCategories));
            
        }


        //remote validation
        [AcceptVerbs("Get", "Post")]

        public async Task<IActionResult> IsCategoryNameAvailable(string name)
        {
            var exists = await CategoryService.NameExistsAsync(name);
            return Json(exists ? $"Category with name '{name}' already exists." : (object)true);
        }
        [AcceptVerbs("Get", "Post")]

        public async Task<IActionResult> IsCategoryNameENAvailable(string nameEN)
        {
            var exists = await CategoryService.NameENExistsAsync(nameEN);
            return Json(exists ? $"Category with name '{nameEN}' already exists." : (object)true);
        }

    }
}
