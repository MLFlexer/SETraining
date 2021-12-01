using SETraining.Shared;
using SETraining.Shared.DTOs;

namespace SETraining.Server.Repositories;

public interface IContentRepository
{


    Task<ContentsDTO> CreateAsync(ContentCreateDTO content);
    Task<Option<ContentDTO>> ReadAsync(int contentId);
    Task<Option<IEnumerable<ContentDTO>>>ReadAsync(string title);
    Task<IEnumerable<ContentDTO>> ReadAsync();
   
  
    Task<Status> UpdateAsync(int id, ContentUpdateDTO content);
    Task<Status> DeleteAsync(int contentId);
}
