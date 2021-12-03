using System;
using System.Collections.Generic;
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

namespace Server.Controllers.Tests;

public class VideoControllerTest
{
    
    [Fact]
    public async void Get_Returns_Video_From_Repo()
    {
        //Arrange 
        var logger = new Mock<ILogger<VideoController>>();
        var expected = Array.Empty<VideoDTO>();
        var repository = new Mock<IVideoRepository>();
        repository.Setup(m => m.ReadAllAsync()).ReturnsAsync(expected);

        var controller = new VideoController(logger.Object, repository.Object);

        //act
        var actual = await controller.Get();


        //Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async void Create_creates_Video(){

        var logger = new Mock<ILogger<VideoController>>();
        var toCreate = new VideoCreateDTO("Dette er en title", "Video");  //should VideoCreateDTO have an empty constructor
        var created = new VideoDTO(1, "Dette er en title", null, null, null, null, "Video");        
        var repository = new Mock<IVideoRepository>();
        repository.Setup(m => m.CreateAsync(toCreate)).ReturnsAsync(created);
        var controller = new VideoController(logger.Object, repository.Object);

        // Act
        var result = await controller.Post(toCreate) as CreatedAtRouteResult;

        // Assert
        Assert.Equal(created, result?.Value);
        Assert.Equal("Get", result?.RouteName);
        Assert.Equal(KeyValuePair.Create("Id", (object?)1), result?.RouteValues?.Single());
        
    } 
   
    [Fact]
    public async void Get_given_non_existing_id_returns_notfound(){
        //Arrange
        var logger = new Mock<ILogger<VideoController>>();
        var repository = new Mock<IVideoRepository>();
        repository.Setup(m => m.ReadFromIdAsync(42)).ReturnsAsync(default(VideoDTO));
        var controller = new VideoController(logger.Object, repository.Object);
        //Act
        var actual = await controller.Get(42);
        //Assert
        Assert.IsType<NotFoundResult>(actual.Result);
    }  

    [Fact]
       public async void Get_given_existing_id_returns_video(){
        //Arrange
        var logger = new Mock<ILogger<VideoController>>();
        var expected = new VideoDTO(1, "Dette er en title", null, null, null, null, "Video");
        var repository = new Mock<IVideoRepository>();
        repository.Setup(m => m.ReadFromIdAsync(1)).ReturnsAsync(expected);
        var controller = new VideoController(logger.Object, repository.Object);
        //Act
        var actual = await controller.Get(1);
        //Assert
        Assert.Equal(expected, actual.Value);
        
    }

    //TODO, denne test skal måske skrives om så den retunerer NotFound
    [Fact]
    public async void Get_given_non_existing_title_returns_null(){
       //Arrange
        var logger = new Mock<ILogger<VideoController>>();
        var repository = new Mock<IVideoRepository>();
        var created = new List<VideoDTO> {new VideoDTO(1, "This is a title", null, null, null, null, "Video")};
        repository.Setup(m => m.ReadFromTitleAsync("DOES_NOT_EXIST")).ReturnsAsync(created);
        var controller = new VideoController(logger.Object, repository.Object);
        //Act
        var actual = await controller.Get("DOES_NOT_EXIST");
        //Assert
        Assert.Null(actual.Result);
    } 


     [Fact]
       public async void Get_given_existing_title_returns_video(){
        //Arrange
        var logger = new Mock<ILogger<VideoController>>();
        var expected = new List<VideoDTO> {new VideoDTO(1, "This is a title", null, null, null, null, "Video")};
        var repository = new Mock <IVideoRepository>();
        repository.Setup(m => m.ReadFromTitleAsync("title")).ReturnsAsync(expected);
        var controller = new VideoController(logger.Object, repository.Object);
        //Act
        var actual = await controller.Get("title");
        //Assert
        Assert.Equal(expected, actual.Value);
        
    }


    [Fact]
    public async void Put_given_exisiting_video_updates_video(){
        //Arrange
        var logger = new Mock<ILogger<VideoController>>();
        var toCreate = new VideoCreateDTO("Dette er en title", "Video");
        var video = new VideoUpdateDTO(toCreate);
        var repository = new Mock <IVideoRepository>();
        repository.Setup(m => m.UpdateAsync(video.Id, video)).ReturnsAsync(Status.Updated);
        var controller = new VideoController(logger.Object, repository.Object);

        //Act
        var response = await controller.Put(video.Id, video);

        //Arrange
        Assert.IsType<NoContentResult>(response);
    }

    [Fact]
    public async void Put_given_non_exisiting_video_returns_notFound(){
        //Arrange
        var logger = new Mock<ILogger<VideoController>>();
        var toCreate = new VideoCreateDTO("Dette er en title", "Video");
        var video = new VideoUpdateDTO(toCreate);
        var repository = new Mock <IVideoRepository>();
        repository.Setup(m => m.UpdateAsync(video.Id, video)).ReturnsAsync(Status.NotFound);
        var controller = new VideoController(logger.Object, repository.Object);

        //Act
        var response = await controller.Put(video.Id, video);

        //Arrange
        Assert.IsType<NotFoundResult>(response);
    }


    [Fact]
    public async void Delete_given_video_returns_the_NoVideo_Status(){
        //Arrange
        var logger = new Mock<ILogger<VideoController>>();
        var repository = new Mock<IVideoRepository>();
        var created = new VideoDTO(1, "Dette er en title", null, null, null, null, "Video");        
        repository.Setup(m => m.DeleteAsync(created.Id)).ReturnsAsync(Status.Deleted);
        var controller = new VideoController(logger.Object, repository.Object);

        //Act
        var response = await controller.Delete(created.Id);

        //Arrange
        Assert.IsType<NoContentResult>(response);
    }

    [Fact]
    public async void Delete_given_non_existing_video_returns_notfound_status(){
         //Arrange
        var logger = new Mock<ILogger<VideoController>>();
        var repository = new Mock<IVideoRepository>();
        repository.Setup(m => m.DeleteAsync(98)).ReturnsAsync(Status.NotFound);
        var controller = new VideoController(logger.Object, repository.Object);

        //Act
        var response = await controller.Delete(98);

        //Arrange
        Assert.IsType<NotFoundResult>(response);
    }
    

    
}