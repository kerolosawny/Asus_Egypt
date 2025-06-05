
namespace Application.Services.Payment_Method
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IPayment_methodRepository _paymentMethodRepository;
        private readonly ICard_TypeRepository _cardTypeRepository;
        private readonly IMapper _mapper;

        public PaymentMethodService(
            IPayment_methodRepository paymentMethodRepository,
            ICard_TypeRepository cardTypeRepository,
            IMapper mapper)
        {
            _paymentMethodRepository = paymentMethodRepository;
            _cardTypeRepository = cardTypeRepository;
            _mapper = mapper;
        }

        // Create Payment Method
        public async Task<ResultView<PaymentMethodsDTO>> CreateAsync(CreatePaymentMethodsDTO entity)
        {
            ResultView<PaymentMethodsDTO> result = new();
            try
            {


                // Map DTO to Payment_Methods entity
                Payment_Methods paymentMethod = new()
                {
                    Name = entity.Name,
                    //Card_TypeId = entity.Card_TypeId,
                    Card_Number = entity.Card_Number,
                    Expiration_Date = entity.Expiration_Date,
                    Is_Default = entity.Is_Default,
                };

                var createdPaymentMethod = await _paymentMethodRepository.CreateAsync(paymentMethod);
                await _paymentMethodRepository.SaveChangesAsync();

                // Map back to DTO and return
                PaymentMethodsDTO returnPaymentMethod = new(createdPaymentMethod) { Name = createdPaymentMethod.Name };
                result = new ResultView<PaymentMethodsDTO>
                {
                    Entity = returnPaymentMethod,
                    IsSuccess = true,
                    Massage = "Payment Method created successfully"
                };
                return result;
            }
            catch (Exception ex)
            {
                result = new ResultView<PaymentMethodsDTO>
                {
                    IsSuccess = false,
                    Massage = "Error occurred: " + ex.Message
                };
                return result;
            }
        }

        // Update Payment Method
        public async Task<ResultView<PaymentMethodsDTO>> UpdateAsync(UpdatePaymentMethodsDTO entity)
        {
            ResultView<PaymentMethodsDTO> result = new();
            try
            {
                var paymentMethod = await _paymentMethodRepository.GetOneAsync(entity.Id);
                if (paymentMethod == null)
                {
                    result = new ResultView<PaymentMethodsDTO>
                    {
                        IsSuccess = false,
                        Massage = "Payment Method not found",
                    };
                    return result;
                }

               

                paymentMethod.Name = entity.Name;
                 paymentMethod.Card_Number = entity.Card_Number;
                paymentMethod.Expiration_Date = entity.Expiration_Date;
                paymentMethod.Is_Default = entity.Is_Default;

 
                await _paymentMethodRepository.UpdateAsync(paymentMethod);
                await _paymentMethodRepository.SaveChangesAsync();

                PaymentMethodsDTO updatedPaymentMethodDto = new(paymentMethod) { Name = paymentMethod.Name };
                result = new ResultView<PaymentMethodsDTO>
                {
                    Entity = updatedPaymentMethodDto,
                    IsSuccess = true,
                    Massage = "Payment Method updated successfully"
                };
                return result;
            }
            catch (Exception ex)
            {
                result = new ResultView<PaymentMethodsDTO>
                {
                    IsSuccess = false,
                    Massage = "Error occurred: " + ex.Message,
                };
                return result;
            }
        }

        // Delete Payment Method
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var paymentMethod = await _paymentMethodRepository.GetOneAsync(id);
                if (paymentMethod == null) return false;

                await _paymentMethodRepository.DeleteAsync(paymentMethod);
                await _paymentMethodRepository.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Get All Payment Methods
        public async Task<List<PaymentMethodsDTO>> GetAllAsync()
        {
            var paymentMethods = await (await _paymentMethodRepository.GetAllAsync()).ToListAsync();
            return _mapper.Map<List<PaymentMethodsDTO>>(paymentMethods);
        }

        // Get Payment Method by ID
        public async Task<PaymentMethodsDTO> GetByIdAsync(int id)
        {
            var paymentMethod = await _paymentMethodRepository.GetOneAsync(id);
            return new PaymentMethodsDTO(paymentMethod) { Name=paymentMethod.Name};
        }


    }

}
