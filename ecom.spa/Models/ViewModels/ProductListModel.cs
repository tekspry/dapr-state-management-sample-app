using ecom.spa.Models.DomainModels;

namespace ecom.spa.Models.ViewModels
{
    public class ProductListModel
    {
        public IEnumerable<Product> Products { get; set; } = new List<Product>();
        public int NumberOfItems { get; set; }
    }
}
