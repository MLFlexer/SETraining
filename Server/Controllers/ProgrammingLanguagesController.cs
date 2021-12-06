
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SETraining.Server.Repositories;
using SETraining.Shared.DTOs;
using SETraining.Shared;

namespace SETraining.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ProgrammingLanguagesController : ControllerBase
    {
        private readonly ILogger<ProgrammingLanguagesController> _logger;
        private readonly IProgrammingLanguagesRepository _repository;

        public ProgrammingLanguagesController(ILogger<ProgrammingLanguagesController> logger, IProgrammingLanguagesRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<ProgrammingLanguageDTO>> Get()
        {
            return await _repository.ReadAsync();
        }

        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ProgrammingLanguageDTO), 200)]
        [HttpGet("{name}")]
        public async Task<Option<ProgrammingLanguageDTO>> Get(string name)
        {
            return await _repository.ReadAsync(@name);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProgrammingLanguageDTO), 201)]
        public async Task<IActionResult> Post(ProgrammingLanguageCreateDTO language)
        {
            var created = await _repository.CreateAsync(language);

            return CreatedAtRoute(nameof(Get), new { created.Name }, created);
        }
    }
}
