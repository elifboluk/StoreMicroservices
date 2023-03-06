namespace FreeProduct.Services.Catalog.DTOs
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Picture { get; set; }
        public string UserId { get; set; }
        public ProductFeatureDto ProductFeature { get; set; }
        public string CategoryId { get; set; }
    }
}
