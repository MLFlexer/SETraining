using System.ComponentModel.DataAnnotations;

namespace SETraining.Shared.Models;

public class ProgrammingLanguage
{
    public ProgrammingLanguage(string language)
    {
        Language = language;
    }

    [Key]        
    public string Language { get; set; }

    public ICollection<Content> Contents { get; set; } = null!;
}
