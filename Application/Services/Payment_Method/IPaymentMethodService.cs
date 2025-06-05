
namespace Application.Services.Payment_Method
{
    public interface IPaymentMethodService  
    {
        Task<ResultView<PaymentMethodsDTO>> CreateAsync(CreatePaymentMethodsDTO entity);
        Task<ResultView<PaymentMethodsDTO>> UpdateAsync(UpdatePaymentMethodsDTO entity);
        Task<bool> DeleteAsync(int id);
        Task<List<PaymentMethodsDTO>> GetAllAsync();
        Task<PaymentMethodsDTO> GetByIdAsync(int id);

    }
}
