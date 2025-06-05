namespace Application.Services.Categories
{
    public class CategoryService : ICategoryService
    {

        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        // Fetch categories with their subcategories
        public async Task<List<CategoryDTO>> GetCategoriesWithSubcategoriesAsync()
        {
            var categories = await _categoryRepository.GetCategoriesWithSubcategoriesAsync();
            return _mapper.Map<List<CategoryDTO>>(categories);
        }

        // Fetch categories based on specification key
        public async Task<List<CategoryDTO>> GetCategoriesBySpecificationKeyAsync(int specificationKeyId)
        {
            var categories = await _categoryRepository.GetCategoriesBySpecificationKeyAsync(specificationKeyId);
            return _mapper.Map<List<CategoryDTO>>(categories);
        }

        // Create Category
        public async Task<ResultView<CategoryDTO>> CreateAsync(CreateCategoryDTO entity)
        {
            ResultView<CategoryDTO> result = new();
            try
            {
                bool exists = (await _categoryRepository.GetSortedFilterAsync(p => p.Id, c => c.Name == entity.Name)).Any();
                //  bool exists = (await _categoryRepository.GetAllAsync()).Any(c => c.Name == entity.Name);
                if (exists)
                {
                    result = new ResultView<CategoryDTO>
                    {
                         IsSuccess = false,
                        Massage = "Category with the same name already exists"
                    };
                    return result;
                }
                if (entity.ImageData != null)
                {
                    var fileName = Guid.NewGuid() + "_" + entity.ImageData.FileName;

                    string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Categories");

                    Directory.CreateDirectory(uploadFolder);
                    string filePath = Path.Combine(uploadFolder, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await entity.ImageData.CopyToAsync(fileStream);
                    }

                    entity.ImageUrl = "/images/Categories/" + fileName;
                }
                // Map the DTO to the Category entity and create it
                var category = _mapper.Map<Category>(entity);
                category.Code = (await _categoryRepository.MaxCode()) + 1;
                var createdCategory = await _categoryRepository.CreateAsync(category);
                await _categoryRepository.SaveChangesAsync();

                // Map back to DTO and return
                var returnCategory = _mapper.Map<CategoryDTO>(createdCategory);
                result = new ResultView<CategoryDTO>
                {
                    Entity = returnCategory,
                    IsSuccess = true,
                    Massage = "Category created successfully"
                };
                return result;
            }
            catch (Exception ex)
            {
                result = new ResultView<CategoryDTO>
                {
                     IsSuccess = false,
                    Massage = "Error occurred: " + ex.Message
                };
                return result;
            }
        }

        // Update Category
        public async Task<ResultView<CategoryDTO>> UpdateAsync(UpdateCategoryDTO entity)
        {
            ResultView<CategoryDTO> result = new();
            try
            {
                // Retrieve the category
                var category = await _categoryRepository.GetOneAsync(entity.Id);
                if (category == null)
                {
                    result = new ResultView<CategoryDTO>
                    {
                        IsSuccess = false,
                        Massage = "Category not found",
                     };
                    return result;
                }
                if (entity.ImageData != null)
                {
                    if (!string.IsNullOrEmpty(category.ImageUrl))
                    {
                        var oldFilePath = Path.Combine("wwwroot", category.ImageUrl.TrimStart('/'));
                        if (File.Exists(oldFilePath))
                        {
                            File.Delete(oldFilePath);
                        }
                    }

                    var fileName = Guid.NewGuid() + Path.GetExtension(entity.ImageData.FileName);
                    var filePath = Path.Combine("wwwroot/images/Categories", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await entity.ImageData.CopyToAsync(stream);
                    }

                    entity.ImageUrl = $"/images/Categories/{fileName}";
                }
                // Map the updated values and save
                _mapper.Map(entity, category);
                await _categoryRepository.UpdateAsync(category);
                await _categoryRepository.SaveChangesAsync();

                var updatedCategory = _mapper.Map<CategoryDTO>(category);
                result = new ResultView<CategoryDTO>
                {
                    Entity = updatedCategory,
                    IsSuccess = true,
                    Massage = "Category updated successfully"
                };
                return result;
            }
            catch (Exception ex)
            {
                result = new ResultView<CategoryDTO>
                {
                    IsSuccess = false,
                    Massage = "Error occurred: " + ex.Message,
                 };
                return result;
            }
        }

        // Delete Category
        // Delete Category
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var category = await _categoryRepository.GetOneAsync(id);
                if (category == null)
                {
                    return false;
                }

                var subCategories = (await _categoryRepository.GetSubCategories(id)).ToList();
                if (subCategories.Count > 0)
                {
                    return false;
                }
                //if (subCategories != null && subCategories.Any())
                //{
                //    _categoryRepository.RemoveRange(subCategories);
                //}
                else
                {
                    if (category.ImageUrl != null)
                    {
                        string oldFilePath = Path.Combine("wwwroot", category.ImageUrl.TrimStart('/'));
                        if (File.Exists(oldFilePath))
                        {
                            File.Delete(oldFilePath);
                        }
                    }
                    await _categoryRepository.DeleteAsync(category);
                    await _categoryRepository.SaveChangesAsync();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }


        // Get All Categories
        public async Task<List<CategoryDTO>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return _mapper.Map<List<CategoryDTO>>(await categories.ToListAsync());
        }

        // Get Category by ID
        public async Task<CategoryDTO> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetOneAsync(id);
            // return _mapper.Map<CategoryDTO>(category);
            
            return category!=null?
                new CategoryDTO {
                    Id = category.Id,
                    Name = category.Name,
                    Name_EN = category.Name_EN,
                    ParentCategoryID=category.ParentCategoryID }:
                    new CategoryDTO();
            
        }
        public async Task<IEnumerable<CategoryDTO>> GetSubCategoriesByCategoryIdAsync(int parentCategoryId)
        {
            var subCategories = await _categoryRepository.GetSubCategories(parentCategoryId);

            return subCategories.Select(sc => new CategoryDTO
            {
                Id = sc.Id,
                Name = sc.Name
            }).ToList();
        }
        public async Task<IEnumerable<SpecificationKeyDTO>> GetSpecificationKeysByCategoryIdAsync(int categoryId)
        {
            var categorySpecificationKeys = await _categoryRepository.GetCategorySpecificationKeysByCategoryIdAsync(categoryId);

            return categorySpecificationKeys.Select(csk => new SpecificationKeyDTO
            {

                Id = csk.Id,
                Key_Ar = csk.Key_Ar,
                Key = csk.Key
            }).ToList();
        }

       

        public async Task<bool> NameExistsAsync(string name)
        {
            return await _categoryRepository.ExistsAsync(name);
        }

        // Check for duplicate category name in English
        public async Task<bool> NameENExistsAsync(string nameEN)
        {
            return await _categoryRepository.ExistsAsync(nameEN, true);
        }

        public async Task<List<CategoryDTO>> GetSubCategoriesByCategoryNameAsync(string parentCategoryName)
        {
            var parentCategory = await _categoryRepository.GetByNameAsync(parentCategoryName);

            if (parentCategory != null)
            {
                var subCategories = await _categoryRepository.GetAllAsync();
                var subCategoryDtos = subCategories
                    .Where(c => c.ParentCategoryID == parentCategory.Id)
                    .Select(c => new CategoryDTO
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Name_EN = c.Name_EN,
                        ParentCategoryID = c.ParentCategoryID,
                    })
                    .ToList();

                return subCategoryDtos;
            }

            return new List<CategoryDTO>();
        }

        public async Task<CategoryDTO> GetParentCategoryByChildCatId(int ChildId)
        {
            var childCategory = await _categoryRepository.GetCategoryWithParentAsync(ChildId);

            if (childCategory == null || childCategory.ParentCategory == null)
            {
                return null;
            }

            return _mapper.Map<CategoryDTO>(childCategory.ParentCategory);
        }
    }
}
