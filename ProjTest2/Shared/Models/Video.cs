namespace ProjTest2.Shared.Models;
public class Video : Content
{

    //For EF Core 
    private Video(string Title) : base(Title, "Video")
    {
        
    }


    public Video(string Title, RawVideo rawVideo) : base(Title, "Video")
    {
        RawVideo = rawVideo;
    }

    public int? Length { get; set; }
    public RawVideo RawVideo { get; set; }
}

