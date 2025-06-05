

namespace DTO.PaymentMethods
{
    public class CreatePaymentMethodsDTO
    {
          // Ensures valid credit card number format
        public   string? Card_Number { get; set; }
        [Required]
        [DataType(DataType.Date)]  
        public DateOnly Expiration_Date { get; set; }
        [Required]
        public bool Is_Default { get; set; }
         public int Card_TypeId { get; set; }
        public required string Name { get; set; }


    }
}
