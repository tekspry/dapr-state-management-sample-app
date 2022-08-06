using Dapr.Client;
using ecom.product.application.ProductApp;
using ecom.product.database.ProductDB;
using ecom.product.domain.Product;
using Microsoft.Extensions.Logging;

namespace ecom.product.application.ProductApp
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _productRepository;
        string? daprPort = Environment.GetEnvironmentVariable("DAPR_HTTP_PORT");

        private readonly DaprClient daprClient;
        private readonly ILogger<ProductApplication> logger;
        private const string cacheStoreName = "shoppingcache";
        public ProductApplication(IProductRepository productRepository, DaprClient daprClient, ILogger<ProductApplication> logger)
        {
            _productRepository = productRepository;
            this.daprClient = daprClient;
            this.logger = logger;
        }

        public async Task<domain.Product.Product> GetAsync(string id)
        {
            return await _productRepository.GetProductById(id);
        }

        public async Task<IEnumerable<domain.Product.Product>> ListAsync()
        {           
            return await _productRepository.GetProducts();
        }

        public async Task<string> AddAsync(Product product)
        {   
            return await _productRepository.CreateProduct(product);
        }

        private async Task SaveProductToCacheStore(Product product)
        {
            var key = $"productlist";
            var productList = await daprClient.GetStateAsync<List<Product>>(cacheStoreName, "productlist");
            productList.Add(product);            
            await daprClient.SaveStateAsync(cacheStoreName, key, productList);
            logger.LogInformation($"Created new product in cache store {key}");
        }


    }
}
