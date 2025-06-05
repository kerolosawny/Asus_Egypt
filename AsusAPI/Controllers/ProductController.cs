
namespace AsusAPI.Controllers
{
    //[Authorize(Roles ="Admin,User")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IproductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ICategoryService _subCategoryService;
        private readonly ISpecificationKeyService _specificationKeyService;
        private readonly IDiscountService _discountService;
        private readonly IMapper _mapper;
        private string key = "Products";

        public ProductController(
            IproductService productService,
            ICategoryService categoryService,
            ICategoryService subCategoryService,
            ISpecificationKeyService specificationKeyService,
            IDiscountService discountService, IMapper mapper)
        {
            _productService = productService;
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
            _specificationKeyService = specificationKeyService;
            _discountService = discountService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int PageSize = 15, bool IsEnglish = true)
        {
            try
            {
                if (ProductService.AllElementNumber == 0)
                {
                    _productService.Intialize();
                }
                int TotalPage = (int)Math.Ceiling((double)ProductService.AllElementNumber / PageSize);
                var cachedData = _productService.GetFromCache<List<ProductDTO>>(key);
                if (pageNumber >= 1 && pageNumber <= TotalPage)
                {
                    if (cachedData != null)
                    {
                        var PagnetedCachMemory = cachedData.Skip((pageNumber - 1) * PageSize).Take(PageSize).ToList();
                        if (PagnetedCachMemory.Count == 0)
                        {
                            var UpdatedProductList = await _productService.GetAllAsync(pageNumber, PageSize);
                            _productService.AddtoCashMemoery(key, UpdatedProductList.Data);
                        }
                    }
                    else
                    {
                        var allProduct = await _productService.GetAllAsync(pageNumber, PageSize);
                        _productService.AddToCache(key, allProduct.Data, TimeSpan.FromMinutes(30));
                    }
                }
                cachedData = _productService.GetFromCache<List<ProductDTO>>(key);
                cachedData = cachedData.Skip((pageNumber - 1) * PageSize).Take(PageSize).ToList();
                if (IsEnglish)
                {

                    return Ok(cachedData.Select(p => new ProductDTO
                    {
                        Id = p.Id,
                        Name_EN = p.Name_EN,
                        Description_EN = p.Description_EN,
                        image_url = p.image_url,
                        ImageURLs = p.ImageURLs,
                        Discounts = p.Discounts,
                        Categories = p.Categories,
                        Specifications = p.Specifications,
                        Unit_Instock = p.Unit_Instock,
                        priceAfterDiscount = p.priceAfterDiscount,
                        Price = p.Price,
                        Code=p.Code
                    }));
                }
                else
                {
                    return Ok(cachedData.Select(p => new ProductDTO
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        image_url = p.image_url,
                        ImageURLs = p.ImageURLs,
                        Discounts = p.Discounts,
                        Categories = p.Categories,
                        Specifications = p.Specifications,
                        Unit_Instock = p.Unit_Instock,
                        Price = p.Price,
                        priceAfterDiscount = p.priceAfterDiscount,
                        Code = p.Code

                    }));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error retrieving products", error = ex.Message });
            }
        }




        [HttpGet("by-category/{categoryId}")]
        public async Task<IActionResult> GetProductsByCategory(int categoryId)
        {
            try
            {
                //var cachedData = _productService.GetFromCache<List<ProductDTO>>(key);
                //if (cachedData == null)
                //    cachedData = new List<ProductDTO>();
                //cachedData = cachedData.Where(p => p.Categories.Any(p => p == categoryId)).ToList();
                //if (cachedData.Count > 0)
                //{
                //    return Ok(cachedData);
                //}
                //else
                //{
                //var t = await _productService.GetSortedFilterAsync(p => p.Created_at,
                //     p => p.Product_Category.Any(x => x.CategoryId == categoryId));
                //var xx = t.ToList();
                //+++++++++++++++++++++++ test here data


                var productDtos = await _productService.GetProductsByCategoryIdAsync(categoryId);
                if (productDtos == null || !productDtos.Any())
                {
                    return NotFound(new { message = "No products found for the specified category." });
                }
                return Ok(productDtos);
                //}

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error retrieving products", error = ex.Message });
            }
        }

