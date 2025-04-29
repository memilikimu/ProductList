using Microsoft.EntityFrameworkCore;
using ProductList.Data.Entities;
using System;

namespace ProductList.Data.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Sale> Sales { get; set; }
        //public DbSet<Product> Products { get; set; }
        //public DbSet<Transaction> Transactions { get; set; }
        //public DbSet<TransactionItem> TransactionItems { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Transaction>()
        //        .HasMany(t => t.Items)
        //        .WithOne(i => i.Transaction)
        //        .HasForeignKey(i => i.TransactionId)
        //        .OnDelete(DeleteBehavior.Cascade);

        //    // Relasi TransactionItem -> Product
        //    modelBuilder.Entity<TransactionItem>()
        //        .HasOne(i => i.Product)
        //        .WithMany()
        //        .HasForeignKey(i => i.ProductId)
        //        .OnDelete(DeleteBehavior.Cascade);
        //}
    }
}
