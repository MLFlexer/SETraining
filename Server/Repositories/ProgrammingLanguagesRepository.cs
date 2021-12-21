
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
        if (String.IsNullOrWhiteSpace(language.Name))
        {
            return null!;
        }

        var entity = new ProgrammingLanguage(language.Name);

        _context.ProgrammingLanguages.Add(entity);

        await _context.SaveChangesAsync();

        return new ProgrammingLanguageDTO(entity.Name);
    }

    public async Task<Option<ProgrammingLanguageDTO>> ReadAsync(string name)
    {
        if(name is null) return null;
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
