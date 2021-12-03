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

public class ContentControllerTest
{
    
    [Fact]
    public async void Get_Returns_Content_From_Repo()
    {
        //Arrange 
        var logger = new Mock<ILogger<ContentController>>();
        var expected = Array.Empty<ContentDTO>();
        var repository = new Mock<IContentRepository>();
        repository.Setup(m => m.ReadAsync()).ReturnsAsync(expected);

        var controller = new ContentController(logger.Object, repository.Object);

        //act
        var actual = await controller.Get();


        //Assert
        Assert.Equal(expected, actual);
    }

    //TODO: Denne test skal laves færdig
    [Fact]
    public async void Create_creates_Content(){

        var logger = new Mock<ILogger<ContentController>>();
        var toCreate = new ContentCreateDTO("Dette er en title", "Article");  //should ContentCreateDTO have an empty constructor
        var created = new ContentDTO(1, "Dette er en title", null, null, null, null, "Article");        
        var repository = new Mock<IContentRepository>();
        repository.Setup(m => m.CreateAsync(toCreate)).ReturnsAsync(created);
        var controller = new ContentController(logger.Object, repository.Object);

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
        var logger = new Mock<ILogger<ContentController>>();
        var repository = new Mock<IContentRepository>();
        repository.Setup(m => m.ReadAsync(42)).ReturnsAsync(default(ContentDTO));
        var controller = new ContentController(logger.Object, repository.Object);
        //Act
        var actual = await controller.Get(42);
        //Assert
        Assert.IsType<NotFoundResult>(actual.Result);
    }  

    [Fact]
       public async void Get_given_existing_id_returns_content(){
        //Arrange
        var logger = new Mock<ILogger<ContentController>>();
        var expected = new ContentDTO(1, "Dette er en title", null, null, null, null, "Article");
        var repository = new Mock<IContentRepository>();
        repository.Setup(m => m.ReadAsync(1)).ReturnsAsync(expected);
        var controller = new ContentController(logger.Object, repository.Object);
        //Act
        var actual = await controller.Get(1);
        //Assert
        Assert.Equal(expected, actual.Value);
        
    }

    //TODO, denne test skal måske skrives om så den retunerer NotFound
    [Fact]
    public async void Get_given_non_existing_title_returns_null(){
       //Arrange
        var logger = new Mock<ILogger<ContentController>>();
        var repository = new Mock<IContentRepository>();
        var created = new List<ContentDTO> {new ContentDTO(1, "This is a title", null, null, null, null, "Article")};
        repository.Setup(m => m.ReadAsync("DOES_NOT_EXIST")).ReturnsAsync(created);
        var controller = new ContentController(logger.Object, repository.Object);
        //Act
        var actual = await controller.Get("DOES_NOT_EXIST");
        //Assert
        Assert.Null(actual.Result);
    } 


     [Fact]
       public async void Get_given_existing_title_returns_content(){
        //Arrange
        var logger = new Mock<ILogger<ContentController>>();
        var expected = new List<ContentDTO> {new ContentDTO(1, "This is a title", null, null, null, null, "Article")};
        var repository = new Mock <IContentRepository>();
        repository.Setup(m => m.ReadAsync("title")).ReturnsAsync(expected);
        var controller = new ContentController(logger.Object, repository.Object);
        //Act
        var actual = await controller.Get("title");
        //Assert
        Assert.Equal(expected, actual.Value);
        
    }


    [Fact]
    public async void Put_given_exisiting_content_updates_content(){
        //Arrange
        var logger = new Mock<ILogger<ContentController>>();
        var toCreate = new ContentCreateDTO("Dette er en title", "Article");
        var content = new ContentUpdateDTO(toCreate);
        var repository = new Mock <IContentRepository>();
        repository.Setup(m => m.UpdateAsync(content.Id, content)).ReturnsAsync(Status.Updated);
        var controller = new ContentController(logger.Object, repository.Object);

        //Act
        var response = await controller.Put(content.Id, content);

        //Arrange
        Assert.IsType<NoContentResult>(response);
    }

    [Fact]
    public async void Put_given_non_exisiting_content_returns_notFound(){
        //Arrange
        var logger = new Mock<ILogger<ContentController>>();
        var toCreate = new ContentCreateDTO("Dette er en title", "Article");
        var content = new ContentUpdateDTO(toCreate);
        var repository = new Mock <IContentRepository>();
        repository.Setup(m => m.UpdateAsync(content.Id, content)).ReturnsAsync(Status.NotFound);
        var controller = new ContentController(logger.Object, repository.Object);

        //Act
        var response = await controller.Put(content.Id, content);

        //Arrange
        Assert.IsType<NotFoundResult>(response);
    }


    [Fact]
    public async void Delete_given_content_returns_the_NoContent_Status(){
        //Arrange
        var logger = new Mock<ILogger<ContentController>>();
        var repository = new Mock<IContentRepository>();
        var created = new ContentDTO(1, "Dette er en title", null, null, null, null, "Article");        
        repository.Setup(m => m.DeleteAsync(created.Id)).ReturnsAsync(Status.Deleted);
        var controller = new ContentController(logger.Object, repository.Object);

        //Act
        var response = await controller.Delete(created.Id);

        //Arrange
        Assert.IsType<NoContentResult>(response);
    }

    [Fact]
    public async void Delete_given_non_existing_content_returns_notfound_status(){
         //Arrange
        var logger = new Mock<ILogger<ContentController>>();
        var repository = new Mock<IContentRepository>();
        repository.Setup(m => m.DeleteAsync(98)).ReturnsAsync(Status.NotFound);
        var controller = new ContentController(logger.Object, repository.Object);

        //Act
        var response = await controller.Delete(98);

        //Arrange
        Assert.IsType<NotFoundResult>(response);
    }
    

    
}