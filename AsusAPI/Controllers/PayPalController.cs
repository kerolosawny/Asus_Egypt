namespace AsusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayPalController : ControllerBase
    {
        #region payPal
        //    private readonly IConfiguration _configuration;
        //    string ClientId { get; set; } = "";
        //    string Secrete { get; set; } = "";
        //    string Url { get; set; } = "";

        //    public PayPalController(IConfiguration configuration)
        //    {
        //        _configuration = configuration;
        //        ClientId = configuration["PayPalSettings:ClientId"]!;
        //        Secrete = configuration["PayPalSettings:Secret"]!;
        //        Url = configuration["PayPalSettings:Url"]!;
        //    }

        //    private APIContext GetApiContext()
        //    {
        //        var config = ConfigManager.Instance.GetProperties();
        //        var accessToken = new OAuthTokenCredential(ClientId, Secrete).GetAccessToken();
        //        var apiContext = new APIContext(accessToken) { Config = config };
        //        return apiContext;
        //    }
        //    [HttpGet]
        //    public   async Task<string> GetPayPalAccessToken()
        //    {
        //        #region MyRegion
        //        //string AccessToken = "";
        //        //string url = Url + "/v1/oauth2/token";
        //        //using (HttpClient client = new())
        //        //{
        //        //    string creditalsBas64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(ClientId + ":" + Secrete));
        //        //    client.DefaultRequestHeaders.Add("Authorization", "Basic" + creditalsBas64);
        //        //    var requestMassage = new HttpRequestMessage(HttpMethod.Post, url);
        //        //    requestMassage.Content = new StringContent("grant_type=client_credentials", null, "application/x-www-form-urlencoded");
        //        //    var response = await client.SendAsync(requestMassage);
        //        //    if (response.IsSuccessStatusCode)
        //        //    {
        //        //        var strResponse = await response.Content.ReadAsStringAsync();
        //        //        var jsonResponse = JsonNode.Parse(strResponse);
        //        //        if (jsonResponse != null)
        //        //        {
        //        //            AccessToken = jsonResponse["access_token"]?.ToString() ?? "";
        //        //        }
        //        //    }
        //        //}
        //        //return AccessToken;
        //        #endregion
        //        var AccessToken = GetApiContext();
        //        return   AccessToken.AccessToken;
        //    }

        //    [HttpPost]
        //    public async Task<JsonResult> CreateOrder([FromBody] string totalAmount)
        //    {
        //       // var totalAmount = data?["amount"]?.ToString();
        //        if (totalAmount == null)
        //        {
        //            return new JsonResult(new { Id = "" });
        //        }
        //        // create the request body
        //        JsonObject createOrderRequest = new JsonObject();
        //        createOrderRequest.Add("intent", "CAPTURE");
        //        JsonObject amount = new JsonObject();
        //        amount.Add("currency_code", "USD");
        //        amount.Add("value", totalAmount);
        //        JsonObject purchaseUnit1 = new JsonObject();
        //        purchaseUnit1.Add("amount", amount);
        //        JsonArray purchaseUnits = new JsonArray();
        //        purchaseUnits.Add(purchaseUnit1);
        //        createOrderRequest.Add("purchase_units", purchaseUnits);

        //        string accesstoken = await GetPayPalAccessToken();
        //        string url = Url + "/v2/checkout/orders";

        //        using (HttpClient client = new())
        //        {
        //            client.DefaultRequestHeaders.Add("Authorization", accesstoken);
        //            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
        //            requestMessage.Content = new StringContent(createOrderRequest.ToString(), null, "application/json");
        //            var httpResponse = await client.SendAsync(requestMessage);
        //            if (httpResponse.IsSuccessStatusCode)
        //            {
        //                var strResponse = await httpResponse.Content.ReadAsStringAsync();
        //                var jsonResponse = JsonNode.Parse(strResponse);
        //                if (jsonResponse != null)
        //                {
        //                    string paypalOrderId = jsonResponse["id"]?.ToString() ?? "";
        //                    return new JsonResult(new { Id = paypalOrderId });
        //                }
        //            }
        //        }
        //        return new JsonResult(new { Id = "" });
        //    }


        //    [HttpPost("CompleteOrder")]
        //    public async Task<JsonResult> CompleteOrder([FromBody] JsonObject data)
        //    {
        //        var orderId = data?["orderID"]?.ToString();
        //        if (orderId == null)
        //        {
        //            return new JsonResult("error");

        //        }
        //        // get access token
        //        string accessToken = await GetPayPalAccessToken();


        //        string url = Url + "/v2/checkout/orders/" + orderId + "/capture";

        //        using (HttpClient client = new())
        //        {
        //            client.DefaultRequestHeaders.Add("Authorization", accessToken);
        //            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
        //            requestMessage.Content = new StringContent("", null, "application/json");
        //            var httpResponse = await client.SendAsync(requestMessage);

        //            if (httpResponse.IsSuccessStatusCode)
        //            {
        //                var strResponse = await httpResponse.Content.ReadAsStringAsync();
        //                var jsonResponse = JsonNode.Parse(strResponse);
        //                if (jsonResponse != null)
        //                {
        //                    string paypalOrderStatus = jsonResponse["status"]?.ToString() ?? "";
        //                    if (paypalOrderStatus == "COMPLETED")
        //                    {
        //                        return new JsonResult("success");
        //                    }
        //                }
        //            }
        //        }


        //        return new JsonResult("error");
        //    } 
        #endregion
    }
}
