using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieStore.Data.Domain;

namespace MovieStore.Data.Configuration;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
        builder.Property(x => x.IdentityNumber).IsRequired().HasMaxLength(11);

        builder.HasMany(x => x.Orders)
            .WithOne(x => x.Customer)
            .HasForeignKey(x => x.CustomerId);
        
        // Many-to-Many relationship with Genre
        builder.HasMany(c => c.FavoriteGenres)
            .WithMany(g => g.Customers)
            .UsingEntity<Dictionary<string, object>>(
                "CustomerFavoriteGenre",
                j => j
                    .HasOne<Genre>()
                    .WithMany()
                    .HasForeignKey("GenreId"),
                j => j
                    .HasOne<Customer>()
                    .WithMany()
                    .HasForeignKey("CustomerId"));
    }
}