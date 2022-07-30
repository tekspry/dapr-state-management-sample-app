namespace ecom.spa.Models.ViewModels
{
    public class CartItemModel
    {
        public Guid ItemId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = String.Empty;
        public DateTimeOffset Date { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}
