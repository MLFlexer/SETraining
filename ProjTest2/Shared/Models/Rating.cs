using System.Threading;

namespace ProjTest2.Shared.Models
{
    public class Rating
    {
        public Rating(int value, Content content,  Learner learner)
        {
            Value = value;
            Content = content;
            Learner = learner;
        }

        public int Value { get; set; }
        public Content Content { get; set; }
        public Learner Learner { get; set; }
    }
}