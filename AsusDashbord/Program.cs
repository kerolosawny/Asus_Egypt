namespace AsusDashbord
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<AsusDbContext>(options =>
            options.UseSqlServer(connectionString, o => o.EnableRetryOnFailure(6).CommandTimeout(300).UseQuerySplittingBehavior(querySplittingBehavior: QuerySplittingBehavior.SingleQuery)));
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IproductService, ProductService>();
            builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
            builder.Services.AddScoped<IDiscountService, DiscountService>();

            builder.Services.AddScoped<IPayment_methodRepository, PaymentMethodRepository>();
            builder.Services.AddScoped<IPaymentMethodService, PaymentMethodService>();
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();


            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IproductService, ProductService>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IOrderService, OrderService>();

            builder.Services.AddScoped<IOrder_ItemsRepository, Order_ItemsRepository>();
            builder.Services.AddScoped<IOrder_stateRepository, Order_stateRepository>();
            builder.Services.AddScoped<IOrder_StateService, Order_StateService>();

            builder.Services.AddScoped<IReviewsRepository, ReviewRepository>();
            builder.Services.AddScoped<IReviewsService, ReviewsService>();

            builder.Services.AddScoped<IShipping_MethodRepository, Shipping_MethodRepository>();
            builder.Services.AddScoped<IShippingMethodService, ShippingMethodService>();

            builder.Services.AddScoped<ISpecificationKeyRepository, SpecificationKeyRepository>();
            builder.Services.AddScoped<ISpecificationKeyService, SpecificationKeyService>();
            builder.Services.AddScoped<ICard_TypeRepository, Card_TypeRepository>();
            builder.Services.AddScoped<ICard_TypeService, Card_TypeService>();
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            //for cash memory
            builder.Services.AddMemoryCache();
            builder.Services.AddIdentity<User, IdentityRole<int>>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<AsusDbContext>().AddDefaultTokenProviders();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                 .AddCookie(options =>
                 {
                     options.LoginPath = "/Account/Login";
                     options.AccessDeniedPath = "/Account/AccessDenied"; // Optional: Redirect for unauthorized access attempts
                 });

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");
            app.MapRazorPages();
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
            }

            app.Run();
        }
    }
}
