
namespace SETraining.Shared.DTOs;

public record ProgrammingLanguageDTO(string Name);

public record ProgrammingLanguageCreateDTO
{
    public string Name { get; init; }
}

public record ProgrammingLanguageUpdateDTO : ProgrammingLanguageCreateDTO
{
    public int Id { get; init; }
}
