
namespace SETraining.Shared.DTOs;

public record ProgrammingLanguageDTO(string Name);

public record ProgrammingLanguageCreateDTO
{
    public ProgrammingLanguageCreateDTO(string name)
    {
        Name = name;
    }

    public string Name { get; init; }
}

public record ProgrammingLanguageUpdateDTO : ProgrammingLanguageCreateDTO
{
    public ProgrammingLanguageUpdateDTO(string name) : base(name)
    {
    }

    public int Id { get; init; }
}
