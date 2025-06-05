namespace AsusDashbord.Controllers
{
    [Authorize(Roles = "Admin")]

    public class PaymentMethodController : Controller
    {
        private readonly IPaymentMethodService paymentMethodService;
        private readonly ICard_TypeService cardTypeService;
        private readonly IMapper mapper;

        public PaymentMethodController(IPaymentMethodService _paymentMethodService, ICard_TypeService _cardTypeService, IMapper _mapper)
        {
            paymentMethodService = _paymentMethodService;
            cardTypeService = _cardTypeService;
            mapper = _mapper;
        }
        public async Task<IActionResult> Index()
        {
            var paymentMethods = await paymentMethodService.GetAllAsync();
            return View(paymentMethods);
        }

        public IActionResult Create()
        {

            var paymentMethod = new CreatePaymentMethodsDTO
            {
                Name = string.Empty,
                Expiration_Date = DateOnly.FromDateTime(DateTime.Today),
                Is_Default = false,
                Card_TypeId = 0,
            };

            return PartialView(nameof(Create), paymentMethod);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePaymentMethodsDTO model)
        {
            if (ModelState.IsValid)
            {

                var result = await paymentMethodService.CreateAsync(model);
                if (result.IsSuccess)
                {
                    TempData["SuccessMessageCreate"] = "Payment method created successfully!";
                    return RedirectToAction(nameof(Index));
                }

            }

            return PartialView(nameof(Create), model);
        }
        public async Task<IActionResult> Update(int id)
        {
            var existingPaymentMethod = await paymentMethodService.GetByIdAsync(id);

            if (existingPaymentMethod == null)
            {
                return NotFound();
            }
            var updatepayment = mapper.Map<UpdatePaymentMethodsDTO>(existingPaymentMethod);         
            return PartialView(nameof(Update), updatepayment);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdatePaymentMethodsDTO model)
        {
            if (ModelState.IsValid)
            {
                var res = await paymentMethodService.UpdateAsync(model);
                if (res.IsSuccess)
                {
                    TempData["SuccessMessageUpdate"] = "Payment method Updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
            }
            return PartialView(nameof(Update), model);
        }
        public async Task<IActionResult> Delete(int Id)
        {
            var del = await paymentMethodService.DeleteAsync(Id);
            if (del)

            {
                TempData["SuccessMessageDelete"] = "Payment method deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Error occurred while deleting the payment method.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
