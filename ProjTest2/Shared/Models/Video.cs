namespace ProjTest2.Shared.Models;
public class Video : Content
{
    private Video(string Title) : base(Title, "Video")
    {
        //Constructor for EF Core.
    }

    public Video(string Title, RawVideo rawData) : base(Title, "Video")
    {
        RawData = rawData;
    }

    public int? Length { get; set; }
    public RawVideo RawData { get; set; }
}
