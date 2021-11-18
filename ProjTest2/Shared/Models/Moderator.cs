using System.Collections;
using System.Collections.Generic;

namespace ProjTest2.Shared.Models
{
    public class Moderator
    {
        public Moderator(int id, string name)
        {
            Id = id;
            Name = name;
            Contents = new List<Content>();
        }
        
        private int Id { get; set; }
        private string Name { get; set; }
        public ICollection<Content> Contents { get; set; }

    }
}