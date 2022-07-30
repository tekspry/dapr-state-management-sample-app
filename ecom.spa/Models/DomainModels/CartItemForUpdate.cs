using System.ComponentModel.DataAnnotations;

namespace ecom.spa.Models.DomainModels
{
    public class CartItemForUpdate
    {
        [Required]
        public Guid ItemId { get; set; }
        [Required]
        public int ProductCount { get; set; }
    }
}
