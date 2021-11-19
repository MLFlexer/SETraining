using ProjTest2.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjTest2.Server;

public class KhanContext : DbContext, IKhanContext
{
    public DbSet<Content> Content => Set<Content>();
    public DbSet<Learner> Learner => Set<Learner>();
    public DbSet<Moderator> Moderator => Set<Moderator>();
    public DbSet<Rating> Rating => Set<Rating>();
    public DbSet<ProgrammingLanguage> ProgrammingLanguage => Set<ProgrammingLanguage>();
    public DbSet<HistoryEntry> HistoryEntry => Set<HistoryEntry>();
    public DbSet<Image> Image => Set<Image>();


    public KhanContext(DbContextOptions<KhanContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Content>().HasDiscriminator(c => c.Type);
        
        modelBuilder.Entity<Content>()
            .HasDiscriminator<string>("Type")
            .HasValue<Article>("Article")
            .HasValue<Video>("Video");
    }
}
