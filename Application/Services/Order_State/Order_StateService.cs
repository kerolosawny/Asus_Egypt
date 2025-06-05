namespace Application.Services.Ordrs
{
    public class Order_StateService : IOrder_StateService
    {
        private readonly IOrder_stateRepository _orderStateRepository;
        private readonly IMapper _mapper;

        public Order_StateService(IOrder_stateRepository orderStateRepository, IMapper mapper)
        {
            _orderStateRepository = orderStateRepository;
            _mapper = mapper;
        }

        // Create Order State
        public async Task<ResultView<Order_StateDTO>> CreateAsync(CreateOrder_StateDTO entity)
        {
            ResultView<Order_StateDTO> result = new();
            try
            {
                // Check if Order_State with the same name already exists
                bool exists = (await _orderStateRepository.GetAllAsync()).Any(os => os.Name == entity.Name);
                if (exists)
                {
                    result = new ResultView<Order_StateDTO>
                    {
                         IsSuccess = false,
                        Massage = "Order State with the same name already exists"
                    };
                    return result;
                }

                // Map DTO to Order_State entity and create it
                var orderState = _mapper.Map<Order_State>(entity);
                var createdOrderState = await _orderStateRepository.CreateAsync(orderState);
                await _orderStateRepository.SaveChangesAsync();

                // Map back to DTO and return
                var returnOrderState = _mapper.Map<Order_StateDTO>(createdOrderState);
                result = new ResultView<Order_StateDTO>
                {
                    Entity = returnOrderState,
                    IsSuccess = true,
                    Massage = "Order State created successfully"
                };
                return result;
            }
            catch (Exception ex)
            {
                result = new ResultView<Order_StateDTO>
                {
                     IsSuccess = false,
                    Massage = "Error occurred: " + ex.Message
                };
                return result;
            }
        }

        // Update Order State
        public async Task<ResultView<Order_StateDTO>> UpdateAsync(UpdateOrder_StateDTO entity)
        {
            ResultView<Order_StateDTO> result = new();
            try
            {
                var orderState = await _orderStateRepository.GetOneAsync(entity.Id);
                if (orderState == null)
                {
                    result = new ResultView<Order_StateDTO>
                    {
                        IsSuccess = false,
                        Massage = "Order State not found",
                     };
                    return result;
                }

                // Update Order_State entity
                _mapper.Map(entity, orderState);
                await _orderStateRepository.UpdateAsync(orderState);
                await _orderStateRepository.SaveChangesAsync();

                var updatedOrderState = _mapper.Map<Order_StateDTO>(orderState);
                result = new ResultView<Order_StateDTO>
                {
                    Entity = updatedOrderState,
                    IsSuccess = true,
                    Massage = "Order State updated successfully"
                };
                return result;
            }
            catch (Exception ex)
            {
                result = new ResultView<Order_StateDTO>
                {
                    IsSuccess = false,
                    Massage = "Error occurred: " + ex.Message,
                 };
                return result;
            }
        }

        // Delete Order State
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var orderState = await _orderStateRepository.GetOneAsync(id);
                if (orderState == null) return false;

                await _orderStateRepository.DeleteAsync(orderState);
                await _orderStateRepository.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Get All Order States
        public async Task<List<Order_StateDTO>> GetAllAsync()
        {
            var orderStates = await _orderStateRepository.GetAllAsync();
            return _mapper.Map<List<Order_StateDTO>>(orderStates);
        }

        // Get Order State by ID
        public async Task<Order_StateDTO> GetByIdAsync(int id)
        {
            var orderState = await _orderStateRepository.GetOneAsync(id);
            return _mapper.Map<Order_StateDTO>(orderState);
        }
        public async Task<bool> NameExistsAsync(string name)
        {
            return await _orderStateRepository.ExistsAsync(name);
        }

        public async Task<bool> NameENExistsAsync(string nameEN)
        {
            return await _orderStateRepository.ExistsAsync(nameEN, true);
        }
    }

}
