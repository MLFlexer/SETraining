
using SETraining.Shared.Models;

namespace SETraining.Shared.DTOs;



public record VideoDTO(int Id, string Title, string? Description, ICollection<string>? ProgrammingLanguages, DifficultyLevel? Difficulty, int? AvgRating, string Path);

public record VideoCreateDTO
{
    public string Title { get; init; }
    public string? Description { get; init; }
    public ICollection<string>? ProgrammingLanguages { get; init; }
    public DifficultyLevel? Difficulty { get; init; }

    public int? AvgRating { get; init; }

    public string Path { get; init; }
}

public record VideoUpdateDTO : VideoCreateDTO
{ 
    public int Id { get; init; }
}
