using SETraining.Shared;
using SETraining.Shared.DTOs;

namespace SETraining.Server.Repositories;

public interface IContentRepository
{
    Task<ContentDetailsDTO> CreateAsync(ContentCreateDTO content);
    Task<Option<ContentDetailsDTO>> ReadAsync(int contentId);
    Task<Option<IEnumerable<ContentDetailsDTO>>>ReadAsync(string title);
    Task<IEnumerable<ContentDetailsDTO>> ReadAsync();
   
    Task<Status> UpdateAsync(int id, ContentUpdateDTO content);
    Task<Status> DeleteAsync(int contentId);
}
