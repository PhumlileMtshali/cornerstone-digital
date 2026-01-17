using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CornerstoneDigital.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Service> Services { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed initial services
            modelBuilder.Entity<Service>().HasData(
                new Service
                {
                    Id = 1,
                    Name = "Website Development",
                    Description = "Custom web solutions built with modern technologies. From e-commerce platforms to corporate websites, we deliver scalable and responsive applications.",
                    Price = 15000.00M,
                    DeliveryTime = "2-4 weeks",
                    IsActive = true
                },
                new Service
                {
                    Id = 2,
                    Name = "UX/UI Design",
                    Description = "Beautiful, intuitive interfaces that enhance user experience and drive engagement. User-centered design that converts visitors into customers.",
                    Price = 8000.00M,
                    DeliveryTime = "1-2 weeks",
                    IsActive = true
                },
                new Service
                {
                    Id = 3,
                    Name = "E-Commerce Solutions",
                    Description = "Complete online store setup with payment integration, inventory management, and secure checkout. Build your digital storefront with confidence.",
                    Price = 20000.00M,
                    DeliveryTime = "3-5 weeks",
                    IsActive = true
                },
                new Service
                {
                    Id = 4,
                    Name = "Mobile App Development",
                    Description = "Native and cross-platform mobile applications for iOS and Android. Deliver seamless experiences on any device your customers use.",
                    Price = 25000.00M,
                    DeliveryTime = "4-6 weeks",
                    IsActive = true
                },
                new Service
                {
                    Id = 5,
                    Name = "Digital Marketing",
                    Description = "SEO optimization, social media strategy, and content marketing to boost your online presence and drive traffic to your digital platforms.",
                    Price = 6000.00M,
                    DeliveryTime = "Ongoing",
                    IsActive = true
                },
                new Service
                {
                    Id = 6,
                    Name = "Brand Identity Design",
                    Description = "Complete brand identity packages including logo design, color palette, typography, and brand guidelines to establish your unique presence.",
                    Price = 10000.00M,
                    DeliveryTime = "2-3 weeks",
                    IsActive = true
                }
            );
        }
    }
}