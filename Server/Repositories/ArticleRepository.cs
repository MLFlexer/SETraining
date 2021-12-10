
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SETraining.Server.Contexts;
using SETraining.Shared;
using SETraining.Shared.DTOs;
using SETraining.Shared.Models;

namespace SETraining.Server.Repositories;
public class ArticleRepository : IArticleRepository
{
    private readonly ISETrainingContext _context;

    public ArticleRepository(ISETrainingContext context)
    {
        _context = context;
    }

    public async Task<ArticleDTO> CreateArticleAsync(ArticleCreateDTO article)
    {
        
           var entity = new Article(article.Title, article.Body)
            {
                Description = article.Description,
                ProgrammingLanguages = await GetProgrammingLanguagesAsync(article.ProgrammingLanguages).ToListAsync(),
                Difficulty = article.Difficulty,
                AvgRating = article.AvgRating
            };
            _context.Articles.Add(entity);
           
        
        await _context.SaveChangesAsync();

        return new ArticleDTO(
                entity.Id,
                entity.Title,
                entity.Description,
                entity.ProgrammingLanguages.Select(p => p.Name).ToList(),
                entity.Difficulty,
                entity.AvgRating,
                entity.Body
            ); 
    }

    public async Task<Status> DeleteArticleAsync(int contentId)
    {
        var entity = await _context.Articles.FindAsync(contentId);

        if (entity == null)
        {
            return Status.NotFound;
        }

        _context.Articles.Remove(entity);
        await _context.SaveChangesAsync();
        return Status.Deleted;
    }

    public async Task<Option<ArticleDTO>> ReadArticleFromIdAsync(int contentId, FilterSetting? filters)
    {
        return await _context.Articles.Where(c => c.Id == contentId)
            .Select(c => new ArticleDTO(
                c.Id,
                c.Title,
                c.Description,
                c.ProgrammingLanguages.Select(p => p.Name).ToList(),
                c.Difficulty,
                c.AvgRating,
                c.Body
                
            ))
            .FirstOrDefaultAsync();
        
    }


   

      //ReadAsync on a string
    public async Task<Option<IEnumerable<ArticleDTO>>> ReadArticlesFromTitleAsync(string contentTitle, FilterSetting? filters)
    {
        
        return await _context.Articles.Where(c => c.Title.ToLower().Contains(contentTitle.ToLower().Trim()))
            .Select(c => new ArticleDTO(
                c.Id,
                c.Title,
                c.Description,
                c.ProgrammingLanguages.Select(p => p.Name).ToList(),
                c.Difficulty,
                c.AvgRating,
                c.Body)).ToListAsync();
    }

    public async Task<IEnumerable<ArticleDTO>> ReadAllArticlesAsync(FilterSetting? filters)

    {
        var all = await _context.Articles.Select(content =>
            new ArticleDTO(
                    content.Id,
                    content.Title,
                    content.Description,
                    content.ProgrammingLanguages.Select(p => p.Name).ToList(),
                    content.Difficulty,
                    content.AvgRating,
                    content.Body)
                ).ToListAsync();

        return all;
    }
    

    public async Task<Status> UpdateArticleAsync(int id, ArticleUpdateDTO article)
    { 
        var entity = _context.Articles.ToList().Find(c => c.Id == id);

        if (entity == null)
        {
            return Status.NotFound;
        }

        _context.Articles.Remove(entity);
        await _context.SaveChangesAsync();
        
        
        entity.Description = article.Description;
        entity.Difficulty = article.Difficulty;
        entity.Title = article.Title;
        entity.Body = article.Body;
        entity.AvgRating = article.AvgRating;
        entity.ProgrammingLanguages = await GetProgrammingLanguagesAsync(article.ProgrammingLanguages).ToListAsync();
        
        
        _context.Articles.Add(entity);
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
