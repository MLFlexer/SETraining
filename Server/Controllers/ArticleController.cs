
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SETraining.Server.Repositories;
using SETraining.Shared.DTOs;
using SETraining.Shared;
using BDSAProject.Server;


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
        public async Task<IEnumerable<ArticleDTO>> Get([FromQuery] FilterSetting? filters)
        {
            return await _repository.ReadAllArticlesAsync(filters);
        }

        
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ArticleDTO), 200)]
        [HttpGet("id={id}")]
        public async Task<ActionResult<ArticleDTO>> GetFromId(int id,  [FromQuery] FilterSetting? filters)
            => (await _repository.ReadArticleFromIdAsync(id, filters)).ToActionResult();

         //Get from a string
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ArticleDTO), 200)]
        [HttpGet("title={title}")]
        public async Task<ActionResult<IEnumerable<ArticleDTO>>> GetFromTitle(string title, [FromQuery] FilterSetting? filters)
        {
            return (await _repository.ReadArticlesFromTitleAsync(title, filters)).ToActionResult();
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
