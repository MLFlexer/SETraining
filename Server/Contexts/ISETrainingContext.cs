using SETraining.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace SETraining.Server.Contexts;

public interface ISETrainingContext : IDisposable
{
    DbSet<Article> Articles { get; }
    DbSet<ProgrammingLanguage> ProgrammingLanguages { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}