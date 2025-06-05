namespace AsusDashbord.Controllers
{
    [Authorize(Roles = "Admin")]

    public class CardTypeController : Controller
    {
        private readonly ICard_TypeService CardTypeService;
        private readonly IMapper  Mapper;

        public CardTypeController(ICard_TypeService _CardTypeService , IMapper _mapper)
        {
            CardTypeService = _CardTypeService;
            Mapper = _mapper;
        }
        
        public async Task<IActionResult> GetALLCards()
        {
            var Cards = await CardTypeService.GetAllAsync();
           
            return View(Cards);
        }
        public IActionResult CreateCards()
        {
            return  PartialView(nameof(CreateCards));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCards(CreateCard_TypeDTO model)
        {
            if (ModelState.IsValid)
            {
                var res = await CardTypeService.CreateAsync(model);
                if (res.IsSuccess)
                {
                    TempData["SuccessMessageCreate"] = "Card created successfully!";
                    return RedirectToAction(nameof(GetALLCards));
                }
            }

            return PartialView(nameof(CreateCards), model);
        }

        public async Task<IActionResult> UpdateCards(int id)
        {
            var ID = await CardTypeService.GetByIdAsync(id);
            UpdateCard_TypeDTO model = new UpdateCard_TypeDTO
            {
                Id = ID.Id,
                Name = ID.Name,

            };
            return PartialView(nameof(UpdateCards), model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCards(UpdateCard_TypeDTO model)
        {
            if (ModelState.IsValid)
            {
               var x=  await CardTypeService.UpdateAsync(model);
                if (x.IsSuccess)
                {
                    TempData["SuccessMessageUpdate"] = "Card Updated successfully!";
                    return RedirectToAction(nameof(GetALLCards));
                }
            }

            return PartialView(nameof(UpdateCards), model);
        }

        public async Task<IActionResult> DeleteCards(int id)
        {
            var del = await CardTypeService.DeleteAsync(id);
            if (del)
            {
                TempData["SuccessMessageDelete"] = "Card deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Error occurred while deleting the card.";
            }
            return RedirectToAction(nameof(GetALLCards));
        }

        [AcceptVerbs("Get", "Post")]

        public async Task<IActionResult> IsCardNameAvailable(string name)
        {
            var exists = await CardTypeService.NameExistsAsync(name);
            return Json(exists ? $"Card with name '{name}' already exists." : (object)true);
        }

    }
}
