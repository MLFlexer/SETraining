using SETraining.Shared;
using SETraining.Shared.DTOs;

namespace SETraining.Server.Repositories;

public interface IArticleRepository
{


    Task<ArticleDTO> CreateArticleAsync(ArticleCreateDTO article);
    Task<Option<ArticleDTO>> ReadArticleFromIdAsync(int articleId, FilterSetting? filters);
    Task<Option<IEnumerable<ArticleDTO>>>ReadArticlesFromTitleAsync(string title, FilterSetting? filters);
    Task<IEnumerable<ArticleDTO>> ReadAllArticlesAsync(FilterSetting? filters);
    Task<Status> UpdateArticleAsync(int id, ArticleUpdateDTO article);
    Task<Status> DeleteArticleAsync(int articleId);
}
