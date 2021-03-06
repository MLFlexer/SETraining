using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using SETraining.Server.Repositories;
using SETraining.Shared;

namespace SETraining.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class VideoUploadController : Controller
{
    private readonly IUploadRepository _repository;
    
    private readonly IReadOnlyCollection<string> _allowedContent = new[]
    {
        "video/mp4"
    };

    public VideoUploadController(IUploadRepository repository)
    {
        _repository = repository;
    }

    [Authorize]
    [HttpPost("{name}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(string name, [FromForm] IFormFile file)
    {
        if (!_allowedContent.Contains(file.ContentType))
        {
            return BadRequest("This content type is not permitted");
        }
        
        var (status, uri) = await _repository.CreateUploadAsync(name.ToString(), file.ContentType, file.OpenReadStream());
        
        return status == Status.Created
            ? new CreatedResult(uri, null)
            : status.ToActionResult();
    }
}