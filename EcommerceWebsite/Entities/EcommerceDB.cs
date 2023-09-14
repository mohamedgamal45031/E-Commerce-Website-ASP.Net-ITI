using EcommerceWebsite.Models;
using Microsoft.EntityFrameworkCore;


namespace EcommerceWebsite.Entities
{
    public class EcommerceDB:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = .; Database = EcommerceDB; Trusted_Connection = True; TrustServerCertificate=True;");

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<User> Users { get; set;}
        public virtual DbSet<Seller> Sellers{ get; set;}
        public virtual DbSet<Product> Products{ get; set;}
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<IbnEl4e5> IbnEl4e5s { get; set; }
    }
}
