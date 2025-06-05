
namespace Application.Services.Ordrs
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrder_ItemsRepository _orderItemsRepository;
        private readonly IPayment_methodRepository _paymentMethodRepository;
        private readonly IShipping_MethodRepository _shippingMethodRepository;
        private readonly IOrder_stateRepository _orderStateRepository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly IUserService _userService;
        private string key = "Orders";
        public static int AllElementNumber { get; set; }

        public OrderService(
            IOrderRepository orderRepository,
            IOrder_ItemsRepository orderItemsRepository,
            IPayment_methodRepository paymentMethodRepository,
            IShipping_MethodRepository shippingMethodRepository,
            IOrder_stateRepository orderStateRepository,
            IMemoryCache memoryCache,
            IUserService userService,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _orderItemsRepository = orderItemsRepository;
            _paymentMethodRepository = paymentMethodRepository;
            _shippingMethodRepository = shippingMethodRepository;
            _orderStateRepository = orderStateRepository;
            _memoryCache = memoryCache;
            _mapper = mapper;
            _userService = userService;

        }

        public void Intialize()
        {
            AllElementNumber = _orderRepository.EntityCount().Result;
        }


        public async Task<IQueryable<Order_Items>> GetTopSellingProductsAsync()
        {
            var top = await _orderItemsRepository.GetAllAsync();
            return top;
        }
        // Create Order
        public async Task<ResultView<OrderDTO>> CreateAsync(CreateOrderDTO entity)
        {
            ResultView<OrderDTO> result = new();
            try
            {
                //// Validate related entities
                var paymentMethod = await _paymentMethodRepository.GetOneAsync(entity.PaymentMethodId);
                var shippingMethod = await _shippingMethodRepository.GetOneAsync(entity.ShippingMethodId);
                var orderState = await _orderStateRepository.GetOneAsync(entity.OrderStateId);
                //if (paymentMethod == null || shippingMethod == null || orderState == null)
                //{
                //    result = new ResultView<OrderDTO>
                //    {
                //        IsSuccess = false,
                //        Massage = "Invalid payment method, shipping method, or order state"
                //    };
                //    return result;
                //}

                // Map the Order DTO to Order entity
                var order = new Order
                {
                    Total_amout = entity.TotalAmount,
                    tracking_number = entity.TrackingNumber,
                    shipping_address = entity.ShippingAddress,
                    order_date = entity.OrderDate,
                    ShippingMethodsID = entity.ShippingMethodId,
                    OrderStateID = entity.OrderStateId,
                    UserId = entity.UserId,
                    PaymentMethodsID = entity.PaymentMethodId,
                    Code = (await _orderRepository.MaxCode()) + 1


                };
                order.payment_Methods = paymentMethod;
                order.shipping_Methods = shippingMethod;
                order.order_State = orderState;


                var createdOrder = await _orderRepository.CreateAsync(order);
                await _orderRepository.SaveChangesAsync();
                //===============================================================
                //===============================================================            
                // Create Order Items and link to the order
                foreach (var item in entity.OrderItems)
                {
                    var orderItem = new Order_Items
                    {
                        Quantity = item.Quantity,
                        Price = item.Price,
                        Tax = item.Tax,
                        OrderId = createdOrder.Id,
                        Orders = createdOrder,
                        ProductId = item.ProductId,

                    };
                    await _orderItemsRepository.CreateAsync(orderItem);
                }
                await _orderRepository.SaveChangesAsync();

                //===============================================================
                //===============================================================
                // Map to OrderDTO for returning
                createdOrder.order_State = (await _orderStateRepository.GetOneAsync(entity.OrderStateId));
                createdOrder.shipping_Methods = await _shippingMethodRepository.GetOneAsync(entity.ShippingMethodId);
                createdOrder.payment_Methods = await _paymentMethodRepository.GetOneAsync(entity.PaymentMethodId);
                createdOrder.User = _mapper.Map<User>(await _userService.GetByIdAsync(entity.UserId));
                var returnOrder = new OrderDTO(createdOrder);
                result = new ResultView<OrderDTO>
                {
                    Entity = returnOrder,
                    IsSuccess = true,
                    Massage = "Order created successfully"
                };
                return result;
            }
            catch (Exception ex)
            {
                result = new ResultView<OrderDTO>
                {
                    IsSuccess = false,
                    Massage = "Error occurred: " + ex.Message,
                 };
                return result;
            }
        }

        // Update Order
        public async Task<ResultView<OrderDTO>> UpdateAsync(UpdateOrderDTO entity)
        {
            ResultView<OrderDTO> result = new();
            try
            {
                var order = await _orderRepository.GetOneAsync(entity.Id);
                if (order == null)
                {
                    result = new ResultView<OrderDTO>
                    {
                        IsSuccess = false,
                        Massage = "Order not found"
                     };
                    return result;
                }

                // Update order details
                _mapper.Map(entity, order);

                // Update related entities
                var paymentMethod = await _paymentMethodRepository.GetOneAsync(entity.PaymentMethodId);
                var shippingMethod = await _shippingMethodRepository.GetOneAsync(entity.ShippingMethodId);
                var orderState = await _orderStateRepository.GetOneAsync(entity.OrderStateId);
                order.payment_Methods = paymentMethod;
                order.shipping_Methods = shippingMethod;
                order.order_State = orderState;

                // Update order items
                if (entity.OrderItems != null && entity.OrderItems.Any())
                {
                    foreach (var itemDto in entity.OrderItems)
                    {
                        var orderItem = await _orderItemsRepository.GetOneAsync(itemDto.Id);
                        if (orderItem != null)
                        {
                            var orderi = _mapper.Map(itemDto, orderItem);
                            await _orderItemsRepository.UpdateAsync(orderi);
                        }
                        else
                        {
                            var newOrderItem = _mapper.Map<Order_Items>(itemDto);
                            if(order.Order_Items!=null)
                            order.Order_Items.Add(newOrderItem);
                        }
                    }
                }

                await _orderRepository.UpdateAsync(order);
                await _orderRepository.SaveChangesAsync();

                var updatedOrderDto = _mapper.Map<OrderDTO>(order);
                result = new ResultView<OrderDTO>
                {
                    Entity = updatedOrderDto,
                    IsSuccess = true,
                    Massage = "Order updated successfully"
                };
                return result;
            }
            catch (Exception ex)
            {
                result = new ResultView<OrderDTO>
                {
                    IsSuccess = false,
                    Massage = "Error occurred: " + ex.Message
                 };
                return result;
            }
        }

        // Delete Order
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var order = await _orderRepository.GetOneAsync(id);
                if (order == null) return false;
                var orderItems = order.Order_Items!=null? order.Order_Items.ToList():new List<Order_Items>();

                foreach (var orderItem in orderItems)
                {
                    await _orderItemsRepository.DeleteAsync(orderItem);
                }

                await _orderRepository.DeleteAsync(order);
                await _orderRepository.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Get All Orders
        public async Task<List<OrderDTO>> GetAllAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return (await orders.Select(p => new OrderDTO(p)).ToListAsync());
        }
        // Get Order by ID
        public async Task<OrderDTO> GetByIdAsync(int id)
        {
            var order = await _orderRepository.GetOneAsync(id);
            return new OrderDTO(order) { UserName = (await _userService.GetByIdAsync(order.UserId)).UserName };
        }

        //update order state 
        public async Task UpdateOrderState(int orderId, int orderStateId)
        {

            try
            {
                var order = await _orderRepository.GetOneAsync(orderId);
                order.OrderStateID = orderStateId;
                await _orderRepository.UpdateAsync(order);

            }
            catch (Exception)
            {

            }
        }
        // Paginate Orders

        public async Task<EntityPaginated<OrderDTO>> GetAllAsync(int PageNumber = 1, int Pagesize = 15)
        {

            if (AllElementNumber == 0)
            {
                Intialize();
            }
            var Allproducts = await _orderRepository.GetAllAsync(PageNumber, Pagesize);


            EntityPaginated<OrderDTO> paginatedProduct = new()
            {
                CurrentPage = PageNumber,
                ItemsPerPage = Pagesize,
                TotalPages = (int)Math.Ceiling((double)AllElementNumber / Pagesize),
                Data = (await Allproducts.ToListAsync()).Select(p => new OrderDTO(p) { UserName =  p.User?.UserName?? string.Empty }).ToList()

            };
            return paginatedProduct;


        }

        public async Task<IQueryable<Order>> GetSortedFilterAsync<TKey>(Expression<Func<Order, TKey>> orderBy, Expression<Func<Order, bool>> searchPredicate = null, bool ascending = true)
        {
            return await _orderRepository.GetSortedFilterAsync(orderBy, searchPredicate, ascending);
        }
        //===========================================================
        // for cash memory
        public void AddToCache(string key, object value, TimeSpan? absoluteExpiration = null)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpiration
            };

            _memoryCache.Set(key, value, cacheEntryOptions);
        }

        public T GetFromCache<T>(string key)
        {
            _memoryCache.TryGetValue(key, out T value);
            return value;
        }
        public List<OrderDTO> AddtoCashMemoery(string key, List<OrderDTO> items)
        {
            var DataIncash = GetFromCache<List<OrderDTO>>(key);
            if (DataIncash != null && DataIncash.Count > 0)
            {
                foreach (var item in items)
                {
                    if (!DataIncash.Any(existingItem => existingItem.Id == item.Id))
                    {
                        DataIncash.Insert(0, item);
                    }
                }
                AddToCache(key, DataIncash, TimeSpan.FromMinutes(30));
                return DataIncash;
            }
            else
            {
                AddToCache(key, items, TimeSpan.FromMinutes(30));
                return items;
            }

        }
        public void RemoveFromCashMemoery(string key, OrderDTO item)
        {
            var DataIncash = GetFromCache<List<OrderDTO>>(key);
            if (DataIncash != null && DataIncash.Count > 0)
            {
                if (DataIncash.Where(existingItem => existingItem.Id == item.Id).ToList().Count == 1)
                {
                    var data = DataIncash.FirstOrDefault(p => p.Id == item.Id);
                    if (data != null)
                    {
                        DataIncash.Remove(data);
                        AddToCache(key, DataIncash, TimeSpan.FromMinutes(30));
                    }
                }
            }
        }
        public void RemoveFromCache(string key)
        {
            _memoryCache.Remove(key);
        }
        public async Task<List<OrderDTO>> GetByUserIdAsync(int userId)
        {
            var orders = await _orderRepository.GetByUserIdAsync(userId)
                .Include(order => order.shipping_Methods)
                .Include(order => order.payment_Methods)
                .Include(order => order.order_State)
                .Include(order => order.Order_Items)
                .ToListAsync();

            return orders.Select(order => new OrderDTO
            {
                Id = order.Id,
                Code = order.Code ?? 0,
                OrderDate = order.order_date,
                TotalAmount = order.Total_amout,
                TrackingNumber = order.tracking_number,
                ShippingAddress = order.shipping_address,
                ShippingMethod = order.shipping_Methods?.Method_Name ?? "N/A",
                ShippingCost = order.shipping_Methods?.Cost,
                UserId = order.UserId,
                OrderState = order.order_State?.Name_EN ?? "N/A",
                OrderStateArabic = order.order_State?.Name ?? "N/A",
                PaymentMethod = order.payment_Methods?.Name ?? string.Empty,
                OrderItems = order.Order_Items?.Select(item => new OrderItemDTO
                {
                    Id = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList() ?? new List<OrderItemDTO>()
            }).ToList();
        }


    }
}