namespace AsusDashbord.Controllers
{
    [Authorize(Roles = "Admin")]

    public class RoleController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public RoleController(IMapper mapper, IUserService userService)
        {  
            _mapper = mapper;
            _userService = userService;
        }
       
        public async Task<IActionResult> Index()
        {
          var allRoles=  await _userService.GetAllRolesAsync();
            return View(allRoles);
        }
        public IActionResult CreateRole()
        {
             return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(IdentityRole<int>role)
        {
            if (ModelState.IsValid)
            {
                var reult = await _userService.CreateRole(role);
                ViewData["result"] = reult.Massage;
                return RedirectToAction("Index");
            }
            else
                return PartialView(role);
        }
        public async Task<IActionResult> UpdateRole(string id)
        {
            var role = await _userService.GetRoleById(id);

            return PartialView(role.Entity);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateRole(IdentityRole<int> role)
        {
            if (ModelState.IsValid) {
              var reult = await _userService.UpdateRole(role);
                if (reult.IsSuccess) { 
                return RedirectToAction("Index");
                }
                return PartialView(role);
            }
            return View(role);
        }
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _userService.GetRoleById(id);
            var result = await _userService.DeleteRole(role.Entity);
            return RedirectToAction("Index");

        }
    }
}
