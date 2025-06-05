

namespace Application.Contracts
{
    public interface ISpecificationKeyRepository : IGenericRepository<SpecificationKey, int>
    {
        public  Task<IQueryable<SpecificationKey>> GetSpecificationKeysByCategoryIdAsync(int categoryId);
        public void RemoveCategorySpecificationKeys(ICollection<CategorySpecificationKey> categorySpecificationKeys);
        public Task<SpecificationKey?> GetOneAsyncWithCatSpecs(int id);
        public Task<bool> ExistsAsync(string name);
        public Task AddCategorySpecificationKeyAsync(CategorySpecificationKey specification);




    }
}
