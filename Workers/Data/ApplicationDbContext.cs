using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Workers.Models;

namespace Workers.Data;

public class ApplicationDbContext
    : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Country> Countries => Set<Country>();
    public DbSet<City> Cities => Set<City>();
    public DbSet<ProviderProfile> ProviderProfiles => Set<ProviderProfile>();
    public DbSet<ServiceCategory> ServiceCategories => Set<ServiceCategory>();
    public DbSet<ProviderService> ProviderServices => Set<ProviderService>();
    public DbSet<ServiceRequest> ServiceRequests => Set<ServiceRequest>();
    public DbSet<Review> Reviews => Set<Review>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Country>(entity =>
        {
            entity.HasIndex(x => x.Iso2Code).IsUnique();
        });

        builder.Entity<City>(entity =>
        {
            entity.HasIndex(x => new { x.CountryId, x.NameAr });

            entity.HasOne(x => x.Country)
                .WithMany(x => x.Cities)
                .HasForeignKey(x => x.CountryId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<ApplicationUser>(entity =>
        {
            entity.HasOne(x => x.Country)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.City)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.CityId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<ProviderProfile>(entity =>
        {
            entity.HasKey(x => x.UserId);

            entity.Property(x => x.WorkLocation)
                .HasColumnType("geography");

            entity.HasOne(x => x.User)
                .WithOne(x => x.ProviderProfile)
                .HasForeignKey<ProviderProfile>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(x => new { x.Kind, x.IsAvailable });
        });

        builder.Entity<ServiceCategory>(entity =>
        {
            entity.HasOne(x => x.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.ParentId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<ProviderService>(entity =>
        {
            entity.Property(x => x.BasePrice)
                .HasPrecision(18, 2);

            entity.HasOne(x => x.Provider)
                .WithMany(x => x.Services)
                .HasForeignKey(x => x.ProviderId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(x => x.Category)
                .WithMany(x => x.ProviderServices)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(x => new
            {
                x.ProviderId,
                x.CategoryId,
                x.IsActive
            });
        });

        builder.Entity<ServiceRequest>(entity =>
        {
            entity.Property(x => x.CustomerBudget)
                .HasPrecision(18, 2);

            entity.Property(x => x.AgreedPrice)
                .HasPrecision(18, 2);

            entity.Property(x => x.ServiceLocation)
                .HasColumnType("geography");

            entity.HasOne(x => x.Customer)
                .WithMany(x => x.CustomerRequests)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.Provider)
                .WithMany(x => x.Requests)
                .HasForeignKey(x => x.ProviderId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.ProviderService)
                .WithMany(x => x.Requests)
                .HasForeignKey(x => x.ProviderServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(x => new { x.ProviderId, x.Status });
            entity.HasIndex(x => new { x.CustomerId, x.Status });
        });

        builder.Entity<Review>(entity =>
        {
            entity.HasIndex(x => x.ServiceRequestId)
                .IsUnique();

            entity.HasOne(x => x.ServiceRequest)
                .WithOne(x => x.Review)
                .HasForeignKey<Review>(x => x.ServiceRequestId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.ToTable(table =>
                table.HasCheckConstraint(
                    "CK_Reviews_Rating",
                    "[Rating] >= 1 AND [Rating] <= 5"));
        });
    }
}