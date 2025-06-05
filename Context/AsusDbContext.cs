using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model;


namespace Context
{
    public class AsusDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public AsusDbContext(DbContextOptions<AsusDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product_Categoty> Product_Categories { get; set; }
        public DbSet<Card_Type> card_Types { get; set; }

        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Order_Items> Order_Items { get; set; }
        public DbSet<Order_State> Order_States { get; set; }
        public DbSet<Payment_Methods> Payment_Methods { get; set; }
        public DbSet<Product_Discount> Product_Discounts { get; set; }
        public DbSet<Product_Image> Product_Images { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Shipping_Methods> Shipping_Methods { get; set; }
        public DbSet<User_Reviews> User_Reviews { get; set; }


        public DbSet<SpecificationKey> SpecificationKeys { get; set; }
        public DbSet<ProductSpecification> ProductSpecifications { get; set; }
        public DbSet<CategorySpecificationKey> CategorySpecificationKeys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region Seeding data
            // Seeding Order_State table
            modelBuilder.Entity<Order_State>().HasData(
                new Order_State
                {
                    Id = 1,
                    Name = "قيد الانتظار",
                    Name_EN = "Pending"
                },
                new Order_State
                {
                    Id = 2,
                    Name = "مؤكد",
                    Name_EN = "Confirmed"
                },
                new Order_State
                {
                    Id = 3,
                    Name = "تم الشحن",
                    Name_EN = "Shipped"
                },
                new Order_State
                {
                    Id = 4,
                    Name = "تم التوصيل",
                    Name_EN = "Delivered"
                },
                new Order_State
                {
                    Id = 5,
                    Name = "تم الإلغاء",
                    Name_EN = "Canceled"
                },
                new Order_State
                {

                    Id = 6,
                    Name = "تم إرجاع الطلب",
                    Name_EN = "Returned"
                },
                new Order_State
                {
                    Id = 7,
                    Name = "مكتمل",
                    Name_EN = "Completed"
                }
            );

            // Seeding Payment_Methods table
            modelBuilder.Entity<Payment_Methods>().HasData(
                new Payment_Methods
                {
                    Id = 1,
                    Name = "PayPal",
                    Is_Default = false
                },
                new Payment_Methods
                {
                    Id = 2,
                    Name = "MasterCard",
                    Is_Default = false
                },
                new Payment_Methods
                {
                    Id = 3,
                    Name = "Cash",
                    Is_Default = false
                },
                new Payment_Methods
                {
                    Id = 4,
                    Name = "Visa",
                    Is_Default = false
                }
            );

            // Seeding Discount table
            modelBuilder.Entity<Discount>().HasData(
                new Discount
                {
                    Id = 1,
                    Name = "Remove Discount",
                    Name_EN = "Remove Discount",
                    Code = 123456,
                    Start_date = new DateTime(2024, 06, 01),
                    End_date = new DateTime(2024, 08, 31),
                    IsActive = true,
                    Percentage = 0.0m
                }, new Discount
                {
                    Id = 2,
                    Name = "تخفيضات الصيف",
                    Name_EN = "Summer Sale",
                    Code = 123456,
                    Start_date = new DateTime(2024, 06, 01),
                    End_date = new DateTime(2024, 08, 31),
                    IsActive = true,
                    Percentage = 10.0m
                },
                new Discount
                {
                    Id = 3,
                    Name = "Black Friday",
                    Name_EN = "Black Friday",
                    Code = 654321,
                    Start_date = new DateTime(2024, 11, 01),
                    End_date = new DateTime(2024, 11, 30),
                    IsActive = true,
                    Percentage = 25.0m
                },
                new Discount
                {
                    Id = 4,
                    Name = "خصم الشتاء",
                    Name_EN = "Winter Discount",
                    Code = 112233,
                    Start_date = new DateTime(2024, 12, 01),
                    End_date = new DateTime(2024, 12, 31),
                    IsActive = false,
                    Percentage = 15.0m
                }
            );

            modelBuilder.Entity<Shipping_Methods>().HasData(
    new Shipping_Methods
    {
        Id = 1,
        Method_Name = "Standard Shipping",
        Cost = 5.99m,
        Estimated_Delivery_Time = new DateTime(2024, 11, 15)
    },
    new Shipping_Methods
    {

        Id = 2,
        Method_Name = "Express Shipping",
        Cost = 12.99m,
        Estimated_Delivery_Time = new DateTime(2024, 11, 10)
    },
    new Shipping_Methods
    {

        Id = 3,
        Method_Name = "Overnight Shipping",
        Cost = 25.99m,
        Estimated_Delivery_Time = new DateTime(2024, 11, 07)
    }
    //new Shipping_Methods
    //{
    //    Id = 4,
    //    Method_Name = "Free Shipping",
    //    Cost = 0.00m,
    //    Estimated_Delivery_Time = new DateTime(2024, 11, 20)
    //}
);

            modelBuilder.Entity<Category>().HasData(
    // Parent Categories
    new Category
    {
        Id = 1,
        Name = "Mobile",
        Name_EN = "Mobile",
        Code = 101,
        ParentCategoryID = null
    },
    new Category
    {
        Id = 2,
        Name = "لابتوب",
        Name_EN = "Laptops",
        Code = 102,
        ParentCategoryID = null
    },
    new Category
    {
        Id = 3,
        Name = "كمبيوتر / شاشات",
        Name_EN = "Displays / Desktops",
        Code = 103,
        ParentCategoryID = null
    },
    new Category
    {
        Id = 4,
        Name = "اللوحات الأم/مكونات",
        Name_EN = "Motherboards / Components",
        Code = 104,
        ParentCategoryID = null
    },
    new Category
    {
        Id = 5,
        Name = "الشبكات / إنترنت الأشياء / السيرفرات",
        Name_EN = "Networking / IoT / Servers",
        Code = 105,
        ParentCategoryID = null
    },
    new Category
    {
        Id = 6,
        Name = "إكسسوارات",
        Name_EN = "Accessories",
        Code = 106,
        ParentCategoryID = null
    },

    new Category
    {
        Id = 7,
        Name = "Accessories",
        Name_EN = "Accessories",
        Code = 201,
        ParentCategoryID = 1 // Parent is "Mobile"
    },


    new Category
    {
        Id = 8,
        Name = "للمنزل",
        Name_EN = "Home Laptops",
        Code = 205,
        ParentCategoryID = 2 // Parent is "Laptops"
    },
    new Category
    {
        Id = 9,
        Name = "للعمل",
        Name_EN = "Workstation Laptops",
        Code = 204,
        ParentCategoryID = 2 // Parent is "Laptops"
    },
     new Category
     {
         Id = 10,
         Name = "للمبدعين",
         Name_EN = "For Creators",
         Code = 202,
         ParentCategoryID = 2 // Parent is "Laptops"
     },
     new Category
     {
         Id = 11,
         Name = "للطلبة",
         Name_EN = "For Students",
         Code = 202,
         ParentCategoryID = 2 // Parent is "Laptops"
     },
        new Category
        {
            Id = 12,
            Name = "للألعاب",
            Name_EN = "Gaming Laptops",
            Code = 203,
            ParentCategoryID = 2 // Parent is "Laptops"
        },
        new Category
        {
            Id = 13,
            Name = "إكسسوارات",
            Name_EN = "Accessories",
            Code = 203,
            ParentCategoryID = 2 // Parent is "Laptops"
        },
        new Category
        {
            Id = 14,
            Name = "Software",
            Name_EN = "Software",
            Code = 203,
            ParentCategoryID = 2 // Parent is "Laptops"
        },
    new Category
    {
        Id = 15,
        Name = "الشاشات",
        Name_EN = "Monitors",
        Code = 301,
        ParentCategoryID = 3 // Parent is "Displays / Desktops"
    },
    new Category
    {
        Id = 16,
        Name = "اجهزة البروجيكتور",
        Name_EN = "Projectors",
        Code = 302,
        ParentCategoryID = 3 // Parent is "Displays / Desktops"
    },
    new Category
    {
        Id = 17,
        Name = "PCs الكل في واحد",
        Name_EN = "All-in-One PCs",
        Code = 303,
        ParentCategoryID = 3 // Parent is "Displays / Desktops" اللوحات الأم
    },
    new Category
    {
        Id = 18,
        Name = "Tower PCs",
        Name_EN = "Tower PCs",
        Code = 304,
        ParentCategoryID = 3 // Parent is "Displays / Desktops" اللوحات الأم
    },
    new Category
    {
        Id = 19,
        Name = "Gaming Tower PCs",
        Name_EN = "Gaming Tower PCs",
        Code = 305,
        ParentCategoryID = 3 // Parent is "Displays / Desktops" اللوحات الأم
    },
    new Category
    {
        Id = 20,
        Name = "NUCs",
        Name_EN = "NUCs",
        Code = 306,
        ParentCategoryID = 3 // Parent is "Displays / Desktops" اللوحات الأم
    },
    new Category
    {
        Id = 21,
        Name = "Mini PCs",
        Name_EN = "Mini PCs",
        Code = 307,
        ParentCategoryID = 3 // Parent is "Displays / Desktops" اللوحات الأم
    },
    new Category
    {
        Id = 22,
        Name = "Workstations",
        Name_EN = "Workstations",
        Code = 308,
        ParentCategoryID = 3 // Parent is "Displays / Desktops" اللوحات الأم
    },
    new Category
    {
        Id = 23,
        Name = "اللوحات الأم",
        Name_EN = "Motherboards",
        Code = 401,
        ParentCategoryID = 4 // Parent is "Motherboards / Components" 
    }
    ,
     new Category
     {
         Id = 24,
         Name = "معالج الرسوميات",
         Name_EN = "Graphics Cards",
         Code = 403,
         ParentCategoryID = 4 // Parent is "Motherboards / Components" 
     },
    new Category
    {
        Id = 25,
        Name = "حقيبة الألعاب",
        Name_EN = "Gaming Case",
        Code = 402,
        ParentCategoryID = 4 // Parent is "Motherboards / Components" 
    },
     new Category
     {
         Id = 26,
         Name = "تبريد",
         Name_EN = "Cooling",
         Code = 404,
         ParentCategoryID = 4 // Parent is "Motherboards / Components" 
     },
     new Category
     {
         Id = 27,
         Name = "وحدات تزويد الطاقة",
         Name_EN = "Power Supply Units",
         Code = 405,
         ParentCategoryID = 4 // Parent is "Motherboards / Components" 
     },
     new Category
     {
         Id = 28,
         Name = "كروت الصوت",
         Name_EN = "Sound Cards",
         Code = 406,
         ParentCategoryID = 4 // Parent is "Motherboards / Components" 
     },
     new Category
     {
         Id = 29,
         Name = "محركات الأقراص الضوئية",
         Name_EN = "Optical Drives",
         Code = 407,
         ParentCategoryID = 4 // Parent is "Motherboards / Components" 
     },
     new Category
     {
         Id = 30,
         Name = "ادوات الرسومات الخارجية",
         Name_EN = "External Graphics Docks",
         Code = 408,
         ParentCategoryID = 4 // Parent is "Motherboards / Components" 
     },
     new Category
     {
         Id = 31,
         Name = "كمبيوتر بلوحة واحدة",
         Name_EN = "Single Board Computer",
         Code = 409,
         ParentCategoryID = 4 // Parent is "Motherboards / Components" 
     },
     new Category
     {
         Id = 32,
         Name = "WiFi 6",
         Name_EN = "WiFi 6",
         Code = 501,
         ParentCategoryID = 5 // Parent is "Networking / IoT / Servers" 
     }
     ,
     new Category
     {
         Id = 33,
         Name = "WiFi موجهات",
         Name_EN = "WiFi Routers",
         Code = 502,
         ParentCategoryID = 5 // Parent is "Networking / IoT / Servers" 
     },
     new Category
     {
         Id = 34,
         Name = "شبكي منزلي بالكامل WiFi نظام",
         Name_EN = "Whole Home Mesh WiFi System",
         Code = 503,
         ParentCategoryID = 5 // Parent is "Networking / IoT / Servers" 
     },
     new Category
     {
         Id = 35,
         Name = "موسعات النطاق",
         Name_EN = "Range Extenders",
         Code = 504,
         ParentCategoryID = 5 // Parent is "Networking / IoT / Servers" 
     },
     new Category
     {
         Id = 36,
         Name = "أجهزة توجيه المودم",
         Name_EN = "Modem Routers",
         Code = 505,
         ParentCategoryID = 5 // Parent is "Networking / IoT / Servers" 
     },
     new Category
     {
         Id = 37,
         Name = "محولات لاسلكية وسلكية",
         Name_EN = "Wireless & Wired Adapters",
         Code = 506,
         ParentCategoryID = 5 // Parent is "Networking / IoT / Servers" 
     },
     new Category
     {
         Id = 38,
         Name = "مفاتيح الشبكة",
         Name_EN = "Network Switches",
         Code = 507,
         ParentCategoryID = 5 // Parent is "Networking / IoT / Servers" 
     },
     new Category
     {
         Id = 39,
         Name = "Zenbo",
         Name_EN = "Zenbo",
         Code = 508,
         ParentCategoryID = 5 // Parent is "Networking / IoT / Servers" 
     },
     new Category
     {
         Id = 40,
         Name = "الذكاء الاصطناعي والحلول الصناعية",
         Name_EN = "AIoT & Industrial Solution",
         Code = 509,
         ParentCategoryID = 5 // Parent is "Networking / IoT / Servers" 
     },
     new Category
     {
         Id = 41,
         Name = "سيرفر",
         Name_EN = "Servers",
         Code = 509,
         ParentCategoryID = 5 // Parent is "Networking / IoT / Servers" 
     },
     new Category
     {
         Id = 42,
         Name = "لوحات المفاتيح",
         Name_EN = "Keyboards",
         Code = 601,
         ParentCategoryID = 6 // Parent is "Accessories" 
     },
     new Category
     {
         Id = 43,
         Name = "لوحات الماوس والفأرة",
         Name_EN = "Mice and Mouse Pads",
         Code = 602,
         ParentCategoryID = 6 // Parent is "Accessories" 
     },
     new Category
     {
         Id = 44,
         Name = "سماعات الراس والصوت",
         Name_EN = "Headsets and Audio",
         Code = 603,
         ParentCategoryID = 6 // Parent is "Accessories" 
     },
     new Category
     {
         Id = 45,
         Name = "عدة البث",
         Name_EN = "Streaming Kit",
         Code = 603,
         ParentCategoryID = 6 // Parent is "Accessories" 
     },
     new Category
     {
         Id = 46,

         Name = "ASUS ملابس وحقائب وملحقات",
         Name_EN = "Apparels, Bags and Gears",
         Code = 604,
         ParentCategoryID = 6 // Parent is "Accessories" 
     },
     new Category
     {
         Id = 47,

         Name = "Cases and Protection",
         Name_EN = "Cases and Protection",
         Code = 605,
         ParentCategoryID = 6 // Parent is "Accessories" 
     },
     new Category
     {
         Id = 48,

         Name = "شاحن",
         Name_EN = "Adapters and Chargers",
         Code = 606,
         ParentCategoryID = 6 // Parent is "Accessories" 
     },
     new Category
     {
         Id = 49,

         Name = "القواعد والدونجل والكابلات",
         Name_EN = "Docks, Dongles and Cable",
         Code = 606,
         ParentCategoryID = 6 // Parent is "Accessories" 
     },
     new Category
     {
         Id = 50,

         Name = "بنوك الطاقة",
         Name_EN = "Power Banks",
         Code = 607,
         ParentCategoryID = 6 // Parent is "Accessories" 
     },
     new Category
     {
         Id = 51,

         Name = "قلم",
         Name_EN = "Stylus",
         Code = 608,
         ParentCategoryID = 6 // Parent is "Accessories" 
     },
     new Category
     {

         Id = 52,

         Name = "متحكم",
         Name_EN = "Controler",
         Code = 609,
         ParentCategoryID = 6 // Parent is "Accessories" 
     },
     new Category
     {

         Id = 53,

         Name = "Gimbal",
         Name_EN = "Gimbal",
         Code = 6010,
         ParentCategoryID = 6 // Parent is "Accessories" 
     }

);

            // Seeding roles (Admin, User)
            modelBuilder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int> { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole<int> { Id = 2, Name = "User", NormalizedName = "USER" }
            );


            // Seeding the User
            var passwordHasher = new PasswordHasher<User>();

            modelBuilder.Entity<User>()
                    .HasData(new User
                    {
                        Id = 1,
                        UserName = "admin",
                        NormalizedUserName = "ADMIN",
                        Email = "admin@gmail.com",
                        NormalizedEmail = "ADMIN@GMAIL.COM",
                        EmailConfirmed = true,
                        PasswordHash = passwordHasher.HashPassword(null, "Y@$$er00"),
                        BirthDate = new DateTime(1990, 1, 1),
                        City = "City",
                        Region = "Region",
                        PostalCode = "12345",
                        Country = "Country",
                        SecurityStamp = Guid.NewGuid().ToString(),
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                        PhoneNumberConfirmed = true,
                        TwoFactorEnabled = false,
                        LockoutEnabled = false,
                        LockoutEnd = null,
                        AccessFailedCount = 0
                    });
            // Assign the 'Admin' role to the user 
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int>
                {
                    UserId = 1,
                    RoleId = 1
                }
            );

            #region Product seeding
            // Seeding Products table
            modelBuilder.Entity<Product>().HasData(
                 new Product
                 {
                     Id = 1,
                     Name = "Asus Zenbook S 14",
                     Name_EN = "Asus Zenbook S 14",
                     price = 99300m,
                     image_url = "https://example.com/images/rog_strix_g15.jpg",
                     Unit_Instock = 10,
                     Code = 333
                 }, new Product
                 {
                     Id = 2,
                     Name = "Asus Zenbook S 16", // English Name
                     Name_EN = "Asus Zenbook S 16", // English Name
                     price = 100799m,
                     image_url = "https://example.com/images/rog_strix_g15.jpg",
                     Unit_Instock = 10,
                     Code = 334
                 }, new Product
                 {
                     Id = 3,
                     Name = "ASUS Vivobook S 15", // English Name
                     Name_EN = "ASUS Vivobook S 15", // English Name
                     price = 38299.00m,
                     image_url = "https://example.com/images/rog_strix_g15.jpg",
                     Unit_Instock = 10,
                     Code = 335
                 }, new Product
                 {
                     Id = 4,
                     Name = "ASUS Zenbook 14 OLED", // English Name
                     Name_EN = "ASUS Zenbook 14 OLED", // English Name
                     price = 72499.00m,
                     image_url = "https://example.com/images/rog_strix_g15.jpg",
                     Unit_Instock = 10,
                     Code = 336
                 },
                new Product
                {
                    Id = 5,
                    Name = "Asus ROG Strix Scar 17", // English Name
                    description = "لابتوب ألعاب 17.3 بوصة مع معالج intel core i9، بطاقة نفيديا rtx 3080، 32 جيجابايت رام", // Arabic Description
                    Name_EN = "Asus ROG Strix Scar 17", // English Name
                    description_EN = "17.3-inch gaming laptop with intel core i9, nvidia rtx 3080, 32gb ram", // English Description
                    price = 2499.99m,
                    image_url = "https://example.com/images/rog_strix_scar_17.jpg",
                    Unit_Instock = 5,
                    Code = 337
                },
                    new Product
                    {
                        Id = 6,
                        Name = "Asus ExpertBook P1", // English Name
                        description = "لابتوب مخصص للأعمال مع معالج intel core i3، 4 جيجابايت رام، 256 جيجابايت ssd", // Arabic Description
                        Name_EN = "Asus ExpertBook P1", // English Name
                        description_EN = "Affordable business laptop with intel core i3, 4gb ram, 256gb ssd", // English Description
                        price = 499.99m,
                        image_url = "https://example.com/images/expertbook_p1.jpg",
                        Unit_Instock = 40,
                        Code = 338
                    },
                    new Product
                    {
                        Id = 7,
                        Name = "Asus ROG Flow Z13", // English Name
                        description = "لابتوب ألعاب 2 في 1 مع معالج intel core i7، بطاقة نفيديا rtx 3050 تي، 16 جيجابايت رام", // Arabic Description
                        Name_EN = "Asus ROG Flow Z13", // English Name
                        description_EN = "Compact 2-in-1 gaming laptop with intel core i7, nvidia rtx 3050 ti, 16gb ram", // English Description
                        price = 1699.99m,
                        image_url = "https://example.com/images/rog_flow_z13.jpg",
                        Unit_Instock = 20,
                        Code = 339
                    },
                    new Product
                    {
                        Id = 8,
                        Name = "Asus ZenBook Duo 14", // English Name
                        description = "لابتوب بشاشة مزدوجة مع معالج intel core i7، 16 جيجابايت رام، 1 تيرابايت ssd", // Arabic Description
                        Name_EN = "Asus ZenBook Duo 14", // English Name
                        description_EN = "Dual-screen laptop with intel core i7, 16gb ram, 1tb ssd", // English Description
                        price = 1399.99m,
                        image_url = "https://example.com/images/zenbook_duo_14.jpg",
                        Unit_Instock = 10,
                        Code = 340
                    },
                    new Product
                    {
                        Id = 9,
                        Name = "Asus TUF Dash F15", // English Name
                        description = "لابتوب ألعاب 15.6 بوصة مع معالج intel core i7، بطاقة نفيديا rtx 3070، 16 جيجابايت رام", // Arabic Description
                        Name_EN = "Asus TUF Dash F15", // English Name
                        description_EN = "15.6-inch gaming laptop with intel core i7, nvidia rtx 3070, 16gb ram", // English Description
                        price = 1799.99m,
                        image_url = "https://example.com/images/tuf_dash_f15.jpg",
                        Unit_Instock = 8,
                        Code = 341
                    },
                    new Product
                    {
                        Id = 10,
                        Name = "Asus ProArt Studiobook 16", // English Name
                        description = "لابتوب للمبدعين مع معالج intel core i9، بطاقة نفيديا rtx 3070، 32 جيجابايت رام، 1 تيرابايت ssd", // Arabic Description
                        Name_EN = "Asus ProArt Studiobook 16", // English Name
                        description_EN = "Creative laptop with intel core i9, nvidia rtx 3070, 32gb ram, 1tb ssd", // English Description
                        price = 2199.99m,
                        image_url = "https://example.com/images/proart_studiobook_16.jpg",
                        Unit_Instock = 3,
                        Code = 342
                    },
                    new Product
                    {
                        Id = 11,
                        Name = "Asus VivoBook Flip 14", // English Name
                        description = "لابتوب 2 في 1 مع معالج intel core i5، 8 جيجابايت رام، 512 جيجابايت ssd", // Arabic Description
                        Name_EN = "Asus VivoBook Flip 14", // English Name
                        description_EN = "2-in-1 laptop with intel core i5, 8gb ram, 512gb ssd", // English Description
                        price = 799.99m,
                        image_url = "https://example.com/images/vivobook_flip_14.jpg",
                        Unit_Instock = 12,
                        Code = 343
                    },
                    new Product
                    {
                        Id = 12,
                        Name = "Asus ROG Strix G17", // English Name
                        description = "لابتوب ألعاب 17.3 بوصة مع معالج amd ryzen 9، بطاقة نفيديا rtx 3070، 32 جيجابايت رام", // Arabic Description
                        Name_EN = "Asus ROG Strix G17", // English Name
                        description_EN = "17.3-inch gaming laptop with amd ryzen 9, nvidia rtx 3070, 32gb ram", // English Description
                        price = 2199.99m,
                        image_url = "https://example.com/images/rog_strix_g17.jpg",
                        Unit_Instock = 8,
                        Code = 344
                    },
                    new Product
                    {
                        Id = 13,
                        Name = "Asus VivoBook 14", // English Name
                        description = "لابتوب 14 بوصة مع معالج intel core i3، 4 جيجابايت رام، 256 جيجابايت ssd", // Arabic Description
                        Name_EN = "Asus VivoBook 14", // English Name
                        description_EN = "14-inch laptop with intel core i3, 4gb ram, 256gb ssd", // English Description
                        price = 499.99m,
                        image_url = "https://example.com/images/vivobook_14.jpg",
                        Unit_Instock = 30,
                        Code = 345
                    },
                    new Product
                    {
                        Id = 14,
                        Name = "Asus TUF Gaming F17", // English Name
                        description = "لابتوب ألعاب 17.3 بوصة مع معالج intel core i7، بطاقة نفيديا rtx 3060، 16 جيجابايت رام", // Arabic Description
                        Name_EN = "Asus TUF Gaming F17", // English Name
                        description_EN = "17.3-inch gaming laptop with intel core i7, nvidia rtx 3060, 16gb ram", // English Description
                        price = 1599.99m,
                        image_url = "https://example.com/images/tuf_gaming_f17.jpg",
                        Unit_Instock = 7,
                        Code = 346
                    },
                    new Product
                    {
                        Id = 15,
                        Name = "Asus Zenbook Pro Duo 15", // English Name
                        description = "لابتوب بشاشتين مع معالج intel core i9، بطاقة نفيديا rtx 3080، 32 جيجابايت رام، 1 تيرابايت ssd", // Arabic Description
                        Name_EN = "Asus Zenbook Pro Duo 15", // English Name
                        description_EN = "Dual-screen laptop with intel core i9, nvidia rtx 3080, 32gb ram, 1tb ssd", // English Description
                        price = 2999.99m,
                        image_url = "https://example.com/images/zenbook_pro_duo_15.jpg",
                        Unit_Instock = 5,
                        Code = 347
                    },
                    new Product
                    {
                        Id = 16,
                        Name = "Asus TUF Gaming A17", // English Name
                        description = "لابتوب ألعاب 17.3 بوصة مع معالج amd ryzen 7، بطاقة نفيديا rtx 3070، 16 جيجابايت رام", // Arabic Description
                        Name_EN = "Asus TUF Gaming A17", // English Name
                        description_EN = "17.3-inch gaming laptop with amd ryzen 7, nvidia rtx 3070, 16gb ram", // English Description
                        price = 1799.99m,
                        image_url = "https://example.com/images/tuf_gaming_a17.jpg",
                        Unit_Instock = 10,
                        Code = 348
                    },
                    new Product
                    {
                        Id = 17,
                        Name = "Asus ExpertBook P2", // English Name
                        description = "لابتوب للأعمال مع معالج intel core i5، 8 جيجابايت رام، 512 جيجابايت ssd", // Arabic Description
                        Name_EN = "Asus ExpertBook P2", // English Name
                        description_EN = "Business laptop with intel core i5, 8gb ram, 512gb ssd", // English Description
                        price = 699.99m,
                        image_url = "https://example.com/images/expertbook_p2.jpg",
                        Unit_Instock = 25,
                        Code = 349
                    },
                    new Product
                    {
                        Id = 18,
                        Name = "Asus Chromebook Flip", // English Name
                        description = "لابتوب كروم بوك قابل للطي مع شاشة 14 بوصة، 4 جيجابايت رام، 64 جيجابايت eMMC", // Arabic Description
                        Name_EN = "Asus Chromebook Flip", // English Name
                        description_EN = "Convertible chromebook with 14-inch display, 4gb ram, 64gb emmc", // English Description
                        price = 399.99m,
                        image_url = "https://example.com/images/chromebook_flip.jpg",
                        Unit_Instock = 20,
                        Code = 350
                    },

                    new Product
                    {
                        Id = 19,
                        Name = "Asus TUF Gaming F17", // English Name
                        description = "لابتوب ألعاب 17.3 بوصة مع معالج intel core i7، بطاقة نفيديا rtx 3060، 16 جيجابايت رام", // Arabic Description
                        Name_EN = "Asus TUF Gaming F17", // English Name
                        description_EN = "17.3-inch gaming laptop with intel core i7, nvidia rtx 3060, 16gb ram", // English Description
                        price = 1599.99m,
                        image_url = "https://example.com/images/tuf_gaming_f17.jpg",
                        Unit_Instock = 7,
                        Code = 350
                    },
                    new Product
                    {
                        Id = 20,
                        Name = "Asus Zenbook Pro Duo 15", // English Name
                        description = "لابتوب بشاشتين مع معالج intel core i9، بطاقة نفيديا rtx 3080، 32 جيجابايت رام، 1 تيرابايت ssd", // Arabic Description
                        Name_EN = "Asus Zenbook Pro Duo 15", // English Name
                        description_EN = "Dual-screen laptop with intel core i9, nvidia rtx 3080, 32gb ram, 1tb ssd", // English Description
                        price = 2999.99m,
                        image_url = "https://example.com/images/zenbook_pro_duo_15.jpg",
                        Unit_Instock = 5,
                        Code = 351
                    },
                    new Product
                    {
                        Id = 21,
                        Name = "Asus TUF Gaming A17", // English Name
                        description = "لابتوب ألعاب 17.3 بوصة مع معالج amd ryzen 7، بطاقة نفيديا rtx 3070، 16 جيجابايت رام", // Arabic Description
                        Name_EN = "Asus TUF Gaming A17", // English Name
                        description_EN = "17.3-inch gaming laptop with amd ryzen 7, nvidia rtx 3070, 16gb ram", // English Description
                        price = 1799.99m,
                        image_url = "https://example.com/images/tuf_gaming_a17.jpg",
                        Unit_Instock = 10,
                        Code = 352
                    },
                    new Product
                    {
                        Id = 23,
                        Name = "Asus ExpertBook P2", // English Name
                        description = "لابتوب للأعمال مع معالج intel core i5، 8 جيجابايت رام، 512 جيجابايت ssd", // Arabic Description
                        Name_EN = "Asus ExpertBook P2", // English Name
                        description_EN = "Business laptop with intel core i5, 8gb ram, 512gb ssd", // English Description
                        price = 699.99m,
                        image_url = "https://example.com/images/expertbook_p2.jpg",
                        Unit_Instock = 20,
                        Code = 353
                    },
                    new Product
                    {
                        Id = 24,
                        Name = "Asus ROG Strix Scar 15", // English Name
                        description = "لابتوب ألعاب 15.6 بوصة مع معالج intel core i9، بطاقة نفيديا rtx 3080، 32 جيجابايت رام", // Arabic Description
                        Name_EN = "Asus ROG Strix Scar 15", // English Name
                        description_EN = "15.6-inch gaming laptop with intel core i9, nvidia rtx 3080, 32gb ram", // English Description
                        price = 2599.99m,
                        image_url = "https://example.com/images/rog_strix_scar_15.jpg",
                        Unit_Instock = 6,
                        Code = 354
                    },
                    new Product
                    {
                        Id = 25,
                        Name = "Asus ZenBook 15", // English Name
                        description = "لابتوب 15.6 بوصة مع معالج intel core i7، بطاقة نفيديا gtx 1650، 16 جيجابايت رام", // Arabic Description
                        Name_EN = "Asus ZenBook 15", // English Name
                        description_EN = "15.6-inch laptop with intel core i7, nvidia gtx 1650, 16gb ram", // English Description
                        price = 1299.99m,
                        image_url = "https://example.com/images/zenbook_15.jpg",
                        Unit_Instock = 25,
                        Code = 355
                    },
                    new Product
                    {
                        Id = 26,
                        Name = "Asus ROG Zephyrus G14", // English Name
                        description = "لابتوب ألعاب 14 بوصة مع معالج amd ryzen 9، بطاقة نفيديا rtx 3060، 16 جيجابايت رام", // Arabic Description
                        Name_EN = "Asus ROG Zephyrus G14", // English Name
                        description_EN = "14-inch gaming laptop with amd ryzen 9, nvidia rtx 3060, 16gb ram", // English Description
                        price = 1899.99m,
                        image_url = "https://example.com/images/rog_zephyrus_g14.jpg",
                        Unit_Instock = 15,
                        Code = 356
                    },
                    new Product
                    {
                        Id = 27,
                        Name = "Asus VivoBook S14", // English Name
                        description = "لابتوب 14 بوصة مع معالج intel core i5، 8 جيجابايت رام، 512 جيجابايت ssd", // Arabic Description
                        Name_EN = "Asus VivoBook S14", // English Name
                        description_EN = "14-inch laptop with intel core i5, 8gb ram, 512gb ssd", // English Description
                        price = 799.99m,
                        image_url = "https://example.com/images/vivobook_s14.jpg",
                        Unit_Instock = 18,
                        Code = 357
                    },
                    new Product
                    {
                        Id = 28,
                        Name = "Asus ProArt StudioBook Pro 17", // English Name
                        description = "لابتوب للأعمال المبدعة مع معالج intel xeon، بطاقة نفيديا rtx 3000، 64 جيجابايت رام، 1 تيرابايت ssd", // Arabic Description
                        Name_EN = "Asus ProArt StudioBook Pro 17", // English Name
                        description_EN = "Creative workstation laptop with intel xeon, nvidia rtx 3000, 64gb ram, 1tb ssd", // English Description
                        price = 3499.99m,
                        image_url = "https://example.com/images/proart_studiobook_pro_17.jpg",
                        Unit_Instock = 4,
                        Code = 358
                    },
                    new Product
                    {
                        Id = 29,
                        Name = "Asus ROG Flow X13", // English Name
                        description = "لابتوب 2 في 1 مع معالج amd ryzen 9، بطاقة نفيديا rtx 3050، 16 جيجابايت رام", // Arabic Description
                        Name_EN = "Asus ROG Flow X13", // English Name
                        description_EN = "2-in-1 laptop with amd ryzen 9, nvidia rtx 3050, 16gb ram", // English Description
                        price = 1799.99m,
                        image_url = "https://example.com/images/rog_flow_x13.jpg",
                        Unit_Instock = 12,
                        Code = 359
                    },
                    new Product
                    {
                        Id = 30,
                        Name = "Asus ZenBook Flip 14", // English Name
                        description = "لابتوب 2 في 1 مع معالج intel core i7، 16 جيجابايت رام، 1 تيرابايت ssd", // Arabic Description
                        Name_EN = "Asus ZenBook Flip 14", // English Name
                        description_EN = "2-in-1 laptop with intel core i7, 16gb ram, 1tb ssd", // English Description
                        price = 1399.99m,
                        image_url = "https://example.com/images/zenbook_flip_14.jpg",
                        Unit_Instock = 14,
                        Code = 360
                    },
                    new Product
                    {
                        Id = 31,
                        Name = "Asus TUF Gaming FX505", // English Name
                        description = "لابتوب ألعاب 15.6 بوصة مع معالج intel core i5، بطاقة نفيديا gtx 1650، 8 جيجابايت رام", // Arabic Description
                        Name_EN = "Asus TUF Gaming FX505", // English Name
                        description_EN = "15.6-inch gaming laptop with intel core i5, nvidia gtx 1650, 8gb ram", // English Description
                        price = 899.99m,
                        image_url = "https://example.com/images/tuf_gaming_fx505.jpg",
                        Unit_Instock = 10,
                        Code = 361
                    },
                    new Product
                    {
                        Id = 32,
                        Name = "Asus ExpertBook B9", // English Name
                        description = "لابتوب أعمال فائق الخفة مع معالج intel core i7، 16 جيجابايت رام، 512 جيجابايت ssd", // Arabic Description
                        Name_EN = "Asus ExpertBook B9", // English Name
                        description_EN = "Ultra-light business laptop with intel core i7, 16gb ram, 512gb ssd", // English Description
                        price = 1499.99m,
                        image_url = "https://example.com/images/expertbook_b9.jpg",
                        Unit_Instock = 6,
                        Code = 362
                    },
                    new Product
                    {
                        Id = 33,
                        Name = "Asus VivoBook Ultra 15", // English Name
                        description = "لابتوب 15.6 بوصة مع معالج intel core i7، 8 جيجابايت رام، 1 تيرابايت ssd", // Arabic Description
                        Name_EN = "Asus VivoBook Ultra 15", // English Name
                        description_EN = "15.6-inch laptop with intel core i7, 8gb ram, 1tb ssd", // English Description
                        price = 1099.99m,
                        image_url = "https://example.com/images/vivobook_ultra_15.jpg",
                        Unit_Instock = 11,
                        Code = 363
                    },
                    new Product
                    {
                        Id = 34,
                        Name = "Asus ROG Zephyrus M16", // English Name
                        description = "لابتوب ألعاب 16 بوصة مع معالج intel core i9، بطاقة نفيديا rtx 3070، 32 جيجابايت رام", // Arabic Description
                        Name_EN = "Asus ROG Zephyrus M16", // English Name
                        description_EN = "16-inch gaming laptop with intel core i9, nvidia rtx 3070, 32gb ram", // English Description
                        price = 2499.99m,
                        image_url = "https://example.com/images/rog_zephyrus_m16.jpg",
                        Unit_Instock = 7,
                        Code = 364
                    },
                        new Product
                        {
                            Id = 35,
                            Name = "Asus TUF Gaming F15", // English Name
                            description = "لابتوب ألعاب 15.6 بوصة مع معالج intel core i7، بطاقة نفيديا rtx 3070، 16 جيجابايت رام", // Arabic Description
                            Name_EN = "Asus TUF Gaming F15", // English Name
                            description_EN = "15.6-inch gaming laptop with intel core i7, nvidia rtx 3070, 16gb ram", // English Description
                            price = 1599.99m,
                            image_url = "https://example.com/images/tuf_gaming_f15.jpg",
                            Unit_Instock = 14,
                            Code = 365
                        },
                        new Product
                        {
                            Id = 36,
                            Name = "Asus Zenbook Flip 13", // English Name
                            description = "لابتوب 2 في 1 مع معالج intel core i5، 8 جيجابايت رام، 256 جيجابايت ssd", // Arabic Description
                            Name_EN = "Asus Zenbook Flip 13", // English Name
                            description_EN = "2-in-1 laptop with intel core i5, 8gb ram, 256gb ssd", // English Description
                            price = 849.99m,
                            image_url = "https://example.com/images/zenbook_flip_13.jpg",
                            Unit_Instock = 20,
                            Code = 366
                        },
                        new Product
                        {
                            Id = 37,
                            Name = "Asus TUF Dash A15", // English Name
                            description = "لابتوب ألعاب 15.6 بوصة مع معالج amd ryzen 7، بطاقة نفيديا rtx 3070، 16 جيجابايت رام", // Arabic Description
                            Name_EN = "Asus TUF Dash A15", // English Name
                            description_EN = "15.6-inch gaming laptop with amd ryzen 7, nvidia rtx 3070, 16gb ram", // English Description
                            price = 1699.99m,
                            image_url = "https://example.com/images/tuf_dash_a15.jpg",
                            Unit_Instock = 13,
                            Code = 367
                        },
                        new Product
                        {
                            Id = 38,
                            Name = "Asus ProArt StudioBook 15", // English Name
                            description = "لابتوب للمبدعين مع معالج intel xeon، بطاقة نفيديا rtx 3000، 32 جيجابايت رام، 1 تيرابايت ssd", // Arabic Description
                            Name_EN = "Asus ProArt StudioBook 15", // English Name
                            description_EN = "Creative laptop with intel xeon, nvidia rtx 3000, 32gb ram, 1tb ssd", // English Description
                            price = 3199.99m,
                            image_url = "https://example.com/images/proart_studiobook_15.jpg",
                            Unit_Instock = 5,
                            Code = 368
                        },
                        new Product
                        {
                            Id = 39,
                            Name = "Asus VivoBook Ultra K15", // English Name
                            description = "لابتوب 15.6 بوصة مع معالج intel core i5، 8 جيجابايت رام، 512 جيجابايت ssd", // Arabic Description
                            Name_EN = "Asus VivoBook Ultra K15", // English Name
                            description_EN = "15.6-inch laptop with intel core i5, 8gb ram, 512gb ssd", // English Description
                            price = 799.99m,
                            image_url = "https://example.com/images/vivobook_ultra_k15.jpg",
                            Unit_Instock = 18,
                            Code = 369
                        },
                        new Product
                        {
                            Id = 40,
                            Name = "Asus ExpertBook L1", // English Name
                            description = "لابتوب مخصص للأعمال مع معالج intel celeron، 4 جيجابايت رام، 128 جيجابايت eMMC", // Arabic Description
                            Name_EN = "Asus ExpertBook L1", // English Name
                            description_EN = "Business laptop with intel celeron, 4gb ram, 128gb eMMC", // English Description
                            price = 379.99m,
                            image_url = "https://example.com/images/expertbook_l1.jpg",
                            Unit_Instock = 25,
                            Code = 370
                        },
                        new Product
                        {
                            Id = 41,
                            Name = "Asus VivoBook 17", // English Name
                            description = "لابتوب 17.3 بوصة مع معالج intel core i7، 16 جيجابايت رام، 512 جيجابايت ssd", // Arabic Description
                            Name_EN = "Asus VivoBook 17", // English Name
                            description_EN = "17.3-inch laptop with intel core i7, 16gb ram, 512gb ssd", // English Description
                            price = 1099.99m,
                            image_url = "https://example.com/images/vivobook_17.jpg",
                            Unit_Instock = 12,
                            Code = 371
                        },
                        new Product
                        {
                            Id = 42,
                            Name = "Asus ROG Strix G14", // English Name
                            description = "لابتوب ألعاب 14 بوصة مع معالج intel core i9، بطاقة نفيديا rtx 3070، 32 جيجابايت رام", // Arabic Description
                            Name_EN = "Asus ROG Strix G14", // English Name
                            description_EN = "14-inch gaming laptop with intel core i9, nvidia rtx 3070, 32gb ram", // English Description
                            price = 2299.99m,
                            image_url = "https://example.com/images/rog_strix_g14.jpg",
                            Unit_Instock = 8,
                            Code = 372
                        },
                        new Product
                        {
                            Id = 43,
                            Name = "Asus ZenBook 13", // English Name
                            description = "لابتوب 13.3 بوصة مع معالج intel core i5، 8 جيجابايت رام، 512 جيجابايت ssd", // Arabic Description
                            Name_EN = "Asus ZenBook 13", // English Name
                            description_EN = "13.3-inch laptop with intel core i5, 8gb ram, 512gb ssd", // English Description
                            price = 849.99m,
                            image_url = "https://example.com/images/zenbook_13.jpg",
                            Unit_Instock = 22,
                            Code = 373
                        },
                        new Product
                        {
                            Id = 44,
                            Name = "Asus TUF Gaming K7", // English Name
                            description = "لوحة مفاتيح ألعاب ميكانيكية مع مفاتيح ROG RX، إضاءة RGB قابلة للتخصيص", // Arabic Description
                            Name_EN = "Asus TUF Gaming K7", // English Name
                            description_EN = "Mechanical gaming keyboard with ROG RX switches, customizable RGB lighting", // English Description
                            price = 129.99m,
                            image_url = "https://example.com/images/tuf_gaming_k7.jpg",
                            Unit_Instock = 50,
                            Code = 374
                        },
                        new Product
                        {
                            Id = 45,
                            Name = "Asus ROG Chakram X", // English Name
                            description = "ماوس ألعاب لاسلكي مع مستشعر HERO 25K، زر قابل للتخصيص، إضاءة RGB", // Arabic Description
                            Name_EN = "Asus ROG Chakram X", // English Name
                            description_EN = "Wireless gaming mouse with HERO 25K sensor, customizable button, RGB lighting", // English Description
                            price = 99.99m,
                            image_url = "https://example.com/images/rog_chakram_x.jpg",
                            Unit_Instock = 35,
                            Code = 375
                        },
                        new Product
                        {
                            Id = 46,
                            Name = "Asus ROG Strix Fusion 700", // English Name
                            description = "سماعة رأس ألعاب لاسلكية مع صوت محيطي 7.1، إضاءة RGB", // Arabic Description
                            Name_EN = "Asus ROG Strix Fusion 700", // English Name
                            description_EN = "Wireless gaming headset with 7.1 surround sound, RGB lighting", // English Description
                            price = 179.99m,
                            image_url = "https://example.com/images/rog_strix_fusion_700.jpg",
                            Unit_Instock = 40,
                            Code = 376
                        },
                        new Product
                        {
                            Id = 47,
                            Name = "Asus ZenBook 14X", // English Name
                            description = "لابتوب 14 بوصة مع معالج intel core i7، 16 جيجابايت رام، 1 تيرابايت ssd", // Arabic Description
                            Name_EN = "Asus ZenBook 14X", // English Name
                            description_EN = "14-inch laptop with intel core i7, 16gb ram, 1tb ssd", // English Description
                            price = 1299.99m,
                            image_url = "https://example.com/images/zenbook_14x.jpg",
                            Unit_Instock = 15,
                            Code = 377
                        },
                        new Product
                        {
                            Id = 48,
                            Name = "Asus ProArt Display PA32UCG", // English Name
                            description = "شاشة احترافية بدقة 4K مع دعم HDR، 98% DCI-P3", // Arabic Description
                            Name_EN = "Asus ProArt Display PA32UCG", // English Name
                            description_EN = "4K professional monitor with HDR support, 98% DCI-P3", // English Description
                            price = 1799.99m,
                            image_url = "https://example.com/images/proart_display_pa32ucg.jpg",
                            Unit_Instock = 5,
                            Code = 378
                        },
                        new Product
                        {
                            Id = 49,
                            Name = "Asus ROG Swift PG259QN", // English Name
                            description = "شاشة ألعاب 24.5 بوصة مع معدل تحديث 360Hz و 1ms زمن استجابة", // Arabic Description
                            Name_EN = "Asus ROG Swift PG259QN", // English Name
                            description_EN = "24.5-inch gaming monitor with 360Hz refresh rate and 1ms response time", // English Description
                            price = 699.99m,
                            image_url = "https://example.com/images/rog_swift_pg259qn.jpg",
                            Unit_Instock = 8,
                            Code = 379
                        },
                        new Product
                        {
                            Id = 50,
                            Name = "Asus ROG Strix XG27AQ", // English Name
                            description = "شاشة ألعاب 27 بوصة بدقة 1440p مع معدل تحديث 170Hz", // Arabic Description
                            Name_EN = "Asus ROG Strix XG27AQ", // English Name
                            description_EN = "27-inch gaming monitor with 1440p resolution and 170Hz refresh rate", // English Description
                            price = 499.99m,
                            image_url = "https://example.com/images/rog_strix_xg27aq.jpg",
                            Unit_Instock = 10,
                            Code = 380
                        },
                        new Product
                        {
                            Id = 51,
                            Name = "Asus TUF Gaming VG27AQL1A", // English Name
                            description = "شاشة ألعاب 27 بوصة بدقة 1440p مع معدل تحديث 170Hz و G-SYNC", // Arabic Description
                            Name_EN = "Asus TUF Gaming VG27AQL1A", // English Name
                            description_EN = "27-inch gaming monitor with 1440p resolution, 170Hz refresh rate, and G-SYNC", // English Description
                            price = 379.99m,
                            image_url = "https://example.com/images/tuf_gaming_vg27aql1a.jpg",
                            Unit_Instock = 9,
                            Code = 381
                        },
                        new Product
                        {
                            Id = 52,
                            Name = "Asus ZenScreen MB16ACE", // English Name
                            description = "شاشة محمولة 15.6 بوصة بدقة Full HD مع منفذ USB-C", // Arabic Description
                            Name_EN = "Asus ZenScreen MB16ACE", // English Name
                            description_EN = "15.6-inch portable screen with Full HD resolution and USB-C port", // English Description
                            price = 219.99m,
                            image_url = "https://example.com/images/zenScreen_mb16ace.jpg",
                            Unit_Instock = 30,
                            Code = 382
                        },
                         new Product
                         {
                             Id = 53,
                             Name = "Asus ROG Strix G17", // English Name
                             description = "لابتوب ألعاب 17.3 بوصة مع معالج amd ryzen 9، بطاقة نفيديا rtx 3070، 16 جيجابايت رام", // Arabic Description
                             Name_EN = "Asus ROG Strix G17", // English Name
                             description_EN = "17.3-inch gaming laptop with amd ryzen 9, nvidia rtx 3070, 16gb ram", // English Description
                             price = 1999.99m,
                             image_url = "https://example.com/images/rog_strix_g17.jpg",
                             Unit_Instock = 10,
                             Code = 383
                         },
                        new Product
                        {
                            Id = 54,
                            Name = "Asus ZenBook 20", // English Name
                            description = "لابتوب 15.6 بوصة مع معالج intel core i7، 16 جيجابايت رام، 512 جيجابايت ssd", // Arabic Description
                            Name_EN = "Asus ZenBook 20", // English Name
                            description_EN = "15.6-inch laptop with intel core i7, 16gb ram, 512gb ssd", // English Description
                            price = 1099.99m,
                            image_url = "https://example.com/images/zenbook_15.jpg",
                            Unit_Instock = 22,
                            Code = 384
                        },
                        new Product
                        {
                            Id = 55,
                            Name = "Asus VivoBook S15", // English Name
                            description = "لابتوب 14 بوصة مع معالج intel core i5، 8 جيجابايت رام، 256 جيجابايت ssd", // Arabic Description
                            Name_EN = "Asus VivoBook S15", // English Name
                            description_EN = "14-inch laptop with intel core i5, 8gb ram, 256gb ssd", // English Description
                            price = 799.99m,
                            image_url = "https://example.com/images/vivobook_s14.jpg",
                            Unit_Instock = 18,
                            Code = 385
                        },
                        new Product
                        {
                            Id = 56,
                            Name = "Asus TUF Gaming F21", // English Name
                            description = "لابتوب ألعاب 17.3 بوصة مع معالج intel core i7، بطاقة نفيديا rtx 3060، 16 جيجابايت رام", // Arabic Description
                            Name_EN = "Asus TUF Gaming F21", // English Name
                            description_EN = "17.3-inch gaming laptop with intel core i7, nvidia rtx 3060, 16gb ram", // English Description
                            price = 1799.99m,
                            image_url = "https://example.com/images/tuf_gaming_f17.jpg",
                            Unit_Instock = 8,
                            Code = 386
                        },
                        new Product
                        {
                            Id = 57,
                            Name = "Asus ROG Strix Scar 19", // English Name
                            description = "لابتوب ألعاب 15.6 بوصة مع معالج intel core i9، بطاقة نفيديا rtx 3080، 32 جيجابايت رام", // Arabic Description
                            Name_EN = "Asus ROG Strix Scar 19", // English Name
                            description_EN = "15.6-inch gaming laptop with intel core i9, nvidia rtx 3080, 32gb ram", // English Description
                            price = 2399.99m,
                            image_url = "https://example.com/images/rog_strix_scar_15.jpg",
                            Unit_Instock = 6,
                            Code = 387
                        },
                        new Product
                        {
                            Id = 58,
                            Name = "Asus ExpertBook B1", // English Name
                            description = "لابتوب مخصص للأعمال مع معالج intel core i5، 8 جيجابايت رام، 512 جيجابايت ssd", // Arabic Description
                            Name_EN = "Asus ExpertBook B1", // English Name
                            description_EN = "Business laptop with intel core i5, 8gb ram, 512gb ssd", // English Description
                            price = 799.99m,
                            image_url = "https://example.com/images/expertbook_b1.jpg",
                            Unit_Instock = 15,
                            Code = 388
                        },
                        new Product
                        {
                            Id = 59,
                            Name = "Asus Zenbook Pro Duo 5", // English Name
                            description = "لابتوب مزدوج الشاشة مع معالج intel core i9، بطاقة نفيديا rtx 3070، 32 جيجابايت رام، 1 تيرابايت ssd", // Arabic Description
                            Name_EN = "Asus Zenbook Pro Duo 5", // English Name
                            description_EN = "Dual-screen laptop with intel core i9, nvidia rtx 3070, 32gb ram, 1tb ssd", // English Description
                            price = 2899.99m,
                            image_url = "https://example.com/images/zenbook_pro_duo.jpg",
                            Unit_Instock = 4,
                            Code = 389
                        },
                        new Product
                        {
                            Id = 60,
                            Name = "Asus TUF Gaming A19", // English Name
                            description = "لابتوب ألعاب 17.3 بوصة مع معالج amd ryzen 7، بطاقة نفيديا rtx 3060، 16 جيجابايت رام", // Arabic Description
                            Name_EN = "Asus TUF Gaming A19", // English Name
                            description_EN = "17.3-inch gaming laptop with amd ryzen 7, nvidia rtx 3060, 16gb ram", // English Description
                            price = 1699.99m,
                            image_url = "https://example.com/images/tuf_gaming_a17.jpg",
                            Unit_Instock = 7,
                            Code = 390
                        },
                        new Product
                        {
                            Id = 61,
                            Name = "Asus VivoBook X512", // English Name
                            description = "لابتوب 15.6 بوصة مع معالج intel core i3، 4 جيجابايت رام، 128 جيجابايت ssd", // Arabic Description
                            Name_EN = "Asus VivoBook X512", // English Name
                            description_EN = "15.6-inch laptop with intel core i3, 4gb ram, 128gb ssd", // English Description
                            price = 449.99m,
                            image_url = "https://example.com/images/vivobook_x512.jpg",
                            Unit_Instock = 25,
                            Code = 391
                        },
                        new Product
                        {
                            Id = 62,
                            Name = "Asus ZenBook 14 UX425", // English Name
                            description = "لابتوب 14 بوصة مع معالج intel core i7، 16 جيجابايت رام، 512 جيجابايت ssd", // Arabic Description
                            Name_EN = "Asus ZenBook 14 UX425", // English Name
                            description_EN = "14-inch laptop with intel core i7, 16gb ram, 512gb ssd", // English Description
                            price = 999.99m,
                            image_url = "https://example.com/images/zenbook_14_ux425.jpg",
                            Unit_Instock = 19,
                            Code = 392
                        },
                        new Product
                        {
                            Id = 63,
                            Name = "Asus ProArt StudioBook 17", // English Name
                            description = "لابتوب مخصص للمبدعين مع معالج intel core i7، بطاقة نفيديا rtx 3060، 16 جيجابايت رام، 512 جيجابايت ssd", // Arabic Description
                            Name_EN = "Asus ProArt StudioBook 17", // English Name
                            description_EN = "Creative laptop with intel core i7, nvidia rtx 3060, 16gb ram, 512gb ssd", // English Description
                            price = 1699.99m,
                            image_url = "https://example.com/images/proart_studiobook_17.jpg",
                            Unit_Instock = 10,
                            Code = 393
                        },
                        new Product
                        {
                            Id = 64,
                            Name = "Asus ROG Strix SCAR III", // English Name
                            description = "لابتوب ألعاب 15.6 بوصة مع معالج intel core i7، بطاقة نفيديا rtx 2080، 16 جيجابايت رام", // Arabic Description
                            Name_EN = "Asus ROG Strix SCAR III", // English Name
                            description_EN = "15.6-inch gaming laptop with intel core i7, nvidia rtx 2080, 16gb ram", // English Description
                            price = 1899.99m,
                            image_url = "https://example.com/images/rog_strix_scar_iii.jpg",
                            Unit_Instock = 5,
                            Code = 394
                        },
                        new Product
                        {
                            Id = 65,
                            Name = "Asus VivoBook 14 X413", // English Name
                            description = "لابتوب 14 بوصة مع معالج intel core i5، 8 جيجابايت رام، 512 جيجابايت ssd", // Arabic Description
                            Name_EN = "Asus VivoBook 14 X413", // English Name
                            description_EN = "14-inch laptop with intel core i5, 8gb ram, 512gb ssd", // English Description
                            price = 749.99m,
                            image_url = "https://example.com/images/vivobook_14_x413.jpg",
                            Unit_Instock = 20,
                            Code = 395
                        },
                        new Product
                        {
                            Id = 66,
                            Name = "Asus ZenBook 19", // English Name
                            description = "لابتوب 13.3 بوصة مع معالج intel core i7، 16 جيجابايت رام، 512 جيجابايت ssd", // Arabic Description
                            Name_EN = "Asus ZenBook 19", // English Name
                            description_EN = "13.3-inch laptop with intel core i7, 16gb ram, 512gb ssd", // English Description
                            price = 849.99m,
                            image_url = "https://example.com/images/zenbook_13.jpg",
                            Unit_Instock = 14,
                            Code = 396
                        },
                        new Product
                        {
                            Id = 67,
                            Name = "Asus ROG Flow X17", // English Name
                            description = "لابتوب 2 في 1 مع معالج amd ryzen 9، بطاقة نفيديا rtx 3050، 16 جيجابايت رام", // Arabic Description
                            Name_EN = "Asus ROG Flow X17", // English Name
                            description_EN = "2-in-1 laptop with amd ryzen 9, nvidia rtx 3050, 16gb ram", // English Description
                            price = 1799.99m,
                            image_url = "https://example.com/images/rog_flow_x13.jpg",
                            Unit_Instock = 8,
                            Code = 397
                        },
                        new Product
                        {
                            Id = 68,
                            Name = "Asus VivoBook 19", // English Name
                            description = "لابتوب 17.3 بوصة مع معالج intel core i7، 16 جيجابايت رام، 1 تيرابايت ssd", // Arabic Description
                            Name_EN = "Asus VivoBook 19", // English Name
                            description_EN = "17.3-inch laptop with intel core i7, 16gb ram, 1tb ssd", // English Description
                            price = 999.99m,
                            image_url = "https://example.com/images/vivobook_17.jpg",
                            Unit_Instock = 12,
                            Code = 398
                        },
                        new Product
                        {
                            Id = 69,
                            Name = "Asus TUF Gaming FX509", // English Name
                            description = "لابتوب ألعاب 15.6 بوصة مع معالج amd ryzen 7، بطاقة نفيديا rtx 2060، 16 جيجابايت رام", // Arabic Description
                            Name_EN = "Asus TUF Gaming FX509", // English Name
                            description_EN = "15.6-inch gaming laptop with amd ryzen 7, nvidia rtx 2060, 16gb ram", // English Description
                            price = 1399.99m,
                            image_url = "https://example.com/images/tuf_gaming_fx505.jpg",
                            Unit_Instock = 9,
                            Code = 399
                        },
                        new Product
                        {
                            Id = 70,
                            Name = "Asus ProArt Display PA27AC", // English Name
                            description = "شاشة بدقة 1440p مع دعم HDR، 100% sRGB", // Arabic Description
                            Name_EN = "Asus ProArt Display PA27AC", // English Name
                            description_EN = "1440p display with HDR support, 100% sRGB", // English Description
                            price = 499.99m,
                            image_url = "https://example.com/images/proart_display_pa27ac.jpg",
                            Unit_Instock = 12,
                            Code = 400
                        }


            );


            #endregion


            #endregion



            // Index on product name
            modelBuilder.Entity<Product>().HasIndex(p => p.Name);
            modelBuilder.Entity<Discount>()
                .HasMany(p => p.product_Discounts)
                .WithOne(p => p.discount)
                .HasForeignKey(p => p.DiscountId);
            modelBuilder.Entity<Reviews>()
                .HasOne(o => o.product)
                .WithMany(o => o.Reviews)
                .HasForeignKey(o => o.ProductId);
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Order_Items)
                .WithOne(oi => oi.Orders)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.shipping_Methods)
                .WithMany()
                .HasForeignKey(o => o.ShippingMethodsID);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.payment_Methods)
                .WithMany()
                .HasForeignKey(o => o.PaymentMethodsID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(o => o.Orders)
                .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.order_State)
                .WithMany()
                .HasForeignKey(o => o.OrderStateID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payment_Methods>()
                .HasOne(p => p.Cardtype)
                .WithMany()
                .HasForeignKey(p => p.Card_TypeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.SubCategories)
                .WithOne(c => c.ParentCategory)
                .HasForeignKey(c => c.ParentCategoryID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CategorySpecificationKey>()
                .HasOne(csk => csk.Category)
                .WithMany(c => c.CategorySpecificationKeys)
                .HasForeignKey(csk => csk.CategoryId);

            modelBuilder.Entity<CategorySpecificationKey>()
                .HasOne(csk => csk.SpecificationKey)
                .WithMany(sk => sk.CategorySpecificationKeys)
                .HasForeignKey(csk => csk.SpecificationKeyId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Payment_Methods)
                .WithOne()
                .HasForeignKey<User>(u => u.Payment_MethodsID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CategorySpecificationKey>()
            .HasOne(csk => csk.Category)
            .WithMany(c => c.CategorySpecificationKeys)
            .HasForeignKey(csk => csk.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);


            // Set index on email to enhance search and make unique
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        }

    }



}


