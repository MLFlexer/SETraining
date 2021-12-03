
namespace SETraining.Shared.Models
{
    public class Rating
    {
        private Rating() { } //Constructor for EF Core.

        public Rating(int value, Learner learner) //Article article,
        {
            Value = value;
            // Article = article;
            Learner = learner;
        }
        public int Id {get; set;}
        public int Value { get; set; }
        // public Article Article { get; set; }
        public Learner Learner { get; set; }
    }
}