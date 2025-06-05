
namespace Application.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Product Mappings
            CreateMap<Product, ProductDTO>()
                        .ForMember(dest => dest.ImageURLs, opt =>
                            opt.MapFrom(src => src.Images!.Select(img => img.image_url).ToList()))
                        .ReverseMap(); CreateMap<CreateProductDTO, Product>().ReverseMap();
            //CreateMap<UpdateProductDTO, Product>()
            //            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        
            CreateMap<ProductDTO, UpdateProductDTO>().ReverseMap();
            CreateMap<Product, UpdateProductDTO>()
                .ForMember(dest => dest.Is_Active, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Product_Category != null? (src.Product_Category.FirstOrDefault()!.CategoryId) :0))
                .ForMember(dest => dest.Discounts, opt => opt.MapFrom(src =>  src.Discount.Select(d => d.DiscountId) ))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.description))
                .ForMember(dest => dest.Description_EN, opt => opt.MapFrom(src => src.description_EN));



            // Category Mappings
            CreateMap<CreateCategoryDTO, Category>().ReverseMap();
            CreateMap<UpdateCategoryDTO, Category>().ReverseMap();
            CreateMap<CategoryDTO, Category>().ReverseMap();

            // Order Mappings
            CreateMap<OrderDTO, Order>().ReverseMap();
            CreateMap<CreateOrderDTO, Order>().ReverseMap();
            CreateMap<UpdateOrderDTO, Order>().ReverseMap();

            // OrderState Mappings
            CreateMap<Order_State, Order_StateDTO>().ReverseMap();
            CreateMap<UpdateOrder_StateDTO, Order_State>().ReverseMap();
            CreateMap<UpdateOrder_StateDTO, Order_StateDTO>().ReverseMap();
            CreateMap<CreateOrder_StateDTO, Order_StateDTO>().ReverseMap();
            CreateMap<CreateOrder_StateDTO, Order_State>().ReverseMap();
            CreateMap<UpdateOrder_StateDTO, CreateOrder_StateDTO>().ReverseMap();




            // Discount Mappings
            CreateMap<CreateDiscountDTO, Discount>();
            CreateMap<Discount, DiscountDTO>();

            CreateMap<DiscountDTO, UpdateDiscountDTO>().ReverseMap();

            CreateMap<UpdateDiscountDTO, Discount>();
            CreateMap<Discount, UpdateDiscountDTO>();

            // Reviews Mappings
            CreateMap<ReviewsDTO, Reviews>().ReverseMap();
            CreateMap<CreateReviewsDTO, Reviews>().ReverseMap();
            CreateMap<UpdateReviewsDTO, Reviews>().ReverseMap();
            CreateMap<ProductReviewDTO, Reviews>().ReverseMap();


            // User Mappings
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<CreateUserDTO, User>().ReverseMap();
            CreateMap<UpdateUserDTO, User>().ReverseMap();

            // Payment Methods Mappings
            CreateMap<PaymentMethodsDTO, Payment_Methods>().ReverseMap();
            CreateMap<CreatePaymentMethodsDTO, Payment_Methods>().ReverseMap();
            CreateMap<UpdatePaymentMethodsDTO, Payment_Methods>().ReverseMap();
            CreateMap<PaymentMethodsDTO, UpdatePaymentMethodsDTO>().ReverseMap();


            // Order Item Mappings
            CreateMap<OrderItemDTO, Order_Items>().ReverseMap();
            CreateMap<CreateOrderItemDTO, Order_Items>().ReverseMap();
            CreateMap<UpdateOrderItemDTO, Order_Items>().ReverseMap();

            // SpecificationKeyDTO Mappings
            CreateMap<SpecificationKey, SpecificationKeyDTO>() .ReverseMap();


            CreateMap<CreateSpecificationKeyDTO, SpecificationKey>().ReverseMap();


            CreateMap<SpecificationKey, UpdateSpecificationKeyDTO>().ReverseMap();
 

            //Card Type
            CreateMap<Card_Type, Card_TypeDTO>().ReverseMap();
            CreateMap<UpdateCard_TypeDTO, Card_Type>().ReverseMap();
            CreateMap<CreateCard_TypeDTO, Card_Type>().ReverseMap();



            //user
            CreateMap<CreateUserDTO, User>();
            CreateMap<CreateUserDTO, UserDTO>().ReverseMap(); 
            CreateMap<User, UserDTO>();



            //Shippings Methods 
            CreateMap<ShippingMethodsDTO, Shipping_Methods>().ReverseMap();
            CreateMap<CreateShippingMethodsDTO, ShippingMethodsDTO > ().ReverseMap();

            CreateMap<UpdateShippingMethodsDTO, Shipping_Methods>().ReverseMap();
            CreateMap<UpdateShippingMethodsDTO, ShippingMethodsDTO>().ReverseMap();
            CreateMap<UpdateShippingMethodsDTO, CreateShippingMethodsDTO>().ReverseMap();

            CreateMap<CreateShippingMethodsDTO, Shipping_Methods>().ReverseMap();

        }

    }
}
