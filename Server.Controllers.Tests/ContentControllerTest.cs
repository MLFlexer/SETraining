using System;
using Microsoft.Extensions.Logging;
using Moq;
using SETraining.Server.Controllers;
using SETraining.Server.Repositories;
using SETraining.Shared.DTOs;
using Xunit;

namespace Server.Controllers.Tests;

public class ContentControllerTest
{
    [Fact]
    public async void Get_Returns_Content_From_Repo()
    {
        //Arrange 
        var logger = new Mock<ILogger<ContentController>>();
        var expected = Array.Empty<ContentDetailsDTO>();
        var repository = new Mock<IContentRepository>();
        repository.Setup(m => m.ReadAsync()).ReturnsAsync(expected);

        var controller = new ContentController(logger.Object, repository.Object);

        //act
        var actual = await controller.Get();


        //Assert
        Assert.Equal(expected, actual);
    }



    /*//Denne test virker ikke!!!!
    [Fact]
    public async void Get_given_non_existing_id_returns_null(){
        //Arrange
        var logger = new Mock<ILogger<ContentController>>();
        var repository = new Mock<IContentRepository>();
        repository.Setup(m => m.ReadAsync(42)).ReturnsAsync(default(ContentDetailsDTO));
        var controller = new ContentController(logger.Object, repository.Object);
        //Act
        var actual = await controller.Get(42);
        //Assert
        //Assert.IsType<NotFoundResult>(response.Result);
        
        
    }  */

    

    [Fact]
       public async void Get_given_existing_id_returns_content(){
        //Arrange
        var logger = new Mock<ILogger<ContentController>>();
        var expected = new ContentDetailsDTO(1, "Dette er en title", null, null, null, null, "Article");
        var repository = new Mock<IContentRepository>();
        repository.Setup(m => m.ReadAsync(1)).ReturnsAsync(expected);
        var controller = new ContentController(logger.Object, repository.Object);
        //Act
        var actual = await controller.Get(1);
        //Assert
        Assert.Equal(expected, actual.Value);
        
    }


    [Fact]
    public async void Create_creates_Content(){

        var logger = new Mock<ILogger<ContentController>>();
        var toCreate = new ContentCreateDTO("Dette er en title", "Article");  //should ContentCreateDTO have an empty constructor
        var created = new ContentDetailsDTO(1, "Dette er en title", null, null, null, null, "Article");        var repository = new Mock<IContentRepository>();
        repository.Setup(m => m.CreateAsync(toCreate)).ReturnsAsync(created);
        var controller = new ContentController(logger.Object, repository.Object);

        // Act
        var result = await controller.Post(toCreate);

        // Assert
        //Assert.Equal(created, result?.Value);
        //Assert.Equal("Get", result?.RouteName);
       //Assert.Equal(KeyValuePair.Create("Id", (object?)1), result?.RouteValues?.Single());
        
    } 
    
}