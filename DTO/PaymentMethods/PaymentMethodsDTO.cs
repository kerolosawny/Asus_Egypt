
namespace DTO.PaymentMethods
{
    public class PaymentMethodsDTO
    {
        public PaymentMethodsDTO()
        {
                
        }
        public PaymentMethodsDTO(Payment_Methods pay)
        {
            Name = pay.Name;
            Id = pay.Id;
            Card_Number = pay.Card_Number;
            Expiration_Date=  pay.Expiration_Date;
            Is_Default = pay.Is_Default;
            Card_TypeId=pay.Card_TypeId;
        }
        public int Id { get; set; }
        public string? Card_Number { get; set; }
        [Required]
        public DateOnly? Expiration_Date { get; set; }
        public bool Is_Default { get; set; }
        public int? Card_TypeId { get; set; }
        public required string Name { get; set; }


    }
}
