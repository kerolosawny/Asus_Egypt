

namespace DTO.PaymentMethods
{
    public class UpdatePaymentMethodsDTO
    {
        [Required]
        public int Id { get; set; }

        [CreditCard]
        public string? Card_Number { get; set; }

        [DataType(DataType.Date)]
        public DateOnly? Expiration_Date { get; set; }

        public bool Is_Default { get; set; }

        public int Card_TypeId { get; set; }
        public required string Name { get; set; }


    }
}
