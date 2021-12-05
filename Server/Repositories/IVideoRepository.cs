using SETraining.Shared;
using SETraining.Shared.DTOs;

namespace SETraining.Server.Repositories;

public interface IVideoRepository
{


    Task<VideoDTO> CreateAsync(VideoCreateDTO video);
    Task<Option<VideoDTO>> ReadFromIdAsync(int videoId, FilterSetting? filters);
    Task<Option<IEnumerable<VideoDTO>>>ReadFromTitleAsync(string title, FilterSetting? filters);
    Task<IEnumerable<VideoDTO>> ReadAllAsync(FilterSetting? filters);
    Task<Status> UpdateAsync(int id, VideoUpdateDTO video);
    Task<Status> DeleteAsync(int videoId);
}
