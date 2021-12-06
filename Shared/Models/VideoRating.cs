
namespace SETraining.Shared.Models
{
    public class VideoRating
    {
        private VideoRating() { } //Constructor for EF Core.

        public VideoRating(int value, Learner learner, Video video) //Article article,
        {
            Value = value;
            Learner = learner;
            Video = video;
        }
        public int Id {get; set;}
        public int Value { get; set; }
        public Video Video { get; set; }
        public Learner Learner { get; set; }
    }
}