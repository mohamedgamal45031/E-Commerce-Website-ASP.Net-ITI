namespace EcommerceWebsite.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string password { get; set; }
        public virtual ICollection<Product>? Products { get; } = new HashSet<Product>();
    }
}
