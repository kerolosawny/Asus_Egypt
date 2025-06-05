
namespace DTO.Ordere
{
    public class OrderCreate
    {
        public int UserId { get; set; }
        public List<Products> Products { get; set; }=new List<Products>();
        public int ShippingMethodId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public class Product
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }
    }
    public class Products
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

    }
}
