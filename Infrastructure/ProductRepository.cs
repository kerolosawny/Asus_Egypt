namespace Infrastructure
{
    public class ProductRepository : GenericRepository<Product, int>, IProductRepository
    {

        public ProductRepository(AsusDbContext asusDbContext) : base(asusDbContext)
        {
        }

        public override async ValueTask<Product> GetOneAsync(int id)
        {
            var entity = dbset.AsQueryable();
            var product = await entity.Where(p => p.Id == id && !p.IsDeleted)
                .Include(p => p.Reviews)
                .Include(p => p.Product_Category)!
                .ThenInclude(p => p.Category)
                .Include(p => p.Images)
                .Include(p => p.ProductSpecifications)!
                .ThenInclude(p => p.SpecificationKey)
                .Include(p => p.Discount)!
                .ThenInclude(p => p.discount)
                .FirstOrDefaultAsync();
            return product;
        }



        // Method to get products with their specifications
        public async Task<IQueryable<Product>> GetProductsWithSpecificationsAsync()
        {
            return await Task.FromResult(dbset.Include(p => p.ProductSpecifications));
        }

        // Method to filter products by category
        public async Task<IQueryable<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await Task.FromResult(dbset
                .Include(p => p.Images)
                .Include(p => p.Product_Category)!
                .ThenInclude(p => p.Category)
                .Include(p => p.Discount)!
                .ThenInclude(p => p.discount)
                .Include(p => p.ProductSpecifications)!
                .ThenInclude(p => p.SpecificationKey)
                .Include(p => p.Reviews)
                .Where(p => p.Product_Category!=null&& p.Product_Category.Any(pc => pc.CategoryId == categoryId)));
        }


        public async Task AddProductCategoryAsync(Product_Categoty specification)
        {
            await asusDbContext.Product_Categories.AddAsync(specification);
            await asusDbContext.SaveChangesAsync();

        }

        public async Task CreateSpecificationAsync(ProductSpecification specification)
        {
            await asusDbContext.ProductSpecifications.AddAsync(specification);
        }
        public async Task DeleteSpecification(ProductSpecification specification)
        {
            asusDbContext.ProductSpecifications.Remove(specification);
            await asusDbContext.SaveChangesAsync();
        }


        public async Task RemoveProductCategory(Product_Categoty productCategory)
        {
            asusDbContext.Product_Categories.Remove(productCategory);
            await asusDbContext.SaveChangesAsync();
        }


        public async Task AddProductDiscountAsync(Product_Discount productDiscount)
        {
            await asusDbContext.Product_Discounts.AddAsync(productDiscount);
            await asusDbContext.SaveChangesAsync();
        }

        public async Task RemoveProductDiscount(Product_Discount productDiscount)
        {
            asusDbContext.Product_Discounts.Remove(productDiscount);
            await asusDbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await dbset.AnyAsync(c => c.Name == name);
        }
        public async Task<bool> ExistsAsync(string nameEN, bool isEnglish)
        {
            return await dbset.AnyAsync(c => c.Name_EN == nameEN);
        }
        //================
        public override Task<IQueryable<Product>> GetAllAsync(int pageNumber = 1, int pageSize = 15)
        {
            return Task.FromResult(dbset
               .Include(p => p.Images)
               .Include(p => p.Product_Category)!
               .ThenInclude(pc => pc.Category)
               .Include(p => p.Discount)!
               .ThenInclude(p => p.discount)
               .Include(p => p.ProductSpecifications)!
               .ThenInclude(ps => ps.SpecificationKey)
               .Include(p => p.Reviews).
               Where(entity => !entity.IsDeleted).
               Skip((pageNumber - 1) * pageSize).
               Take(pageSize)
               );
        }
        public override Task<IQueryable<Product>> GetAllAsync()
        {
            return Task.FromResult(dbset.OrderByDescending(p => p.Created_at)
               .Include(p => p.Images)
               .Include(p => p.Product_Category)!
               .ThenInclude(pc => pc.Category)
               .Include(p => p.Discount)!
               .ThenInclude(p => p.discount)
               .Include(p => p.ProductSpecifications)!
               .ThenInclude(ps => ps.SpecificationKey)
               .Include(p => p.Reviews).

               Where(entity => !entity.IsDeleted));
        }

        //=======================================
        //*************************************
        //*************************************
        public override async Task<IQueryable<Product>> GetSortedFilterAsync<TKey>(Expression<Func<Product, TKey>> orderBy, Expression<Func<Product, bool>> searchPredicate = null, bool ascending = true)
        {
            var query = dbset.AsQueryable();

            if (searchPredicate != null)
            {
                query = query.Where(searchPredicate);
            }

            query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);

            return await Task.FromResult(query.Any() ? query.Include(p => p.Images)
                .Include(p => p.Reviews)
                .Include(p => p.Discount)!
                .ThenInclude(p => p.discount)
                .Include(p => p.Product_Category)!
                .ThenInclude(p => p.Category)
                .Include(p => p.ProductSpecifications)!
                .ThenInclude(p => p.SpecificationKey)!
                .Where(entity => !entity.IsDeleted)
                : Enumerable.Empty<Product>().AsQueryable());
        }

        public async Task<int> MaxCode()
        {
            var maxCode = await dbset.MaxAsync(p => p.Code);
            return maxCode ?? 332;
        }
        //*************************************
        //*************************************
    }
}