        [HttpGet("by-specification-values")]
        public async Task<IActionResult> GetProductsBySpecificationValues([FromQuery] List<string> specificationValues)
        {
            if (specificationValues == null || !specificationValues.Any())
            {
                return BadRequest("At least one specification value is required.");
            }

            try
            {
                //var cachedData = _productService.GetFromCache<List<ProductDTO>>(key);
                //if (cachedData == null)
                //    cachedData = new List<ProductDTO>();
                //cachedData = cachedData.Where(p => specificationValues.All(specValue => p.Specifications.Any(ps => ps.Value == specValue))).ToList();
                //if (cachedData.Count > 0)
                //{
                //    return Ok(cachedData);
                //}
                //else
                //{

                var products = await _productService.GetProductBySpecificationValues(specificationValues);
                if (products == null || !products.Any())
                {
                    return Ok(new List<ProductDTO>()); 
                }
                return Ok(products);
                //}

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error retrieving products", error = ex.Message });
            }
        }


        #region payPal

        //    private static string AccessToken;

        //    private APIContext GetApiContext()
        //    {
        //        var config = ConfigManager.Instance.GetProperties();
        //        var accessToken = new OAuthTokenCredential(clientId, clientSecret).GetAccessToken();
        //        var apiContext = new APIContext(accessToken) { Config = config };
        //        AccessToken = apiContext.AccessToken;
        //        return apiContext;
        //    }

        //    private string CreateOrder()
        //    {
        //        var apiContext = GetApiContext();

        //        var payer = new Payer() { payment_method = "paypal" };
        //        var redirectUrls = new RedirectUrls()
        //        {
        //            //cancel_url = Url.Action("Cancel", "Payment", null, Request.Method),
        //            //return_url = Url.Action("Success", "Payment", null, Request.Scheme)
        //            cancel_url = "http://example.com/cancel",
        //            return_url = "http://example.com/success"
        //        };

        //        var amount = new Amount()
        //        {
        //            currency = "USD",
        //            total = "100.00"
        //        };

        //        var transactionList = new List<Transaction>
        //{
        //    new Transaction
        //    {
        //        description = "Transaction description.",
        //        amount = amount
        //    }
        //};

        //        var payment = new Payment
        //        {
        //            intent = "sale",
        //            payer = payer,
        //            transactions = transactionList,
        //            redirect_urls = redirectUrls
        //        };

        //        var createdPayment = payment.Create(apiContext);
        //        var approvalUrl = createdPayment.links.FirstOrDefault(link => link.rel.ToLower() == "approval_url")?.href;

        //        return approvalUrl; // Return the approval URL to redirect the user
        //    }
        //    [HttpGet("Pay")]
        //    public ActionResult Pay()
        //    {
        //        var approvalUrl = CreateOrder();
        //        return Redirect(approvalUrl); // Redirect to PayPal for approval
        //    }

        //    private void CapturePayment(string paymentId)
        //    {
        //        var apiContext = GetApiContext();
        //        var paymentExecution = new PaymentExecution() { payer_id = paymentId }; // Obtain the payer_id from the query string

        //        var payment = new Payment() { id = paymentId };
        //        var executedPayment = payment.Execute(apiContext, paymentExecution);

        //        if (executedPayment.state.ToLower() == "approved")
        //        {
        //            // Payment was successful
        //            Console.WriteLine("Payment captured successfully.");
        //            // Perform additional actions like updating your database
        //        }
        //        else
        //        {
        //            // Handle payment failure
        //            Console.WriteLine("Payment not approved.");
        //        }
        //    }
        //    [HttpGet("Success")]
        //    // In your return action method:
        //    public ActionResult Success(string paymentId, string token)
        //    {
        //        CapturePayment(paymentId); // Capture the payment using the payment ID
        //        return Ok(); // Return a view to indicate success
        //    }

