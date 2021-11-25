
using Microsoft.EntityFrameworkCore;
using ProjTest2.Shared;
using ProjTest2.Shared.DTOs;
using ProjTest2.Shared.Models;

namespace ProjTest2.Server.Repositories;
public class ContentRepository : IContentRepository
{
    private readonly IKhanContext _context;

    public ContentRepository(IKhanContext context)
    {
        _context = context;
    }

    public async Task<ContentDetailsDTO> CreateAsync(ContentCreateDTO content)
    {
        Content entity = null;
        if (content.Type == "Article")
        {
            entity = new Article(content.Title, "")
            {
                Description = content.Description,
                ProgrammingLanguages = content.ProgrammingLanguages.Select(p => new ProgrammingLanguage(p)).ToList(),
                Difficulty = content.Difficulty,
                AvgRating = content.AvgRating
            };

            _context.Content.Add(entity);
        }
        else
        {
            entity = new Video(content.Title, new RawVideo(new byte[1]))
            {
                Description = content.Description,
                ProgrammingLanguages = content.ProgrammingLanguages.Select(p => new ProgrammingLanguage(p)).ToList(),
                Difficulty = content.Difficulty,
                AvgRating = content.AvgRating
            };

            _context.Content.Add(entity);
        }
        
        await _context.SaveChangesAsync();

        return new ContentDetailsDTO(
                entity.Id,
                entity.Title,
                entity.Description,
                entity.ProgrammingLanguages.Select(p => p.Language).ToList(),
                entity.Difficulty,
                entity.AvgRating,
                content.Type
            ); 
    }

    public Task<Status> DeleteAsync(int contentId)
    {
        throw new NotImplementedException();
    }

    public async Task<Option<ContentDetailsDTO>> ReadAsync(int contentId)
    {
        return await _context.Content.Where(c => c.Id == contentId)
            .Select(c => new ContentDetailsDTO(
                c.Id,
                c.Title,
                c.Description,
                c.ProgrammingLanguages.Count > 0 ? c.ProgrammingLanguages.Select(p => p.Language).ToList() : null,
                c.Difficulty,
                c.AvgRating,
                c.Type))
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<ContentDetailsDTO>> ReadAsync()
    {
        var all = await _context.Content.Select(content =>
            new ContentDetailsDTO(
                    content.Id,
                    content.Title,
                    content.Description,
                    content.ProgrammingLanguages.Count > 0 ? content.ProgrammingLanguages.Select(p => p.Language).ToList() : null,
                    content.Difficulty,
                    content.AvgRating,
                    content.Type)
                ).ToListAsync();

        return all;
    }

    public async Task<Status> UpdateAsync(int id, ContentUpdateDTO content)
    {
        var entity = await _context.Content.Include(c => c.ProgrammingLanguages).FirstOrDefaultAsync(c => c.Id == content.Id);

        if (entity == null)
        {
            return Status.NotFound;
        }

        entity.Description = content.Description;
        entity.Difficulty = content.Difficulty;
        entity.Title = content.Title;
        entity.Type = content.Type;
        entity.AvgRating = content.AvgRating;
        
        //TODO: hvorfor virker dette ikke?
        //entity.ProgrammingLanguages = await GetProgrammingLanguagesAsync(content.ProgrammingLanguages).ToListAsync();;

        await _context.SaveChangesAsync();

        return Status.Updated;
    }
    private async IAsyncEnumerable<ProgrammingLanguage> GetProgrammingLanguagesAsync(IEnumerable<string> languages)
    {
        //TODO: Denne metode er direkte kopieret, skal nok laves lidt om.
        var existing = await _context.ProgrammingLanguages.Where(l => languages.Contains(l.Language)).ToDictionaryAsync(p => p.Language);

        foreach (var language in languages)
        {
            yield return existing.TryGetValue(language, out var p) ? p : new ProgrammingLanguage(language);
        }
    }
}
