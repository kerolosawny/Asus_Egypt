
namespace Application.Services.Discounte
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;

        public DiscountService(IDiscountRepository discountRepository, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
        }

        // Create Discount
        public async Task<ResultView<DiscountDTO>> CreateAsync(CreateDiscountDTO entity)
        {
            ResultView<DiscountDTO> result = new();
            try
            {
                // Check if a Discount with the same name or code exists
                // bool exists = (await _discountRepository.GetAllAsync()).Any(d => d.Name == entity.Name);
                bool exists = (await _discountRepository.GetSortedFilterAsync(p => p.Id, d => d.Name == entity.Name)).Any();
                if (exists)
                {
                    result = new ResultView<DiscountDTO>
                    {
                         IsSuccess = false,
                        Massage = "Discount with the same name or code already exists"
                    };
                    return result;
                }
                if (entity.ImageData != null)
                {
                    var fileName = Guid.NewGuid() + "_" + entity.ImageData.FileName;

                    string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Discounts");

                    Directory.CreateDirectory(uploadFolder);
                    string filePath = Path.Combine(uploadFolder, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await entity.ImageData.CopyToAsync(fileStream);
                    }

                    entity.ImageUrl = "/images/Discounts/" + fileName;
                }
                // Map DTO to Discount entity and create it
                var discount = _mapper.Map<Discount>(entity);
                var createdDiscount = await _discountRepository.CreateAsync(discount);
                await _discountRepository.SaveChangesAsync();

                // Map back to DTO and return
                var returnDiscount = _mapper.Map<DiscountDTO>(createdDiscount);
                result = new ResultView<DiscountDTO>
                {
                    Entity = returnDiscount,
                    IsSuccess = true,
                    Massage = "Discount created successfully"
                };
                return result;
            }
            catch (Exception ex)
            {
                result = new ResultView<DiscountDTO>
                {
                     IsSuccess = false,
                    Massage = "Error occurred: " + ex.Message
                };
                return result;
            }
        }

        // Update Discount
        public async Task<ResultView<DiscountDTO>> UpdateAsync(UpdateDiscountDTO entity)
        {
            ResultView<DiscountDTO> result = new();
            try
            {
                // Retrieve the existing discount
                var discount = await _discountRepository.GetOneAsync(entity.Id);
                if (discount == null)
                {
                    result = new ResultView<DiscountDTO>
                    {
                        IsSuccess = false,
                        Massage = "Discount not found"
                     };
                    return result;
                }
                if (entity.ImageData != null)
                {
                    if (!string.IsNullOrEmpty(discount.ImageUrl))
                    {
                        var oldFilePath = Path.Combine("wwwroot", discount.ImageUrl.TrimStart('/'));
                        if (File.Exists(oldFilePath))
                        {
                            File.Delete(oldFilePath);
                        }
                    }

                    var fileName = Guid.NewGuid() + Path.GetExtension(entity.ImageData.FileName);
                    var filePath = Path.Combine("wwwroot/images/Discounts", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await entity.ImageData.CopyToAsync(stream);
                    }

                    entity.ImageUrl = $"/images/Discounts/{fileName}";
                }
                _mapper.Map(entity, discount);
                var updatedDiscounte = await _discountRepository.UpdateAsync(discount);
                var updatedDiscount = _mapper.Map<DiscountDTO>(discount);
                result = new ResultView<DiscountDTO>
                {
                    Entity = updatedDiscount,
                    IsSuccess = true,
                    Massage = "Discount updated successfully"
                };
                return result;
            }
            catch (Exception ex)
            {
                result = new ResultView<DiscountDTO>
                {
                    IsSuccess = false,
                    Massage = "Error occurred: " + ex.Message,
                 };
                return result;
            }
        }

        // Delete Discount
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var discount = await _discountRepository.GetOneAsync(id);
                if (discount == null)
                {
                    return false;
                }
                if (discount.ImageUrl != null)
                {
                    var oldFilePath = Path.Combine("wwwroot", discount.ImageUrl.TrimStart('/'));
                    if (File.Exists(oldFilePath))
                    {
                        File.Delete(oldFilePath);
                    }
                }

                await _discountRepository.DeleteAsync(discount);
                await _discountRepository.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Get All Discounts
        public async Task<List<DiscountDTO>> GetAllAsync()
        {
            var discounts = await (await _discountRepository.GetAllAsync()).ToListAsync();
            return _mapper.Map<List<DiscountDTO>>(discounts);
        }


        // Get Discount by ID
        public async Task<DiscountDTO> GetByIdAsync(int id)
        {
            var discount = await _discountRepository.GetOneAsync(id);
            return _mapper.Map<DiscountDTO>(discount);
        }

        public async Task<bool> NameExistsAsync(string name)
        {
            return await _discountRepository.ExistsAsync(name);
        }

        public async Task<bool> NameENExistsAsync(string nameEN)
        {
            return await _discountRepository.ExistsAsync(nameEN, true);
        }

    }
}
