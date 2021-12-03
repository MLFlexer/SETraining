
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
        public async Task<IEnumerable<ArticleDTO>> Get(string? filter)
        {
            return await _repository.ReadAllAsync(filter);
        }

        
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ArticleDTO), 200)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleDTO>> Get(int id,  string? filter)
            => (await _repository.ReadFromIdAsync(id, filter)).ToActionResult();

         //Get from a string
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ArticleDTO), 200)]
        [HttpGet("{title}")]
        public async Task<ActionResult<IEnumerable<ArticleDTO>>> Get(string title, string? filter)
        {
            return (await _repository.ReadFromTitleAsync(title, filter.)).ToActionResult();
        }


        [HttpPost]
        [ProducesResponseType(typeof(ArticleDTO), 201)]
        public async Task<IActionResult> Post(ArticleCreateDTO article)
        {
            var created = await _repository.CreateAsync(article);

            return CreatedAtRoute(nameof(Get), new { created.Id }, created);
        }

        //put = update
        [Authorize]
        [HttpPut ("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task <IActionResult> Put(int id, [FromBody] ArticleUpdateDTO article)
            => (await _repository.UpdateAsync(id, article)).ToActionResult();
        

        //delete
        [Authorize]
        [HttpPut ("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task <IActionResult> Delete(int id)
            => (await _repository.DeleteAsync(id)).ToActionResult();
    }
}
