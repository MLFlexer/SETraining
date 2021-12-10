using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SETraining.Server.Repositories;
using SETraining.Shared.DTOs;
using SETraining.Shared.ExtensionMethods;

namespace SETraining.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ArticleController : ControllerBase
    {
        private readonly ILogger<ArticleController> _logger;
        private readonly IArticleRepository _repository;

        public ArticleController(ILogger<ArticleController> logger, IArticleRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<ArticleDTO>> Get(string difficulty, params string[] languages)
        {
            if (String.IsNullOrEmpty(difficulty) && languages.IsNullOrEmpty())
            {
                return await GetAllArticles();
            }
            else if (String.IsNullOrEmpty(difficulty) && !languages.IsNullOrEmpty())
            {
                return await GetAllArticlesWithLanguages(languages);
            }
            else if (!String.IsNullOrEmpty(difficulty) && languages.IsNullOrEmpty())
            {
                return await GetAllArticlesWithDifficulty(difficulty);
            }

            return await _repository.ReadAllArticlesAsync(difficulty, languages);
        }

        private async Task<IEnumerable<ArticleDTO>> GetAllArticles() => await _repository.ReadAllArticlesAsync();
        private async Task<IEnumerable<ArticleDTO>> GetAllArticlesWithDifficulty(string difficulty) => 
            await _repository.ReadAllArticlesAsync(difficulty);
        private async Task<IEnumerable<ArticleDTO>> GetAllArticlesWithLanguages(string[] languages) => 
            await _repository.ReadAllArticlesAsync(languages);

        
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ArticleDTO), 200)]
        [HttpGet("id={id}")]
        public async Task<ActionResult<ArticleDTO>> GetFromId(int id)
            => (await _repository.ReadArticleFromIdAsync(id)).ToActionResult();

         //Get from a string
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ArticleDTO), 200)]
        [HttpGet("title={title}")]
        public async Task<ActionResult<IEnumerable<ArticleDTO>>> GetFromTitle(string title)
        {
            return (await _repository.ReadArticlesFromTitleAsync(title)).ToActionResult();
        }


        [HttpPost]
        [ProducesResponseType(typeof(ArticleDTO), 201)]
        public async Task<IActionResult> Post(ArticleCreateDTO article)
        {
            var created = await _repository.CreateArticleAsync(article);
            Console.WriteLine(created);
            return CreatedAtAction(nameof(Get), new { created.Id }, created);
        }

        //put = update
        [Authorize]
        [HttpPut ("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task <IActionResult> Put(int id, [FromBody] ArticleUpdateDTO article)
            => (await _repository.UpdateArticleAsync(id, article)).ToActionResult();
        

        //delete
        [Authorize]
        [HttpDelete ("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task <IActionResult> Delete(int id)
            => (await _repository.DeleteArticleAsync(id)).ToActionResult();
    }
}
