
using System.Diagnostics;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var url = builder.Configuration["jwt:Audience"];
        Debug.WriteLine(url);
        Debug.WriteLine("================================================");

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAngularApp",
                builder =>
                {
                    builder.WithOrigins(url!)
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials(); 
                });
        });

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<AsusDbContext>(options =>
        options.UseSqlServer(connectionString/*, o => o.EnableRetryOnFailure(40, TimeSpan.FromMinutes(3), errorNumbersToAdd: null), c => c.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))*/));

        builder.Services.AddIdentity<User, IdentityRole<int>>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<AsusDbContext>();

        builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
        builder.Services.AddMemoryCache();

        builder.Services.AddScoped<DiscountService>();
        builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
        builder.Services.AddScoped<IDiscountService, DiscountService>();

        builder.Services.AddScoped<IPayment_methodRepository, PaymentMethodRepository>();
        builder.Services.AddScoped<IPaymentMethodService, PaymentMethodService>();

        builder.Services.AddScoped<ICard_TypeRepository, Card_TypeRepository>();
        builder.Services.AddScoped<ICard_TypeService, Card_TypeService>();

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserService, UserService>();

        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IproductService, ProductService>();

        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();

        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IOrderService, OrderService>();

        //builder.Services.AddScoped<IOrder_ItemsRepository, Order_ItemsRepository>();


        builder.Services.AddScoped<IShipping_MethodRepository, Shipping_MethodRepository>();
        builder.Services.AddScoped<IShippingMethodService, ShippingMethodService>();

        builder.Services.AddScoped<IOrder_stateRepository, Order_stateRepository>();
        builder.Services.AddScoped<IOrder_StateService, Order_StateService>();

        builder.Services.AddScoped<ISpecificationKeyRepository, SpecificationKeyRepository>();
        builder.Services.AddScoped<ISpecificationKeyService, SpecificationKeyService>();

        builder.Services.AddScoped<IOrder_ItemsRepository, Order_ItemsRepository>();
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
       .AddJwtBearer(options =>
       {
           options.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuer = true,
               ValidateAudience = true,
               ValidateLifetime = true,
               ValidateIssuerSigningKey = true,
               ValidIssuer = builder.Configuration["jwt:Issuer"],
               ValidAudience = builder.Configuration["jwt:Audience"],
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:key"]!))
           };
       });
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "My API",
                Version = "v1"
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT with Bearer into field",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                                   {
                                     new OpenApiSecurityScheme
                                     {
                                       Reference = new OpenApiReference
                                       {
                                         Type = ReferenceType.SecurityScheme,
                                         Id = "Bearer"
                                       }
                                      },
                                      new string[] { }
                                    }
                                  });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();

        app.UseCors("AllowAngularApp");

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
