using System.Threading;

namespace ProjTest2.Shared.Models
{
    public class Rating
    {
       

        public Rating(int value)
        {
            Value = value;
        }
        public int Id {get; set;}
        public int Value { get; set; }
        public Content? Content { get; set; }
        public Learner? Learner { get; set; }
    }
}