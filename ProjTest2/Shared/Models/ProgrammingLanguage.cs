using System.ComponentModel.DataAnnotations;

namespace ProjTest2.Shared.Models;

public class ProgrammingLanguage
{
    public ProgrammingLanguage(string language)
    {
        Language = language;
      
        public ProgrammingLanguage(string language)
        {
            Language = language;
        }
        //Skal være unik

        public int Id { get; set; }
        public string Language { get; set; }
    }

    [Key]        
    public string Language { get; set; }

    public ICollection<Content> Contents { get; set; } = null!;
}
