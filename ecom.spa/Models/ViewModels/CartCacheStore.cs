using ecom.spa.Models.DomainModels;

namespace ecom.spa.Models.ViewModels
{
    public class CartCacheStore
    {
        public Guid CartId { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public Guid UserId { get; set; }
    }
}
