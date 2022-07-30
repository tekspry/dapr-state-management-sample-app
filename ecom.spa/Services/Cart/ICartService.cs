using ecom.spa.Models.DomainModels;

namespace ecom.spa.Services
{
    public interface ICartService
    {
        Task<CartItem> AddToCart(Guid cartId, CartItemForCreation cartItem);
        Task<IEnumerable<CartItem>> GetItemsForCart(Guid cartId);
        Task<Cart> GetCart(Guid cartId);
        Task UpdateItem(Guid cartId, CartItemForUpdate cartItemForUpdate);
        Task RemoveItem(Guid cartId, Guid itemId);
        Task ClearCart(Guid currentcartId);
    }
}
