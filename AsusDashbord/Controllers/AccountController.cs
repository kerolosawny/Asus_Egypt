namespace AsusDashbord.Controllers
{
     public class AccountController : Controller
    {
        private readonly IPaymentMethodService paymentMethodService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private static int AllElementNumber;
        private string key = "ALLUser";
        public AccountController(IUserService userService, IMapper mapper, IPaymentMethodService _paymentMethodService)
        {
            paymentMethodService = _paymentMethodService;
            _mapper = mapper;
            _userService = userService;
            AllElementNumber = _userService.EntityCount().Result;
        }
        public IActionResult Register()
        {
            var model = new CreateUserDTO
            {
                UserName = string.Empty,
                Email = string.Empty,
                Password = string.Empty,
                ConfirmPassword = string.Empty,
                RememberMe = false
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Register(CreateUserDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.CreateAsync(model);
                if (result.IsSuccess)
                {
                    _userService.AddtoCashMemoery(key, new List<UserDTO> { result.Entity });

                    return RedirectToAction("Index");
                }
                else
                    return View(model);
                #region MyRegion
                //var user = _mapper.Map<User>(model);
                ////user.Payment_MethodsID = 1;

                //var result = await _userManager.CreateAsync(user, model.Password);
                //if (result.Succeeded)
                //{
                //    await _signInManager.SignInAsync(user, isPersistent: model.RememberMe);
                //    return RedirectToAction("Index", "Home"); 
                //}

                //foreach (var error in result.Errors)
                //{
                //    ModelState.AddModelError(string.Empty, error.Description);
                //}
                #endregion
            }
            return View(model);
        }

        public  IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginAsync(loginDTO);
                if (result.IsSuccess)
                {
                    return RedirectToAction("Index", "Home");
                }
                loginDTO.Result = result.Massage;
                return View(loginDTO);
            }
            return View(loginDTO);
        }
         [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(int pageNumber = 1, int PageSize = 15)
        {
            var TotalPage = (int)Math.Ceiling((double)AllElementNumber / PageSize);
            var cachedData = _userService.GetFromCache<List<UserDTO>>(key);
            if (pageNumber >= 1 && pageNumber <= TotalPage)
            {
                if (cachedData != null)
                {
                    var PagnetedCachMemory = cachedData.Skip((pageNumber - 1) * PageSize).Take(PageSize).ToList();
                    if (PagnetedCachMemory.Count == 0)
                    {
                        var UpdatedUserList = await _userService.GetAllAsync(pageNumber, PageSize);
                        for (int i = 0; i < UpdatedUserList.Data.Count; i++)
                        {
                            UpdatedUserList.Data[i].Roles = string.Join(",", await _userService.GetallUserRolesAsync(new User { Email = UpdatedUserList.Data[i].Email, UserName = UpdatedUserList.Data[i].UserName, Id = UpdatedUserList.Data[i].Id }));
                        }
                        _userService.AddtoCashMemoery(key, UpdatedUserList.Data);
                        return View(new EntityPaginated<UserDTO>() { Data = UpdatedUserList.Data, CurrentPage = pageNumber, ItemsPerPage = PageSize, TotalPages = TotalPage });
                    }
                    return View(new EntityPaginated<UserDTO>() { Data = PagnetedCachMemory, CurrentPage = pageNumber, ItemsPerPage = PageSize, TotalPages = TotalPage });
                }
                var allUseres = await _userService.GetAllAsync(pageNumber, PageSize);
                for (int i = 0; i < allUseres.Data.Count; i++)
                {
                    allUseres.Data[i].Roles = string.Join(",", await _userService.GetallUserRolesAsync(new User { Email = allUseres.Data[i].Email, UserName = allUseres.Data[i].UserName, Id = allUseres.Data[i].Id }));
                }
                _userService.AddToCache(key, allUseres.Data, TimeSpan.FromMinutes(30));
                cachedData = allUseres.Data;
            }
            return View(new EntityPaginated<UserDTO>() { Data = cachedData, CurrentPage = pageNumber, ItemsPerPage = PageSize, TotalPages = (int)Math.Ceiling((double)AllElementNumber / PageSize) });
        }
        public async Task<IActionResult> PartialIndex(string id)
        {


            var cachedData = _userService.GetFromCache<List<UserDTO>>(key);

            if (cachedData != null)
            {
                if (!string.IsNullOrEmpty(id))
                {
                    cachedData = cachedData.Where(p =>
                                p.UserName.IndexOf(id, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                p.Email.IndexOf(id, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                (p.Roles != null && p.Roles.IndexOf(id, StringComparison.OrdinalIgnoreCase) >= 0)
                                  ).ToList();

                    if (cachedData.Count == 0)
                    {
                        var data = await _userService.GetSortedFilterAsync(p => p.UserName, p => p.UserName!.Contains(id) || p.Email!.Contains(id));
                        //if no data back return empty list
                        if (data.ToList().Count == 0)
                            return PartialView(nameof(PartialIndex), new List<UserDTO>());
                        var newData = _mapper.Map<List<UserDTO>>(await data.ToListAsync());
                        //if (newData.Count > 0)
                        //    _userService.AddtoCashMemoery("ALLUser", newData); ^ScriptDocument7680 GetALLCategories꞉273_9.txt
                        return PartialView(newData);
                    }
                }
                cachedData = cachedData.Take(15).ToList();
            }

            return PartialView(cachedData);

            //var allUseres = await _userService.GetAllAsync();
            //for (int i = 0; i < allUseres.Data.Count; i++)
            //{
            //    allUseres.Data[i].Roles = string.Join(",", await _userService.GetallUserRolesAsync(new User { Email = allUseres.Data[i].Email, UserName = allUseres.Data[i].UserName, Id = allUseres.Data[i].Id }));
            //}
            //_userService.AddToCache("ALLUser", allUseres.Data, TimeSpan.FromMinutes(30));
            //cachedData = _userService.GetFromCache<List<UserDTO>>("ALLUser");

        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            UpdateUserDTO model = new()
            {
                UserName = user.UserName,
                Id = id,
                Email = user.Email,
                BirthDate =  user.BirthDate.HasValue.ToString(),
                City = user.City??string.Empty,
                Country = user.Country,
                PhoneNumber = user.PhoneNumber,
                Region = user.Region,
                PostalCode = user.PostalCode,
            };
            return PartialView(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateUserDTO user)
        {
            var result = await _userService.UpdateAsync(user);
            if (result.IsSuccess)
            {
                TempData["SuccessMessageUpdate"] = "User Updated successfully!";
                _userService.RemoveFromCashMemoery(key,await _userService.GetByIdAsync(user.Id));
                _userService.AddtoCashMemoery(key, new List<UserDTO> { result.Entity });
                return RedirectToAction(nameof(Index));
            }
            return PartialView(user);
        }
         [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            _userService.RemoveFromCashMemoery(key, await _userService.GetByIdAsync(id));
            var result = await _userService.DeleteAsync(id);
            if (result)
            {
                var cachedData = _userService.GetFromCache<List<UserDTO>>(key);
                cachedData = cachedData.Take(15).ToList();
                TempData["SuccessMessageDelete"] = "User deleted successfully!";
                return PartialView(nameof(PartialIndex), cachedData);
            }
            return RedirectToAction(nameof(Index));

        }
          [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRoleToUser(int id)
        {
            var rols = await _userService.GetAllRolesAsync();
            UserRole userRole = new() { Id = id, Roles = rols };
            return PartialView(userRole);
        }
        [HttpPost]
        public async Task<IActionResult> AddRoleToUser(UserRole userRole)
        {
           await _userService.AddUserToRoleAsync(userRole.SelectedRoleName!, userRole.Id);
            _userService.RemoveFromCashMemoery(key, await _userService.GetByIdAsync(userRole.Id));
            var user = await _userService.GetByIdAsync(userRole.Id);
            user.Roles = userRole.SelectedRoleName;
            _userService.AddtoCashMemoery(key, new List<UserDTO> { user });

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            var result = await _userService.LogoutAsync();
            if (result.IsSuccess)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, result.Massage);
            return View();
        }

        public IActionResult AccessDenied()
        {
             return View();
        }
    }
}


