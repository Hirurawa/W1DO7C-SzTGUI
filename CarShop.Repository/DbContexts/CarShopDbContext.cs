using CarShop.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Repository.DbContexts
{
    public class CarShopDbContext : DbContext
    {
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Car> Cars { get; set; }

        public CarShopDbContext()
        {
          try
          {
            Database.EnsureCreated();
          }
          catch (Exception)
          {
          }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\CarShopDb.mdf;Integrated Security=true;MultipleActiveResultSets=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set relation
            modelBuilder.Entity<Car>(e =>
                e.HasOne(c => c.Brand)
                .WithMany(b => b.Cars)
                .HasForeignKey(c => c.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull));

            // Seed
            var mazda = new Brand() { Id = 1, Name = "mazda" };
            var opel = new Brand() { Id = 2, Name = "opel" };
            var bmw = new Brand() { Id = 3, Name = "bmw" };

            var car1 = new Car() { Id = 1, Model = "MX8", BrandId = mazda.Id, Price = 2600 };
            var car2 = new Car() { Id = 2, Model = "mx6", BrandId = mazda.Id, Price = 1200 };
            var car3 = new Car() { Id = 3, Model = "Astra G", BrandId = opel.Id, Price = 3800 };
            var car4 = new Car() { Id = 4, Model = "Vectra", BrandId = opel.Id, Price = 1100 };
            var car5 = new Car() { Id = 5, Model = "1", BrandId = bmw.Id, Price = 2200 };
            var car6 = new Car() { Id = 6, Model = "5", BrandId = bmw.Id, Price = 5000 };

            modelBuilder.Entity<Brand>().HasData(mazda, opel, bmw);
            modelBuilder.Entity<Car>().HasData(car1, car2, car3, car4, car5, car6);
        }
    }
}
