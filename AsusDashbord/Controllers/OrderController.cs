 
namespace AsusDashbord.Controllers
{
    [Authorize(Roles = "Admin")]

    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IShippingMethodService _shippingMethodService;
        private readonly IPaymentMethodService _paymentMethodService;
        private readonly IOrder_StateService _orderStateService;
        private readonly IproductService _productService;
        private readonly IMapper _mapper;
        private string key = "Orders";

        public OrderController(
            IOrderService orderService,
            IShippingMethodService shippingMethodService,
            IPaymentMethodService paymentMethodService,
            IOrder_StateService orderStateService,
            IproductService productService, IMapper mapper)
        {
            _orderService = orderService;
            _shippingMethodService = shippingMethodService;
            _paymentMethodService = paymentMethodService;
            _orderStateService = orderStateService;
            _productService = productService;
            _mapper = mapper;
        }


        public IActionResult UpdateOrderState(int id)
        {
            OrederStatusDTO model = new();
            model.orderId = id;
            model.orderStat = new Dictionary<string, int>
             {
                { "Confirmed", 2 },
                { "Shipped", 3},
                { "Delivered", 4},
                { "Canceled", 5},
                { "Returned",6},
                { "Pending", 1 },
                { "Completed",7}
             };
            return PartialView(model);
        }

        [HttpPost]
        public async Task UpdateOrderState(int id, int OrderStateId)
        {
            await _orderService.UpdateOrderState(id, OrderStateId);
            var order=await _orderService.GetByIdAsync(id);
            _orderService.RemoveFromCashMemoery(key, order);
            _orderService.AddtoCashMemoery(key,new List<OrderDTO>(){ order});

        }


        public async Task<IActionResult> OrderDetails(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            return View(order);
        }
        public async Task<IActionResult> Index(int pageNumber = 1, int PageSize = 15)
        {
            if (OrderService.AllElementNumber == 0)
            {
                _orderService.Intialize();
            }
            int TotalPage = (int)Math.Ceiling((double)OrderService.AllElementNumber / PageSize);

            var cachedData = _productService.GetFromCache<List<OrderDTO>>(key);
            if (pageNumber >= 1 && pageNumber <= TotalPage)
            {
                if (cachedData != null)
                {
                    var PagnetedCachMemory = cachedData.Skip((pageNumber - 1) * PageSize).Take(PageSize).ToList();
                    if (PagnetedCachMemory.Count == 0)
                    {
                        var UpdatedProductList = await _orderService.GetAllAsync(pageNumber, PageSize);
                        _orderService.AddtoCashMemoery(key, UpdatedProductList.Data);
                        return View(UpdatedProductList);
                    }
                    if(PagnetedCachMemory.Count < 15)
                    {
                        var UpdatedProductList = await _orderService.GetAllAsync(pageNumber, PageSize);
                        var addnewOrderToCash = UpdatedProductList.Data.Where(p => !PagnetedCachMemory.Any(a => a.Id == p.Id)).ToList();
                        _orderService.AddtoCashMemoery(key, addnewOrderToCash);
                        return View(UpdatedProductList);

                    }
                    return View(new EntityPaginated<OrderDTO>() { Data = PagnetedCachMemory, CurrentPage = pageNumber, ItemsPerPage = PageSize, TotalPages = TotalPage });
                }
                var allProduct = await _orderService.GetAllAsync(pageNumber, PageSize);

                _orderService.AddToCache(key, allProduct.Data, TimeSpan.FromMinutes(30));
                cachedData = allProduct.Data;
            }
            return View(new EntityPaginated<OrderDTO>() { Data = cachedData, CurrentPage = pageNumber, ItemsPerPage = PageSize, TotalPages = TotalPage });
        }


        public async Task<IActionResult> PartialIndex(string id)
        {
            var cachedData = _productService.GetFromCache<List<OrderDTO>>(key);
            if (cachedData != null)
            {
                if (!string.IsNullOrEmpty(id))
                {
                    cachedData = cachedData.Where(p =>
                               p.Code.ToString()!.Contains(id) ||
                               p.OrderState.IndexOf(id, StringComparison.OrdinalIgnoreCase) >= 0 ||
                               p.UserName.IndexOf(id, StringComparison.OrdinalIgnoreCase) >= 0 ||
                               p.ShippingMethod.IndexOf(id, StringComparison.OrdinalIgnoreCase) >= 0 ||
                               p.ShippingAddress!.IndexOf(id, StringComparison.OrdinalIgnoreCase) >= 0 ||
                               p.TrackingNumber!.Contains(id) ||
                               p.TotalAmount.ToString().Contains(id) ||
                               p.OrderDate.ToString().Contains(id)).ToList();

                    if (cachedData.Count == 0)
                    {
                        var data = await _orderService.GetSortedFilterAsync(p => p.order_date, p =>
                               p.Code.ToString()!.Contains(id) ||
                               p.order_State!.Name_EN!.Contains(id) ||
                               p.shipping_Methods!.Method_Name.Contains(id) ||
                               p.User!.UserName!.Contains(id) ||
                               p.tracking_number!.Contains(id) ||
                               p.Total_amout.ToString().Contains(id) ||
                               p.order_date.ToString().Contains(id));
                        //if no data back return empty list
                        if (data ==null||data.ToList().Count == 0)
                            return PartialView(nameof(PartialIndex), new List<OrderDTO>());
                        var newData =  (await data.Select(p => new OrderDTO(p)).ToListAsync());
                        return PartialView(newData);
                    }
                    else
                        return PartialView(cachedData);
                }
                cachedData = cachedData.Take(15).ToList();
            }
            return PartialView(cachedData);
        }



        public async Task<IActionResult> Create()
        {
            ViewBag.ShippingMethods = await _shippingMethodService.GetAllAsync();
            ViewBag.PaymentMethods = await _paymentMethodService.GetAllAsync();
            ViewBag.OrderStates = await _orderStateService.GetAllAsync();
            ViewBag.Products = await _productService.GetAllAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOrderDTO order)
        {
            if (ModelState.IsValid)
            {
                var result = await _orderService.CreateAsync(order);
                if (result.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Massage);
            }

            ViewBag.ShippingMethods = await _shippingMethodService.GetAllAsync();
            ViewBag.PaymentMethods = await _paymentMethodService.GetAllAsync();
            ViewBag.OrderStates = await _orderStateService.GetAllAsync();
            ViewBag.Products = await _productService.GetAllAsync();

            return View(order);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            ViewBag.ShippingMethods = await _shippingMethodService.GetAllAsync();
            ViewBag.PaymentMethods = await _paymentMethodService.GetAllAsync();
            ViewBag.OrderStates = await _orderStateService.GetAllAsync();
            ViewBag.Products = await _productService.GetAllAsync();

            return View(_mapper.Map<UpdateOrderDTO>(order));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateOrderDTO order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var result = await _orderService.UpdateAsync(order);
                if (result.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Massage);
            }

            ViewBag.ShippingMethods = await _shippingMethodService.GetAllAsync();
            ViewBag.PaymentMethods = await _paymentMethodService.GetAllAsync();
            ViewBag.OrderStates = await _orderStateService.GetAllAsync();
            ViewBag.Products = await _productService.GetAllAsync();

            return View(order);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _orderService.DeleteAsync(id);
            if (success)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }

}
