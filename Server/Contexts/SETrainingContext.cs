using SETraining.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SETraining.Server.Contexts;

public class SETrainingContext : DbContext, ISETrainingContext
{
    public DbSet<Video> Videos => Set<Video>();
    public DbSet<Article> Articles => Set<Article>();
    public DbSet<Learner> Learners => Set<Learner>();
    public DbSet<Moderator> Moderators => Set<Moderator>();
    public DbSet<Rating> Ratings => Set<Rating>();
    public DbSet<ProgrammingLanguage> ProgrammingLanguages => Set<ProgrammingLanguage>();
    public DbSet<ArticleHistoryEntry> ArticleHistoryEntries => Set<ArticleHistoryEntry>();
    public DbSet<VideoHistoryEntry> VideoHistoryEntries => Set<VideoHistoryEntry>();
    public DbSet<Image> Images => Set<Image>();

    public SETrainingContext(DbContextOptions<SETrainingContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {   
        // modelBuilder.Entity<Article>()
        //     .HasDiscriminator(c => c.Type)
        //     .HasValue<Article>("Article")
        //     .HasValue<Video>("Video");
            
        modelBuilder.Entity<Article>()
            .Property(c => c.Difficulty)
            .HasConversion(new EnumToStringConverter<DifficultyLevel>());
    }
}

