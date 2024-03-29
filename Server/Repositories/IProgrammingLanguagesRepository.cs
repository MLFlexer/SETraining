﻿
using SETraining.Shared;
using SETraining.Shared.DTOs;

namespace SETraining.Server.Repositories;

public interface IProgrammingLanguagesRepository
{
    Task<ProgrammingLanguageDTO> CreateAsync(ProgrammingLanguageDTO language);
    Task<Option<ProgrammingLanguageDTO>> ReadAsync(string name);
    Task<Option<IEnumerable<ProgrammingLanguageDTO>>> ReadAsync();
}
