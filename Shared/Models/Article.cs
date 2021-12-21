using System.ComponentModel.DataAnnotations;

namespace SETraining.Shared.Models;

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
    [StringLength(25)] 
    public string Title { get; set; }
    public ArticleType Type { get; set; }
    public DateTime Created { get; set; }
    public DifficultyLevel Difficulty { get; set; }
    [StringLength(50)] 
    public string? Description { get; set; }
    public ICollection<ProgrammingLanguage>? ProgrammingLanguages { get; set; } = null!;
    [Range(0, 5)]
    public int? AvgRating { get; set; }
    [Url] 
    public string? ImageURL { get; set; }
    [Url] 
    public string? VideoURL { get; set; }
    
    public string? Body { get; set; }
}