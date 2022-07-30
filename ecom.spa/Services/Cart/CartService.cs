using ecom.spa.Models;
using ecom.spa.Models.DomainModels;
using ecom.spa.Models.ViewModels;
using System.Text.Json;

namespace ecom.spa.Services
{
    public class CartService : ICartService
    {
        private readonly HttpClient httpClient;
        private readonly IProductService productService;
        private readonly Configurations configurations;
        private readonly ILogger<CartService> logger;

        public CartService(HttpClient httpClient,
        IProductService productService,
        Configurations configurations,
        ILogger<CartService> logger)
        {
            this.httpClient = httpClient;
            this.productService = productService;
            this.configurations = configurations;
            this.logger = logger;
        }
        public async Task<CartItem> AddToCart(Guid cartId, CartItemForCreation cartItemForCreation)
        {

            logger.LogInformation($"ADD TO BASKET {cartId}");
            var cart = await GetCartFromCache(cartId);
            var @product = await GetProductFromCache(cartItemForCreation.ProductId);

            var cartItem = new CartItem()
            {
                ProductId = cartItemForCreation.ProductId,
                ProductCount = cartItemForCreation.ProductCount,
                Product = @product,
                CartId = cart.CartId,
                CartItemId = Guid.NewGuid(),
                Price = cartItemForCreation.Price
            };
            cart.Items.Add(cartItem);
            logger.LogInformation($"SAVING CART {cart.CartId}");
            await SaveCartToCacheStore(cart);
            return cartItem;
        }

        

        public async Task ClearCart(Guid currentcartId)
        {
            var cart = await GetCartFromCache(currentcartId);
            if (cart != null)
            {
                cart.Items.Clear();
                await SaveCartToCacheStore(cart);
            }
        }

        public async Task<Cart> GetCart(Guid cartId)
        {
            logger.LogInformation($"GET BASKET {cartId}");
            var cart = await GetCartFromCache(cartId);

            return new Cart()
            {
                CartId = cartId,
                NumberOfItems = cart.Items.Count,
                UserId = cart.UserId
            };
        }

        public async Task<IEnumerable<CartItem>> GetItemsForCart(Guid cartId)
        {
            var cart = await GetCartFromCache(cartId);
            return cart.Items;
        }

        public async Task RemoveItem(Guid cartId, Guid itemId)
        {
            var cart = await GetCartFromCache(cartId);
            var index = cart.Items.FindIndex(bl => bl.CartItemId== itemId);
            if (index >= 0) cart.Items.RemoveAt(index);
            await SaveCartToCacheStore(cart);
        }

        public async Task UpdateItem(Guid cartId, CartItemForUpdate cartItemForUpdate)
        {
            var cart = await GetCartFromCache(cartId);
            var index = cart.Items.FindIndex(bl => bl.CartItemId == cartItemForUpdate.ItemId);
            cart.Items[index].ProductCount = cartItemForUpdate.ProductCount;
            await SaveCartToCacheStore(cart);
        }

        private async Task<CartCacheStore> GetCartFromCache(Guid cartId)
        {
            var key = $"cart-{cartId}";
            var cart = await GetFromCache<CartCacheStore>(key);
            if (cart == null)
            {
                if (cartId == Guid.Empty) cartId = Guid.NewGuid();
                logger.LogInformation($"CREATING NEW BASKET {cartId}");
                cart = new CartCacheStore();
                cart.CartId = cartId;
                cart.UserId = configurations.UserId;
                cart.Items = new List<CartItem>();
                await SaveCartToCacheStore(cart);
            }
            return cart;
        }

        private async Task<Product> GetProductFromCache(Guid productId)
        {
            var key = $"prduct-{productId}";
            var @product = await GetFromCache<Product>(key);

            if (@product != null)
            {
                logger.LogInformation("Using cached event");
            }
            else
            {
                @product = await productService.GetProduct(productId);
                await SaveProductToCacheStore(@product);
            }
            return @product;
        }



        private async Task<T?> GetFromCache<T>(string key)
        {
            var response = await httpClient.GetAsync(key);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(json))
            {
                var b = JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return b;
            }
            return default(T);
        }

        private async Task SaveCartToCacheStore(CartCacheStore cart)
        {
            var key = $"cart-{cart.CartId}";
            await SaveToCacheStore(key, cart);
            logger.LogInformation($"Created new cart in cache store {key}");
        }

        private async Task SaveProductToCacheStore(Product @product)
        {
            var key = $"product-{product.ProductId}";
            logger.LogInformation($"Saving product to cache store {key}");
            await SaveToCacheStore(key, @product);
        }

        private async Task SaveToCacheStore(string key, object value)
        {
            var resp = await httpClient.PostAsJsonAsync("", new[] { new { key, value = value } });
            if (!resp.IsSuccessStatusCode)
            {
                var body = await resp.Content.ReadAsStringAsync();
                logger.LogError($"Failed to save state {key}: {body}");
            }
            resp.EnsureSuccessStatusCode();
        }
    }
}
