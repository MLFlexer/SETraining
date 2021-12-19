
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using SETraining.Server.Contexts;
using SETraining.Shared;
using SETraining.Shared.DTOs;
using SETraining.Shared.Models;

namespace SETraining.Server.Repositories;
public class ProgrammingLanguagesRepository : IProgrammingLanguagesRepository
{
    private readonly ISETrainingContext _context;

    public ProgrammingLanguagesRepository(ISETrainingContext context)
    {
        _context = context;
    }

    public async Task<ProgrammingLanguageDTO> CreateAsync(ProgrammingLanguageCreateDTO language)
    {
        //TODO: skal der returneres en Option her?
        if (String.IsNullOrWhiteSpace(language.Name))
        {
            return new ProgrammingLanguageDTO("ProgrammingLanguage with empty or null name will not be added to context");
        }

        var entity = new ProgrammingLanguage(language.Name);

        _context.ProgrammingLanguages.Add(entity);

        await _context.SaveChangesAsync();

        return new ProgrammingLanguageDTO(entity.Name);
    }

    public async Task<Option<ProgrammingLanguageDTO>> ReadAsync(string name)
    {
        return await _context.ProgrammingLanguages
            .Where(c => c.Name.ToLower() == name.ToLower().Trim())
            .Select(c => new ProgrammingLanguageDTO(c.Name))
            .FirstOrDefaultAsync();
    }

    public async Task<Option<IEnumerable<ProgrammingLanguageDTO>>> ReadAsync()
    {
        return await _context.ProgrammingLanguages.Select(language =>
            new ProgrammingLanguageDTO(language.Name)).ToListAsync();
    }
}
