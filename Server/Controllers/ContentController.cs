
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SETraining.Server.Repositories;
using SETraining.Shared.DTOs;
using SETraining.Shared;

namespace SETraining.Server.Controllers
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
        public async Task<IEnumerable<ContentDetailsDTO>> Get()
        {
            return await _repository.ReadAsync();
        }

        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ContentDetailsDTO), 200)]
        [HttpGet("{id}")]
        public async Task<Option<ContentDetailsDTO>> Get(int id)
        {
            return await _repository.ReadAsync(id);
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
