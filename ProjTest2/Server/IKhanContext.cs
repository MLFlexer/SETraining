using ProjTest2.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjTest2.Server;

public interface IKhanContext :IDisposable
{
    DbSet<Content> Content { get; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
