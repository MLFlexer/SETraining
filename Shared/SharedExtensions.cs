namespace SETraining.Shared;

public static class SharedExtensions
{
    // This method was written by Rasmus Lystrøm
    public static async Task<List<T>> ToListAsync<T>(this IAsyncEnumerable<T> items,
        CancellationToken cancellationToken = default)
    {
        var results = new List<T>();
        await foreach (var item in items.WithCancellation(cancellationToken)
                           .ConfigureAwait(false))
            results.Add(item);
        return results;
    }

    public static bool IsNullOrEmpty<T>(this ICollection<T> col) => (col == null || col.Count == 0);
}