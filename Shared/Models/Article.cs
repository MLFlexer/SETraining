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
    
    [Required]
    [StringLength(25)] 
    public string Title { get; set; }

    [Required]
    public ArticleType Type { get; set; }

    [Required]
    public DifficultyLevel Difficulty { get; set; }

    [Required]
    public DateTime Created { get; set; }

    [StringLength(50)]
    public string? Description { get; set; }

    public ICollection<ProgrammingLanguage>? ProgrammingLanguages { get; set; } = null!;
  
    [Url] 
    public string? ImageURL { get; set; }

    [Url] 
    public string? VideoURL { get; set; }
    
    public string? Body { get; set; }
}