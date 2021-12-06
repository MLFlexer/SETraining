
namespace SETraining.Shared.Models;
public class VideoHistoryEntry
{
    private VideoHistoryEntry() { } //Constructor for EF Core.

    public VideoHistoryEntry(DateTime date, Video video, Learner learner)
    {
        Date = date;
        Video = video;
        Learner = learner;
    }

    public int Id {get; set; }
    public DateTime Date { get; set; }
    public Video Video { get; set; }
    public Learner Learner { get; set; }
}
