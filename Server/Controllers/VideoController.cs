
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SETraining.Server.Repositories;
using SETraining.Shared.DTOs;

namespace SETraining.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class VideoController : ControllerBase
    {
        private readonly ILogger<VideoController> _logger;
        private readonly IVideoRepository _repository;

        public VideoController(ILogger<VideoController> logger, IVideoRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<VideoDTO>> Get()
        {
            return await _repository.ReadAllAsync();
        }

        
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(VideoDTO), 200)]
        [HttpGet("id={id}")]
        public async Task<ActionResult<VideoDTO>> Get(int id)
            => (await _repository.ReadFromIdAsync(id)).ToActionResult();

         //Get from a string
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(VideoDTO), 200)]
        [HttpGet("title={title}")]
        public async Task<ActionResult<IEnumerable<VideoDTO>>> Get(string title)
        {
            return (await _repository.ReadFromTitleAsync(title)).ToActionResult();
        }


        [HttpPost]
        [ProducesResponseType(typeof(VideoDTO), 201)]
        public async Task<IActionResult> Post(VideoCreateDTO video)
        {
            var created = await _repository.CreateAsync(video);

            return CreatedAtAction(nameof(Get), new { created.Id }, created);
        }

        //put = update
        [Authorize]
        [HttpPut ("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task <IActionResult> Put(int id, [FromBody] VideoUpdateDTO video)
            => (await _repository.UpdateAsync(id, video)).ToActionResult();
        

        //delete
        [Authorize]
        [HttpDelete ("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task <IActionResult> Delete(int id)
            => (await _repository.DeleteAsync(id)).ToActionResult();
    }
}
