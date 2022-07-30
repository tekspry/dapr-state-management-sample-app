using System.ComponentModel.DataAnnotations;

namespace ecom.spa.Models.DomainModels
{
    public class CartItemForCreation
    {
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public int ProductCount { get; set; }
        [Required]
        public int Price { get; set; }
    }
}
