
namespace ProjTest2.Shared.Models;

public class Article : Content
{


    public Article(string Title, string textBody) : base(Title, "Article")
    {
        TextBody = textBody;
    }
    public string TextBody { get; set; }
}
