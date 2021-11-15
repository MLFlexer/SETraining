using ProjTest2.Shared;
using ProjTest2.Shared.DTOs;

namespace ProjTest2.Server.Repositories;

public interface IContentRepository
{
    Task<ContentDetailsDTO> CreateAsync(ContentCreateDTO content);
    Task<Option<ContentDetailsDTO>> ReadAsync(int contentId);
    Task<IEnumerable<ContentDetailsDTO>> ReadAsync();
    Task<Status> UpdateAsync(int id, ContentUpdateDTO content);
    Task<Status> DeleteAsync(int contentId);
}
