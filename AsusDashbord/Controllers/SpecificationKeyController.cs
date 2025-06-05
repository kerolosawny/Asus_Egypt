namespace AsusDashbord.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SpecificationKeyController : Controller
    {
        private readonly ISpecificationKeyService specificationKeyService;
        private readonly ICategoryService CategoryService;
        private readonly IMapper _mapper;
        public SpecificationKeyController(ISpecificationKeyService _specificationKeyService, IMapper mapper, ICategoryService _CategoryService )
        {
            specificationKeyService = _specificationKeyService;
            CategoryService = _CategoryService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var specsKeys = await specificationKeyService.GetAllSpecificationKeysAsync();

            foreach (var spec in specsKeys)
            {              
                    var category = await CategoryService.GetCategoriesBySpecificationKeyAsync(spec.Id);
                    if (category != null && category.Any())
                    {
                        spec.SubCategoryName = string.Join(",",category.Select(p=>p.Name_EN)); 
                    }              
            }
            return View(specsKeys);
        }

        public async Task<IActionResult> Create()
        {
            CreateSpecificationKeyDTO spec = new CreateSpecificationKeyDTO
            {
                Key = string.Empty,
                Key_Ar=string.Empty,
            };
            var categories = await CategoryService.GetAllAsync();
            ViewBag.Categories = categories.Where(c => c.ParentCategoryID != null).ToList();
            return PartialView(spec);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSpecificationKeyDTO spec)
        {
            if(ModelState.IsValid)
            {

                var result = await specificationKeyService.CreateAsync(spec);
                if (result.IsSuccess)
                {
                    TempData["SuccessMessageCreate"] = "Specification Key created successfully!";
                    return RedirectToAction(nameof(Index));
                }

            }
             var categories = await CategoryService.GetAllAsync();
            ViewBag.Categories = categories.Where(c => c.ParentCategoryID != null).ToList();
            return PartialView(nameof(Create), spec);
        }

        public async Task<IActionResult> Update(int id)
        {
            var specificationKey = await specificationKeyService.GetByIdAsync(id);

            UpdateSpecificationKeyDTO updateSpec = new UpdateSpecificationKeyDTO
            {
                Id = specificationKey.Id,
                Key = specificationKey.Key,
                Key_Ar = specificationKey.Key_Ar,
                SubCategoryId = specificationKey.SubCategoryId 
            };
            var categories = await CategoryService.GetAllAsync();
            ViewBag.Categories = categories.Where(c => c.ParentCategoryID != null).ToList();

            return View(updateSpec);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateSpecificationKeyDTO updateSpec)
        {
            if (ModelState.IsValid)
            {
                var result = await specificationKeyService.UpdateAsync(updateSpec);
                if (result.IsSuccess)
                {
                    TempData["SuccessMessageUpdate"] = "Specification Key updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
            }

            var categories = await CategoryService.GetAllAsync();
            ViewBag.Categories = categories.Where(c => c.ParentCategoryID != null).ToList();
            return View(updateSpec);
        }



        public async Task<IActionResult> Delete(int Id)
        {
            bool deleted = await specificationKeyService.DeleteAsync(Id);
            if (deleted)
            {
                TempData["SuccessMessageDelete"] = "Specification deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Error occurred while deleting the Specification key.";
            }
            return RedirectToAction(nameof(Index));
        }

        [AcceptVerbs("Get", "Post")]

        public async Task<IActionResult> IsSpecNameAvailable(string Key)
        {
            var exists = await specificationKeyService.NameExistsAsync(Key);
            return Json(exists ? $"Specification with name '{Key}' already exists." : (object)true);
        }


    }
}
