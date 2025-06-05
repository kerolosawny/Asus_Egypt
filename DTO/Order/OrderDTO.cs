

namespace DTO.Ordere
{
    public record OrderDTO
    {
        public OrderDTO()
        {


        }
        public OrderDTO(Order order)
        {
            Id = order.Id;
            OrderDate = order.order_date;
            TotalAmount = order.Total_amout;
            TrackingNumber = order.tracking_number;
            OrderItems = order.Order_Items!.Select(p => new OrderItemDTO
            {
                Id = p.Id,
                Quantity = p.Quantity,
                OrderId = p.OrderId,
                Price = p.Price,
                Tax = p.Tax,
                ProductId = p.ProductId,
                ProductName=p.Products!=null?p.Products.Name:string.Empty
            }).ToList();
            ShippingAddress = order.shipping_address;
            UserId = order.UserId;

            OrderState = order.order_State!=null? order.order_State.Name_EN! : "Pending";
            OrderStateArabic = order.order_State!.Name ?? "قيد الانتظار";
            ShippingMethod = order.shipping_Methods!.Method_Name;
            PaymentMethod = order.payment_Methods!.Name ?? "Paypal";
            Code = order.Code;
            UserName = order.User!=null?order.User.UserName??string.Empty:string.Empty;
            
        }
        public int Id { get; set; }
        public int? Code { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string? TrackingNumber { get; set; }
        public string? ShippingAddress { get; set; }
        public ICollection<OrderItemDTO> OrderItems { get; set; }
        public string ShippingMethod { get; set; }
        public string PaymentMethod { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string OrderState { get; set; }
        public string? OrderStateArabic { get; set; }

        public decimal? ShippingCost { get; set; }

    }
    public record OrederStatusDTO
    {
        public int orderId { get; set; }
        public string? OrderStatus { get; set; }
        public Dictionary<string, int>? orderStat {  get; set; }
    }
}