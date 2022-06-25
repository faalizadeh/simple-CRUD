using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static OnlineShopping.Models.CarShopping;

namespace OnlineShopping.Models
{
    public class CarShoppingDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=AutoShopping;Trusted_Connection=True;");

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        //public DbSet<Model> Models { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasKey(p => p.ID);
            modelBuilder.Entity<Customer>().Property(p => p.ID)
                .ValueGeneratedOnAdd()
                .IsRequired(true);

            //modelBuilder.Entity<Model>().HasKey(p => p.ID);
            //modelBuilder.Entity<Model>().Property(p => p.ID)
            //    .ValueGeneratedOnAdd()
            //    .IsRequired(true);
            //modelBuilder.Entity<Model>().HasOne(p => p.Brand).WithMany(p => p.Models).HasForeignKey(p => p.BrandID);


            modelBuilder.Entity<Brand>().HasKey(p => p.ID);
            modelBuilder.Entity<Brand>().Property(p => p.ID)
                .ValueGeneratedOnAdd()
                .IsRequired(true);


          


            modelBuilder.Entity<Category>().HasKey(p => p.ID);
            modelBuilder.Entity<Category>().Property(p => p.ID)
                .ValueGeneratedOnAdd()
                .IsRequired(true);


            modelBuilder.Entity<Car>().HasKey(p => p.ID);
            modelBuilder.Entity<Car>().Property(p => p.ID)
                .ValueGeneratedOnAdd()
                .IsRequired(true);

            //modelBuilder.Entity<Car>().HasOne(p => p.Color).WithMany(p => p.Cars).HasForeignKey(p => p.ColorID);
            modelBuilder.Entity<Car>().HasOne(p => p.Category).WithMany(p => p.Cars).HasForeignKey(p => p.CategoryID);
            modelBuilder.Entity<Car>().HasOne(p => p.Brand).WithMany(p => p.Cars).HasForeignKey(p => p.BrandID);



            modelBuilder.Entity<Color>().HasKey(p => p.ID);
            modelBuilder.Entity<Color>().Property(p => p.ID)
                .ValueGeneratedOnAdd()
                .IsRequired(true);


            modelBuilder.Entity<Order>().HasKey(p => p.ID);
            modelBuilder.Entity<Order>().Property(p => p.ID)
                .ValueGeneratedOnAdd()
                .IsRequired(true);
            modelBuilder.Entity<Order>().HasOne(p => p.Customer).WithMany(p => p.orders).HasForeignKey(p => p.CustomerID);

            modelBuilder.Entity<OrderDetails>().HasKey(p => p.ID);
            modelBuilder.Entity<OrderDetails>().Property(p => p.ID)
                .ValueGeneratedOnAdd()
                .IsRequired(true);
            modelBuilder.Entity<OrderDetails>().HasOne(p => p.Order).WithMany(p => p.OrderDetails).HasForeignKey(p => p.OrderID);
            




        }
    }
}
