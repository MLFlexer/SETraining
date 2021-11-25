
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

    public Task<Status> UpdateAsync(int id, ContentUpdateDTO content)
    {
        throw new NotImplementedException();
    }
}
