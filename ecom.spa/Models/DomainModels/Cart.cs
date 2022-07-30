namespace ecom.spa.Models.DomainModels
{
    public class Cart
    {
        public Guid CartId { get; set; }
        public Guid UserId { get; set; }
        public int NumberOfItems { get; set; }
    }
}

