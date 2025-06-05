using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Context.Migrations
{
    /// <inheritdoc />
    public partial class startfordeploy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "card_Types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_card_Types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name_EN = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    ParentCategoryID = table.Column<int>(type: "int", nullable: true),
                    Code = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_By = table.Column<int>(type: "int", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryID",
                        column: x => x.ParentCategoryID,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name_EN = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Code = table.Column<int>(type: "int", nullable: true),
                    Start_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Percentage = table.Column<decimal>(type: "money", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_By = table.Column<int>(type: "int", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order_States",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name_EN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_States", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name_EN = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    description_EN = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    image_url = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Code = table.Column<int>(type: "int", nullable: true),
                    price = table.Column<decimal>(type: "money", nullable: false),
                    priceAfterDiscount = table.Column<decimal>(type: "money", nullable: true),
                    Unit_Instock = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_By = table.Column<int>(type: "int", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shipping_Methods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Method_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Cost = table.Column<decimal>(type: "money", nullable: false),
                    Estimated_Delivery_Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_By = table.Column<int>(type: "int", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipping_Methods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpecificationKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Key_Ar = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecificationKeys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payment_Methods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Card_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Expiration_Date = table.Column<DateOnly>(type: "date", nullable: true),
                    Is_Default = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Card_TypeId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_By = table.Column<int>(type: "int", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment_Methods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payment_Methods_card_Types_Card_TypeId",
                        column: x => x.Card_TypeId,
                        principalTable: "card_Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product_Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Categories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Categories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product_Discounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiscountId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Discounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Discounts_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Discounts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product_Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    image_url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Images_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsAccpted = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_By = table.Column<int>(type: "int", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategorySpecificationKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    SpecificationKeyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategorySpecificationKeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategorySpecificationKeys_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategorySpecificationKeys_SpecificationKeys_SpecificationKeyId",
                        column: x => x.SpecificationKeyId,
                        principalTable: "SpecificationKeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductSpecifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecificationKeyId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSpecifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSpecifications_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSpecifications_SpecificationKeys_SpecificationKeyId",
                        column: x => x.SpecificationKeyId,
                        principalTable: "SpecificationKeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Region = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Payment_MethodsID = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Payment_Methods_Payment_MethodsID",
                        column: x => x.Payment_MethodsID,
                        principalTable: "Payment_Methods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: true),
                    order_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total_amout = table.Column<decimal>(type: "money", nullable: false),
                    tracking_number = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    shipping_address = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true),
                    ShippingMethodsID = table.Column<int>(type: "int", nullable: false),
                    PaymentMethodsID = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OrderStateID = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_By = table.Column<int>(type: "int", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Order_States_OrderStateID",
                        column: x => x.OrderStateID,
                        principalTable: "Order_States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Payment_Methods_PaymentMethodsID",
                        column: x => x.PaymentMethodsID,
                        principalTable: "Payment_Methods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Shipping_Methods_ShippingMethodsID",
                        column: x => x.ShippingMethodsID,
                        principalTable: "Shipping_Methods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ReviewID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Reviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Reviews_Reviews_ReviewID",
                        column: x => x.ReviewID,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order_Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Tax = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Items_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_Items_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, null, "Admin", "ADMIN" },
                    { 2, null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "City", "ConcurrencyStamp", "Country", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Payment_MethodsID", "PhoneNumber", "PhoneNumberConfirmed", "PostalCode", "Region", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1, 0, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "City", "002030cb-4ff0-473a-a858-98c5704cc16a", "Country", "admin@gmail.com", true, null, null, false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEGNx7Tr4IXIy5wRactIKPR/A0vxSq0l0e2QWQ/syc3TKH2UnzO7PwJbFrxtX9O1mlw==", null, null, true, "12345", "Region", "019b2a27-f805-42e7-9c27-9e7a021d5c17", false, "admin" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Code", "CreatedBy", "Created_at", "ImageUrl", "IsDeleted", "Name", "Name_EN", "ParentCategoryID", "Updated_By", "Updated_at" },
                values: new object[,]
                {
                    { 1, 101, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4615), null, false, "Mobile", "Mobile", null, null, null },
                    { 2, 102, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4637), null, false, "لابتوب", "Laptops", null, null, null },
                    { 3, 103, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4639), null, false, "كمبيوتر / شاشات", "Displays / Desktops", null, null, null },
                    { 4, 104, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4641), null, false, "اللوحات الأم/مكونات", "Motherboards / Components", null, null, null },
                    { 5, 105, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4643), null, false, "الشبكات / إنترنت الأشياء / السيرفرات", "Networking / IoT / Servers", null, null, null },
                    { 6, 106, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4645), null, false, "إكسسوارات", "Accessories", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "Code", "CreatedBy", "Created_at", "End_date", "ImageUrl", "IsActive", "IsDeleted", "Name", "Name_EN", "Percentage", "Start_date", "Updated_By", "Updated_at" },
                values: new object[,]
                {
                    { 1, 123456, null, new DateTime(2024, 11, 14, 3, 21, 48, 551, DateTimeKind.Local).AddTicks(3070), new DateTime(2024, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Remove Discount", "Remove Discount", 0.0m, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 2, 123456, null, new DateTime(2024, 11, 14, 3, 21, 48, 551, DateTimeKind.Local).AddTicks(3115), new DateTime(2024, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "تخفيضات الصيف", "Summer Sale", 10.0m, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 3, 654321, null, new DateTime(2024, 11, 14, 3, 21, 48, 551, DateTimeKind.Local).AddTicks(3118), new DateTime(2024, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Black Friday", "Black Friday", 25.0m, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 4, 112233, null, new DateTime(2024, 11, 14, 3, 21, 48, 551, DateTimeKind.Local).AddTicks(3120), new DateTime(2024, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, "خصم الشتاء", "Winter Discount", 15.0m, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null }
                });

            migrationBuilder.InsertData(
                table: "Order_States",
                columns: new[] { "Id", "Name", "Name_EN" },
                values: new object[,]
                {
                    { 1, "قيد الانتظار", "Pending" },
                    { 2, "مؤكد", "Confirmed" },
                    { 3, "تم الشحن", "Shipped" },
                    { 4, "تم التوصيل", "Delivered" },
                    { 5, "تم الإلغاء", "Canceled" },
                    { 6, "تم إرجاع الطلب", "Returned" },
                    { 7, "مكتمل", "Completed" }
                });

            migrationBuilder.InsertData(
                table: "Payment_Methods",
                columns: new[] { "Id", "Card_Number", "Card_TypeId", "CreatedBy", "Created_at", "Expiration_Date", "IsDeleted", "Is_Default", "Name", "Updated_By", "Updated_at" },
                values: new object[,]
                {
                    { 1, null, null, null, new DateTime(2024, 11, 14, 3, 21, 48, 549, DateTimeKind.Local).AddTicks(2506), null, false, false, "PayPal", null, null },
                    { 2, null, null, null, new DateTime(2024, 11, 14, 3, 21, 48, 549, DateTimeKind.Local).AddTicks(2566), null, false, false, "MasterCard", null, null },
                    { 3, null, null, null, new DateTime(2024, 11, 14, 3, 21, 48, 549, DateTimeKind.Local).AddTicks(2568), null, false, false, "Cash", null, null },
                    { 4, null, null, null, new DateTime(2024, 11, 14, 3, 21, 48, 549, DateTimeKind.Local).AddTicks(2569), null, false, false, "Visa", null, null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Code", "CreatedBy", "Created_at", "IsActive", "IsDeleted", "Name", "Name_EN", "Unit_Instock", "Updated_By", "Updated_at", "description", "description_EN", "image_url", "price", "priceAfterDiscount" },
                values: new object[,]
                {
                    { 1, 333, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7180), true, false, "Asus Zenbook S 14", "Asus Zenbook S 14", 10, null, null, null, null, "https://example.com/images/rog_strix_g15.jpg", 99300m, null },
                    { 2, 334, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7255), true, false, "Asus Zenbook S 16", "Asus Zenbook S 16", 10, null, null, null, null, "https://example.com/images/rog_strix_g15.jpg", 100799m, null },
                    { 3, 335, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7258), true, false, "ASUS Vivobook S 15", "ASUS Vivobook S 15", 10, null, null, null, null, "https://example.com/images/rog_strix_g15.jpg", 38299.00m, null },
                    { 4, 33, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7263), true, false, "ASUS Zenbook 14 OLED", "ASUS Zenbook 14 OLED", 10, null, null, null, null, "https://example.com/images/rog_strix_g15.jpg", 72499.00m, null },
                    { 5, 337, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7266), true, false, "Asus ROG Strix Scar 17", "Asus ROG Strix Scar 17", 5, null, null, "لابتوب ألعاب 17.3 بوصة مع معالج intel core i9، بطاقة نفيديا rtx 3080، 32 جيجابايت رام", "17.3-inch gaming laptop with intel core i9, nvidia rtx 3080, 32gb ram", "https://example.com/images/rog_strix_scar_17.jpg", 2499.99m, null },
                    { 6, 338, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7269), true, false, "Asus ExpertBook P1", "Asus ExpertBook P1", 40, null, null, "لابتوب مخصص للأعمال مع معالج intel core i3، 4 جيجابايت رام، 256 جيجابايت ssd", "Affordable business laptop with intel core i3, 4gb ram, 256gb ssd", "https://example.com/images/expertbook_p1.jpg", 499.99m, null },
                    { 7, 339, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7272), true, false, "Asus ROG Flow Z13", "Asus ROG Flow Z13", 20, null, null, "لابتوب ألعاب 2 في 1 مع معالج intel core i7، بطاقة نفيديا rtx 3050 تي، 16 جيجابايت رام", "Compact 2-in-1 gaming laptop with intel core i7, nvidia rtx 3050 ti, 16gb ram", "https://example.com/images/rog_flow_z13.jpg", 1699.99m, null },
                    { 8, 340, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7275), true, false, "Asus ZenBook Duo 14", "Asus ZenBook Duo 14", 10, null, null, "لابتوب بشاشة مزدوجة مع معالج intel core i7، 16 جيجابايت رام، 1 تيرابايت ssd", "Dual-screen laptop with intel core i7, 16gb ram, 1tb ssd", "https://example.com/images/zenbook_duo_14.jpg", 1399.99m, null },
                    { 9, 341, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7277), true, false, "Asus TUF Dash F15", "Asus TUF Dash F15", 8, null, null, "لابتوب ألعاب 15.6 بوصة مع معالج intel core i7، بطاقة نفيديا rtx 3070، 16 جيجابايت رام", "15.6-inch gaming laptop with intel core i7, nvidia rtx 3070, 16gb ram", "https://example.com/images/tuf_dash_f15.jpg", 1799.99m, null },
                    { 10, 342, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7280), true, false, "Asus ProArt Studiobook 16", "Asus ProArt Studiobook 16", 3, null, null, "لابتوب للمبدعين مع معالج intel core i9، بطاقة نفيديا rtx 3070، 32 جيجابايت رام، 1 تيرابايت ssd", "Creative laptop with intel core i9, nvidia rtx 3070, 32gb ram, 1tb ssd", "https://example.com/images/proart_studiobook_16.jpg", 2199.99m, null },
                    { 11, 343, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7288), true, false, "Asus VivoBook Flip 14", "Asus VivoBook Flip 14", 12, null, null, "لابتوب 2 في 1 مع معالج intel core i5، 8 جيجابايت رام، 512 جيجابايت ssd", "2-in-1 laptop with intel core i5, 8gb ram, 512gb ssd", "https://example.com/images/vivobook_flip_14.jpg", 799.99m, null },
                    { 12, 344, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7291), true, false, "Asus ROG Strix G17", "Asus ROG Strix G17", 8, null, null, "لابتوب ألعاب 17.3 بوصة مع معالج amd ryzen 9، بطاقة نفيديا rtx 3070، 32 جيجابايت رام", "17.3-inch gaming laptop with amd ryzen 9, nvidia rtx 3070, 32gb ram", "https://example.com/images/rog_strix_g17.jpg", 2199.99m, null },
                    { 13, 345, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7293), true, false, "Asus VivoBook 14", "Asus VivoBook 14", 30, null, null, "لابتوب 14 بوصة مع معالج intel core i3، 4 جيجابايت رام، 256 جيجابايت ssd", "14-inch laptop with intel core i3, 4gb ram, 256gb ssd", "https://example.com/images/vivobook_14.jpg", 499.99m, null },
                    { 14, 346, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7296), true, false, "Asus TUF Gaming F17", "Asus TUF Gaming F17", 7, null, null, "لابتوب ألعاب 17.3 بوصة مع معالج intel core i7، بطاقة نفيديا rtx 3060، 16 جيجابايت رام", "17.3-inch gaming laptop with intel core i7, nvidia rtx 3060, 16gb ram", "https://example.com/images/tuf_gaming_f17.jpg", 1599.99m, null },
                    { 15, 347, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7299), true, false, "Asus Zenbook Pro Duo 15", "Asus Zenbook Pro Duo 15", 5, null, null, "لابتوب بشاشتين مع معالج intel core i9، بطاقة نفيديا rtx 3080، 32 جيجابايت رام، 1 تيرابايت ssd", "Dual-screen laptop with intel core i9, nvidia rtx 3080, 32gb ram, 1tb ssd", "https://example.com/images/zenbook_pro_duo_15.jpg", 2999.99m, null },
                    { 16, 348, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7329), true, false, "Asus TUF Gaming A17", "Asus TUF Gaming A17", 10, null, null, "لابتوب ألعاب 17.3 بوصة مع معالج amd ryzen 7، بطاقة نفيديا rtx 3070، 16 جيجابايت رام", "17.3-inch gaming laptop with amd ryzen 7, nvidia rtx 3070, 16gb ram", "https://example.com/images/tuf_gaming_a17.jpg", 1799.99m, null },
                    { 17, 349, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7332), true, false, "Asus ExpertBook P2", "Asus ExpertBook P2", 25, null, null, "لابتوب للأعمال مع معالج intel core i5، 8 جيجابايت رام، 512 جيجابايت ssd", "Business laptop with intel core i5, 8gb ram, 512gb ssd", "https://example.com/images/expertbook_p2.jpg", 699.99m, null },
                    { 18, 350, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7335), true, false, "Asus Chromebook Flip", "Asus Chromebook Flip", 20, null, null, "لابتوب كروم بوك قابل للطي مع شاشة 14 بوصة، 4 جيجابايت رام، 64 جيجابايت eMMC", "Convertible chromebook with 14-inch display, 4gb ram, 64gb emmc", "https://example.com/images/chromebook_flip.jpg", 399.99m, null },
                    { 19, 350, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7339), true, false, "Asus TUF Gaming F17", "Asus TUF Gaming F17", 7, null, null, "لابتوب ألعاب 17.3 بوصة مع معالج intel core i7، بطاقة نفيديا rtx 3060، 16 جيجابايت رام", "17.3-inch gaming laptop with intel core i7, nvidia rtx 3060, 16gb ram", "https://example.com/images/tuf_gaming_f17.jpg", 1599.99m, null },
                    { 20, 351, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7341), true, false, "Asus Zenbook Pro Duo 15", "Asus Zenbook Pro Duo 15", 5, null, null, "لابتوب بشاشتين مع معالج intel core i9، بطاقة نفيديا rtx 3080، 32 جيجابايت رام، 1 تيرابايت ssd", "Dual-screen laptop with intel core i9, nvidia rtx 3080, 32gb ram, 1tb ssd", "https://example.com/images/zenbook_pro_duo_15.jpg", 2999.99m, null },
                    { 21, 352, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7345), true, false, "Asus TUF Gaming A17", "Asus TUF Gaming A17", 10, null, null, "لابتوب ألعاب 17.3 بوصة مع معالج amd ryzen 7، بطاقة نفيديا rtx 3070، 16 جيجابايت رام", "17.3-inch gaming laptop with amd ryzen 7, nvidia rtx 3070, 16gb ram", "https://example.com/images/tuf_gaming_a17.jpg", 1799.99m, null },
                    { 23, 353, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7348), true, false, "Asus ExpertBook P2", "Asus ExpertBook P2", 20, null, null, "لابتوب للأعمال مع معالج intel core i5، 8 جيجابايت رام، 512 جيجابايت ssd", "Business laptop with intel core i5, 8gb ram, 512gb ssd", "https://example.com/images/expertbook_p2.jpg", 699.99m, null },
                    { 24, 354, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7350), true, false, "Asus ROG Strix Scar 15", "Asus ROG Strix Scar 15", 6, null, null, "لابتوب ألعاب 15.6 بوصة مع معالج intel core i9، بطاقة نفيديا rtx 3080، 32 جيجابايت رام", "15.6-inch gaming laptop with intel core i9, nvidia rtx 3080, 32gb ram", "https://example.com/images/rog_strix_scar_15.jpg", 2599.99m, null },
                    { 25, 355, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7353), true, false, "Asus ZenBook 15", "Asus ZenBook 15", 25, null, null, "لابتوب 15.6 بوصة مع معالج intel core i7، بطاقة نفيديا gtx 1650، 16 جيجابايت رام", "15.6-inch laptop with intel core i7, nvidia gtx 1650, 16gb ram", "https://example.com/images/zenbook_15.jpg", 1299.99m, null },
                    { 26, 356, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7356), true, false, "Asus ROG Zephyrus G14", "Asus ROG Zephyrus G14", 15, null, null, "لابتوب ألعاب 14 بوصة مع معالج amd ryzen 9، بطاقة نفيديا rtx 3060، 16 جيجابايت رام", "14-inch gaming laptop with amd ryzen 9, nvidia rtx 3060, 16gb ram", "https://example.com/images/rog_zephyrus_g14.jpg", 1899.99m, null },
                    { 27, 357, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7358), true, false, "Asus VivoBook S14", "Asus VivoBook S14", 18, null, null, "لابتوب 14 بوصة مع معالج intel core i5، 8 جيجابايت رام، 512 جيجابايت ssd", "14-inch laptop with intel core i5, 8gb ram, 512gb ssd", "https://example.com/images/vivobook_s14.jpg", 799.99m, null },
                    { 28, 358, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7361), true, false, "Asus ProArt StudioBook Pro 17", "Asus ProArt StudioBook Pro 17", 4, null, null, "لابتوب للأعمال المبدعة مع معالج intel xeon، بطاقة نفيديا rtx 3000، 64 جيجابايت رام، 1 تيرابايت ssd", "Creative workstation laptop with intel xeon, nvidia rtx 3000, 64gb ram, 1tb ssd", "https://example.com/images/proart_studiobook_pro_17.jpg", 3499.99m, null },
                    { 29, 359, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7365), true, false, "Asus ROG Flow X13", "Asus ROG Flow X13", 12, null, null, "لابتوب 2 في 1 مع معالج amd ryzen 9، بطاقة نفيديا rtx 3050، 16 جيجابايت رام", "2-in-1 laptop with amd ryzen 9, nvidia rtx 3050, 16gb ram", "https://example.com/images/rog_flow_x13.jpg", 1799.99m, null },
                    { 30, 360, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7368), true, false, "Asus ZenBook Flip 14", "Asus ZenBook Flip 14", 14, null, null, "لابتوب 2 في 1 مع معالج intel core i7، 16 جيجابايت رام، 1 تيرابايت ssd", "2-in-1 laptop with intel core i7, 16gb ram, 1tb ssd", "https://example.com/images/zenbook_flip_14.jpg", 1399.99m, null },
                    { 31, 361, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7371), true, false, "Asus TUF Gaming FX505", "Asus TUF Gaming FX505", 10, null, null, "لابتوب ألعاب 15.6 بوصة مع معالج intel core i5، بطاقة نفيديا gtx 1650، 8 جيجابايت رام", "15.6-inch gaming laptop with intel core i5, nvidia gtx 1650, 8gb ram", "https://example.com/images/tuf_gaming_fx505.jpg", 899.99m, null },
                    { 32, 362, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7374), true, false, "Asus ExpertBook B9", "Asus ExpertBook B9", 6, null, null, "لابتوب أعمال فائق الخفة مع معالج intel core i7، 16 جيجابايت رام، 512 جيجابايت ssd", "Ultra-light business laptop with intel core i7, 16gb ram, 512gb ssd", "https://example.com/images/expertbook_b9.jpg", 1499.99m, null },
                    { 33, 363, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7377), true, false, "Asus VivoBook Ultra 15", "Asus VivoBook Ultra 15", 11, null, null, "لابتوب 15.6 بوصة مع معالج intel core i7، 8 جيجابايت رام، 1 تيرابايت ssd", "15.6-inch laptop with intel core i7, 8gb ram, 1tb ssd", "https://example.com/images/vivobook_ultra_15.jpg", 1099.99m, null },
                    { 34, 364, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7380), true, false, "Asus ROG Zephyrus M16", "Asus ROG Zephyrus M16", 7, null, null, "لابتوب ألعاب 16 بوصة مع معالج intel core i9، بطاقة نفيديا rtx 3070، 32 جيجابايت رام", "16-inch gaming laptop with intel core i9, nvidia rtx 3070, 32gb ram", "https://example.com/images/rog_zephyrus_m16.jpg", 2499.99m, null },
                    { 35, 365, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7382), true, false, "Asus TUF Gaming F15", "Asus TUF Gaming F15", 14, null, null, "لابتوب ألعاب 15.6 بوصة مع معالج intel core i7، بطاقة نفيديا rtx 3070، 16 جيجابايت رام", "15.6-inch gaming laptop with intel core i7, nvidia rtx 3070, 16gb ram", "https://example.com/images/tuf_gaming_f15.jpg", 1599.99m, null },
                    { 36, 366, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7385), true, false, "Asus Zenbook Flip 13", "Asus Zenbook Flip 13", 20, null, null, "لابتوب 2 في 1 مع معالج intel core i5، 8 جيجابايت رام، 256 جيجابايت ssd", "2-in-1 laptop with intel core i5, 8gb ram, 256gb ssd", "https://example.com/images/zenbook_flip_13.jpg", 849.99m, null },
                    { 37, 367, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7388), true, false, "Asus TUF Dash A15", "Asus TUF Dash A15", 13, null, null, "لابتوب ألعاب 15.6 بوصة مع معالج amd ryzen 7، بطاقة نفيديا rtx 3070، 16 جيجابايت رام", "15.6-inch gaming laptop with amd ryzen 7, nvidia rtx 3070, 16gb ram", "https://example.com/images/tuf_dash_a15.jpg", 1699.99m, null },
                    { 38, 368, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7390), true, false, "Asus ProArt StudioBook 15", "Asus ProArt StudioBook 15", 5, null, null, "لابتوب للمبدعين مع معالج intel xeon، بطاقة نفيديا rtx 3000، 32 جيجابايت رام، 1 تيرابايت ssd", "Creative laptop with intel xeon, nvidia rtx 3000, 32gb ram, 1tb ssd", "https://example.com/images/proart_studiobook_15.jpg", 3199.99m, null },
                    { 39, 369, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7419), true, false, "Asus VivoBook Ultra K15", "Asus VivoBook Ultra K15", 18, null, null, "لابتوب 15.6 بوصة مع معالج intel core i5، 8 جيجابايت رام، 512 جيجابايت ssd", "15.6-inch laptop with intel core i5, 8gb ram, 512gb ssd", "https://example.com/images/vivobook_ultra_k15.jpg", 799.99m, null },
                    { 40, 370, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7422), true, false, "Asus ExpertBook L1", "Asus ExpertBook L1", 25, null, null, "لابتوب مخصص للأعمال مع معالج intel celeron، 4 جيجابايت رام، 128 جيجابايت eMMC", "Business laptop with intel celeron, 4gb ram, 128gb eMMC", "https://example.com/images/expertbook_l1.jpg", 379.99m, null },
                    { 41, 371, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7425), true, false, "Asus VivoBook 17", "Asus VivoBook 17", 12, null, null, "لابتوب 17.3 بوصة مع معالج intel core i7، 16 جيجابايت رام، 512 جيجابايت ssd", "17.3-inch laptop with intel core i7, 16gb ram, 512gb ssd", "https://example.com/images/vivobook_17.jpg", 1099.99m, null },
                    { 42, 372, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7428), true, false, "Asus ROG Strix G14", "Asus ROG Strix G14", 8, null, null, "لابتوب ألعاب 14 بوصة مع معالج intel core i9، بطاقة نفيديا rtx 3070، 32 جيجابايت رام", "14-inch gaming laptop with intel core i9, nvidia rtx 3070, 32gb ram", "https://example.com/images/rog_strix_g14.jpg", 2299.99m, null },
                    { 43, 373, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7431), true, false, "Asus ZenBook 13", "Asus ZenBook 13", 22, null, null, "لابتوب 13.3 بوصة مع معالج intel core i5، 8 جيجابايت رام، 512 جيجابايت ssd", "13.3-inch laptop with intel core i5, 8gb ram, 512gb ssd", "https://example.com/images/zenbook_13.jpg", 849.99m, null },
                    { 44, 374, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7434), true, false, "Asus TUF Gaming K7", "Asus TUF Gaming K7", 50, null, null, "لوحة مفاتيح ألعاب ميكانيكية مع مفاتيح ROG RX، إضاءة RGB قابلة للتخصيص", "Mechanical gaming keyboard with ROG RX switches, customizable RGB lighting", "https://example.com/images/tuf_gaming_k7.jpg", 129.99m, null },
                    { 45, 375, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7436), true, false, "Asus ROG Chakram X", "Asus ROG Chakram X", 35, null, null, "ماوس ألعاب لاسلكي مع مستشعر HERO 25K، زر قابل للتخصيص، إضاءة RGB", "Wireless gaming mouse with HERO 25K sensor, customizable button, RGB lighting", "https://example.com/images/rog_chakram_x.jpg", 99.99m, null },
                    { 46, 376, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7439), true, false, "Asus ROG Strix Fusion 700", "Asus ROG Strix Fusion 700", 40, null, null, "سماعة رأس ألعاب لاسلكية مع صوت محيطي 7.1، إضاءة RGB", "Wireless gaming headset with 7.1 surround sound, RGB lighting", "https://example.com/images/rog_strix_fusion_700.jpg", 179.99m, null },
                    { 47, 377, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7443), true, false, "Asus ZenBook 14X", "Asus ZenBook 14X", 15, null, null, "لابتوب 14 بوصة مع معالج intel core i7، 16 جيجابايت رام، 1 تيرابايت ssd", "14-inch laptop with intel core i7, 16gb ram, 1tb ssd", "https://example.com/images/zenbook_14x.jpg", 1299.99m, null },
                    { 48, 378, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7446), true, false, "Asus ProArt Display PA32UCG", "Asus ProArt Display PA32UCG", 5, null, null, "شاشة احترافية بدقة 4K مع دعم HDR، 98% DCI-P3", "4K professional monitor with HDR support, 98% DCI-P3", "https://example.com/images/proart_display_pa32ucg.jpg", 1799.99m, null },
                    { 49, 379, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7448), true, false, "Asus ROG Swift PG259QN", "Asus ROG Swift PG259QN", 8, null, null, "شاشة ألعاب 24.5 بوصة مع معدل تحديث 360Hz و 1ms زمن استجابة", "24.5-inch gaming monitor with 360Hz refresh rate and 1ms response time", "https://example.com/images/rog_swift_pg259qn.jpg", 699.99m, null },
                    { 50, 380, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7451), true, false, "Asus ROG Strix XG27AQ", "Asus ROG Strix XG27AQ", 10, null, null, "شاشة ألعاب 27 بوصة بدقة 1440p مع معدل تحديث 170Hz", "27-inch gaming monitor with 1440p resolution and 170Hz refresh rate", "https://example.com/images/rog_strix_xg27aq.jpg", 499.99m, null },
                    { 51, 381, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7454), true, false, "Asus TUF Gaming VG27AQL1A", "Asus TUF Gaming VG27AQL1A", 9, null, null, "شاشة ألعاب 27 بوصة بدقة 1440p مع معدل تحديث 170Hz و G-SYNC", "27-inch gaming monitor with 1440p resolution, 170Hz refresh rate, and G-SYNC", "https://example.com/images/tuf_gaming_vg27aql1a.jpg", 379.99m, null },
                    { 52, 382, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7456), true, false, "Asus ZenScreen MB16ACE", "Asus ZenScreen MB16ACE", 30, null, null, "شاشة محمولة 15.6 بوصة بدقة Full HD مع منفذ USB-C", "15.6-inch portable screen with Full HD resolution and USB-C port", "https://example.com/images/zenScreen_mb16ace.jpg", 219.99m, null },
                    { 53, 383, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7460), true, false, "Asus ROG Strix G17", "Asus ROG Strix G17", 10, null, null, "لابتوب ألعاب 17.3 بوصة مع معالج amd ryzen 9، بطاقة نفيديا rtx 3070، 16 جيجابايت رام", "17.3-inch gaming laptop with amd ryzen 9, nvidia rtx 3070, 16gb ram", "https://example.com/images/rog_strix_g17.jpg", 1999.99m, null },
                    { 54, 384, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7462), true, false, "Asus ZenBook 20", "Asus ZenBook 20", 22, null, null, "لابتوب 15.6 بوصة مع معالج intel core i7، 16 جيجابايت رام، 512 جيجابايت ssd", "15.6-inch laptop with intel core i7, 16gb ram, 512gb ssd", "https://example.com/images/zenbook_15.jpg", 1099.99m, null },
                    { 55, 385, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7465), true, false, "Asus VivoBook S15", "Asus VivoBook S15", 18, null, null, "لابتوب 14 بوصة مع معالج intel core i5، 8 جيجابايت رام، 256 جيجابايت ssd", "14-inch laptop with intel core i5, 8gb ram, 256gb ssd", "https://example.com/images/vivobook_s14.jpg", 799.99m, null },
                    { 56, 386, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7468), true, false, "Asus TUF Gaming F21", "Asus TUF Gaming F21", 8, null, null, "لابتوب ألعاب 17.3 بوصة مع معالج intel core i7، بطاقة نفيديا rtx 3060، 16 جيجابايت رام", "17.3-inch gaming laptop with intel core i7, nvidia rtx 3060, 16gb ram", "https://example.com/images/tuf_gaming_f17.jpg", 1799.99m, null },
                    { 57, 387, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7470), true, false, "Asus ROG Strix Scar 19", "Asus ROG Strix Scar 19", 6, null, null, "لابتوب ألعاب 15.6 بوصة مع معالج intel core i9، بطاقة نفيديا rtx 3080، 32 جيجابايت رام", "15.6-inch gaming laptop with intel core i9, nvidia rtx 3080, 32gb ram", "https://example.com/images/rog_strix_scar_15.jpg", 2399.99m, null },
                    { 58, 388, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7473), true, false, "Asus ExpertBook B1", "Asus ExpertBook B1", 15, null, null, "لابتوب مخصص للأعمال مع معالج intel core i5، 8 جيجابايت رام، 512 جيجابايت ssd", "Business laptop with intel core i5, 8gb ram, 512gb ssd", "https://example.com/images/expertbook_b1.jpg", 799.99m, null },
                    { 59, 389, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7476), true, false, "Asus Zenbook Pro Duo 5", "Asus Zenbook Pro Duo 5", 4, null, null, "لابتوب مزدوج الشاشة مع معالج intel core i9، بطاقة نفيديا rtx 3070، 32 جيجابايت رام، 1 تيرابايت ssd", "Dual-screen laptop with intel core i9, nvidia rtx 3070, 32gb ram, 1tb ssd", "https://example.com/images/zenbook_pro_duo.jpg", 2899.99m, null },
                    { 60, 390, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7478), true, false, "Asus TUF Gaming A19", "Asus TUF Gaming A19", 7, null, null, "لابتوب ألعاب 17.3 بوصة مع معالج amd ryzen 7، بطاقة نفيديا rtx 3060، 16 جيجابايت رام", "17.3-inch gaming laptop with amd ryzen 7, nvidia rtx 3060, 16gb ram", "https://example.com/images/tuf_gaming_a17.jpg", 1699.99m, null },
                    { 61, 391, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7506), true, false, "Asus VivoBook X512", "Asus VivoBook X512", 25, null, null, "لابتوب 15.6 بوصة مع معالج intel core i3، 4 جيجابايت رام، 128 جيجابايت ssd", "15.6-inch laptop with intel core i3, 4gb ram, 128gb ssd", "https://example.com/images/vivobook_x512.jpg", 449.99m, null },
                    { 62, 392, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7509), true, false, "Asus ZenBook 14 UX425", "Asus ZenBook 14 UX425", 19, null, null, "لابتوب 14 بوصة مع معالج intel core i7، 16 جيجابايت رام، 512 جيجابايت ssd", "14-inch laptop with intel core i7, 16gb ram, 512gb ssd", "https://example.com/images/zenbook_14_ux425.jpg", 999.99m, null },
                    { 63, 393, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7513), true, false, "Asus ProArt StudioBook 17", "Asus ProArt StudioBook 17", 10, null, null, "لابتوب مخصص للمبدعين مع معالج intel core i7، بطاقة نفيديا rtx 3060، 16 جيجابايت رام، 512 جيجابايت ssd", "Creative laptop with intel core i7, nvidia rtx 3060, 16gb ram, 512gb ssd", "https://example.com/images/proart_studiobook_17.jpg", 1699.99m, null },
                    { 64, 394, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7516), true, false, "Asus ROG Strix SCAR III", "Asus ROG Strix SCAR III", 5, null, null, "لابتوب ألعاب 15.6 بوصة مع معالج intel core i7، بطاقة نفيديا rtx 2080، 16 جيجابايت رام", "15.6-inch gaming laptop with intel core i7, nvidia rtx 2080, 16gb ram", "https://example.com/images/rog_strix_scar_iii.jpg", 1899.99m, null },
                    { 65, 395, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7519), true, false, "Asus VivoBook 14 X413", "Asus VivoBook 14 X413", 20, null, null, "لابتوب 14 بوصة مع معالج intel core i5، 8 جيجابايت رام، 512 جيجابايت ssd", "14-inch laptop with intel core i5, 8gb ram, 512gb ssd", "https://example.com/images/vivobook_14_x413.jpg", 749.99m, null },
                    { 66, 396, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7522), true, false, "Asus ZenBook 19", "Asus ZenBook 19", 14, null, null, "لابتوب 13.3 بوصة مع معالج intel core i7، 16 جيجابايت رام، 512 جيجابايت ssd", "13.3-inch laptop with intel core i7, 16gb ram, 512gb ssd", "https://example.com/images/zenbook_13.jpg", 849.99m, null },
                    { 67, 397, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7524), true, false, "Asus ROG Flow X17", "Asus ROG Flow X17", 8, null, null, "لابتوب 2 في 1 مع معالج amd ryzen 9، بطاقة نفيديا rtx 3050، 16 جيجابايت رام", "2-in-1 laptop with amd ryzen 9, nvidia rtx 3050, 16gb ram", "https://example.com/images/rog_flow_x13.jpg", 1799.99m, null },
                    { 68, 398, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7527), true, false, "Asus VivoBook 19", "Asus VivoBook 19", 12, null, null, "لابتوب 17.3 بوصة مع معالج intel core i7، 16 جيجابايت رام، 1 تيرابايت ssd", "17.3-inch laptop with intel core i7, 16gb ram, 1tb ssd", "https://example.com/images/vivobook_17.jpg", 999.99m, null },
                    { 69, 399, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7530), true, false, "Asus TUF Gaming FX509", "Asus TUF Gaming FX509", 9, null, null, "لابتوب ألعاب 15.6 بوصة مع معالج amd ryzen 7، بطاقة نفيديا rtx 2060، 16 جيجابايت رام", "15.6-inch gaming laptop with amd ryzen 7, nvidia rtx 2060, 16gb ram", "https://example.com/images/tuf_gaming_fx505.jpg", 1399.99m, null },
                    { 70, 400, null, new DateTime(2024, 11, 14, 3, 21, 48, 624, DateTimeKind.Local).AddTicks(7532), true, false, "Asus ProArt Display PA27AC", "Asus ProArt Display PA27AC", 12, null, null, "شاشة بدقة 1440p مع دعم HDR، 100% sRGB", "1440p display with HDR support, 100% sRGB", "https://example.com/images/proart_display_pa27ac.jpg", 499.99m, null }
                });

            migrationBuilder.InsertData(
                table: "Shipping_Methods",
                columns: new[] { "Id", "Cost", "CreatedBy", "Created_at", "Estimated_Delivery_Time", "IsDeleted", "Method_Name", "Updated_By", "Updated_at" },
                values: new object[,]
                {
                    { 1, 5.99m, null, new DateTime(2024, 11, 14, 3, 21, 48, 552, DateTimeKind.Local).AddTicks(743), new DateTime(2024, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Standard Shipping", null, null },
                    { 2, 12.99m, null, new DateTime(2024, 11, 14, 3, 21, 48, 552, DateTimeKind.Local).AddTicks(762), new DateTime(2024, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Express Shipping", null, null },
                    { 3, 25.99m, null, new DateTime(2024, 11, 14, 3, 21, 48, 552, DateTimeKind.Local).AddTicks(764), new DateTime(2024, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Overnight Shipping", null, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Code", "CreatedBy", "Created_at", "ImageUrl", "IsDeleted", "Name", "Name_EN", "ParentCategoryID", "Updated_By", "Updated_at" },
                values: new object[,]
                {
                    { 7, 201, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4647), null, false, "Accessories", "Accessories", 1, null, null },
                    { 8, 205, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4649), null, false, "للمنزل", "Home Laptops", 2, null, null },
                    { 9, 204, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4652), null, false, "للعمل", "Workstation Laptops", 2, null, null },
                    { 10, 202, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4653), null, false, "للمبدعين", "For Creators", 2, null, null },
                    { 11, 202, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4656), null, false, "للطلبة", "For Students", 2, null, null },
                    { 12, 203, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4658), null, false, "للألعاب", "Gaming Laptops", 2, null, null },
                    { 13, 203, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4659), null, false, "إكسسوارات", "Accessories", 2, null, null },
                    { 14, 203, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4661), null, false, "Software", "Software", 2, null, null },
                    { 15, 301, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4663), null, false, "الشاشات", "Monitors", 3, null, null },
                    { 16, 302, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4665), null, false, "اجهزة البروجيكتور", "Projectors", 3, null, null },
                    { 17, 303, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4667), null, false, "PCs الكل في واحد", "All-in-One PCs", 3, null, null },
                    { 18, 304, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4669), null, false, "Tower PCs", "Tower PCs", 3, null, null },
                    { 19, 305, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4671), null, false, "Gaming Tower PCs", "Gaming Tower PCs", 3, null, null },
                    { 20, 306, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4672), null, false, "NUCs", "NUCs", 3, null, null },
                    { 21, 307, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4674), null, false, "Mini PCs", "Mini PCs", 3, null, null },
                    { 22, 308, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4676), null, false, "Workstations", "Workstations", 3, null, null },
                    { 23, 401, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4678), null, false, "اللوحات الأم", "Motherboards", 4, null, null },
                    { 24, 403, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4680), null, false, "معالج الرسوميات", "Graphics Cards", 4, null, null },
                    { 25, 402, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4682), null, false, "حقيبة الألعاب", "Gaming Case", 4, null, null },
                    { 26, 404, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4721), null, false, "تبريد", "Cooling", 4, null, null },
                    { 27, 405, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4723), null, false, "وحدات تزويد الطاقة", "Power Supply Units", 4, null, null },
                    { 28, 406, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4725), null, false, "كروت الصوت", "Sound Cards", 4, null, null },
                    { 29, 407, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4727), null, false, "محركات الأقراص الضوئية", "Optical Drives", 4, null, null },
                    { 30, 408, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4729), null, false, "ادوات الرسومات الخارجية", "External Graphics Docks", 4, null, null },
                    { 31, 409, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4731), null, false, "كمبيوتر بلوحة واحدة", "Single Board Computer", 4, null, null },
                    { 32, 501, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4733), null, false, "WiFi 6", "WiFi 6", 5, null, null },
                    { 33, 502, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4735), null, false, "WiFi موجهات", "WiFi Routers", 5, null, null },
                    { 34, 503, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4737), null, false, "شبكي منزلي بالكامل WiFi نظام", "Whole Home Mesh WiFi System", 5, null, null },
                    { 35, 504, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4738), null, false, "موسعات النطاق", "Range Extenders", 5, null, null },
                    { 36, 505, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4740), null, false, "أجهزة توجيه المودم", "Modem Routers", 5, null, null },
                    { 37, 506, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4742), null, false, "محولات لاسلكية وسلكية", "Wireless & Wired Adapters", 5, null, null },
                    { 38, 507, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4744), null, false, "مفاتيح الشبكة", "Network Switches", 5, null, null },
                    { 39, 508, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4746), null, false, "Zenbo", "Zenbo", 5, null, null },
                    { 40, 509, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4748), null, false, "الذكاء الاصطناعي والحلول الصناعية", "AIoT & Industrial Solution", 5, null, null },
                    { 41, 509, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4749), null, false, "سيرفر", "Servers", 5, null, null },
                    { 42, 601, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4751), null, false, "لوحات المفاتيح", "Keyboards", 6, null, null },
                    { 43, 602, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4754), null, false, "لوحات الماوس والفأرة", "Mice and Mouse Pads", 6, null, null },
                    { 44, 603, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4756), null, false, "سماعات الراس والصوت", "Headsets and Audio", 6, null, null },
                    { 45, 603, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4758), null, false, "عدة البث", "Streaming Kit", 6, null, null },
                    { 46, 604, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4760), null, false, "ASUS ملابس وحقائب وملحقات", "Apparels, Bags and Gears", 6, null, null },
                    { 47, 605, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4762), null, false, "Cases and Protection", "Cases and Protection", 6, null, null },
                    { 48, 606, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4764), null, false, "شاحن", "Adapters and Chargers", 6, null, null },
                    { 49, 606, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4766), null, false, "القواعد والدونجل والكابلات", "Docks, Dongles and Cable", 6, null, null },
                    { 50, 607, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4768), null, false, "بنوك الطاقة", "Power Banks", 6, null, null },
                    { 51, 608, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4770), null, false, "قلم", "Stylus", 6, null, null },
                    { 52, 609, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4771), null, false, "متحكم", "Controler", 6, null, null },
                    { 53, 6010, null, new DateTime(2024, 11, 14, 3, 21, 48, 555, DateTimeKind.Local).AddTicks(4774), null, false, "Gimbal", "Gimbal", 6, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Payment_MethodsID",
                table: "AspNetUsers",
                column: "Payment_MethodsID",
                unique: true,
                filter: "[Payment_MethodsID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryID",
                table: "Categories",
                column: "ParentCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_CategorySpecificationKeys_CategoryId",
                table: "CategorySpecificationKeys",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategorySpecificationKeys_SpecificationKeyId",
                table: "CategorySpecificationKeys",
                column: "SpecificationKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Items_OrderId",
                table: "Order_Items",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Items_ProductId",
                table: "Order_Items",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStateID",
                table: "Orders",
                column: "OrderStateID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentMethodsID",
                table: "Orders",
                column: "PaymentMethodsID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShippingMethodsID",
                table: "Orders",
                column: "ShippingMethodsID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_Methods_Card_TypeId",
                table: "Payment_Methods",
                column: "Card_TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Categories_CategoryId",
                table: "Product_Categories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Categories_ProductId",
                table: "Product_Categories",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Discounts_DiscountId",
                table: "Product_Discounts",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Discounts_ProductId",
                table: "Product_Discounts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Images_ProductId",
                table: "Product_Images",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSpecifications_ProductId",
                table: "ProductSpecifications",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSpecifications_SpecificationKeyId",
                table: "ProductSpecifications",
                column: "SpecificationKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProductId",
                table: "Reviews",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Reviews_ReviewID",
                table: "User_Reviews",
                column: "ReviewID");

            migrationBuilder.CreateIndex(
                name: "IX_User_Reviews_UserId",
                table: "User_Reviews",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CategorySpecificationKeys");

            migrationBuilder.DropTable(
                name: "Order_Items");

            migrationBuilder.DropTable(
                name: "Product_Categories");

            migrationBuilder.DropTable(
                name: "Product_Discounts");

            migrationBuilder.DropTable(
                name: "Product_Images");

            migrationBuilder.DropTable(
                name: "ProductSpecifications");

            migrationBuilder.DropTable(
                name: "User_Reviews");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "SpecificationKeys");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Order_States");

            migrationBuilder.DropTable(
                name: "Shipping_Methods");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Payment_Methods");

            migrationBuilder.DropTable(
                name: "card_Types");
        }
    }
}
