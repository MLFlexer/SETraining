
namespace ProjTest2.Shared.DTOs;

public record ContentDTO(int Id, string Title, string Type);

public record ContentDetailsDTO(int Id, string Title, string? Description, string? Language, int? Difficulty, float? Rating/*, IReadOnlySet<Tag>? Tags,*/, string Type) : ContentDTO(Id, Title, Type);

public record ContentCreateDTO
{
    public ContentCreateDTO(string title, string type)
    {
        Title = title;
        Type = type;
    }

    public string Title { get; init; }
    public string? Description { get; init; }
    public string? Language { get; init; }
    public int? Difficulty { get; init; }

    public float? Rating { get; init; }

    public string Type { get; init; }
}

public record ContentUpdateDTO : ContentCreateDTO
{
    protected ContentUpdateDTO(ContentCreateDTO original) : base(original)
    {
        // Det her er auto-genereret kode. Skal det laves om?
    }

    public int Id { get; init; }
}
