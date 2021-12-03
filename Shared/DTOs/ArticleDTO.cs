using SETraining.Shared.Models;

namespace SETraining.Shared.DTOs;

public record ArticleDTO(int Id, string Title, string? Description, ICollection<string>? ProgrammingLanguages, DifficultyLevel? Difficulty, int? AvgRating, string Type, string TextBody) : ContentDTO(Id,  Title, Description, ProgrammingLanguages, Difficulty, AvgRating, Type);