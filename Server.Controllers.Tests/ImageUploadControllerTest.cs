using System;
using System.Collections.Generic;
using System.IO;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SETraining.Server.Controllers;
using SETraining.Server.Repositories;
using SETraining.Shared.DTOs;
using Xunit;
using SETraining.Shared;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Engine.ClientProtocol;
using SETraining.Shared.Models;

namespace Server.Controllers.Tests;

public class UploadControllerTest
{
    private UploadController _controller;
    public UploadControllerTest()
    {
        var repository = new Mock<IUploadRepository>();
        //Instantiate a IFormFile
        repository.Setup(m => m.CreateUploadAsync("imageTest", "image.jpg", new MemoryStream()));
        _controller = new UploadController(repository.Object);

    }

    [Fact]
    public async Task Post_returns_uri_response()
    {
        
        
    }
    
    [Fact]
    public async Task Post_returns_Created_Status()
    {
        
        /*//Arrange
        var stream = File.OpenRead("/Users/nikolajworsoelarsen/Desktop/billede.png");
        IFormFile formFile = new FormFile(new MemoryStream(), 0, stream.Length, "billede.png", "test");
        //content.Add();
        
        
        
        var posted = await _controller.Post("billede.png", formFile);
        
        ActionResult a = new BadRequestResult();
        Assert.Equal(a, posted);*/
        
    }
    
}