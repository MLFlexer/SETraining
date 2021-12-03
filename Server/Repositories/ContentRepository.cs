
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

    public async Task<ContentDTO> CreateAsync(ContentCreateDTO content)
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
            entity = new Video(content.Title, "*invalid filepath, used for testing*")
            {
                Description = content.Description,
                ProgrammingLanguages = content.ProgrammingLanguages.Select(p => new ProgrammingLanguage(p)).ToList(),
                Difficulty = content.Difficulty,
                AvgRating = content.AvgRating
            };

            _context.Contents.Add(entity);
        }
        
        await _context.SaveChangesAsync();

        return new ContentDTO(
                entity.Id,
                entity.Title,
                entity.Description,
                entity.ProgrammingLanguages.Select(p => p.Name).ToList(),
                entity.Difficulty,
                entity.AvgRating,
                content.Type
            ); 
    }

    public async Task<Status> DeleteAsync(int contentId)
    {
        var entity = await _context.Contents.FindAsync(contentId);

        if (entity == null)
        {
            return Status.NotFound;
        }

        _context.Contents.Remove(entity);
        await _context.SaveChangesAsync();
        return Status.Deleted;
    }

    public async Task<Option<ContentDTO>> ReadAsync(int contentId)
    {
        return await _context.Contents.Where(c => c.Id == contentId)
            .Select(c => new ContentDTO(
                c.Id,
                c.Title,
                c.Description,
                c.ProgrammingLanguages.Select(p => p.Name).ToList(),
                c.Difficulty,
                c.AvgRating,
                c.Type
                
            ))
            .FirstOrDefaultAsync();
        
    }


   

      //ReadAsync on a string
    public async Task<Option<IEnumerable<ContentDTO>>> ReadAsync(string contentTitle)
    {
        return await _context.Contents.Where(c => c.Title.Contains(contentTitle))
            .Select(c => new ContentDTO(
                c.Id,
                c.Title,
                c.Description,
                c.ProgrammingLanguages.Select(p => p.Name).ToList(),
                c.Difficulty,
                c.AvgRating,
                c.Type)).ToListAsync();
    }

    public async Task<IEnumerable<ContentDTO>> ReadAsync()

    {
        var all = await _context.Contents.Select(content =>
            new ContentDTO(
                    content.Id,
                    content.Title,
                    content.Description,
                    content.ProgrammingLanguages.Select(p => p.Name).ToList(),
                    content.Difficulty,
                    content.AvgRating,
                    content.Type)
                ).ToListAsync();

        return all;
    }

    
    
    //TODO: Vi beholder nedarvning. Dette udvides med 1) videorepository + videocontroller 2) article repository + article controller.
    //Vi kalder content api hver gang vi skal sortere på deres fælles ting, ellers bruger vi de specifikke apis
    
    // public async Task<IEnumerable<ContentDTO>> ReadVideosAsync()
    // {
    //     var all = await _context.Articles.Select(article =>
    //         new ArticleDTO(
    //             article.Id,
    //             article.Title,
    //             article.Description,
    //             article.ProgrammingLanguages.Select(p => p.Name).ToList(),
    //             article.Difficulty,
    //             article.AvgRating,
    //             article.Type,
    //             article.TextBody
    //             )
    //     ).ToListAsync();
    //     
    //     return all;
    // }
    
    


    public async Task<Status> UpdateAsync(int id, ContentUpdateDTO content)
    {
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
        var existing = await _context.ProgrammingLanguages.Where(l => languages.Contains(l.Name)).ToDictionaryAsync(p => p.Name);

        foreach (var language in languages)
        {
            yield return existing.TryGetValue(language, out var p) ? p : new ProgrammingLanguage(language);
        }
    }
}
