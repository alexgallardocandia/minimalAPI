using Microsoft.EntityFrameworkCore;
using TechnicalTestApi.Models;

namespace TechnicalTestApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<Currency> Currencies => Set<Currency>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // User.Email único
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // Currency.Code único
        modelBuilder.Entity<Currency>()
            .HasIndex(c => c.Code)
            .IsUnique();

        // Default IsActive = true
        modelBuilder.Entity<User>()
            .Property(u => u.IsActive)
            .HasDefaultValue(true);
    }
}
