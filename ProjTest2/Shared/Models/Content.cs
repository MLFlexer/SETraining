
namespace ProjTest2.Shared.Models;

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
    public string? Language { get; set; }
    public int? Difficulty { get; set; }
    public float? Rating { get; set; }

    public string Type { get; set; }
}
