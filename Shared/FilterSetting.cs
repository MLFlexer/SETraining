using SETraining.Shared.Models;

namespace SETraining.Shared;

public record FilterSetting
{
    public ProgrammingLanguage? Langs { get; set; }
    public DifficultyLevel? Level { get; set; }
    //public Ordering? Order { get; set; }
}