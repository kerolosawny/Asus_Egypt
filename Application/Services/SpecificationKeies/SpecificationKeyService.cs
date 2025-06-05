namespace Application.Services.SpecificationKeies
{
    public class SpecificationKeyService : ISpecificationKeyService
    {
        private readonly ISpecificationKeyRepository _specificationKeyRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public SpecificationKeyService(ISpecificationKeyRepository specificationKeyRepository, ICategoryRepository category, IMapper mapper)
        {
            _specificationKeyRepository = specificationKeyRepository;
            _categoryRepository = category;
            _mapper = mapper;
        }

        public async Task<List<SpecificationKeyDTO>> GetAllSpecificationKeysAsync()
        {
            var specificationKeys = await _specificationKeyRepository.GetAllAsync();
            return _mapper.Map<List<SpecificationKeyDTO>>(specificationKeys);
        }

        public async Task<ResultView<CreateSpecificationKeyDTO>> CreateAsync(CreateSpecificationKeyDTO entity)
        {
            ResultView<CreateSpecificationKeyDTO> result = new();

            try
            {
                if (string.IsNullOrWhiteSpace(entity.Key))
                {
                    result.IsSuccess = false;
                    result.Massage = "Specification key cannot be empty.";
                    return result;
                }

                var specificationKey = new SpecificationKey { Key = entity.Key, Key_Ar = entity.Key_Ar };

                var createdSpecificationKey = await _specificationKeyRepository.CreateAsync(specificationKey);
                await _specificationKeyRepository.SaveChangesAsync();
                if (entity.SubCategoryId != null)
                    foreach (var categoryId in entity.SubCategoryId)
                    {
                        var specCat = new CategorySpecificationKey
                        {
                            CategoryId = categoryId,
                            SpecificationKeyId = specificationKey.Id
                        };
                        await _specificationKeyRepository.AddCategorySpecificationKeyAsync(specCat);
                    }



                result.Entity = _mapper.Map<CreateSpecificationKeyDTO>(createdSpecificationKey);
                result.IsSuccess = true;
                result.Massage = "Specification key created successfully!";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Massage = $"Error occurred: {ex.Message}";
            }

            return result;
        }

        public async Task<ResultView<UpdateSpecificationKeyDTO>> UpdateAsync(UpdateSpecificationKeyDTO entity)
        {
            ResultView<UpdateSpecificationKeyDTO> result = new();

            try
            {
                var specificationKey = await _specificationKeyRepository.GetOneAsyncWithCatSpecs(entity.Id);
                if (specificationKey == null)
                {
                    result.IsSuccess = false;
                    result.Massage = "Specification key not found";
                    return result;
                }

                specificationKey.Key = entity.Key;
                specificationKey.Key_Ar = entity.Key_Ar;



                if (specificationKey.CategorySpecificationKeys != null)
                {
                    foreach (var spe in specificationKey.CategorySpecificationKeys)
                    {
                        _specificationKeyRepository.RemoveCategorySpecificationKeys(specificationKey.CategorySpecificationKeys);
                    }
                }

                if (entity.SubCategoryId?.Count > 0)
                {
                    if (entity.SubCategoryId != null)
                        foreach (var categoryId in entity.SubCategoryId)
                        {
                            var specCat = new CategorySpecificationKey
                            {
                                CategoryId = categoryId,
                                SpecificationKeyId = specificationKey.Id
                            };
                            await _specificationKeyRepository.AddCategorySpecificationKeyAsync(specCat);
                        }
                }

                await _specificationKeyRepository.UpdateAsync(specificationKey);
                await _specificationKeyRepository.SaveChangesAsync();

                // Prepare the result DTO
                result.Entity = new UpdateSpecificationKeyDTO
                {
                    Id = specificationKey.Id,
                    Key = specificationKey.Key,
                    Key_Ar = specificationKey.Key_Ar,
                    SubCategoryId = entity.SubCategoryId
                };
                result.IsSuccess = true;
                result.Massage = "Specification key updated successfully!";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Massage = $"Error occurred: {ex.Message}";
            }

            return result;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var specificationKey = await _specificationKeyRepository.GetOneAsync(id);
                if (specificationKey == null) return false;

                await _specificationKeyRepository.DeleteAsync(specificationKey);
                await _specificationKeyRepository.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<SpecificationKeyDTO> GetByIdAsync(int id)
        {
            var spec = await _specificationKeyRepository.GetOneAsync(id);
            return _mapper.Map<SpecificationKeyDTO>(spec);
        }

        public async Task<bool> NameExistsAsync(string name)
        {
            return await _specificationKeyRepository.ExistsAsync(name);
        }

        public async Task<List<SpecificationKeyDTO>> GetAllSpecificationKeysByCategoryAsync(int CatId)
        {
            var cat = await _categoryRepository.GetOneAsync(CatId);
            var children = await _categoryRepository.GetSubCategories(cat.ParentCategoryID!=null? cat.ParentCategoryID.Value:0);

            var allSpecificationKeys = new List<SpecificationKeyDTO>();

            foreach (var child in children)
            {
                var childSpecificationKeys = await _categoryRepository.GetCategorySpecificationKeysByCategoryIdAsync(child.Id);

                allSpecificationKeys.AddRange(childSpecificationKeys.Select(csk => new SpecificationKeyDTO
                {
                    Id = csk.Id,
                    Key = csk.Key,
                    Key_Ar = csk.Key_Ar,
                    SubCategoryId = csk.CategorySpecificationKeys?.Select(p => p.CategoryId).ToList(),
                    values = csk.ProductSpecifications?
                        .Select(ps => ps.Value)
                        .Distinct()
                        .ToList() ?? new List<string>()
                }));
            }
            var distinctSpecificationKeys = allSpecificationKeys
                                           .GroupBy(s => s.Id).Select(g => g.First()).ToList();
            return distinctSpecificationKeys;
        }

    }
}
