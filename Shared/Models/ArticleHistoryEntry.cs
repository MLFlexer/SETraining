
namespace SETraining.Shared.Models;
public class ArticleHistoryEntry
{
    private ArticleHistoryEntry() { } //Constructor for EF Core.

    public ArticleHistoryEntry(DateTime date, Article article, Learner learner)
    {
        Date = date;
        Article = article;
        Learner = learner;
    }

    public int Id {get; set; }
    public DateTime Date { get; set; }
    public Article Article { get; set; }
    public Learner Learner { get; set; }
}
