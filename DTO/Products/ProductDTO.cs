namespace DTO.Products
{
    public record ProductDTO
    {
        public ProductDTO()
        {

        }
        public ProductDTO(UpdateProductDTO dTO)
        {
            Id = dTO.Id;
            Name = dTO.Name??string.Empty;
            Description = dTO.Description;
            Name_EN = dTO.Name_EN ?? string.Empty;
            Price = dTO.Price ?? 0;
            Unit_Instock = dTO.Unit_Instock ?? 0;
            image_url = dTO.Image ?? string.Empty;
            Description_EN = dTO.Description_EN;
            Code = dTO.Code;
            priceAfterDiscount = dTO.priceAfterDiscount;
         }
        public ProductDTO(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Name_EN = product.Name_EN;
            Description = product.description;
            Description_EN = product.description_EN;
            Price = product.price;
            Unit_Instock=product.Unit_Instock;
            image_url = product.image_url;
            ImageURLs = product.Images?.Select(image => image.image_url).ToList() ?? new List<string>();
            Categories = product.Product_Category?.Select(pc => pc.CategoryId).ToList() ?? new List<int>();
            Specifications = product.ProductSpecifications?.Select(spec => new ProductSpecificationDTO
            {
                SpecificationKeyId = spec.SpecificationKeyId,
                Value = spec.Value,
                Name=spec.SpecificationKey.Key,
                Name_AR = spec.SpecificationKey.Key_Ar
            }).ToList();
            if(product.Discount!=null)
            Discounts = product.Discount.Select(p=> p.discount!=null? p.discount.Percentage.ToString():string.Empty).ToList();
            Code = product.Code;
            priceAfterDiscount= product.priceAfterDiscount;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Name_EN { get; set; }
        public string? Description { get; set; }
        public string? Description_EN { get; set; }
        public decimal Price { get; set; }
        public int Unit_Instock { get; set; }
        public string image_url { get; set; }
        public int? Code { get; set; }
        public decimal? priceAfterDiscount { get; set; }

        public ICollection<string> ImageURLs { get; set; }
        public ICollection<int> Categories { get; set; }
        public ICollection<string> Discounts { get; set; }
        public ICollection<ProductSpecificationDTO>? Specifications { get; set; }
    }
}
