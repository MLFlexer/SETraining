using SETraining.Shared;
using SETraining.Shared.DTOs;

namespace SETraining.Server.Repositories;

public interface IArticleRepository
{
    Task<ArticleDTO> CreateArticleAsync(ArticleCreateDTO article);
    Task<Option<ArticleDTO>> ReadArticleFromIdAsync(int articleId);
    Task<Option<IEnumerable<ArticlePreviewDTO>>> ReadAllArticlesFromParametersAsync(string title, string difficulty, string[] languages);
    Task<Option<IEnumerable<ArticlePreviewDTO>>> ReadAllArticlesAsync(string title, string difficulty, string[] languages);
    Task<Option<IEnumerable<ArticlePreviewDTO>>> ReadAllArticlesAsync(string title, string[] languages);
    Task<Option<IEnumerable<ArticlePreviewDTO>>> ReadAllArticlesAsync(string title, string difficulty);
    Task<Option<IEnumerable<ArticlePreviewDTO>>> ReadArticlesFromTitleAsync(string title);
    Task<Option<IEnumerable<ArticlePreviewDTO>>> ReadAllArticlesAsync();
    Task<Status> UpdateArticleAsync(int id, ArticleUpdateDTO article);
    Task<Status> DeleteArticleAsync(int articleId);
}
