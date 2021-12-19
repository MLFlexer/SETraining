using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SETraining.Server;
using SETraining.Server.Controllers;
using SETraining.Server.Repositories;
using SETraining.Shared.DTOs;
using Xunit;

namespace Server.Controllers.Tests;

public class ProgrammingLanguagesControllerTest
{
    [Fact]
    public async void Get_returns_ProgrammingLanguages_from_repo()
    {
        //Arrange 
        var logger = new Mock<ILogger<ProgrammingLanguagesController>>();
        var expected = Array.Empty<ProgrammingLanguageDTO>();
        var repository = new Mock<IProgrammingLanguagesRepository>();
        repository.Setup(m => m.ReadAsync()).ReturnsAsync(expected);

        var controller = new ProgrammingLanguagesController(logger.Object, repository.Object);

        //act
        var actual = await controller.Get();

        //Assert
        Assert.Equal(expected, actual.Value);
    }

     [Fact]
     public async void Get_given_non_existing_name_returns_NotFound(){
         //Arrange
         var logger = new Mock<ILogger<ProgrammingLanguagesController>>();
         var repository = new Mock<IProgrammingLanguagesRepository>();
         repository.Setup(m => m.ReadAsync("NotALanguage")).ReturnsAsync(default(ProgrammingLanguageDTO));
         var controller = new ProgrammingLanguagesController(logger.Object, repository.Object);

         //Act
         var response = await controller.Get("NotALanguage");
         Assert.IsType<NotFoundResult>(response.Result);
     } 

    [Fact]
    public async void Get_given_existing_name_returns_ProgrammingLanguage()
    {
        //Arrange
        var logger = new Mock<ILogger<ProgrammingLanguagesController>>();
        var expected = new ProgrammingLanguageDTO("Java");
        var repository = new Mock<IProgrammingLanguagesRepository>();
        repository.Setup(m => m.ReadAsync("Java")).ReturnsAsync(expected);
        var controller = new ProgrammingLanguagesController(logger.Object, repository.Object);
        //Act
        var actual = await controller.Get("Java");
        //Assert
        Assert.Equal(expected, actual.Value);
    }

    //TODO: fix
    //[Fact]
    public async void Create_creates_ProgrammingLanguage()
    {
        //TODO FIX LOGIC
        var logger = new Mock<ILogger<ProgrammingLanguagesController>>();
        var toCreate = new ProgrammingLanguageCreateDTO{Name = "NewLanguage"};
        var created = new ProgrammingLanguageDTO("NewLanguage");
        var repository = new Mock<IProgrammingLanguagesRepository>();
        repository.Setup(m => m.CreateAsync(toCreate)).ReturnsAsync(created);
        var controller = new ProgrammingLanguagesController(logger.Object, repository.Object);

        // Act
        var result = await controller.Post(toCreate) as CreatedAtRouteResult;

        // Assert
        Assert.Equal(created, result?.Value);
        Assert.Equal("Get", result?.RouteName);
        Assert.Equal(KeyValuePair.Create("Name", (object?)"NewLanguage"), result?.RouteValues?.Single());
    }
}
