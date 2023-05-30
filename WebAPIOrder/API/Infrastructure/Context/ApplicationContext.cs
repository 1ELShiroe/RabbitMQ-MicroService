using Microsoft.EntityFrameworkCore;
using API.Domain.Models;

namespace API.Infrastructure.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<CustomerModel> Customers => Set<CustomerModel>();
        public DbSet<ProductModel> Products => Set<ProductModel>();
        public DbSet<PaymentOrder> Payments => Set<PaymentOrder>();
        public DbSet<OrderModel> Orders => Set<OrderModel>();
        public DbSet<OrderProductModel> OrderProduct => Set<OrderProductModel>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Server=127.0.0.1;Port=5432;User Id=postgres;Password=55152733;Database=api;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerModel>().HasKey(p => p.Id);
            
            modelBuilder.Entity<ProductModel>().HasKey(p => p.Id);

            modelBuilder.Entity<PaymentOrder>().HasKey(p => p.Id);

            modelBuilder.Entity<OrderModel>().HasKey(p => p.Id);
            modelBuilder.Entity<OrderModel>().Property(p => p.Cupom).IsRequired(false);
            modelBuilder.Entity<OrderModel>().Property(p => p.Payment).IsRequired(false);

            // Definir que é um chave composta pelo ProdutoID e pelo PedidoID
            modelBuilder.Entity<OrderProductModel>()
                .HasKey(p => new { p.ProductId, p.OrderId });

            modelBuilder.Entity<OrderProductModel>()
                .HasOne(p => p.Order)
                .WithMany(p => p.OrderProduct)
                .HasForeignKey(p => p.OrderId);

            modelBuilder.Entity<OrderProductModel>()
                .HasOne(p => p.Product)
                .WithMany(p => p.ProductOrder)
                .HasForeignKey(p => p.ProductId);
        }
    }
}