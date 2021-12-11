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

public class ArticleControllerTest
{
    
    //TODO: hvad gør denne test?
    //[Fact]
    // public async void Get_Returns_Article_From_Repo()
    // {
    //     //Arrange 
    //     var filter = new FilterSetting();
    //     var logger = new Mock<ILogger<ArticleController>>();
    //     var expected = Array.Empty<ArticleDTO>();
    //     var repository = new Mock<IArticleRepository>();
    //     repository.Setup(m => m.ReadFromIdAsync()).ReturnsAsync(expected);
    //
    //     var controller = new ArticleController(logger.Object, repository.Object);
    //
    //     //act
    //     var actual = await controller.Get(filter);
    //
    //
    //     //Assert
    //     Assert.Equal(expected, actual);
    // }

    //TODO: Denne test skal laves færdig
    [Fact]
    public async void Create_creates_Article()
    {

        var logger = new Mock<ILogger<ArticleController>>();
        var toCreate = new ArticleCreateDTO{Title ="Dette er en title", Body ="Article" };  //should ArticleCreateDTO have an empty constructor
        var created = new ArticleDTO(1, "Dette er en title", null, null, null, null, "Article");        
        var repository = new Mock<IArticleRepository>();
        repository.Setup(m => m.CreateArticleAsync(toCreate)).ReturnsAsync(created);
        var controller = new ArticleController(logger.Object, repository.Object);

        // Act
        var result = await controller.Post(toCreate) as CreatedAtActionResult;

        // Assert
        Assert.Equal(created, result?.Value);
        Assert.Equal("Get", result?.ActionName);
        Assert.Equal(KeyValuePair.Create("Id", (object?)1), result?.RouteValues?.Single());
        
    } 
   
    [Fact]
    public async void Get_given_non_existing_id_returns_notfound()
    {
        //Arrange
        var logger = new Mock<ILogger<ArticleController>>();
        var repository = new Mock<IArticleRepository>();
        repository.Setup(m => m.ReadArticleFromIdAsync(42)).ReturnsAsync(default(ArticleDTO));
        var controller = new ArticleController(logger.Object, repository.Object);
        //Act
        var actual = await controller.GetFromId(42);
        //Assert
        Assert.IsType<NotFoundResult>(actual.Result);
    }  

    [Fact]
       public async void Get_given_existing_id_returns_article()
       {
        //Arrange
        var logger = new Mock<ILogger<ArticleController>>();
        var expected = new ArticleDTO(1, "Dette er en title", null, null, null, null, "Article");
        var repository = new Mock<IArticleRepository>();
        repository.Setup(m => m.ReadArticleFromIdAsync(1)).ReturnsAsync(expected);
        var controller = new ArticleController(logger.Object, repository.Object);
        //Act
        var actual = await controller.GetFromId(1);
        //Assert
        Assert.Equal(expected, actual.Value);
        
    }

    //TODO, denne test skal måske skrives om så den retunerer NotFound
    //[Fact]
    // public async void Get_given_non_existing_title_returns_null()
    // {
    //    //Arrange
    //     var logger = new Mock<ILogger<ArticleController>>();
    //     var repository = new Mock<IArticleRepository>();
    //     var created = new List<ArticleDTO> {new ArticleDTO(1, "This is a title", null, null, null, null, "Article")};
    //     repository.Setup(m => m.ReadArticlesFromTitleAsync("DOES_NOT_EXIST")).ReturnsAsync(created);
    //     var controller = new ArticleController(logger.Object, repository.Object);
    //     //Act
    //     var actual = await controller.GetFromTitle("DOES_NOT_EXIST");
    //     //Assert
    //     Assert.Null(actual.Result);
    // } 


     //[Fact]
       // public async void Get_given_existing_title_returns_article()
       // {
       //  //Arrange
       //  var logger = new Mock<ILogger<ArticleController>>();
       //  var expected = new List<ArticleDTO> {new ArticleDTO(1, "This is a title", null, null, null, null, "Article")};
       //  var repository = new Mock <IArticleRepository>();
       //  repository.Setup(m => m.ReadArticlesFromTitleAsync("title")).ReturnsAsync(expected);
       //  var controller = new ArticleController(logger.Object, repository.Object);
       //  //Act
       //  var actual = await controller.GetFromTitle("title");
       //  //Assert
       //  Assert.Equal(expected, actual.Value);
       //  
       // }


    [Fact]
    public async void Put_given_exisiting_article_updates_article()
    {
        //Arrange
        var logger = new Mock<ILogger<ArticleController>>();
        var article = new ArticleUpdateDTO();
        var repository = new Mock <IArticleRepository>();
        repository.Setup(m => m.UpdateArticleAsync(1, article)).ReturnsAsync(Status.Updated);
        var controller = new ArticleController(logger.Object, repository.Object);

        //Act
        var response = await controller.Put(1, article);

        //Arrange
        Assert.IsType<NoContentResult>(response);
    }

    [Fact]
    public async void Put_given_non_exisiting_article_returns_notFound()
    {
        //Arrange
        var logger = new Mock<ILogger<ArticleController>>();
        var article = new ArticleUpdateDTO();
        var repository = new Mock <IArticleRepository>();
        repository.Setup(m => m.UpdateArticleAsync(1, article)).ReturnsAsync(Status.NotFound);
        var controller = new ArticleController(logger.Object, repository.Object);

        //Act
        var response = await controller.Put(1, article);

        //Arrange
        Assert.IsType<NotFoundResult>(response);
    }


    [Fact]
    public async void Delete_given_article_returns_the_NoArticle_Status()
    {
        //Arrange
        var logger = new Mock<ILogger<ArticleController>>();
        var repository = new Mock<IArticleRepository>();
        var created = new ArticleDTO(1, "Dette er en title", null, null, null, null, "Article");        
        repository.Setup(m => m.DeleteArticleAsync(created.Id)).ReturnsAsync(Status.Deleted);
        var controller = new ArticleController(logger.Object, repository.Object);

        //Act
        var response = await controller.Delete(created.Id);

        //Arrange
        Assert.IsType<NoContentResult>(response);
    }

    [Fact]
    public async void Delete_given_non_existing_article_returns_notfound_status()
    {
        //Arrange
        var logger = new Mock<ILogger<ArticleController>>();
        var repository = new Mock<IArticleRepository>();
        repository.Setup(m => m.DeleteArticleAsync(98)).ReturnsAsync(Status.NotFound);
        var controller = new ArticleController(logger.Object, repository.Object);

        //Act
        var response = await controller.Delete(98);

        //Arrange
        Assert.IsType<NotFoundResult>(response);
    }
}