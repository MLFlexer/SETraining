using SETraining.Shared.Models;

namespace SETraining.Shared.DTOs;

public record ArticlePreviewDTO(int Id, string Title, ArticleType Type, DateTime Created, string? Description, ICollection<string>? ProgrammingLanguages, DifficultyLevel Difficulty, string? ImageURL);

public record ArticleDTO(int Id, string Title, ArticleType Type, DateTime Created, string? Description, ICollection<string>? ProgrammingLanguages, DifficultyLevel Difficulty, string? Body, string? ImageURL, string? VideoURL);

public record ArticleCreateDTO
{
    public string Title { get; init; }

    public ArticleType Type { get; init; }
    public string? Description { get; init; }  
    public IEnumerable<string>? ProgrammingLanguages { get; init; }
    public DifficultyLevel Difficulty { get; init; }

    public string? ImageURL { get; init; }
    
    public string? VideoURL { get; init; }

    public string? Body { get; init; }
}

public record ArticleUpdateDTO : ArticleCreateDTO
{
    public int Id { get; init; }
}
