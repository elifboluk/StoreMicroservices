namespace FreeProduct.Services.Basket.DTOs
{
    public class BasketDto
    {
        public string UserId { get; set; }
        public string DiscountCode { get; set; }
        public List<BasketItemDto> basketItems { get; set; }

        
        public decimal TotalPrice // Basket içindeki productları miktarlarıyla çarpıp bize toplam miktarı dönecek.
        {
            get => basketItems.Sum(x => x.Price * x.Quantity);
        }
    }
}
