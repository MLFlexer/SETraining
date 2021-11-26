
namespace SETraining.Shared.Models;
public class HistoryEntry
{
    private HistoryEntry() { } //Constructor for EF Core.

    public HistoryEntry(DateTime date, Content content, Learner learner)
    {
        Date = date;
        Content = content;
        Learner = learner;
    }

    public int Id {get; set; }
    public DateTime Date { get; set; }
    public Content Content { get; set; }
    public Learner Learner { get; set; }
}
