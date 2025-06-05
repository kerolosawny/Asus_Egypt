
namespace Application.Contracts
{
    public interface IRoleService
    {
        //===================================
        //***************************
        //for role
        public ValueTask<List<IdentityRole<int>>> GetAllRolesAsync();
        public Task<ResultView<IdentityRole<int>>> GetRoleById(string id);
        public Task<ResultView<string>> CreateRole(IdentityRole<int> role);
        public Task<ResultView<string>> UpdateRole(IdentityRole<int> role);
        public Task<ResultView<string>> DeleteRole(IdentityRole<int> role);
        //****************************
        //========================================

    }
}
