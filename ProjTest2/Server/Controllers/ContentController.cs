using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjTest2.Server.Repositories;
using ProjTest2.Shared.DTOs;

namespace ProjTest2.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly ILogger<ContentController> _logger;
        private readonly IContentRepository _repository;

        public ContentController(ILogger<ContentController> logger, IContentRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<ContentDTO>> Get()
        {
            return await _repository.ReadAsync();
        }

        [HttpPost]
        [ProducesResponseType(typeof(ContentDetailsDTO), 201)]
        public async Task<IActionResult> Post(ContentCreateDTO content)
        {
            var created = await _repository.CreateAsync(content);

            return CreatedAtRoute(nameof(Get), new { created.Id }, created);
        }
    }
}
