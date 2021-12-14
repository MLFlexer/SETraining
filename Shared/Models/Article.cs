
using SETraining.Shared.Models;

public class Article
{
    public Article(string title, ArticleType type, DateTime created, DifficultyLevel difficulty)
    {
        Title = title;
        Type = type;
        Created = created;
        Difficulty = difficulty;
    }

    
    public int Id { get; set; }
    public string Title { get; set; }
    public ArticleType Type { get; set; }
    public DateTime Created { get; set; }
    public DifficultyLevel Difficulty { get; set; }
    public string? Description { get; set; }
    public Moderator? Creator { get; set; }
    public ICollection<ProgrammingLanguage>? ProgrammingLanguages { get; set; } = null!;
    
    public ICollection<ArticleRating>? ArticleRatings { get; set; }
    public int? AvgRating { get; set; }
    public string? ImageURL { get; set; }
    public string? Body { get; set; }
}
