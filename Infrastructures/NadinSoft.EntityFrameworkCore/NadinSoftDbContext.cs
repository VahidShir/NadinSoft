using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using NadinSoft.Domain;

namespace NadinSoft.EntityFrameworkCore;

public class NadinSoftDbContext : IdentityDbContext
{
    public NadinSoftDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
                .Property(b => b.Id)
                .ValueGeneratedOnAdd();

        modelBuilder.Entity<Product>()
                .Property(b => b.Name)
                .HasMaxLength(150);

        modelBuilder.Entity<Product>()
                .Property(b => b.ManufactureEmail)
                .HasMaxLength(100);

        modelBuilder.Entity<Product>()
                .Property(b => b.ManufacturePhone)
                .HasMaxLength(15);

        modelBuilder.Entity<Product>()
                .HasIndex(b => b.ManufactureEmail)
                .IsUnique();

        modelBuilder.Entity<Product>()
                .HasIndex(b => b.ProduceDate)
                .IsUnique();

        modelBuilder
                .Entity<Product>()
                .Property(e => e.IsAvailable)
                .HasConversion<int>();
    }

    public DbSet<Product> Products { get; set; }
}