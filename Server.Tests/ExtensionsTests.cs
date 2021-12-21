using Microsoft.AspNetCore.Mvc;
using SETraining.Shared;
using SETraining.Server;
using Xunit;

namespace Server.Tests;

public class ExtensionsTests
{
    [Theory]
    [InlineData(Status.Updated)]
    [InlineData(Status.Deleted)]
    [InlineData(Status.NoContent)]
    public void ToActionResult_given_StatusUpdated_or_Deleted_or_NoContent_returns_NoContentResult(Status status)
    {
        Assert.IsType<NoContentResult>(status.ToActionResult());
    }

    [Fact]
    public void ToActionResult_given_StatusNotFound_returns_NotFoundResult()
    {
        Assert.IsType<NotFoundResult>(Status.NotFound.ToActionResult());
    }

    [Fact]
    public void ToActionResult_given_StatusBadRequest_returns_BadRequestResult()
    {
        Assert.IsType<BadRequestResult>(Status.BadRequest.ToActionResult());
    }

    [Fact]
    public void ToActionResult_given_StatusConflict_returns_ConflictResult()
    {
        Assert.IsType<ConflictResult>(Status.Conflict.ToActionResult());
    }

    [Fact]
    public void ToActionResult_given_unsupported_Status_throws_NotSupportedException()
    {
        Assert.Throws<NotSupportedException>(() => ((Status) 7).ToActionResult());
    }
}