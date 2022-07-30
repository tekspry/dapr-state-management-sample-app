using ecom.product.application.ProductApp;
using ecom.product.domain.Product;
using Microsoft.AspNetCore.Mvc;

namespace ecom.ProductService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductApplication _productApplication;

        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductApplication productApplication, ILogger<ProductController> logger)
        {
            _productApplication = productApplication;
            _logger = logger;
        }

        [HttpGet(Name = "GetProducts")]
        public async Task<IEnumerable<Product>> GetAll() => await _productApplication.ListAsync();
        

        [HttpGet("{id}", Name = "GetById")]
        public async Task<Product> GetById(string id) => await _productApplication.GetAsync(id);
        

        [HttpPost (Name = "product")]
        public async Task<string> Add(Product product) => await _productApplication.AddAsync(product);

    }
}