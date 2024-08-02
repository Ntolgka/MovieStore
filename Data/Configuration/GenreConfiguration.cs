using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieStore.Data.Domain;

namespace MovieStore.Data.Configuration;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

        builder.HasMany(g => g.Customers)
            .WithMany(c => c.FavoriteGenres)
            .UsingEntity<Dictionary<string, object>>(
                "CustomerFavoriteGenre",
                j => j
                    .HasOne<Customer>()
                    .WithMany()
                    .HasForeignKey("CustomerId"),
                j => j
                    .HasOne<Genre>()
                    .WithMany()
                    .HasForeignKey("GenreId"));
    }
}