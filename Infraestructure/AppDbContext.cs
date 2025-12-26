using Microsoft.EntityFrameworkCore;
using TechnicalTestApi.Domain.Entities;

namespace TechnicalTestApi.Infraestructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<Currency> Currencies => Set<Currency>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
        
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(a => a.Id);

            entity.Property(a => a.Street).IsRequired();
            entity.Property(a => a.City).IsRequired();
            entity.Property(a => a.Country).IsRequired();

            entity.HasOne(a => a.User)
                .WithMany(u => u.Addresses)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configuración de Currency (Índice único para Code)
        modelBuilder.Entity<Currency>()
            .HasIndex(c => c.Code)
            .IsUnique();
            
        modelBuilder.Entity<Currency>()
            .Property(c => c.RateToBase)
            .HasPrecision(18, 6); // Precisión para decimales
    }
}