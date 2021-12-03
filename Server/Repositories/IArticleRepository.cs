using SETraining.Shared;
using SETraining.Shared.DTOs;

namespace SETraining.Server.Repositories;

public interface IArticleRepository
{


    Task<ArticleDTO> CreateAsync(ArticleCreateDTO article);
    Task<Option<ArticleDTO>> ReadFromIdAsync(int articleId, FilterSetting? filters);
    Task<Option<IEnumerable<ArticleDTO>>>ReadFromTitleAsync(string title, FilterSetting? filters);
    Task<IEnumerable<ArticleDTO>> ReadAllAsync(FilterSetting? filters);
    Task<Status> UpdateAsync(int id, ArticleUpdateDTO article);
    Task<Status> DeleteAsync(int articleId);
}
