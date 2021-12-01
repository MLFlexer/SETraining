
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

        
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ContentDetailsDTO), 200)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ContentDetailsDTO>> Get(int id)
            => (await _repository.ReadAsync(id)).ToActionResult();
        // public async Task<Option<ContentDetailsDTO>> Get(int id)
        // {
        //     return await _repository.ReadAsync(id);
        // }

         //Get from a string
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ContentDetailsDTO), 200)]
        [HttpGet("{title}")]
        public async Task<ActionResult<IEnumerable<ContentDetailsDTO>>> Get(string title)
        {
            return (await _repository.ReadAsync(title)).ToActionResult();
        }


        [HttpPost]
        [ProducesResponseType(typeof(ContentDetailsDTO), 201)]
        public async Task<IActionResult> Post(ContentCreateDTO content)
        {
            var created = await _repository.CreateAsync(content);

            return CreatedAtRoute(nameof(Get), new { created.Id }, created);
        }

        //put = update
        [Authorize]
        [HttpPut ("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task <IActionResult> Put(int id, [FromBody] ContentUpdateDTO content)
            => (await _repository.UpdateAsync(id, content)).ToActionResult();
        

        //delete
        [Authorize]
        [HttpPut ("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task <IActionResult> Delete(int id)
            => (await _repository.DeleteAsync(id)).ToActionResult();
    }
}
