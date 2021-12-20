using Microsoft.AspNetCore.Mvc;
using Moq;
using SETraining.Server.Controllers;
using SETraining.Server.Repositories;
using Xunit;
using SETraining.Shared;
using Microsoft.AspNetCore.Http;

namespace Server.Controllers.Tests;

public class ImageUploadControllerTest
{
    private ImageUploadController _controller;
    
    [Fact]
    public async Task Create_With_Invalid_ContentType_Returns_BadRequest () {
        //Arrange
        var FileMock = new Mock<IFormFile>();
        
        //Setup mock file using a memory stream
        var Content = "Hello World from a Fake File";
        var FileName = "test.mp4";
        var ReturnURI = new Uri($"https://localhost:7021/{FileName}");
        var ContentType = "video/mp4"; //Is invalid in ImageUploadController
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
    public async Task Create_With_JPEG_ContentType_Returns_Created_And_URI () {
        //Arrange
        var FileMock = new Mock<IFormFile>();
        
        //Setup mock file using a memory stream
        var Content = "Hello World from a Fake File";
        var FileName = "test.jpeg";
        var ReturnURI = new Uri($"https://localhost:7021/{FileName}");
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
        var actual = await controller.Post(FileName, file) as CreatedResult;

        //Assert
        Assert.IsType<CreatedResult>(actual);
        Assert.Equal(ReturnURI.ToString(), actual.Location);
    }
    
    [Fact]
    public async Task Create_With_PNG_ContentType_Returns_Created_And_URI () {
        //Arrange
        var FileMock = new Mock<IFormFile>();
        
        //Setup mock file using a memory stream
        var Content = "Hello World from a Fake File";
        var FileName = "test.png";
        var ReturnURI = new Uri($"https://localhost:7021/{FileName}");
        var ContentType = "image/png";
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
        var actual = await controller.Post(FileName, file) as CreatedResult;

        //Assert
        Assert.IsType<CreatedResult>(actual);
        Assert.Equal(ReturnURI.ToString(), actual.Location);
    }
    
    [Fact]
    public async Task Create_With_GIF_ContentType_Returns_Created_And_URI () {
        //Arrange
        var FileMock = new Mock<IFormFile>();
        
        //Setup mock file using a memory stream
        var Content = "Hello World from a Fake File";
        var FileName = "test.png";
        var ReturnURI = new Uri($"https://localhost:7021/{FileName}");
        var ContentType = "image/gif";
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
        var actual = await controller.Post(FileName, file) as CreatedResult;

        //Assert
        Assert.IsType<CreatedResult>(actual);
        Assert.Equal(ReturnURI.ToString(), actual.Location);
    }
    
}

