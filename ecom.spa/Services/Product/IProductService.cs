using ecom.spa.Models.DomainModels;

namespace ecom.spa.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();

        Task<Product> GetProduct(Guid id);
    }
}
