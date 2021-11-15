
namespace ProjTest2.Shared.Models;

public class Video : Content
{
    public Video(string Title) : base(Title, "Video")
    {

    }

    public int? Length { get; set; }
}
