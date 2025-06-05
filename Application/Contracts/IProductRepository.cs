

namespace Application.Contracts
{
    public interface IProductRepository : IGenericRepository<Product, int>
    {


        public Task<IQueryable<Product>> GetProductsWithSpecificationsAsync();
        public Task<IQueryable<Product>> GetProductsByCategoryIdAsync(int categoryId);
        public Task CreateSpecificationAsync(ProductSpecification specification);
        public Task DeleteSpecification(ProductSpecification specification);
        public Task AddProductCategoryAsync(Product_Categoty specification);
        public Task RemoveProductCategory(Product_Categoty productCategory);
        public Task AddProductDiscountAsync(Product_Discount productDiscount);
        public Task RemoveProductDiscount(Product_Discount productDiscount);
        public Task<bool> ExistsAsync(string name);
        public Task<bool> ExistsAsync(string nameEN, bool isEnglish);
        public Task<int> MaxCode();










    }
}
