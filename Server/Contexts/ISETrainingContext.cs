using SETraining.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace SETraining.Server.Contexts;

public interface ISETrainingContext :IDisposable
{
    DbSet<Article> Articles { get; }
    DbSet<ProgrammingLanguage> ProgrammingLanguages { get; }
    
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
