using ecom.spa.Extensions;
using ecom.spa.Models.DomainModels;

namespace ecom.spa.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient client;
        public ProductService(HttpClient client)
        {
            this.client = client;
        }
        public async Task<IEnumerable<Product>> GetAll()
        {
            var response = await client.GetAsync("product");
            var products = response.ReadContentAs<List<Product>>();
            Console.WriteLine("Total Product---------------->>>>>>>>" + products.Result.Count);
            return await response.ReadContentAs<List<Product>>();
        }

        public async Task<Product> GetProduct(Guid id)
        {
            var response = await client.GetAsync($"product/{id}");
            return await response.ReadContentAs<Product>();
        }
    }
}
