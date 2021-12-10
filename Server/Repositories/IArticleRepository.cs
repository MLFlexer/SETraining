using SETraining.Shared;
using SETraining.Shared.DTOs;

namespace SETraining.Server.Repositories;

public interface IArticleRepository
{
    Task<ArticleDTO> CreateArticleAsync(ArticleCreateDTO article);
    Task<Option<ArticleDTO>> ReadArticleFromIdAsync(int articleId);
    Task<Option<IEnumerable<ArticleDTO>>>ReadArticlesFromTitleAsync(string title);
    Task<IEnumerable<ArticleDTO>> ReadAllArticlesAsync(string difficulty, string[] languages);
    Task<IEnumerable<ArticleDTO>> ReadAllArticlesAsync(string[] languages);
    Task<IEnumerable<ArticleDTO>> ReadAllArticlesAsync(string difficulty);
    Task<IEnumerable<ArticleDTO>> ReadAllArticlesAsync();
    Task<Status> UpdateArticleAsync(int id, ArticleUpdateDTO article);
    Task<Status> DeleteArticleAsync(int articleId);
}
