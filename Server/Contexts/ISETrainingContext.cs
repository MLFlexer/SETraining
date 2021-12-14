using System;
using System.Threading;
using System.Threading.Tasks;
using SETraining.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace SETraining.Server.Contexts;

public interface ISETrainingContext :IDisposable
{
    DbSet<Article> Articles { get; }
    DbSet<ProgrammingLanguage> ProgrammingLanguages { get; }
    DbSet<Learner> Learners { get; }
    DbSet<Moderator> Moderators { get; }
    DbSet<ArticleRating> ArticleRatings { get; }
    DbSet<ArticleHistoryEntry> ArticleHistoryEntries { get; }
    
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
