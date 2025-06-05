namespace Application.Services.Shipping_Methodes
{
    public class ShippingMethodService : IShippingMethodService
    {
        private readonly IShipping_MethodRepository _shippingMethodRepository;
        private readonly IMapper _mapper;

        public ShippingMethodService(IShipping_MethodRepository shippingMethodRepository, IMapper mapper)
        {
            _shippingMethodRepository = shippingMethodRepository;
            _mapper = mapper;
        }

        // Create Shipping Method
        public async Task<ResultView<ShippingMethodsDTO>> CreateAsync(CreateShippingMethodsDTO entity)
        {
            ResultView<ShippingMethodsDTO> result = new();
            try
            {
                // Check if Shipping Method with the same name already exists
                bool exists = (await _shippingMethodRepository.GetAllAsync()).Any(sm => sm.Method_Name == entity.Method_Name);
                if (exists)
                {
                    result = new ResultView<ShippingMethodsDTO>
                    {
                         IsSuccess = false,
                        Massage = "Shipping Method with the same name already exists"
                    };
                    return result;
                }

                // Map DTO to Shipping_Methods entity
                var shippingMethod = _mapper.Map<Shipping_Methods>(entity);
                var createdShippingMethod = await _shippingMethodRepository.CreateAsync(shippingMethod);
                await _shippingMethodRepository.SaveChangesAsync();

                // Map back to DTO and return
                var returnShippingMethod = _mapper.Map<ShippingMethodsDTO>(createdShippingMethod);
                result = new ResultView<ShippingMethodsDTO>
                {
                    Entity = returnShippingMethod,
                    IsSuccess = true,
                    Massage = "Shipping Method created successfully"
                };
                return result;
            }
            catch (Exception ex)
            {
                result = new ResultView<ShippingMethodsDTO>
                {
                     IsSuccess = false,
                    Massage = "Error occurred: " + ex.Message
                };
                return result;
            }
        }

        // Update Shipping Method
        public async Task<ResultView<ShippingMethodsDTO>> UpdateAsync(UpdateShippingMethodsDTO entity)
        {
            ResultView<ShippingMethodsDTO> result = new();
            try
            {
                var shippingMethod = await _shippingMethodRepository.GetOneAsync(entity.Id);
                if (shippingMethod == null)
                {
                    result = new ResultView<ShippingMethodsDTO>
                    {
                        IsSuccess = false,
                        Massage = "Shipping Method not found",
                     };
                    return result;
                }

                // Update Shipping_Methods entity
                _mapper.Map(entity, shippingMethod);
                await _shippingMethodRepository.UpdateAsync(shippingMethod);
                await _shippingMethodRepository.SaveChangesAsync();

                var updatedShippingMethodDto = _mapper.Map<ShippingMethodsDTO>(shippingMethod);
                result = new ResultView<ShippingMethodsDTO>
                {
                    Entity = updatedShippingMethodDto,
                    IsSuccess = true,
                    Massage = "Shipping Method updated successfully"
                };
                return result;
            }
            catch (Exception ex)
            {
                result = new ResultView<ShippingMethodsDTO>
                {
                    IsSuccess = false,
                    Massage = "Error occurred: " + ex.Message
                 };
                return result;
            }
        }

        // Delete Shipping Method
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var shippingMethod = await _shippingMethodRepository.GetOneAsync(id);
                if (shippingMethod == null) return false;

                await _shippingMethodRepository.DeleteAsync(shippingMethod);
                await _shippingMethodRepository.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Get All Shipping Methods
        public async Task<List<ShippingMethodsDTO>> GetAllAsync()
        {
            var shippingMethods = await _shippingMethodRepository.GetAllAsync();
            return _mapper.Map<List<ShippingMethodsDTO>>(shippingMethods);
        }

        // Get Shipping Method by ID
        public async Task<ShippingMethodsDTO> GetByIdAsync(int id)
        {
            var shippingMethod = await _shippingMethodRepository.GetOneAsync(id);
            return _mapper.Map<ShippingMethodsDTO>(shippingMethod);
        }
        public async Task<bool> NameExistsAsync(string name)
        {
            return await _shippingMethodRepository.ExistsAsync(name);
        }


    }

}
