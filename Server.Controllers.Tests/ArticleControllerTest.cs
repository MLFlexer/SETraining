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
using SETraining.Shared.Models;

namespace Server.Controllers.Tests;

public class ArticleControllerTest
{

    //TODO: Denne test skal laves færdig
    [Fact]
    public async void Create_creates_Article()
    {
        var logger = new Mock<ILogger<ArticleController>>();
        var toCreate = new ArticleCreateDTO();
        var created = new ArticleDTO(1, "Dette er en title", ArticleType.Written, DateTime.Today,null, null, DifficultyLevel.Expert, null, "Article", null, null);        
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
    public async void GetAll_given_existing_returns_articles(){
        //Arrange
        var logger = new Mock<ILogger<ArticleController>>();
        var expected = Array.Empty<ArticlePreviewDTO>();
        var repository = new Mock<IArticleRepository>();
        repository.Setup(m => m.ReadAllArticlesAsync()).ReturnsAsync(expected);
        var controller = new ArticleController(logger.Object, repository.Object);
        //Act
        var actual = await controller.Get();
        
        //Assert
        Assert.Equal(expected, actual.Value);
    }

    // TODO: skal lige laves færdig, rammer et problem ift default(ArticleDTO) og tror det omhandler den der option klasse.
    //  [Fact]
    // public async void Get_given_non_existing_returns_notfound(){
    //     //Arrange
    //     var logger = new Mock<ILogger<ArticleController>>();
    //     var repository = new Mock<IArticleRepository>();
    //     repository.Setup(m => m.ReadAllArticlesAsync()).ReturnsAsync(default(ArticleDTO));
    //     var controller = new ArticleController(logger.Object, repository.Object);
    //     //Act
    //     var actual = await controller.Get();
        
    //     //Assert
    //     Assert.Equal(expected, actual);
    // }
   
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
        var expected = new ArticleDTO(1, "Dette er en title", ArticleType.Written, DateTime.Today,null, null, DifficultyLevel.Expert, null, "Article", null, null);
        var repository = new Mock<IArticleRepository>();
        repository.Setup(m => m.ReadArticleFromIdAsync(1)).ReturnsAsync(expected);
        var controller = new ArticleController(logger.Object, repository.Object);
        //Act
        var actual = await controller.GetFromId(1);
        //Assert
        Assert.Equal(expected, actual.Value);
        
    }

    //TODO, denne test skal måske skrives om så den retunerer NotFound.
    [Fact]
    public async void Get_given_non_existing_paramerets_returns_null()
    {
       //Arrange
        var logger = new Mock<ILogger<ArticleController>>();
        var repository = new Mock<IArticleRepository>();
        var created = new List<ArticlePreviewDTO> { new (1, "This is a title", ArticleType.Written, DateTime.Today, null, null, DifficultyLevel.Expert, 4, null)};
        repository.Setup(m => m.ReadAllArticlesFromParametersAsync("DOES_NOT_EXIST", "2", new string[]{"java"})).ReturnsAsync(created);
        var controller = new ArticleController(logger.Object, repository.Object);
        //Act
        //var actual = await controller.GetFromTitle("DOES_NOT_EXIST");
        var actual = await controller.GetFromParameters("DOES_NOT_EXIST", "2", null);
        //Assert

        Assert.IsType<NotFoundResult>(actual.Result);
    } 

    //TODO : der skal blive testet med sproget C# til når dette er oppe og køre
     [Fact]
       public async void Get_given_existing_parameters_returns_article()
       {
        //Arrange
        var logger = new Mock<ILogger<ArticleController>>();
        var expected = new List<ArticlePreviewDTO> {new ArticlePreviewDTO(1, "This is a title", ArticleType.Written, DateTime.Today, null, new string[]{"Java"}, DifficultyLevel.Expert, 3,  null)};
        var repository = new Mock <IArticleRepository>();
        repository.Setup(m => m.ReadAllArticlesFromParametersAsync("title", "3", new string[]{"Java"})).ReturnsAsync(expected);
        var controller = new ArticleController(logger.Object, repository.Object);
        //Act
        var actual = await controller.GetFromParameters("title", "3", new string[]{"Java"});
        //Assert
        Assert.Equal(expected, actual.Value);
        
       }

    //TODO : alle disse er navne på test som skal ind i repo!!
    // [Fact]
    // public async void Get_given_existing_title_AND_no_matching_parameter_on_difficulty_and_language_returns_article(){

    // }
    // [Fact]
    // public async void Get_given_difficulty_AND_no_matching_parameter_on_title_and_language_returns_article(){
    // }
       
    // [Fact]
    // public async void Get_given_language_AND_no_matching_parameter_on_title_and_difficulty_returns_article(){
    // }
    

    // [Fact]
    // public async void Get_given_title_and_difficulty_AND_no_matching_parameter_on_language_returns_article(){}


    // [Fact]
    // public async void Get_given_title_and_language_AND_no_matching_parameter_on_difficulty_returns_article(){}


    // [Fact]
    // public async void Get_given_difficulty_and_language_AND_no_matching_parameter_on_title_returns_article(){}

    // [Fact]
    // public async void Get_given_one_language_returns_article(){}

       


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
        var created = new ArticleDTO(1, "Dette er en title", ArticleType.Written, DateTime.Today, null, null, DifficultyLevel.Expert, null, "Article", null, null);        
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