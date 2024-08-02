using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieStore.Data.Domain;

namespace MovieStore.Data.Configuration;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Year).IsRequired();
        builder.Property(x => x.Genre).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Price).IsRequired().HasPrecision(18, 2);
        
        builder.HasMany(m => m.Actors)
            .WithMany(a => a.Movies)
            .UsingEntity(j => j.ToTable("MovieActors"));

        builder.HasMany(m => m.Directors)
            .WithMany(d => d.Movies)
            .UsingEntity(j => j.ToTable("MovieDirectors"));

        builder.HasMany(x => x.Orders)
            .WithOne(x => x.Movie)
            .HasForeignKey(x => x.MovieId);
    }
}