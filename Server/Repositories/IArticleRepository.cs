using SETraining.Shared;
using SETraining.Shared.DTOs;

namespace SETraining.Server.Repositories;

public interface IArticleRepository
{
    Task<ArticleDTO> CreateAsync(ArticleCreateDTO article);
    Task<Option<ArticleDTO>> ReadFromIdAsync(int articleId);
    Task<Option<IEnumerable<ArticlePreviewDTO>>> ReadFromParametersAsync(string title, string difficulty, string[] languages);
    Task<Option<IEnumerable<ArticlePreviewDTO>>> ReadAsync(string title, string difficulty, string[] languages);
    Task<Option<IEnumerable<ArticlePreviewDTO>>> ReadAsync(string title, string[] languages);
    Task<Option<IEnumerable<ArticlePreviewDTO>>> ReadAsync(string title, string difficulty);
    Task<Option<IEnumerable<ArticlePreviewDTO>>> ReadFromTitleAsync(string title);
    Task<Option<IEnumerable<ArticlePreviewDTO>>> ReadAsync();
    Task<Status> UpdateAsync(int id, ArticleUpdateDTO article);
    Task<Status> DeleteAsync(int articleId);
}