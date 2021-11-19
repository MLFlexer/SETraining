namespace ProjTest2.Shared.Models;
public class Video : Content
{
    public Video(string Title, byte[] rawVideo) : base(Title, "Video")
    {
        RawVideo = rawVideo;
    }

    public int? Length { get; set; }
    public byte[] RawVideo { get; set; }
}