        #endregion
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneDetailed(int id)
        {
            try
            {
                //var cachedData = _productService.GetFromCache<List<ProductDTO>>(key);
                //var product = cachedData.FirstOrDefault(p => p.Id == id);
                //if (product == null)
                //{
                var productDto = await _productService.GetOneAsync(id);

                if (productDto == null)
                {
                    return NotFound(new { message = "Product not found." });
                }
                return Ok(productDto);
                //}
                //else
                //{
                //    return Ok(product);

                //}

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error retrieving product", error = ex.Message });
            }
        }

        [HttpGet("check-stock/{productId}")]
        public async Task<IActionResult> CheckStock(int productId)
        {
            try
            {
                var product = await _productService.GetOneAsync(productId);

                if (product == null)
                {
                    return NotFound(new { message = "Product not found." });
                }

                if (product.Unit_Instock > 0)
                {
                    return Ok(new { productId = productId, inStock = true, unitsInStock = product.Unit_Instock });
                }
                else
                {
                    return Ok(new { productId = productId, inStock = false, unitsInStock = 0 });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error checking product stock", error = ex.Message });
            }
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search(string id)
        {
            List<ProductDTO> productData = new List<ProductDTO>();

            if (!string.IsNullOrEmpty(id))
            {

                var data = await _productService.GetSortedFilterAsync(p => p.Name_EN, p => p.Name.Contains(id) || p.Name_EN.Contains(id) || p.description!.ToString().Contains(id) || p.description_EN!.ToString().Contains(id) || p.ProductSpecifications!.Any(s=>s.Value.Contains(id)) || p.Product_Category!.Any(c=> c.Category!.Name_EN.Contains(id)) || p.Product_Category!.Any(c => c.Category!.Name.Contains(id)));


                var dataList =  data.ToList();
                productData = dataList.Select(product => new ProductDTO(product)).ToList();

            }

            return Ok(productData);
        }
        [HttpGet("Panner")]
        public async Task<IActionResult> SliderImages()
        {
            List<ProductDTO> productData = new List<ProductDTO>();

           // var targetNames = new List<string> { "Asus Zenbook S 14", "ASUS Zenbook S 16" };

            var data = await _productService.GetSortedFilterAsync(
                p => p.Name_EN,
                p => p.IsActive == true
            );
            var dataList = data.Take(2);
            if (dataList.Count() == 2)
            {
                productData = dataList.Select(product => new ProductDTO(product)).ToList();
                return Ok(productData);
                
            }

            var top2 = _productService.GetSortedFilterAsync(p => p.Name_EN, p => p.image_url != null);
            var top2List = top2.Result.Take(2).ToList();
            productData = top2List.Select(product => new ProductDTO(product)).ToList();
            return Ok(productData);

        }
        //    [HttpGet("images")]
        //public async Task<IActionResult> images()
        //{
        //    List<ProductDTO> productData = new List<ProductDTO>();

        //    var targetNames = new List<string> { "ASUS Zenbook 14 OLED", "ASUS Vivobook S 15" };

        //    var data = await _productService.GetSortedFilterAsync(
        //        p => p.Name_EN,
        //        p => targetNames.Contains(p.Name_EN)
        //    );

        //    var dataList = data.ToList();
        //    if (dataList.Count ==2)
        //    {
        //        productData = dataList.Select(product => new ProductDTO(product)).ToList();

        //        return Ok(productData);            
        //    }
        //    var top2 = _productService.GetSortedFilterAsync(p => p.Name_EN, p => p.image_url != null);
        //    var top2List = top2.Result.Skip(Math.Max(0, top2.Result.Count() - 2)).Take(2).ToList();

        //    productData = top2List.Select(product => new ProductDTO(product)).ToList();
        //    return Ok(productData);


        //}
        [HttpGet("With-discount-id")]
        public async Task<IActionResult> GetDiscountedProducts(int DiscountId)
        {
            List<ProductDTO> productData = new List<ProductDTO>();

            if (DiscountId > 0)
            {
                var data = await _productService.GetSortedFilterAsync(p=> p.Name,p => p.Discount!=null? p.Discount.Any(d => d.DiscountId == DiscountId):false);

                var dataList = data.ToList();
                productData = dataList.Select(product => new ProductDTO(product)).ToList();
            }

            return Ok(productData);
        }
        [HttpGet("best-Deals")]
        public async Task<IActionResult> BestDeals()
        {
            List<ProductDTO> productData = new List<ProductDTO>();

            var data = await _productService.GetSortedFilterAsync(p => p.Name_EN, p => p.Discount!=null? p.Discount.Where(p=>p.DiscountId!=1).Any(d => d.DiscountId != 0):false);
            
            var dataList = data.Take(6);

            productData = data.Select(product => new ProductDTO(product)).ToList();

            return Ok(productData);
        }








    }
}











#region PayPal 
//    private static string AccessToken;
//    private readonly string clientId = "AV_7qzBjFtWINtu9Iu8kABwSUV3hJEW6E-eiLr6i6nVsv6Ar1Mki_lNDhWxYSHjKtF7ezr7cDXkmr83o";
//    private readonly string clientSecret = "EK6pT1l2_6n-1PwYCE_usq-7u-kfYj_rqpH8DWpBgWbQX0E5-EpHmUlGQgRUWnaI5Ff_90Fp0kLwQlQl";

//    private APIContext GetApiContext()
//    {
//        var config = ConfigManager.Instance.GetProperties();
//        var accessToken = new OAuthTokenCredential(clientId, clientSecret).GetAccessToken();
//        var apiContext = new APIContext(accessToken) { Config = config };
//        AccessToken = apiContext.AccessToken;
//        return apiContext;
//    }

//    private string CreateOrder()
//    {
//        var apiContext = GetApiContext();

//        var payer = new Payer() { payment_method = "paypal" };
//        var redirectUrls = new RedirectUrls()
//        {
//            //cancel_url = Url.Action("Cancel", "Payment", null, Request.Method),
//            //return_url = Url.Action("Success", "Payment", null, Request.Scheme)
//            cancel_url = "http://example.com/cancel",
//            return_url = "http://example.com/success"
//        };

//        var amount = new Amount()
//        {
//            currency = "USD",
//            total = "100.00"
//        };

//        var transactionList = new List<Transaction>
//{
//    new Transaction
//    {
//        description = "Transaction description.",
//        amount = amount
//    }
//};

//        var payment = new Payment
//        {
//            intent = "sale",
//            payer = payer,
//            transactions = transactionList,
//            redirect_urls = redirectUrls
//        };

//        var createdPayment = payment.Create(apiContext);
//        var approvalUrl = createdPayment.links.FirstOrDefault(link => link.rel.ToLower() == "approval_url")?.href;
//        var id = createdPayment.id;
//        var cartID = createdPayment.cart;
//        CapturePayment(id);
//        return approvalUrl;
//    }
//    [HttpGet("Pay")]
//    public ActionResult Pay()
//    {
//        var approvalUrl = CreateOrder();
//        return Redirect(approvalUrl); // Redirect to PayPal for approval
//    }

//    private void CapturePayment(string paymentId)
//    {
//        var apiContext = GetApiContext();
//        var paymentExecution = new PaymentExecution() { payer_id = paymentId };

//        var payment = new Payment() { id = paymentId };
//        var executedPayment = payment.Execute(apiContext, paymentExecution);

//        if (executedPayment.state.ToLower() == "approved")
//        {
//            // Payment was successful
//            Console.WriteLine("Payment captured successfully.");
//            // Perform additional actions like updating your database
//        }
//        else
//        {
//            // Handle payment failure
//            Console.WriteLine("Payment not approved.");
//        }
//    }
//    [HttpGet("Success")]
//    // In your return action method:
//    public ActionResult Success(string paymentId, string token)
//    {
//        CapturePayment(paymentId); // Capture the payment using the payment ID
//        return Ok(); // Return a view to indicate success
//    }

#endregion
