
namespace ProjTest2.Shared.DTOs;

public record ArticleDTO(int Id, string Title) : ContentDTO(Id, Title, "Article");

public record ArticleDetailsDTO(int Id, string Title, string? Description, string? Language, int? Difficulty, float? Rating) : ArticleDTO(Id, Title);

public record ArticleCreateDTO(string Title, string Type) : ContentCreateDTO(Title, Type);

public record ArticleUpdateDTO : ArticleCreateDTO
{
    protected ArticleUpdateDTO(ArticleCreateDTO original) : base(original)
    {
    }

    public int Id { get; init; }
}
