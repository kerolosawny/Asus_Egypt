
namespace AsusDashbord.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly ICategoryService _category;
        private readonly IproductService _iproduct;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private static decimal TotalAmount;
        private static decimal TotalAmountLastmonth;
        private static int order;
        private static int YasterdayOrder;
        public HomeController(IproductService iproduct, ICategoryService category, IOrderService orderService, IUserService userService)
        {
            _iproduct = iproduct;
            _category = category;
            _orderService = orderService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await GetTopSellingProductsAsync();
            ViewBag.user = await _userService.EntityCount();
            ViewBag.order = order;
            ViewBag.IncreseOr = order - YasterdayOrder;
            ViewBag.TotalAmount = TotalAmount;
            ViewBag.DefferinceAmount = TotalAmount - TotalAmountLastmonth;
            return View(model);
        }

        public async Task<List<TopSellingProductDto>> GetTopSellingProductsAsync()
        {
            var today = DateTime.Today;
            var startOfMonth = new DateTime(today.Year, today.Month, 1);
            var startOfLastMonth = startOfMonth.AddMonths(-1);
            var endOfLastMonth = startOfMonth.AddDays(-1);
            var orderItems = await _orderService.GetTopSellingProductsAsync();
            var topSellingProducts = orderItems
               .Where(oi => oi.Orders.order_date.Date == today)
               .GroupBy(oi => new { oi.ProductId, oi.Products.Name, oi.Products.image_url, oi.Products.price })
               .Select(productGroup => new TopSellingProductDto
               {
                   ImageUrl = productGroup.Key.image_url,
                   ProductName = productGroup.Key.Name,
                   Price = productGroup.Key.price,
                   QuantitySold = productGroup.Sum(x => x.Quantity),
                   Revenue = productGroup.Sum(x => x.Quantity * x.Price)
               })
               .OrderByDescending(product => product.Revenue)
               .Take(15)
               .ToList();
            order = orderItems.Where(p => p.Orders.order_date.Date == today).Select(p => p.OrderId).Distinct().Count();
            YasterdayOrder = orderItems.Where(p => p.Orders.order_date.Date == today.AddDays(-1)).Select(p => p.OrderId).Distinct().Count();
            TotalAmount = orderItems
                         .Where(p => p.Orders.order_date >= startOfMonth && p.Orders.order_date < startOfMonth.AddMonths(1))
                         .Sum(x => x.Quantity * x.Price);
            TotalAmountLastmonth = orderItems
                .Where(p => p.Orders.order_date >= startOfLastMonth && p.Orders.order_date <= endOfLastMonth)
                .Sum(x => x.Quantity * x.Price);
            return topSellingProducts;

        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
#region MyRegion
//[HttpPost]
//public async Task<JsonResult> CreateOrder([FromBody] JsonObject data)
//{
//    var totalAmount = data?["amount"]?.ToString();
//    if (totalAmount == null)
//    {
//        return new JsonResult(new { Id = "" });
//    }


//    // create the request body
//    JsonObject createOrderRequest = new JsonObject();
//    createOrderRequest.Add("intent", "CAPTURE");
//    JsonObject amount = new JsonObject();
//    amount.Add("currency_code", "USD");
//    amount.Add("value", totalAmount);

//    JsonObject purchaseUnit1 = new JsonObject();
//    purchaseUnit1.Add("amount", amount);
//    JsonArray purchaseUnits = new JsonArray();
//    purchaseUnits.Add(purchaseUnit1);
//    createOrderRequest.Add("purchase_units", purchaseUnits);

//    string accesstoken = await GetPayPalAccessToken();
//    string url = Url + "/v2/checkout/orders";

//    using (HttpClient client = new())
//    {
//        client.DefaultRequestHeaders.Add("Authorization", accesstoken);
//        var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
//        requestMessage.Content = new StringContent(createOrderRequest.ToString(), null, "application/json");
//        var httpResponse = await client.SendAsync(requestMessage);
//        if (httpResponse.IsSuccessStatusCode)
//        {
//            var strResponse = await httpResponse.Content.ReadAsStringAsync();
//            var jsonResponse = JsonNode.Parse(strResponse);
//            if (jsonResponse != null)
//            {
//                string paypalOrderId = jsonResponse["id"]?.ToString() ?? "";
//                return new JsonResult(new { Id = paypalOrderId });
//            }
//        }
//    }
//    return new JsonResult(new { Id = "" });
//}


//[HttpPost]
//public async Task<JsonResult> CompleteOrder([FromBody] JsonObject data)
//{
//    var orderId = data?["orderID"]?.ToString();
//    if (orderId == null)
//    {
//        return new JsonResult("error");

//    }
//    // get access token
//    string accessToken = await GetPayPalAccessToken();


//    string url = Url + "/v2/checkout/orders/" + orderId + "/capture";

//    using (HttpClient client = new())
//    {
//        client.DefaultRequestHeaders.Add("Authorization",  accessToken);
//        var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
//        requestMessage.Content = new StringContent("", null, "application/json");
//        var httpResponse = await client.SendAsync(requestMessage);

//        if (httpResponse.IsSuccessStatusCode)
//        {
//            var strResponse = await httpResponse.Content.ReadAsStringAsync();
//            var jsonResponse = JsonNode.Parse(strResponse);
//            if (jsonResponse != null)
//            {
//                string paypalOrderStatus = jsonResponse["status"]?.ToString() ?? "";
//                if (paypalOrderStatus == "COMPLETED")
//                {
//                    return new JsonResult("success");
//                }
//            }
//        }
//    }


//    return new JsonResult("error");
//}


//private async Task<string> GetPayPalAccessToken()
//{
//    #region MyRegion
//    //string AccessToken = "";
//    //string url = Url + "/v1/oauth2/token";
//    //using (HttpClient client = new())
//    //{
//    //    string creditalsBas64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(ClientId + ":" + Secrete));
//    //    client.DefaultRequestHeaders.Add("Authorization", "Basic" + creditalsBas64);
//    //    var requestMassage = new HttpRequestMessage(HttpMethod.Post, url);
//    //    requestMassage.Content = new StringContent("grant_type=client_credentials", null, "application/x-www-form-urlencoded");
//    //    var response = await client.SendAsync(requestMassage);
//    //    if (response.IsSuccessStatusCode)
//    //    {
//    //        var strResponse = await response.Content.ReadAsStringAsync();
//    //        var jsonResponse = JsonNode.Parse(strResponse);
//    //        if (jsonResponse != null)
//    //        {
//    //            AccessToken = jsonResponse["access_token"]?.ToString() ?? "";
//    //        }
//    //    }
//    //}
//    //return AccessToken;
//    #endregion
//    var AccessToken = GetApiContext();
//    return AccessToken.AccessToken;
//}
//private APIContext GetApiContext()
//{
//    var config = ConfigManager.Instance.GetProperties();
//    var accessToken = new OAuthTokenCredential(ClientId, Secrete).GetAccessToken();
//    var apiContext = new APIContext(accessToken) { Config = config };
//     return apiContext;
//}
#endregion