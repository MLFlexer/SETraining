
namespace ProjTest2.Shared.Models;

public class Article : Content
{
    public Article(string Title, string text) : base(Title, "Article")
    {
        textBody = text;
    }
    private string textBody { get; set; }
}
