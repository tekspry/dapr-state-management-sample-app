namespace ecom.spa.Models.DomainModels
{
    public class CartItem
    {
        public Guid CartItemId { get; set; }
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public int ProductCount { get; set; }
        public int Price { get; set; }
        public Product Product { get; set; } = new Product();
    }
}
