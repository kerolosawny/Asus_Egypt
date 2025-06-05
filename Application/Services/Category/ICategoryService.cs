
namespace Application.Services.Categories
{
    public interface ICategoryService  
    {
        Task<List<CategoryDTO>> GetCategoriesWithSubcategoriesAsync();
        Task<List<CategoryDTO>> GetCategoriesBySpecificationKeyAsync(int specificationKeyId);
        Task<ResultView<CategoryDTO>> CreateAsync(CreateCategoryDTO entity);
        Task<ResultView<CategoryDTO>> UpdateAsync(UpdateCategoryDTO entity);
        Task<bool> DeleteAsync(int id);
        Task<List<CategoryDTO>> GetAllAsync();
        Task<CategoryDTO> GetByIdAsync(int id);
        Task<IEnumerable<CategoryDTO>> GetSubCategoriesByCategoryIdAsync(int parentCategoryId);
        Task<IEnumerable<SpecificationKeyDTO>> GetSpecificationKeysByCategoryIdAsync(int categoryId);
        Task<bool> NameExistsAsync(string name);
        Task<bool> NameENExistsAsync(string nameEN);
        Task<List<CategoryDTO>> GetSubCategoriesByCategoryNameAsync(string parentCategoryName);

        Task<CategoryDTO> GetParentCategoryByChildCatId(int ChildId);





    }
}
