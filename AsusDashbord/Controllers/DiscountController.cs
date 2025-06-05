namespace AsusDashbord.Controllers
{
    [Authorize(Roles = "Admin")]

    public class DiscountController : Controller
    {
        private readonly IDiscountService discountService;
        private readonly IMapper mapper;

        public DiscountController(IDiscountService _discountService, IMapper _mapper)
        {
            discountService = _discountService;
            mapper = _mapper;

        }

        public IActionResult Index()
        {
            try
            {
                var discounts = discountService.GetAllAsync();
                return View(discounts);

            }
            catch (Exception ex)
            {

                return View(ex.Message);
            }

        }
        public IActionResult CreateDiscount()
        {
            CreateDiscountDTO createDiscountDTO = new CreateDiscountDTO
            {
                Name = string.Empty,
                Name_EN = string.Empty,
            };

            return PartialView(nameof(CreateDiscount), createDiscountDTO);
        }
        [HttpPost]
        public async Task<IActionResult> CreateDiscount(CreateDiscountDTO model)
        {
            if (ModelState.IsValid)
            {

                var result = await discountService.CreateAsync(model);
                if (result.IsSuccess)
                {
                    TempData["SuccessMessageCreate"] = "Discount created successfully!";
                    return RedirectToAction(nameof(Index));
                }

            }

            return PartialView(nameof(CreateDiscount), model);
        }
        public async Task<IActionResult> UpdateDiscount(int id)
        {
            var discount = await discountService.GetByIdAsync(id);
            var model = mapper.Map<UpdateDiscountDTO>(discount);
            return PartialView("UpdateDiscount", model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDiscount(UpdateDiscountDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await discountService.UpdateAsync(model);
                if (result.IsSuccess)
                {
                    TempData["SuccessMessageUpdate"] = "Discount Updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
            }

            return PartialView("UpdateDiscount", model);
        }
        public async Task<IActionResult> Delete(int Id)
        {
            bool deleted = await discountService.DeleteAsync(Id);
            if (deleted)
            {
                TempData["SuccessMessageDelete"] = "Discount deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Error occurred while deleting the discount.";
            }
            return RedirectToAction(nameof(Index));
        }

        //remote validations
        [AcceptVerbs("Get", "Post")]

        public async Task<IActionResult> IsDiscountNameAvailable(string name)
        {
            var exists = await discountService.NameExistsAsync(name);
            return Json(exists ? $"Discount with name '{name}' already exists." : (object)true);
        }
        [AcceptVerbs("Get", "Post")]

        public async Task<IActionResult> IsDiscountNameENAvailable(string nameEN)
        {
            var exists = await discountService.NameENExistsAsync(nameEN);
            return Json(exists ? $"Discount with name '{nameEN}' already exists." : (object)true);
        }

    }
}
