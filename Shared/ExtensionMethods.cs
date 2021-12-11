namespace SETraining.Shared.ExtensionMethods;

public static class ExtensionMethods
{
    public static bool IsNullOrEmpty<T>(this ICollection<T> col) => (col == null || col.Count == 0);
    public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self) => self?.Select((item, index) => (item, index)) ?? new List<(T, int)>();
}
