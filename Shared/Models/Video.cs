namespace SETraining.Shared.Models;

public class Video : Content
{
    private Video(string Title) : base(Title, "Video")
    {
        //Constructor for EF Core.
    }

    public Video(string Title, string filePath) : base(Title, "Video")
    {
        FilePath = filePath;
    }

    public string FilePath { get; set; }

    public int? Length { get; set; }
}
