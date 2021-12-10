using Microsoft.AspNetCore.Mvc;
using SETraining.Shared;

namespace SETraining.Server;

public static class Extensions
{
    public static IActionResult ToActionResult(this Status status) => status switch
    {
        Status.Updated => new NoContentResult(),
        Status.Deleted => new NoContentResult(),
        Status.NotFound => new NotFoundResult(),
        Status.Conflict => new ConflictResult(),
        _ => throw new NotSupportedException($"{status} not supported")
    };

    public static ActionResult<T> ToActionResult<T>(this Option<T> option) where T : class
        => option.IsSome ? option.Value : new NotFoundResult();
}