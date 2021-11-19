
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProjTest2.Shared;
using ProjTest2.Shared.DTOs;
using ProjTest2.Shared.Models;
using System.Linq;
using System.Threading.Tasks;

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
        /* Content entity = null;
        if (content.Type == "Article")
        {
            entity = new Article(content.Title)
            {
                Description = content.Description,
                Language = content.Language,
                Difficulty = content.Difficulty,
                Rating = content.Rating
            };

            _context.Content.Add((Article)entity);
        }
        else if (content.Type == "Video")
        {
            entity = new Video(content.Title)
            {
                Description = content.Description,
                Language = content.Language,
                Difficulty = content.Difficulty,
                Rating = content.Rating
            };

            _context.Content.Add((Video)entity);
        }
        else
        {
            entity = new Article(content.Title)
            {
                Description = content.Description,
                Language = content.Language,
                Difficulty = content.Difficulty,
                Rating = content.Rating
            };
        }

        await _context.SaveChangesAsync();

        return new ContentDetailsDTO(
                entity.Id,
                entity.Title,
                entity.Description,
                entity.Language,
                entity.Difficulty,
                entity.Rating,
                content.Type
            ); */

        return new ContentDetailsDTO(1, ",", "","",1,1.2f,"article");
    }

    public Task<Status> DeleteAsync(int contentId)
    {
        throw new NotImplementedException();
    }

    public Task<Option<ContentDetailsDTO>> ReadAsync(int contentId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ContentDetailsDTO>> ReadAsync()
    {
        var all = await _context.Content.Select(content =>
        new ContentDetailsDTO(
                content.Id,
                content.Title,
                content.Description,
                content.Language,
                content.Difficulty,
                content.Rating,
                content.Type)
            ).ToListAsync();

        /*var videos = await _context.Videos.Select(video =>
            new ContentDetailsDTO(
                video.Id,
                video.Title,
                video.Description,
                video.Language,
                video.Difficulty,
                video.Rating,
                video.Type)
            ).ToListAsync();

        var articles = await _context.Articles.Select(article => 
            new ContentDetailsDTO(
                article.Id, 
                article.Title,
                article.Description,
                article.Language,
                article.Difficulty,
                article.Rating,
                article.Type)
            ).ToListAsync();


        var contents = articles.Concat(videos);*/

        return all;
    }

    public Task<Status> UpdateAsync(int id, ContentUpdateDTO content)
    {
        throw new NotImplementedException();
    }
}
