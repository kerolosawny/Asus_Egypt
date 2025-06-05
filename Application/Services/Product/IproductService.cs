
namespace Application.Services
{
    public interface IproductService : ICacheMemory<ProductDTO>
    {
        public static int AllElementNumber { get; set; }
        public Task<IQueryable<Product>> GetSortedFilterAsync<TKey>(Expression<Func<Product, TKey>> orderBy, Expression<Func<Product, bool>> searchPredicate = null, bool ascending = true);

        //Task<Product?> GetByIdAsync(int id);
        Task<ResultView<CreateProductDTO>> CreateAsync(CreateProductDTO entity);
        Task<ResultView<UpdateProductDTO>> UpdateAsync(UpdateProductDTO entity);
        Task<List<ProductDTO>> GetAllAsync();
        Task<ProductDTO> GetOneAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<List<ProductDTO>> GetProductsByCategoryIdAsync(int categoryId);
        public Task<EntityPaginated<ProductDTO>> GetAllAsync(int PageNumber, int Pagesize);
        Task<ResultView<AddImagesDTO>> AddImagesAsync(AddImagesDTO dto);
        Task RemoveImageFromProductAsync(int productId, string imageUrl);
        Task<ResultView<ProductDTO>> AddSubCategoriesAsync(int productId, List<int> subCategoryIds);
        Task<Product> GetProductWithCategoriesAsync(int id);
        Task<ResultView<List<ProductSpecificationDTO>>> SaveProductSpecificationsAsync(AddSpecsDTO model);
        Task<List<SpecificationKey>> GetProductSpecificationsByIdAsync(int productId);
        Task<Product> GetOneWithDetails(int id);
        Task<bool> NameExistsAsync(string name);
        Task<bool> NameENExistsAsync(string name);
        Task<List<ProductDTO>> GetProductBySpecificationValues(List<string> specificationValues);
        public void Intialize();
        public Task<ResultView<string>> UpdateProductPriceAsync(UpdateProductPrice entity);

    }
}
