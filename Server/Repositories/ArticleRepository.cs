
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SETraining.Server.Contexts;
using SETraining.Shared;
using SETraining.Shared.DTOs;
using SETraining.Shared.ExtensionMethods;
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
        var entity = new Article(article.Title, article.Type, DateTime.Today, DifficultyLevel.Expert)
        {
            Description = article.Description,
            ProgrammingLanguages = await GetProgrammingLanguagesAsync(article.ProgrammingLanguages).ToListAsync(),
            Difficulty = article.Difficulty,
            AvgRating = article.AvgRating,
            ImageURL = article.ImageURL,
            Created = DateTime.Now.ToUniversalTime(),
            Body = article.Body
            
        };
        _context.Articles.Add(entity);
        
        await _context.SaveChangesAsync();

        return new ArticleDTO(
                entity.Id,
                entity.Title,
                entity.Type,
                entity.Created.ToUniversalTime(),
                entity.Description,
                entity.ProgrammingLanguages.Select(p => p.Name).ToList(),
                entity.Difficulty,
                entity.AvgRating,
                entity.Body,
                entity.ImageURL
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

    public async Task<Option<ArticleDTO>> ReadArticleFromIdAsync(int contentId)
    {
        return await _context.Articles
                    .Where(c => c.Id == contentId)
                    .Select(c => new ArticleDTO(
                        c.Id,
                        c.Title,
                        c.Type,
                        c.Created.ToUniversalTime(),
                        c.Description,
                        c.ProgrammingLanguages.Select(p => p.Name).ToList(),
                        c.Difficulty,
                        c.AvgRating,
                        c.Body,
                        c.ImageURL
                    ))
                    .FirstOrDefaultAsync();
    }


    //ReadAsync on a string
    public async Task<Option<IEnumerable<ArticleDTO>>> ReadArticlesFromTitleAsync(string contentTitle)
    {
        return await _context.Articles.Where(c => c.Title.ToLower().Contains(contentTitle.ToLower().Trim()))
            .Select(c => new ArticleDTO(
                c.Id,
                c.Title,
                c.Type,
                c.Created.ToUniversalTime(),
                c.Description,
                c.ProgrammingLanguages.Select(p => p.Name).ToList(),
                c.Difficulty,
                c.AvgRating,
                c.Body,
                c.ImageURL)).ToListAsync();
    }

    public async Task<Option<IEnumerable<ArticleDTO>>> ReadAllArticlesFromParametersAsync(string title, string difficulty, string[] languages)
    {
        if (!String.IsNullOrEmpty(difficulty) && !(languages!.IsNullOrEmpty()))
        {
            return await ReadAllArticlesAsync(title, difficulty, languages);
        }
        else if (String.IsNullOrEmpty(difficulty) && !(languages!.IsNullOrEmpty()))
        {
            return await ReadAllArticlesAsync(title, languages);
        }
        else if (!String.IsNullOrEmpty(difficulty) && languages!.IsNullOrEmpty())
        {
            return await ReadAllArticlesAsync(title, difficulty);
        }
        else if (!String.IsNullOrEmpty(title))
        {
            return await ReadAllArticlesAsync(title);
        }

        return await ReadAllArticlesAsync();
    }

    public async Task<Option<IEnumerable<ArticleDTO>>> ReadAllArticlesAsync()
    {
        var result =  await _context.Articles
                    .Select(content =>
                        new ArticleDTO(
                                content.Id,
                                content.Title,
                                content.Type,
                                content.Created.ToUniversalTime(),
                                content.Description,
                                content.ProgrammingLanguages.Select(p => p.Name).ToList(),
                                content.Difficulty,
                                content.AvgRating,
                                content.Body,
                                content.ImageURL
                                )
                    ).ToListAsync();
        
        return result.Any() ? result : null;
    }

    public async Task<Option<IEnumerable<ArticleDTO>>> ReadAllArticlesAsync(string title)
    {
        var result =  await _context.Articles
                    .Where(c => c.Title.ToLower().Contains(title.ToLower().Trim()))
                    .Select(content =>
                        new ArticleDTO(
                                content.Id,
                                content.Title,
                                content.Type,
                                content.Created.ToUniversalTime(),
                                content.Description,
                                content.ProgrammingLanguages.Select(p => p.Name).ToList(),
                                content.Difficulty,
                                content.AvgRating,
                                content.Body,
                                content.ImageURL)
                    ).ToListAsync();
        
        return result.Any() ? result : null;
    }

    public async Task<Option<IEnumerable<ArticleDTO>>> ReadAllArticlesAsync(string title, string difficulty)
    {
        if (String.IsNullOrEmpty(title)) title = "";
        var diffycultyToEnum = (DifficultyLevel) Enum.Parse(typeof(DifficultyLevel), difficulty);

        var result =  await _context.Articles
                    .Where(c => c.Title.ToLower().Contains(title.ToLower().Trim()))
                    .Where(article => article.Difficulty == diffycultyToEnum)
                    .Select(content =>
                        new ArticleDTO(
                                content.Id,
                                content.Title,
                                content.Type,
                                content.Created.ToUniversalTime(),
                                content.Description,
                                content.ProgrammingLanguages.Select(p => p.Name).ToList(),
                                content.Difficulty,
                                content.AvgRating,
                                content.Body,
                                content.ImageURL)
                    ).ToListAsync();
        
        return result.Any() ? result : null;
    }
    
    public async Task<Option<IEnumerable<ArticleDTO>>> ReadAllArticlesAsync(string title, string[] languages)
    {
        if (String.IsNullOrEmpty(title)) title = "";

        var all = await _context.Articles
                .Where(c => c.Title.ToLower().Contains(title.ToLower().Trim()))
                .Select(content =>
                new ArticleDTO(
                        content.Id,
                        content.Title,
                        content.Type,
                        content.Created.ToUniversalTime(),
                        content.Description,
                        content.ProgrammingLanguages.Select(p => p.Name).ToList() ?? new List<string>(),
                        content.Difficulty,
                        content.AvgRating,
                        content.Body,
                        content.ImageURL)
            ).ToListAsync();

        var result =  all.Where(article => article.ProgrammingLanguages.Intersect(languages).Any())
                  .Select(content =>
                        new ArticleDTO(
                                content.Id,
                                content.Title,
                                content.Type,
                                content.Created.ToUniversalTime(),
                                content.Description,
                                content.ProgrammingLanguages,
                                content.Difficulty,
                                content.AvgRating,
                                content.Body,
                                content.ImageURL)
                    ).ToList();
        
        return result.Any() ? result : null;
    }

    public async Task<Option<IEnumerable<ArticleDTO>>> ReadAllArticlesAsync(string title, string difficulty, string[] languages)
    {
        if (String.IsNullOrEmpty(title)) title = "";
        var diffycultyToEnum = (DifficultyLevel) Enum.Parse(typeof(DifficultyLevel), difficulty);

        var allWithDifficulty = await _context.Articles
                .Where(c => c.Title.ToLower().Contains(title.ToLower().Trim()))
                .Where(article => article.Difficulty == diffycultyToEnum)
                .Select(content =>
                    new ArticleDTO(
                            content.Id,
                            content.Title,
                            content.Type,
                            content.Created.ToUniversalTime(),
                            content.Description,
                            content.ProgrammingLanguages.Select(p => p.Name).ToList() ?? new List<string>(),
                            content.Difficulty,
                            content.AvgRating,
                            content.Body,
                            content.ImageURL)
                ).ToListAsync();

        var result = allWithDifficulty
                    .Where(article => article.ProgrammingLanguages.Intersect(languages).Any())
                    .Select(content =>
                        new ArticleDTO(
                                content.Id,
                                content.Title,
                                content.Type,
                                content.Created.ToUniversalTime(),
                                content.Description,
                                content.ProgrammingLanguages,
                                content.Difficulty,
                                content.AvgRating,
                                content.Body,
                                content.ImageURL)
                    ).ToList();

        return result.Any() ? result : null;
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
        entity.ImageURL = article.ImageURL;
        entity.ProgrammingLanguages = await GetProgrammingLanguagesAsync(article.ProgrammingLanguages).ToListAsync();
        
        
        _context.Articles.Add(entity);
        await _context.SaveChangesAsync();

        return Status.Updated;
    }

    
    //Heavily inspired by github.com/ondfisk/BDSA2021, credit to Author 
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
