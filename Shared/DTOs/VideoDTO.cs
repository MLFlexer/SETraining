using SETraining.Shared.Models;

namespace SETraining.Shared.DTOs;

public record VideoDTO(int Id, string Title, string? Description, ICollection<string>? ProgrammingLanguages, DifficultyLevel? Difficulty, int? AvgRating, string Type, string link) : ContentDTO(Id,  Title, Description, ProgrammingLanguages, Difficulty, AvgRating, Type);