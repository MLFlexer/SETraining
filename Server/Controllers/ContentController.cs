
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web.Resource;
using SETraining.Server.Repositories;
using SETraining.Shared.DTOs;
using SETraining.Shared;
using BDSAProject.Server;


namespace SETraining.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class ContentController : ControllerBase
    {
        private readonly ILogger<ContentController> _logger;
        private readonly IContentRepository _repository;

        public ContentController(ILogger<ContentController> logger, IContentRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<ContentDTO>> Get()
        {
            return await _repository.ReadAsync();
        }

        
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ContentDTO), 200)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ContentDTO>> Get(int id)
            => (await _repository.ReadAsync(id)).ToActionResult();
        // public async Task<Option<ContentDTO>> Get(int id)
        // {
        //     return await _repository.ReadAsync(id);
        // }

         //Get from a string
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ContentDTO), 200)]
        [HttpGet("{title}")]
        public async Task<Option<IEnumerable<ContentDTO>>> Get(string title)
        {
            return await _repository.ReadAsync(title);

        }


        [HttpPost]
        [ProducesResponseType(typeof(ContentDTO), 201)]
        public async Task<IActionResult> Post(ContentCreateDTO content)
        {
            var created = await _repository.CreateAsync(content);

            return CreatedAtRoute(nameof(Get), new { created.Id }, created);
        }

        //put = update
        // [Authorize]
        // [HttpPut ("{id}")]
        // [ProducesResponseType(204)]
        // [ProducesResponseType(404)]
        // public async Task <IActionResult> Put(int id, [FromBody] ContentUpdateDTO content)
        //     => (await _repository.UpdateAsync(id, content)).ToActionResult();
        
            
        



        //delete
    }
}
