namespace Application.Services.Useres
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository,
                IMapper mapper,
                UserManager<User> userManager,
                RoleManager<IdentityRole<int>> roleManager,
                SignInManager<User> signInManager, IMemoryCache memoryCache, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _memoryCache = memoryCache;
            _configuration = configuration;
        }
        public async Task<ResultView<UserDTO>> CreateAsync(CreateUserDTO entity)
        {
            ResultView<UserDTO> result = new();
            try
            {
                // Check if User with the same email already exists
                var existingUser = await _userManager.FindByEmailAsync(entity.Email);
                if (existingUser != null)
                {
                    return new ResultView<UserDTO>
                    {
                        Entity = _mapper.Map<UserDTO>(entity),
                        IsSuccess = false,
                        Massage = "User with the same email already exists"
                    };
                }

                // Create User entity
                var user = _mapper.Map<User>(entity);
                var createUserResult = await _userManager.CreateAsync(user, entity.Password);

                if (!createUserResult.Succeeded)
                {
                    return new ResultView<UserDTO>
                    {
                        IsSuccess = false,
                        Massage = string.Join(", ", createUserResult.Errors.Select(e => e.Description))
                    };
                }

                // Assign roles if provided
                entity.Roles = new List<string> { "User" };
                if (entity.Roles != null && entity.Roles.Count > 0)
                {
                    var roleResult = await _userManager.AddToRolesAsync(user, entity.Roles);
                    if (!roleResult.Succeeded)
                    {
                        return new ResultView<UserDTO>
                        {
                             IsSuccess = false,
                            Massage = string.Join(", ", roleResult.Errors.Select(e => e.Description))
                        };
                    }
                }
                // Sign in the user after creation
                await _signInManager.SignInAsync(user, isPersistent: entity.RememberMe);

                // Map back to DTO and return
                var returnUser = _mapper.Map<UserDTO>(user);
                returnUser.Roles = "User";
                return new ResultView<UserDTO>
                {
                    Entity = returnUser,
                    IsSuccess = true,
                    Massage = "User created successfully"
                };
            }
            catch (Exception ex)
            {
                return new ResultView<UserDTO>
                {
                     IsSuccess = false,
                    Massage = "Error occurred: " + ex.Message
                };
            }
        }

        public async Task<ResultView<UserDTO>> UpdateAsync(UpdateUserDTO entity)
        {
            ResultView<UserDTO> result = new();
            try
            {
                var user = await _userRepository.GetByIDAsync(entity.Id);
                if (user == null)
                {
                    return new ResultView<UserDTO>
                    {
                        IsSuccess = false,
                        Massage = "User not found",
                    };
                }

                user.Email = !string.IsNullOrEmpty(entity.Email) ? entity.Email : user.Email;
                user.UserName = !string.IsNullOrEmpty(entity.UserName) ? entity.UserName : user.UserName;
                user.PhoneNumber = !string.IsNullOrEmpty(entity.PhoneNumber) ? entity.PhoneNumber : user.PhoneNumber;
                user.BirthDate = !string.IsNullOrEmpty(entity.BirthDate) ? DateTime.Parse(entity.BirthDate) : user.BirthDate;
                user.City = !string.IsNullOrEmpty(entity.City) ? entity.City : user.City;
                user.Country = !string.IsNullOrEmpty(entity.Country) ? entity.Country : user.Country;
                user.PostalCode = !string.IsNullOrEmpty(entity.PostalCode) ? entity.PostalCode : user.PostalCode;
                user.Region = !string.IsNullOrEmpty(entity.Region) ? entity.Region : user.Region;
                user.FirstName = !string.IsNullOrEmpty(entity.FirstName) ? entity.FirstName : user.FirstName;
                user.LastName = !string.IsNullOrEmpty(entity.LastName) ? entity.LastName : user.LastName;



                var updateUserResult = await _userManager.UpdateAsync(user);
                if (!updateUserResult.Succeeded)
                {
                    return new ResultView<UserDTO>
                    {
                         IsSuccess = false,
                        Massage = string.Join(", ", updateUserResult.Errors.Select(e => e.Description))
                    };
                }

                var updatedUserDto = _mapper.Map<UserDTO>(user);
                return new ResultView<UserDTO>
                {
                    Entity = updatedUserDto,
                    IsSuccess = true,
                    Massage = "User updated successfully"
                };
            }
            catch (Exception ex)
            {
                return new ResultView<UserDTO>
                {
                    IsSuccess = false,
                    Massage = "Error occurred: " + ex.Message,
                };
            }
        }

        // Delete User
        public async Task<bool> DeleteAsync(int userId)
        {
            try
            {
                var user = await _userRepository.GetByIDAsync(userId);
                if (user == null) return false;
                await _userManager.DeleteAsync(user);
                return true;
            }
            catch
            {
                return false;
            }
        }
        // Get User by ID
        public async Task<UserDTO> GetByIdAsync(int userId)
        {
            var user = await _userRepository.GetByIDAsync(userId);
            return _mapper.Map<UserDTO>(user);
        }


        public async Task<ResultView<string>> LoginAsync(LoginDTO loginDto)
        {
            ResultView<string> result = new();
            try
            {
                User user = null;

                string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                bool isValidEmail = Regex.IsMatch(loginDto.UserNameOrEmail, emailPattern);
                if (isValidEmail)
                {
                    user = await _userManager.FindByEmailAsync(loginDto.UserNameOrEmail);
                }
                else
                {
                    user = await _userManager.FindByNameAsync(loginDto.UserNameOrEmail);
                }
                if (user == null)//|| !await _userManager.CheckPasswordAsync(user, loginDto.Password))
                {
                    return new ResultView<string>
                    {
                        IsSuccess = false,
                        Massage = "Invalid username or password",
                        Entity = string.Empty
                    };
                }

                // Sign in the user
                var signInResult = await _signInManager.PasswordSignInAsync(user, loginDto.Password, isPersistent: false, lockoutOnFailure: false);
                if (!signInResult.Succeeded)
                {
                    return new ResultView<string>
                    {
                        IsSuccess = false,
                        Massage = "Invalid username or password",
                        Entity = string.Empty
                    };
                }
                // Generate token 
                var token = await GenerateJwtToken(user);

                return new ResultView<string>
                {
                    Entity = token,
                    IsSuccess = true,
                    Massage = "Login successful"
                };
            }
            catch (Exception ex)
            {
                return new ResultView<string>
                {
                    IsSuccess = false,
                    Massage = "Error occurred: " + ex.Message,
                    Entity = string.Empty
                };
            }
        }

        private async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
             };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["jwt:Issuer"],
                audience: _configuration["jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<EntityPaginated<UserDTO>> GetAllAsync(int pagenumber, int pageSize)
        {
            var Alluser = await _userRepository.GetAllAsync(pagenumber, pageSize);
            return new EntityPaginated<UserDTO>
            {
                Data = _mapper.Map<List<UserDTO>>(await Alluser.Where(p => p.Id != 1).ToListAsync()),
                CurrentPage = pagenumber,
                ItemsPerPage = pageSize,
            };
        }

        public async Task<List<UserDTO>> GetAllByRoleAsync(string roleName)
        {
            var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);
            return _mapper.Map<List<UserDTO>>(usersInRole);
        }
        public async Task AddUserToRoleAsync(string roleName, int userID)
        {
            var user = await _userRepository.GetByIDAsync(userID);
            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles);
            await _userManager.AddToRoleAsync(user, roleName);
        }
        public async Task<IList<string>> GetallUserRolesAsync(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles;
        }
        //===============================================================
        //*******************************
        //for Role Process
        public async ValueTask<List<IdentityRole<int>>> GetAllRolesAsync()
        {

            var roles = await _roleManager.Roles.ToListAsync();
            return roles;
        }
        public async Task<ResultView<string>> CreateRole(IdentityRole<int> role)
        {
            ResultView<string> result = new();
            try
            {
                var data = await _roleManager.CreateAsync(role);
                if (data.Succeeded)
                    result = new()
                    {
                        IsSuccess = data.Succeeded,
                        Massage = "Role Created Successfuly",
                        Entity = string.Empty
                    };
                else
                    result = new()
                    {
                        IsSuccess = data.Succeeded,
                        Massage = string.Join(",", data.Errors),
                        Entity = string.Empty
                    };
                return result;
            }
            catch (Exception ex)
            {

                result = new() { Massage = ex.Message, IsSuccess = false, Entity = string.Empty };
                return result;
            }
        }
        public async Task<ResultView<string>> UpdateRole(IdentityRole<int> role)
        {
            ResultView<string> result = new();

            try
            {
                var data = await _roleManager.UpdateAsync(role);
                if (data.Succeeded)
                    result = new()
                    {
                        IsSuccess = data.Succeeded,
                        Massage = "Role Created Successfuly",
                        Entity = string.Empty
                    };
                else
                    result = new()
                    {
                        IsSuccess = data.Succeeded,
                        Massage = string.Join(",", data.Errors),
                        Entity = string.Empty
                    };
                return result;
            }
            catch (Exception ex)
            {

                result = new() { Massage = ex.Message, IsSuccess = false, Entity = string.Empty };
                return result;
            }
        }
        public async Task<ResultView<string>> DeleteRole(IdentityRole<int> role)
        {
            ResultView<string> resultView = new();
            try
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    resultView = new()
                    {
                        IsSuccess = result.Succeeded,
                        Massage = "Role Delete Successfuly",
                        Entity = string.Empty
                    };
                }
                else
                {
                    resultView = new()
                    {
                        IsSuccess = result.Succeeded,
                        Massage = string.Join(",", result.Errors),
                        Entity = string.Empty
                    };
                }
                return resultView;
            }
            catch (Exception ex)
            {
                resultView = new()
                {
                    IsSuccess = false,
                    Massage = ex.Message,
                    Entity = string.Empty
                };
                return resultView;
            }
        }
        public async Task<ResultView<IdentityRole<int>>> GetRoleById(string id)
        {
            ResultView<IdentityRole<int>> resultView = new();
            try
            {
                var result = await _roleManager.FindByIdAsync(id);
                //  var result = await _roleManager.FindByNameAsync(rolename);
                if (result != null)
                    resultView = new()
                    {
                        IsSuccess = true,
                        Entity = result,
                        Massage = "done"
                    };
                else
                    resultView = new()
                    {
                        IsSuccess = false,
                        Massage = "Not Found"
                    };
                return resultView;
            }
            catch (Exception ex)
            {

                resultView = new()
                {
                    IsSuccess = false,
                    Massage = ex.Message
                };
                return resultView;
            }
        }

        public async Task<ResultView<string>> LogoutAsync()
        {
            ResultView<string> result = new();
            try
            {
                await _signInManager.SignOutAsync();

                result = new ResultView<string>
                {
                    IsSuccess = true,
                    Massage = "Logout successful",
                    Entity = string.Empty
                };
            }
            catch (Exception ex)
            {
                result = new ResultView<string>
                {
                    IsSuccess = false,
                    Massage = "Error occurred: " + ex.Message,
                    Entity = string.Empty
                };
            }

            return result;
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
        public List<UserDTO> AddtoCashMemoery(string key, List<UserDTO> items)
        {
            var DataIncash = GetFromCache<List<UserDTO>>(key);
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
        public void RemoveFromCashMemoery(string key, UserDTO item)
        {
            var DataIncash = GetFromCache<List<UserDTO>>(key);
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

        public async Task<IQueryable<User>> GetSortedFilterAsync<TKey>(Expression<Func<User, TKey>> orderBy, Expression<Func<User, bool>> searchPredicate = null, bool ascending = true)
        {
            return await _userRepository.GetSortedFilterAsync(orderBy, searchPredicate, ascending);
        }

        public async Task<int> EntityCount()
        {
            return await _userRepository.EntityCount();
        }

        public async Task<ResultView<string>> ChangePasswordAsync(string email, string oldPassword, string newPassword)
        {
            ResultView<string> result = new();
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return new ResultView<string>
                    {
                        IsSuccess = false,
                        Massage = "User not found",
                        Entity = string.Empty
                    };
                }

                var isOldPasswordCorrect = await _userManager.CheckPasswordAsync(user, oldPassword);
                if (!isOldPasswordCorrect)
                {
                    return new ResultView<string>
                    {
                        IsSuccess = false,
                        Massage = "Old password is incorrect",
                        Entity = string.Empty
                    };
                }

                var resultUpdate = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
                if (!resultUpdate.Succeeded)
                {
                    return new ResultView<string>
                    {
                        IsSuccess = false,
                        Massage = string.Join(", ", resultUpdate.Errors.Select(e => e.Description))
                    };
                }

                return new ResultView<string>
                {
                    IsSuccess = true,
                    Massage = "Password changed successfully",
                    Entity = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new ResultView<string>
                {
                    IsSuccess = false,
                    Massage = "Error occurred: " + ex.Message,
                    Entity = string.Empty
                };
            }
        }
        public async Task<ResultView<UserDTO>> GetByEmailAsync(string email)
        {
            ResultView<UserDTO> result = new();
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    result = new ResultView<UserDTO>
                    {
                        IsSuccess = false,
                        Massage = "User not found"
                    };
                }
                else
                {
                    var userDto = _mapper.Map<UserDTO>(user);
                    result = new ResultView<UserDTO>
                    {
                        IsSuccess = true,
                        Massage = "User found",
                        Entity = userDto
                    };
                }
            }
            catch (Exception ex)
            {
                result = new ResultView<UserDTO>
                {
                    IsSuccess = false,
                    Massage = "Error occurred: " + ex.Message
                };
            }
            return result;
        }



    }
}
