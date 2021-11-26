
namespace ProjTest2.Shared.Models
{
    public class Rating
    {
        private Rating() { } //Constructor for EF Core.

        public Rating(int value, Content content, Learner learner)
        {
            Value = value;
            Content = content;
            Learner = learner;
        }
        public int Id {get; set;}
        public int Value { get; set; }
        public Content Content { get; set; }
        public Learner Learner { get; set; }
    }
}