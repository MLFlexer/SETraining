
namespace SETraining.Shared.Models
{
    public class ArticleRating
    {
        private ArticleRating() { } //Constructor for EF Core.

        public ArticleRating(int value, Learner learner, Article article) //Article article,
        {
            Value = value;
            Learner = learner;
            Article = article;
        }
        public int Id {get; set;}
        public int Value { get; set; }
        public Article Article { get; set; }
        public Learner Learner { get; set; }
    }
}