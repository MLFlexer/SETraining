
using Microsoft.EntityFrameworkCore;
using SETraining.Server.Contexts;
using SETraining.Shared;
using SETraining.Shared.DTOs;
using SETraining.Shared.Models;

namespace SETraining.Server.Repositories;
public class ProgrammingLanguagesRepository : IProgrammingLanguagesRepository
{
    private readonly IKhanContext _context;

    public ProgrammingLanguagesRepository(IKhanContext context)
    {
        _context = context;
    }

    public async Task<ProgrammingLanguageDTO> CreateAsync(ProgrammingLanguageCreateDTO language)
    {
        var entity = new ProgrammingLanguage(language.Name);

        _context.ProgrammingLanguages.Add(entity);

        await _context.SaveChangesAsync();

        return new ProgrammingLanguageDTO(entity.Name);
    }

    public async Task<Option<ProgrammingLanguageDTO>> ReadAsync(string name)
    {
        return await _context.ProgrammingLanguages
            .Where(c => c.Name == name)
            .Select(c => new ProgrammingLanguageDTO(name))
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<ProgrammingLanguageDTO>> ReadAsync()
    {
        return await _context.ProgrammingLanguages.Select(language =>
            new ProgrammingLanguageDTO(language.Name)).ToListAsync();
    }
}
