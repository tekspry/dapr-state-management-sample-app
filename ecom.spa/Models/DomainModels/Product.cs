namespace ecom.spa.Models.DomainModels
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = String.Empty;
        public int Price { get; set; }
        public string Seller { get; set; } = String.Empty;
        public DateTime Date { get; set; }
        public string Description { get; set; } = String.Empty;
        public string ImageUrl { get; set; } = String.Empty;
    }
}
