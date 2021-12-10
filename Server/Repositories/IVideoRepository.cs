using SETraining.Shared;
using SETraining.Shared.DTOs;

namespace SETraining.Server.Repositories;

public interface IVideoRepository
{
    Task<VideoDTO> CreateAsync(VideoCreateDTO video);
    Task<Option<VideoDTO>> ReadFromIdAsync(int videoId);
    Task<Option<IEnumerable<VideoDTO>>>ReadFromTitleAsync(string title);
    Task<IEnumerable<VideoDTO>> ReadAllAsync();
    Task<Status> UpdateAsync(int id, VideoUpdateDTO video);
    Task<Status> DeleteAsync(int videoId);
}
