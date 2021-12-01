
using SETraining.Shared;
using SETraining.Shared.DTOs;

namespace SETraining.Server.Repositories;

public interface IProgrammingLanguagesRepository
{
    Task<ProgrammingLanguageDTO> CreateAsync(ProgrammingLanguageCreateDTO language);
    Task<Option<ProgrammingLanguageDTO>> ReadAsync(string name);
    Task<IEnumerable<ProgrammingLanguageDTO>> ReadAsync();
}
