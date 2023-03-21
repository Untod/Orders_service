using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Orders_service.Models;

namespace Orders_service.Data
{
    public class Orders_serviceContext : DbContext
    {
        public DbSet<Orders_service.Models.Order> Order { get; set; } = default!;

        public DbSet<Orders_service.Models.Provider> Provider { get; set; } = default!;

        public DbSet<Orders_service.Models.OrderItem> OrderItem { get; set; } = default!;

        public Orders_serviceContext(DbContextOptions<Orders_serviceContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>().HasOne(o => o.Provider).WithMany(p => p.Orders).HasForeignKey(o => o.ProviderId);
            modelBuilder.Entity<Order>().HasIndex(o => new { o.ProviderId, o.Number }).IsUnique();

            modelBuilder.Entity<OrderItem>().HasOne(i => i.Order).WithMany(o => o.Items).HasForeignKey(i => i.OrderId);
        }
    }
}
