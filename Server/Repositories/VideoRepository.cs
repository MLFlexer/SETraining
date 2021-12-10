
using Microsoft.EntityFrameworkCore;
using SETraining.Server.Contexts;
using SETraining.Shared;
using SETraining.Shared.DTOs;
using SETraining.Shared.Models;

namespace SETraining.Server.Repositories;
public class VideoRepository : IVideoRepository
{
    private readonly ISETrainingContext _context;

    public VideoRepository(ISETrainingContext context)
    {
        _context = context;
    }

    public async Task<VideoDTO> CreateAsync(VideoCreateDTO video)
    {
        var entity = new Video(video.Title, "*invalid filepath, used for testing*")
            {
                Description = video.Description,
                ProgrammingLanguages = video.ProgrammingLanguages.Select(p => new ProgrammingLanguage(p)).ToList(),
                Difficulty = video.Difficulty,
                AvgRating = video.AvgRating
            };

            _context.Videos.Add(entity);
        
        
        await _context.SaveChangesAsync();

        return new VideoDTO(
                entity.Id,
                entity.Title,
                entity.Description,
                entity.ProgrammingLanguages.Select(p => p.Name).ToList(),
                entity.Difficulty,
                entity.AvgRating,
                entity.Path
            ); 
    }

    public async Task<Status> DeleteAsync(int contentId)
    {
        var entity = await _context.Videos.FindAsync(contentId);

        if (entity == null)
        {
            return Status.NotFound;
        }

        _context.Videos.Remove(entity);
        await _context.SaveChangesAsync();
        return Status.Deleted;
    }

    public async Task<Option<VideoDTO>> ReadFromIdAsync(int contentId)
    {
        return await _context.Videos.Where(c => c.Id == contentId)
            .Select(c => new VideoDTO(
                c.Id,
                c.Title,
                c.Description,
                c.ProgrammingLanguages.Select(p => p.Name).ToList(),
                c.Difficulty,
                c.AvgRating,
                c.Path
                
            ))
            .FirstOrDefaultAsync();
        
    }


   

      //ReadAsync on a string
    public async Task<Option<IEnumerable<VideoDTO>>> ReadFromTitleAsync(string contentTitle)
    {
        return await _context.Videos.Where(c => c.Title.ToLower().Trim().Contains(contentTitle.ToLower().Trim()))
            .Select(c => new VideoDTO(
                c.Id,
                c.Title,
                c.Description,
                c.ProgrammingLanguages.Select(p => p.Name).ToList(),
                c.Difficulty,
                c.AvgRating,
                c.Path)).ToListAsync();
    }

    public async Task<IEnumerable<VideoDTO>> ReadAllAsync()
    {
        var all = await _context.Videos.Select(content =>
            new VideoDTO(
                    content.Id,
                    content.Title,
                    content.Description,
                    content.ProgrammingLanguages.Select(p => p.Name).ToList(),
                    content.Difficulty,
                    content.AvgRating,
                    content.Path)
                ).ToListAsync();

        return all;
    }

    public async Task<Status> UpdateAsync(int id, VideoUpdateDTO video)
    {
        var entity = _context.Videos.ToList().Find(c => c.Id == id);

       if (entity == null)
        {
            return Status.NotFound;
        }

        entity.Description = video.Description;
        entity.Difficulty = video.Difficulty;
        entity.Title = video.Title;
        entity.Path = video.Path;
        entity.AvgRating = video.AvgRating;

        entity.ProgrammingLanguages = await GetProgrammingLanguagesAsync(video.ProgrammingLanguages).ToListAsync();
        
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
