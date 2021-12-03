﻿
using SETraining.Shared.Models;

public abstract class Content
{
    public Content(string title, string type)
    {
        Title = title;
        Type = type;
    }

    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DifficultyLevel? Difficulty { get; set; }
    public Moderator? Creator { get; set; }
    public ICollection<ProgrammingLanguage>? ProgrammingLanguages { get; set; } = null!;
    public ICollection<Rating>? Ratings { get; set; }
    public int? AvgRating { get; set; }
    public string Type { get; set; }
}
