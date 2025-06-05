
namespace Infrastructure
{
    public class PaymentMethodRepository : GenericRepository<Payment_Methods, int>, IPayment_methodRepository
    {
        public PaymentMethodRepository(AsusDbContext asusDbContext) : base(asusDbContext)
        {

        }
    }
}
