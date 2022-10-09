using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeCourse.Services.Order.Domain.OrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace FreeCourse.Services.Order.Instrastructure
{
    public class OrderDbContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "ordering";

        public OrderDbContext(DbContextOptions<OrderDbContext> options):base(options)
        {
            
        }

        public DbSet<Domain.OrderAggregate.Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        //address bizim için owned olaracğı için db de karşılığı olmayacak

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.OrderAggregate.Order>().ToTable("Orders", DEFAULT_SCHEMA);

            modelBuilder.Entity<OrderItem>().ToTable("Orders", DEFAULT_SCHEMA);

            modelBuilder.Entity<OrderItem>().Property(i => i.Price).HasColumnType("decimal(18,2)");

            //tüm işlemleri order üzerinden yapıcaz address eklersem order, orderitem larıda order üzerinden kontrol edicem
            modelBuilder.Entity<Domain.OrderAggregate.Order>().OwnsOne(i => i.Address).WithOwner();

            base.OnModelCreating(modelBuilder);
        }
    }
}
