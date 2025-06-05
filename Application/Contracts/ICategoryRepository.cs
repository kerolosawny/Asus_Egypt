

namespace Application.Contracts
{
    public interface ICategoryRepository : IGenericRepository<Category, int>
    {
        public  Task<IQueryable<Category>> GetCategoriesWithSubcategoriesAsync();
        public  Task<IQueryable<Category>> GetCategoriesBySpecificationKeyAsync(int specificationKeyId);
        public Task<IEnumerable<Category>> GetSubCategories(int parentCategoryId);
        Task<IEnumerable<SpecificationKey>> GetCategorySpecificationKeysByCategoryIdAsync(int categoryId);
        public Task<bool> ExistsAsync(string nameEN, bool isEnglish);
        public Task<bool> ExistsAsync(string name);
        void RemoveRange(IEnumerable<Category> entities);
        Task<Category?> GetByNameAsync(string categoryName);
        public Task<Category?> GetCategoryWithParentAsync(int childId);
        public Task<int> MaxCode();





    }
}
