
namespace SETraining.Shared.Models;

public class Learner
{
    public Learner(string name)
    {
        Name = name;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public DifficultyLevel? Level { get; set; }
    public ICollection<ArticleHistoryEntry>? ArticleHistory { get; set; }
    public ICollection<VideoHistoryEntry>? VideoHistory { get; set; }
    public ICollection<Article>? Favorites { get; set; }
    public ICollection<ArticleRating>? Ratings { get; set; }
}