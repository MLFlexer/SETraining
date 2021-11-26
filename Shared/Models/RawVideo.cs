
namespace SETraining.Shared.Models;

public class RawVideo
{
    private RawVideo() { } //Constructor for EF Core.
    
    public RawVideo(byte[] rawVideo)
    {
        Video = rawVideo;
    }
    public int Id { get; set; }
    public byte[] Video { get; set; }
}
