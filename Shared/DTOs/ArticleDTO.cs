
using SETraining.Shared.Models;

namespace SETraining.Shared.DTOs;



public record ArticleDTO(int Id, string Title, ArticleType Type, DateTime Created, string? Description, ICollection<string>? ProgrammingLanguages, DifficultyLevel Difficulty, int? AvgRating, string Body, string? ImageURL);

public record ArticleCreateDTO
{
    public string Title { get; init; }
    public string Body {get; init; }

    public ArticleType Type { get; set; }
    public string? Description { get; init; }  
    public IEnumerable<string>? ProgrammingLanguages { get; init; }
    public DifficultyLevel Difficulty { get; init; }

    public string? ImageURL { get; set; }

    public int? AvgRating { get; init; }
}

public record ArticleUpdateDTO : ArticleCreateDTO
{
    public int Id { get; init; }
}
