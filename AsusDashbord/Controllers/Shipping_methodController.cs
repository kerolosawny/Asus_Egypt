namespace AsusDashbord.Controllers
{
    [Authorize(Roles = "Admin")]

    public class Shipping_methodController : Controller
    {

        private readonly IShippingMethodService shippingMethod_Service;
        private readonly IMapper mapper;

        public Shipping_methodController(IShippingMethodService _shippingMethod_Service , IMapper _mapper)
        {

            shippingMethod_Service = _shippingMethod_Service;
            mapper = _mapper;

        }
        public async  Task< IActionResult > Index()
        {
            var shippings = await shippingMethod_Service.GetAllAsync();
            return View( shippings);
        }


        public async Task<IActionResult> Createshipping(int id)
        {

            CreateShippingMethodsDTO shippingMethodsDTO;
            if (id == 0) // Create case
            {
                shippingMethodsDTO = new CreateShippingMethodsDTO
                {
                    Method_Name = string.Empty,
                    Cost = 0,
                    Estimated_Delivery_Time = DateTime.Now,
                };
            }
            else // Update case
            {
                ShippingMethodsDTO shippingMthd = await shippingMethod_Service.GetByIdAsync(id);
                if (shippingMthd == null)
                {
                    return NotFound();
                }
                shippingMethodsDTO = mapper.Map<CreateShippingMethodsDTO>(shippingMthd);
            }

            return PartialView(nameof(Createshipping), shippingMethodsDTO); // Return partial view
        }

        

        [HttpPost]
        public async Task<IActionResult> Createshipping(CreateShippingMethodsDTO shippingMthd)
        {
            if (ModelState.IsValid)
            {
                if (shippingMthd.Id > 0) // Update case
                {
                    ShippingMethodsDTO existingShipping = await shippingMethod_Service.GetByIdAsync(shippingMthd.Id);
                    if (existingShipping != null)
                    {
                        UpdateShippingMethodsDTO updateShippingDTO = mapper.Map<UpdateShippingMethodsDTO>(shippingMthd);
                        var res = await shippingMethod_Service.UpdateAsync(updateShippingDTO);
                        if (res.IsSuccess)
                        {
                            TempData["SuccessMessageUpdate"] = "Shipping method Updated successfully!";
                            return RedirectToAction("Index");
                        }
                    }
                }
                else // Create case
                {
                    var res =await shippingMethod_Service.CreateAsync(shippingMthd);
                    if (res.IsSuccess)
                    {
                        TempData["SuccessMessageCreate"] = "Shipping method created successfully!";
                        return RedirectToAction("Index");

                    }
                }
                // Return JSON indicating success
                return Json(new { success = true });
            }

            // If model state is invalid, return the form with validation errors
            return PartialView("Createshipping", shippingMthd);
        }

   
        public async Task<IActionResult> Delete(int Id)
        {
          var deletedOrd=  await shippingMethod_Service.DeleteAsync(Id);
            if (deletedOrd)
            {
                TempData["SuccessMessage"] = "Shipping Methods deleted .";
            }
            return RedirectToAction(nameof(Index));
        }

        [AcceptVerbs("Get", "Post")]

        public async Task<IActionResult> IsShippingNameAvailable(string Method_Name)
        {
            var exists = await shippingMethod_Service.NameExistsAsync(Method_Name);
            return Json(exists ? $"Shipping method with name '{Method_Name}' already exists." : (object)true);
        }

    }



}
