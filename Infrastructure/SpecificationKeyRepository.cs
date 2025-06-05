
namespace Infrastructure
{
    public class SpecificationKeyRepository : GloableGenericRepository<SpecificationKey, int>, ISpecificationKeyRepository
    {
        public SpecificationKeyRepository(AsusDbContext asusDbContext) : base(asusDbContext) { }

        // Method to get all specification keys related to a specific category

        public async Task<IQueryable<SpecificationKey>> GetSpecificationKeysByCategoryIdAsync(int categoryId)
        {
            return await Task.FromResult(dbset
                .Where(sk => sk.CategorySpecificationKeys!=null&& sk.CategorySpecificationKeys.Select(p => p.CategoryId).First() == categoryId)
                .Include(sk => sk.ProductSpecifications));
        }

        public async Task AddCategorySpecificationKeyAsync(CategorySpecificationKey specification)
        {
            await asusDbContext.CategorySpecificationKeys.AddAsync(specification);
            await asusDbContext.SaveChangesAsync();
        }


        public void RemoveCategorySpecificationKeys(ICollection<CategorySpecificationKey> categorySpecificationKeys)
        {
            asusDbContext.CategorySpecificationKeys.RemoveRange(categorySpecificationKeys);
        }
        public async Task<SpecificationKey?> GetOneAsyncWithCatSpecs(int id)
        {
            return await asusDbContext.SpecificationKeys
                .Include(sk => sk.CategorySpecificationKeys)
                .FirstOrDefaultAsync(sk => sk.Id == id);
        }
        public async Task<bool> ExistsAsync(string name)
        {
            return await dbset.AnyAsync(c => c.Key == name);
        }
    }

}
