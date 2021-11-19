using System.Collections;
using System.Collections.Generic;

namespace ProjTest2.Shared.Models
{
    public class Moderator
    {
        public Moderator(string name)
        {
            Name = name;
            Contents = new List<Content>();
        }
        
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Content> Contents { get; set; }

    }
}