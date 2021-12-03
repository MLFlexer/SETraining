using System;
using System.Threading;
using System.Threading.Tasks;
using SETraining.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace SETraining.Server.Contexts;

public interface IKhanContext :IDisposable
{
     DbSet<Content> Contents { get; }
     DbSet<Video> Videos { get; }
     DbSet<Article> Articles { get; }
    DbSet<ProgrammingLanguage> ProgrammingLanguages { get; }
    DbSet<Learner> Learners { get; }
    DbSet<Moderator> Moderators { get; }
    DbSet<Rating> Ratings { get; }
    DbSet<HistoryEntry> HistoryEntries { get; }
    DbSet<Image> Images { get; }
    
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
