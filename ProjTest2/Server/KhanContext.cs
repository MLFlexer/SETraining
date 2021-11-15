using ProjTest2.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjTest2.Server;

public class KhanContext : DbContext, IKhanContext
{
    public DbSet<Content> Content => Set<Content>();

    public KhanContext(DbContextOptions<KhanContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Content>()
            .HasDiscriminator<string>("Type")
            .HasValue<Article>("Article")
            .HasValue<Video>("Video");
    }
}
