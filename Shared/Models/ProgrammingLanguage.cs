using System.ComponentModel.DataAnnotations;

namespace SETraining.Shared.Models;

public class ProgrammingLanguage
{
    public ProgrammingLanguage(string name)
    {
        Name = name;
    }

    [Key]        
    public string Name { get; set; }
}
