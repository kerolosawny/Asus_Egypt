namespace Application.Services.Useres
{
    public interface IUserService: ICacheMemory<UserDTO>, IRoleService,IEntityCount
    {
        public Task<ResultView<UserDTO>> CreateAsync(CreateUserDTO entity);
        public Task<ResultView<UserDTO>> UpdateAsync(UpdateUserDTO entity);
        public Task<bool> DeleteAsync(int userId);
        public Task<EntityPaginated<UserDTO>> GetAllAsync(int pagenumber = 1, int pageSize = 15);
        public Task<UserDTO> GetByIdAsync(int userId);
        public Task<ResultView<string>> LoginAsync(LoginDTO loginDto);
        public Task<List<UserDTO>> GetAllByRoleAsync(string roleName);
        public Task AddUserToRoleAsync(string roleName, int userId);
        public  Task<IList<string>> GetallUserRolesAsync(User user);
        public Task<ResultView<string>> LogoutAsync();

        public Task<IQueryable<User>> GetSortedFilterAsync<TKey>(Expression<Func<User, TKey>> orderBy, Expression<Func<User, bool>> searchPredicate = null, bool ascending = true);
        public Task<ResultView<string>> ChangePasswordAsync(string email, string oldPassword, string newPassword);
        public Task<ResultView<UserDTO>> GetByEmailAsync(string email);

    }

}
