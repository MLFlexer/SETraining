using System.Threading;

namespace ProjTest2.Shared.Models
{
    public class Rating
    {
        private Rating()
        {

        }
        public Rating(int id, int value, Content content, Learner learner)
        {
            Id = id;
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