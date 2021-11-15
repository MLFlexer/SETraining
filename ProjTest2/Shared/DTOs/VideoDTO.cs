
namespace ProjTest2.Shared.DTOs;

public record VideoDTO(int Id, string Title) : ContentDTO(Id, Title, "Video");

public record VideoDetailsDTO(int Id, string Title, string? Description, string? Language, int? Difficulty, float? Rating, int? Length) : VideoDTO(Id, Title);

public record VideoCreateDTO(string Title, string Type) : ContentCreateDTO(Title, Type);

public record VideoUpdateDTO : VideoCreateDTO
{
    protected VideoUpdateDTO(VideoCreateDTO original) : base(original)
    {
    }

    public int Id { get; init; }
}
