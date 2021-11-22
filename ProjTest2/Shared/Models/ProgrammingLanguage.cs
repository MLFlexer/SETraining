using System.ComponentModel.DataAnnotations;

namespace ProjTest2.Shared.Models
{
    public class ProgrammingLanguage
    {
      

        public ProgrammingLanguage(string language)
        {
            Language = language;
        }
        //Skal være unik
        [Key]        
        public string Language { get; set; }
    }
}