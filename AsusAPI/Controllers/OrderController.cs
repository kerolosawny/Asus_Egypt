 
namespace AsusAPI.Controllers
{
    //[Authorize(Roles = "Admin,User")]

    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IproductService _productService;
        private string key = "Orders";
        public OrderController(IOrderService orderService, IproductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateOrderInDB([FromBody] OrderCreate order)
        {
            try
            {
                if (order == null)
                {
                    return BadRequest("Order data cannot be null.");
                }

                List<CreateOrderItemDTO> items = new();
                foreach (var product in order.Products)
                {
                    items.Add(new CreateOrderItemDTO
                    {
                        ProductId = product.ProductId,
                        Quantity = product.Quantity,
                        Price = product.Price
                    });
                
                    await _productService.UpdateProductPriceAsync(new UpdateProductPrice
                    {
                        ProductId = product.ProductId,
                        quantity = product.Quantity,
                    });
                    var prd = await _productService.GetOneAsync(product.ProductId);
                    _productService.RemoveFromCashMemoery("Products", prd);
                    _productService.AddtoCashMemoery("Products", new List<ProductDTO> { prd });

                }

                var createOrderDTO = new CreateOrderDTO
                {
                    UserId = order.UserId,
                    OrderDate = DateTime.Now,
                    ShippingMethodId = order.ShippingMethodId,
                    ShippingAddress = order.Address,
                    TotalAmount = order.TotalAmount,
                    TrackingNumber = order.PhoneNumber,
                    OrderItems = items,
                    OrderStateId = 1,
                    PaymentMethodId = 1
                };

                var result = await _orderService.CreateAsync(createOrderDTO);

                if (result.IsSuccess)
                {
                    _orderService.AddtoCashMemoery(key, new List<OrderDTO>{result.Entity});
                    return Ok(new
                    {
                        message = result.Massage,
                        id = result.Entity.Id
                    });
                }
                else
                {
                    return BadRequest(new { message = result.Massage });
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating order: {ex.Message}");
            }
        }
        [HttpDelete("{orderId}")]
        public async Task<IActionResult> CancleOrder(int orderId)
        {
            try
            {
                var order = await _orderService.GetByIdAsync(orderId);
                if (order.OrderItems != null)
                {
                    foreach (var item in order.OrderItems)
                    {
                        await _productService.UpdateProductPriceAsync(new UpdateProductPrice
                        {
                            ProductId = item.ProductId,
                            quantity = item.Quantity,
                            IsSubtract = false
                        });
                      var prd=await  _productService.GetOneAsync(item.ProductId);
                        _productService.RemoveFromCashMemoery("Products", prd);
                        _productService.AddtoCashMemoery("Products",new List<ProductDTO>{ prd});

                    }
                }
                var result = await _orderService.DeleteAsync(orderId);
                _orderService.RemoveFromCashMemoery(key,order);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrdersByUserId(int userId)
        {
            try
            {
                var orders = await _orderService.GetByUserIdAsync(userId);
                if (orders == null || !orders.Any())
                {
                    return NotFound(new { message = "No orders found for the specified user ID." });
                }

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving orders: {ex.Message}");
            }
        }
        [HttpGet("UpdateOrderState")]
        public async Task<IActionResult> UpdateOrderState(int orderId)
        {
            try
            {
                await _orderService.UpdateOrderState(orderId, 7);
                var order = await _orderService.GetByIdAsync(orderId);
                _orderService.RemoveFromCashMemoery(key, order);
                _orderService.AddtoCashMemoery(key,new List<OrderDTO> { order });
                return Ok();
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
