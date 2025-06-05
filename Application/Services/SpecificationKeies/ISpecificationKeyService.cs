 
namespace Application.Services.SpecificationKeies
{
    
        public interface ISpecificationKeyService
        {
            Task<List<SpecificationKeyDTO>> GetAllSpecificationKeysAsync();
            Task<SpecificationKeyDTO> GetByIdAsync(int id);

            Task<ResultView<CreateSpecificationKeyDTO>> CreateAsync(CreateSpecificationKeyDTO entity);
            Task<ResultView<UpdateSpecificationKeyDTO>> UpdateAsync(UpdateSpecificationKeyDTO entity);
            Task<bool> DeleteAsync(int id);
            Task<bool> NameExistsAsync(string name);
            Task<List<SpecificationKeyDTO>> GetAllSpecificationKeysByCategoryAsync(int CatId);


    }



}
