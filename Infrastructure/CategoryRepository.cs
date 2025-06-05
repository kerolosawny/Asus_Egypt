

namespace Infrastructure
{
    public class CategoryRepository : GenericRepository<Category, int>, ICategoryRepository
    {
        public CategoryRepository(AsusDbContext asusDbContext) : base(asusDbContext) { }

        // Method to get all categories with their subcategories
        public async Task<IQueryable<Category>> GetCategoriesWithSubcategoriesAsync()
        {
            return await Task.FromResult(dbset.Include(c => c.SubCategories));
        }

        // Method to get categories by specification key
        public async Task<IQueryable<Category>> GetCategoriesBySpecificationKeyAsync(int specificationKeyId)
        {
            return await Task.FromResult(dbset
                .Where(c => c.CategorySpecificationKeys!=null&& c.CategorySpecificationKeys.Any(csk => csk.SpecificationKeyId == specificationKeyId)));
        }
        public async Task<IEnumerable<Category>> GetSubCategories(int parentCategoryId)
        {
            return await dbset
                .Where(c => c.ParentCategoryID == parentCategoryId)
                .ToListAsync();
        }
        public async Task<IEnumerable<SpecificationKey>> GetCategorySpecificationKeysByCategoryIdAsync(int categoryId)
        {
            return await dbset
                .Where(c => c.Id == categoryId)
                .SelectMany(c => c.CategorySpecificationKeys!)
                .Include(csk => csk.SpecificationKey)
                    .ThenInclude(sk => sk.ProductSpecifications) 
                .Select(csk => csk.SpecificationKey)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await dbset.AnyAsync(c => c.Name == name);
        }
        public async Task<bool> ExistsAsync(string nameEN, bool isEnglish)
        {
            return await dbset.AnyAsync(c => c.Name_EN == nameEN);
        }
        public void RemoveRange(IEnumerable<Category> entities)
        {
            dbset.RemoveRange(entities);
        }

        public async Task<Category?> GetByNameAsync(string categoryName)
        {
            return await dbset.FirstOrDefaultAsync(c => c.Name_EN == categoryName);
        }
        public async Task<Category?> GetCategoryWithParentAsync(int childId)
        {
            return await dbset
                .Include(c => c.ParentCategory) 
                .FirstOrDefaultAsync(c => c.ParentCategoryID == childId);
        }
        public async Task<int> MaxCode()
        {
            var maxCode = await dbset.MaxAsync(p => p.Code);
            return maxCode ?? 6010;
        }

    }

}
