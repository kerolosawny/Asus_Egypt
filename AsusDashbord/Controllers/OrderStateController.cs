namespace AsusDashbord.Controllers
{
         [Authorize(Roles ="Admin")]
    public class OrderStateController : Controller
    {
        private readonly IOrder_StateService order_StateService;
        private readonly IMapper mapper;

        public OrderStateController(IOrder_StateService _order_StateService , IMapper _mapper)
        {
            order_StateService = _order_StateService;
            mapper = _mapper;
        }

        public async Task<IActionResult> Index()
        {
            var OrdState = await order_StateService.GetAllAsync();
            return View("GetAllOrdState", OrdState);         
        }

        public async Task<IActionResult> CreateOrdState(int id = 0)
        {
            CreateOrder_StateDTO orderStateDto;

            if (id == 0) // For create
            {
                orderStateDto = new CreateOrder_StateDTO
                {
                    Name = string.Empty,
                    Name_EN = string.Empty,
                };
            }
            else // For update
            {
                Order_StateDTO orderState = await order_StateService.GetByIdAsync(id);
                if (orderState == null)
                {
                    return NotFound();
                }
                orderStateDto = mapper.Map<CreateOrder_StateDTO>(orderState);
            }

            return PartialView(nameof(CreateOrdState), orderStateDto); 
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrdState(CreateOrder_StateDTO createOrder_StateDTO)
        {
            if (ModelState.IsValid)
            {
                if (createOrder_StateDTO.Id > 0) 
                {
                    // Check if the entity exists
                    Order_StateDTO existingOrdState = await order_StateService.GetByIdAsync(createOrder_StateDTO.Id);
                    if (existingOrdState != null)
                    {
                        UpdateOrder_StateDTO updateOrderStateDto = mapper.Map<UpdateOrder_StateDTO>(createOrder_StateDTO);
                        var result = await order_StateService.UpdateAsync(updateOrderStateDto);
                        if (result.IsSuccess)
                        {
                            TempData["SuccessMessageUpdate"] = "Order state Updated successfully!";

                        }
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else 
                {
                    var result = await order_StateService.CreateAsync(createOrder_StateDTO);
                    if (result.IsSuccess) 
                    {
                        TempData["SuccessMessageCreate"] = "Order state  created successfully!";
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return PartialView(nameof(CreateOrdState), createOrder_StateDTO);
        }
        public async Task<IActionResult> DeleteOrdState(int id)
        {
            var deletedOrd = await order_StateService.DeleteAsync(id);

            if (deletedOrd)

            {
                TempData["SuccessMessageDelete"] = "OrderState deleted .";
            }

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Delete(int Id)
        {
            await order_StateService.DeleteAsync(Id);
            return RedirectToAction(nameof(Index));
        }

        //remote validations
        [AcceptVerbs("Get", "Post")]

        public async Task<IActionResult> IsOrderStateNameAvailable(string name)
        {
            var exists = await order_StateService.NameExistsAsync(name);
            return Json(exists ? $"Discount with name '{name}' already exists." : (object)true);
        }
        [AcceptVerbs("Get", "Post")]
        
        public async Task<IActionResult> IsOrderStateNameENAvailable(string nameEN)
        {
            var exists = await order_StateService.NameENExistsAsync(nameEN);
            return Json(exists ? $"Discount with name '{nameEN}' already exists." : (object)true);
        }
    }
}
