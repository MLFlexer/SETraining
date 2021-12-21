using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SETraining.Server.Repositories;
using SETraining.Shared.DTOs;

namespace SETraining.Server.Controllers;

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

    [ProducesResponseType(404)]
    [ProducesResponseType(typeof(ArticlePreviewDTO), 200)]
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<ArticlePreviewDTO>>> Get()
    {
        return (await _repository.ReadAllArticlesAsync()).ToActionResult();
    }

    [ProducesResponseType(404)]
    [ProducesResponseType(typeof(ArticlePreviewDTO), 200)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ArticlePreviewDTO>>> GetFromParameters([FromQuery]string? title, [FromQuery]string? difficulty, [FromQuery]string[]? languages)
    {
        return (await _repository.ReadAllArticlesFromParametersAsync(title!, difficulty!, languages!)).ToActionResult();
    }
    
    [ProducesResponseType(404)]
    [ProducesResponseType(typeof(ArticleDTO), 200)]
    [HttpGet("id={id}")]
    public async Task<ActionResult<ArticleDTO>> GetFromId(int id)
    {
        return (await _repository.ReadArticleFromIdAsync(id)).ToActionResult();
    }

    [HttpPost]
    [ProducesResponseType(typeof(ArticleDTO), 201)]
    public async Task<IActionResult> Post(ArticleCreateDTO article)
    {
        var created = await _repository.CreateArticleAsync(article);
        return CreatedAtAction(nameof(Get), new { created.Id }, created);
    }

    [Authorize]
    [HttpPut ("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task <IActionResult> Put(int id, [FromBody] ArticleUpdateDTO article)
    {
        return (await _repository.UpdateArticleAsync(id, article)).ToActionResult();
    }

    [Authorize]
    [HttpDelete ("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task <IActionResult> Delete(int id)
    {
        return (await _repository.DeleteArticleAsync(id)).ToActionResult();
    }
}