#region old 
//protected override void OnModelCreating(ModelBuilder modelBuilder)
//{
//    base.OnModelCreating(modelBuilder);
//   //  modelBuilder.Ignore<BaseEntity<int>>();     

//    // set index on product name 
//    modelBuilder.Entity<Product>().HasIndex(p => p.Name);

//    modelBuilder.Entity<Order>()
//        .HasMany(o => o.Order_Items)
//        .WithOne(oi => oi.order)
//        .HasForeignKey(oi => oi.OrderId)
//        .OnDelete(DeleteBehavior.Cascade);

//    modelBuilder.Entity<Order>()

//        .HasOne(o => o.shipping_Methods)
//        .WithMany()
//        .HasForeignKey(o => o.Shipping_Methods_ID);



//    modelBuilder.Entity<Order>()
//        .HasOne(o => o.payment_Methods)
//        .WithMany()
//        .HasForeignKey(o => o.Payment_Methods_ID)
//        .OnDelete(DeleteBehavior.NoAction);



//    modelBuilder.Entity<Order>()
//        .HasOne(o => o.User)
//        .WithMany()
//        .HasForeignKey(o => o.User_Id);

//    modelBuilder.Entity<Order>()
//        .HasOne(o => o.order_State)
//        .WithMany()
//        .HasForeignKey(o => o.Order_State_ID);


