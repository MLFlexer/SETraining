
using SETraining.Shared.Models;

namespace SETraining.Shared.DTOs;



public record ArticleDTO(int Id, string Title, string? Description, ICollection<string>? ProgrammingLanguages, DifficultyLevel? Difficulty, int? AvgRating, string Body);

public record ArticleCreateDTO(string title, string body)
{
    public string Title = title;
    public string Body = body;
    public string? Description { get; init; }
    public ICollection<string>? ProgrammingLanguages { get; init; }
    public DifficultyLevel? Difficulty { get; init; }

    public int? AvgRating { get; init; }
}

public record ArticleUpdateDTO : ArticleCreateDTO
{
    public ArticleUpdateDTO(ArticleCreateDTO original) : base(original)
    {
        // TODO: Det her er auto-genereret kode. Skal det laves om?
    }

    public int Id { get; init; }
}
