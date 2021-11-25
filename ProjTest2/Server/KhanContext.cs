using ProjTest2.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ProjTest2.Server;

public class KhanContext : DbContext, IKhanContext
{
    public DbSet<Content> Content => Set<Content>();

    public KhanContext(DbContextOptions<KhanContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {   
        modelBuilder.Entity<Content>()
            .HasDiscriminator(c => c.Type)
            .HasValue<Article>("Article")
            .HasValue<Video>("Video");

        modelBuilder.Entity<Content>()
            .Property(c => c.Difficulty)
            .HasMaxLength(50)
            .HasConversion(new EnumToStringConverter<DifficultyLevel>());
    }
}
