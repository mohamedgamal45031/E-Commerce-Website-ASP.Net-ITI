using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceWebsite.Models
{
    public class Product
    {
        public int Id { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Display(Name = "Product Name")]
        public string Name { get; set; }
        [StringLength(500, MinimumLength = 3)]
        public string Description { get; set; }
        [Range(5, 100000000)]
        public decimal Price { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set;}   
        [NotMapped]
        public IFormFile? ImageFile2 { get; set;}
        public string? Image { get; set; }
        public string? Image2 { get; set; }
        public int SellerId { get; set; }
        public virtual Seller? Seller { get; set; }
        public virtual ICollection<Order>? Orders { get; } = new HashSet<Order>();


    }
}
