
using Microsoft.EntityFrameworkCore;
using SETraining.Server.Contexts;
using SETraining.Shared;
using SETraining.Shared.DTOs;
using SETraining.Shared.Models;

namespace SETraining.Server.Repositories;
public class ContentRepository : IContentRepository
{
    private readonly IKhanContext _context;

    public ContentRepository(IKhanContext context)
    {
        _context = context;
    }

    public async Task<ContentDetailsDTO> CreateAsync(ContentCreateDTO content)
    {
        Content entity;
        if (content.Type == "Article")
        {
            entity = new Article(content.Title, "")
            {
                Description = content.Description,
                ProgrammingLanguages = content.ProgrammingLanguages.Select(p => new ProgrammingLanguage(p)).ToList(),
                Difficulty = content.Difficulty,
                AvgRating = content.AvgRating
            };

            _context.Contents.Add(entity);
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

            _context.Contents.Add(entity);
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
        return await _context.Contents.Where(c => c.Id == contentId)
            .Select(c => new ContentDetailsDTO(
                c.Id,
                c.Title,
                c.Description,
                c.ProgrammingLanguages.Select(p => p.Language).ToList(),
                c.Difficulty,
                c.AvgRating,
                c.Type))
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<ContentDetailsDTO>> ReadAsync()
    {
        var all = await _context.Contents.Select(content =>
            new ContentDetailsDTO(
                    content.Id,
                    content.Title,
                    content.Description,
                    content.ProgrammingLanguages.Select(p => p.Language).ToList(),
                    content.Difficulty,
                    content.AvgRating,
                    content.Type)
                ).ToListAsync();

        return all;
    }
    public async Task<Status> UpdateAsync(int id, ContentUpdateDTO content)
    {
       //var entity = _context.Contents.Select(c => c.Id == id);
       //var entity = await _context.Contents.Include(c => c.Description).FirstOrDefaultAsync(c => c.Id == content.Id);
       var entity = _context.Contents.ToList().Find(c => c.Id == id);

       if (entity == null)
        {
            return Status.NotFound;
        }

        entity.Description = content.Description;
        entity.Difficulty = content.Difficulty;
        entity.Title = content.Title;
        entity.Type = content.Type;
        entity.AvgRating = content.AvgRating;

        entity.ProgrammingLanguages = await GetProgrammingLanguagesAsync(content.ProgrammingLanguages).ToListAsync();
        
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
