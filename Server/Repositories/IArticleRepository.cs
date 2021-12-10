using SETraining.Shared;
using SETraining.Shared.DTOs;

namespace SETraining.Server.Repositories;

public interface IArticleRepository
{
    Task<ArticleDTO> CreateArticleAsync(ArticleCreateDTO article);
    Task<Option<ArticleDTO>> ReadArticleFromIdAsync(int articleId);
    Task<Option<IEnumerable<ArticleDTO>>>ReadArticlesFromTitleAsync(string title);
    Task<IEnumerable<ArticleDTO>> ReadAllArticlesFromParametersAsync(string title, string difficulty, string[] languages);
    Task<IEnumerable<ArticleDTO>> ReadAllArticlesAsync(string title, string difficulty, string[] languages);
    Task<IEnumerable<ArticleDTO>> ReadAllArticlesAsync(string title, string[] languages);
    Task<IEnumerable<ArticleDTO>> ReadAllArticlesAsync(string title, string difficulty);
    Task<IEnumerable<ArticleDTO>> ReadAllArticlesAsync();
    Task<Status> UpdateArticleAsync(int id, ArticleUpdateDTO article);
    Task<Status> DeleteArticleAsync(int articleId);
}
