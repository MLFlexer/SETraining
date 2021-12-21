using SETraining.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SETraining.Server.Contexts;

public class SETrainingContext : DbContext, ISETrainingContext
{
    public DbSet<Article> Articles => Set<Article>();
    public DbSet<ProgrammingLanguage> ProgrammingLanguages => Set<ProgrammingLanguage>();

    public SETrainingContext(DbContextOptions<SETrainingContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>()
            .Property(a => a.Difficulty)
            .HasConversion(new EnumToStringConverter<DifficultyLevel>());
        
        modelBuilder.Entity<Article>()
            .Property(a => a.Type)
            .HasConversion(new EnumToStringConverter<ArticleType>());
    }
}
