namespace EcommerceWebsite.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal TotalPrice { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Product>? Products { get; set; } = new List<Product>();
    }
}
