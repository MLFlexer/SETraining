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

public class ImageUploadControllerTest
{
    private ImageUploadController _controller;
    
    [Fact]
    public async Task Create_With_Invalid_ContentType_Returns_BadResult () {
        //Arrange
        var FileMock = new Mock<IFormFile>();
        
        //Setup mock file using a memory stream
        var Content = "Hello World from a Fake File";
        var ReturnURI = new Uri("https://localhost:7021/mock_img"); 
        var FileName = "test.jpeg";
        var ContentType = "video/mp4";
        var Stream = new MemoryStream();
        var Writer = new StreamWriter(Stream);
        await Writer.WriteAsync(Content);
        await Writer.FlushAsync();
        Stream.Position = 0;
        
        FileMock.Setup(_ => _.OpenReadStream()).Returns(Stream);
        FileMock.Setup(_ => _.FileName).Returns(FileName);
        FileMock.Setup(_ => _.Length).Returns(Stream.Length);
        FileMock.Setup(_ => _.Headers).Returns(new HeaderDictionary());
        FileMock.Setup(_ => _.ContentType).Returns(ContentType);
        
        var response = (Status.Created, ReturnURI);

        var repository = new Mock<IUploadRepository>();
        repository.Setup(m => m.CreateUploadAsync(FileName, ContentType, Stream )).ReturnsAsync(response);
        var controller = new ImageUploadController(repository.Object);

        var file = FileMock.Object;
        
        //Act
        var actual = await controller.Post(FileName, file);

        //Assert
        Assert.IsType<BadRequestObjectResult>(actual);
    }
    
    [Fact]
    public async Task Create_With_Right_ContentType_Returns_Created () {
        //Arrange
        var FileMock = new Mock<IFormFile>();
        
        //Setup mock file using a memory stream
        var Content = "Hello World from a Fake File";
        var ReturnURI = new Uri("https://localhost:7021/mock_img"); 
        var FileName = "test.jpeg";
        var ContentType = "image/jpeg";
        var Stream = new MemoryStream();
        var Writer = new StreamWriter(Stream);
        await Writer.WriteAsync(Content);
        await Writer.FlushAsync();
        Stream.Position = 0;
        
        FileMock.Setup(_ => _.OpenReadStream()).Returns(Stream);
        FileMock.Setup(_ => _.FileName).Returns(FileName);
        FileMock.Setup(_ => _.Length).Returns(Stream.Length);
        FileMock.Setup(_ => _.Headers).Returns(new HeaderDictionary());
        FileMock.Setup(_ => _.ContentType).Returns(ContentType);
        
        var response = (Status.Created, ReturnURI);
        
        
        var repository = new Mock<IUploadRepository>();
        repository.Setup(m => m.CreateUploadAsync(FileName, ContentType, Stream )).ReturnsAsync(response);
        var controller = new ImageUploadController(repository.Object);

        var file = FileMock.Object;
        
        //Act
        var actual = await controller.Post(FileName, file);

        //Assert
        Assert.IsType<CreatedResult>(actual);
    }
    
}

