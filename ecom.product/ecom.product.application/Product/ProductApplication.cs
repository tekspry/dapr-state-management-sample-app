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
            //var products = new List<domain.Product.Product>();
            //try
            //{
            //    var secrets = await daprClient.GetBulkSecretAsync(cacheStoreName);
                
            //    foreach (var secret in secrets)
            //    {
            //        products.Add(await daprClient.GetStateAsync<Product>(cacheStoreName, secret.Key));
            //    }
            //}
            //catch (Exception ex)
            //{
            //    logger.LogInformation(ex.Message);
            //}

            return await _productRepository.GetProducts();
            //return products;// _productRepository.GetProducts();
        }

        public async Task<string> AddAsync(Product product)
        {
            Console.WriteLine("USING DAPR");
            // Using the DAPR SDK to create a DaprClient, in stead of fiddling with URI's our selves
            product.ProductId = Guid.NewGuid().ToString();
            await this.SaveProductToCacheStore(product);

            //if (String.IsNullOrEmpty(daprPort))
            //{
            //    // we're not running in DAPR - use regular service invocation and an in-memory basket
            //    Console.WriteLine("NOT USING DAPR");
            //    product.ProductId = await _productRepository.CreateProduct(product);
            //}
            //else
            //{
            //    Console.WriteLine("USING DAPR");
            //    // Using the DAPR SDK to create a DaprClient, in stead of fiddling with URI's our selves
            //    product.ProductId = Guid.NewGuid().ToString();
            //    await this.SaveProductToCacheStore(product);


            //}
            return product.ProductId;
        }

        private async Task SaveProductToCacheStore(Product product)
        {
            var key = $"productlist";
            var productList = await daprClient.GetStateAsync<List<Product>>(cacheStoreName, "productlist");
            productList.Add(product);
            //await SaveToCacheStore(key, cart);
            await daprClient.SaveStateAsync(cacheStoreName, key, productList);
            logger.LogInformation($"Created new product in cache store {key}");
        }


    }
}
