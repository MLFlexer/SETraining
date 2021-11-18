namespace ProjTest2.Shared.Models
{
    public class ProgrammingLanguage
    {
        public ProgrammingLanguage(string language)
        {
            Language = language;
        }
        //Skal være unik
        
        public string Language { get; set; }
    }
}