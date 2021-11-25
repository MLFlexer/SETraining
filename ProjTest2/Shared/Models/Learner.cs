
namespace ProjTest2.Shared.Models;
public class Learner
{
    public Learner(string name)
    {
        Name = name;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public DifficultyLevel? Level { get; set; }
    public ICollection<HistoryEntry>? History { get; set; }
    public ICollection<Content>? Favorites { get; set; }
    public ICollection<Rating>? Ratings { get; set; }
}