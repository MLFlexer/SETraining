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
public class UploadController : Controller
{
    private readonly IUploadRepository _repository;
    
    private readonly IReadOnlyCollection<string> _allowedContentTypes = new[]
    {
        "image/gif",
        "image/jpeg",
        "image/png",
        "video/mp4"
    };

    public UploadController(IUploadRepository repository)
    {
        _repository = repository;
    }

    [HttpPost("{name}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(string name, [FromForm] IFormFile file)
    {
        if (!_allowedContentTypes.Contains(file.ContentType))
        {
            return BadRequest("Content type not allowed");
        }
        
        Console.WriteLine(name);
        
        var (status, uri) = await _repository.CreateUploadAsync(name.ToString(), file.ContentType, file.OpenReadStream());

        Console.WriteLine(status.ToString());
        Console.WriteLine(uri);
        
        return status == Status.Created
            ? new CreatedResult(uri, null)
            : status.ToActionResult();
    }
}