//    modelBuilder.Entity<Order>()
//        .HasOne(o => o.order_State)
//        .WithMany()
//        .HasForeignKey(o => o.Order_State_ID)
//        .OnDelete(DeleteBehavior.Restrict);
//    modelBuilder.Entity<Payment_Methods>()
//        .HasOne(p => p.cardtype)
//        .WithMany()
//        .HasForeignKey(p => p.Card_TypeId)
//        .OnDelete(DeleteBehavior.Cascade);
//    modelBuilder.Entity<Category>()
//       .HasMany(c => c.SubCategories)
//       .WithOne(c => c.ParentCategory)
//       .HasForeignKey(c => c.ParentCategoryID)
//       .OnDelete(DeleteBehavior.Restrict);
//    modelBuilder.Entity<CategorySpecificationKey>()
//  .HasKey(csk => new { csk.CategoryId, csk.SpecificationKeyId });


//    modelBuilder.Entity<CategorySpecificationKey>()
//        .HasOne(csk => csk.Category)
//        .WithMany(c => c.CategorySpecificationKeys)
//        .HasForeignKey(csk => csk.CategoryId);

//    modelBuilder.Entity<CategorySpecificationKey>()
//        .HasOne(csk => csk.SpecificationKey)
//        .WithMany(sk => sk.CategorySpecificationKeys)
//        .HasForeignKey(csk => csk.SpecificationKeyId);
//    modelBuilder.Entity<User>()
//        .HasOne(u => u.Payment_Methods)
//        .WithOne()
//        .HasForeignKey<User>(u => u.Id)
//        .OnDelete(DeleteBehavior.Restrict);

//      modelBuilder.Entity<User>()
//       .HasOne(u => u.Payment_Methods)
//       .WithOne() 
//       .HasForeignKey<User>(u => u.Payment_MethodsID) 
//       .OnDelete(DeleteBehavior.Restrict);
//    //making the id identity
//    modelBuilder.Entity<User>()
//      .Property(u => u.Payment_MethodsID)
//      .IsRequired(false);
//    modelBuilder.Entity<User>()
//    .Property(u => u.Id)
//    .ValueGeneratedOnAdd();

//    // set index on email to inhance search and mak unique
//    modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();




//}


#endregion