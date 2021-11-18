using System.Collections.Generic;

namespace ProjTest2.Shared.Models
{
    public class Learner
    {
        public Learner(string name, int id)
        {
            Name = name;
            Id = id;
        }

        public string Name { get; set; }
        public int Id { get; set; }
        public DifficultyLevel? level { get; set; }
        public ICollection<HistoryEntry>? history { get; set; }
        public ICollection<Content>? favorites { get; set; }
        public ICollection<Rating>? ratings { get; set; }

    }
}