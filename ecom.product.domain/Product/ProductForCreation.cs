using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecom.product.domain.Product
{
    public class ProductForCreation
    {
        public string Name { get; set; } = String.Empty;
        public int Price { get; set; }
        public string Seller { get; set; } = String.Empty;
        public string? AvailableSince { get; set; }
        public string Description { get; set; } = String.Empty;
        public string ImageUrl { get; set; } = String.Empty;
    }
}
