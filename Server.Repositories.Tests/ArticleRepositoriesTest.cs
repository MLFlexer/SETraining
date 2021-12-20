using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SETraining.Server.Contexts;
using SETraining.Server.Repositories;
using SETraining.Shared.DTOs;
using SETraining.Shared.Models;
using Xunit;
using SETraining.Shared;
using System.Linq;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace Server.Repositories.Tests;

public class ArticleRepositoriesTest : IDisposable
{
    private readonly SETrainingContext _context;
    private readonly ArticleRepository _repository;
    private bool _disposedValue;
    private const string _mockImageURL = "https://localhost:7021/mock_img";
    private const string _mockVideoURL = "https://localhost:7021/mock_vid";
    
    public ArticleRepositoriesTest()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<SETrainingContext>();
        builder.UseSqlite(connection);
        var context = new SETrainingContext(builder.Options);
        context.Database.EnsureCreated();

        var java = new ProgrammingLanguage("Java");
        var csharp = new ProgrammingLanguage("CSharp");
        var javascript = new ProgrammingLanguage("Javascript");
        var fsharp = new ProgrammingLanguage("FSharp");
        var python = new ProgrammingLanguage("Python");
        var golang = new ProgrammingLanguage("Golang");
        context.AddRange(
                new Article("Introduction to Java", ArticleType.Written, DateTime.Today.ToUniversalTime(), DifficultyLevel.Expert)
                    {
                        Description= "Description",
                        ProgrammingLanguages = new[] { java },
                        AvgRating = 2, 
                        Body = "Text",
                        ImageURL = _mockImageURL,
                    },

                new Article("Introduction to CSharp", ArticleType.Written, DateTime.Today.ToUniversalTime(), DifficultyLevel.Expert)
                    {
                        ProgrammingLanguages = new[] { csharp },
                        AvgRating = 1, 
                        Body = "Coding is fun",
                        ImageURL = _mockImageURL
                    },
                
                new Article("Introduction to Java", ArticleType.Written, DateTime.Today.ToUniversalTime(), DifficultyLevel.Expert) 
                    {
                        Description= "Description",
                        ProgrammingLanguages = new[] { java },
                        AvgRating = 3, 
                        Body = "Text",
                        ImageURL = _mockImageURL
                    },
                
                new Article("Introduction to CSharp", ArticleType.Written, DateTime.Today.ToUniversalTime(), DifficultyLevel.Expert)
                    {
                        Description= "Learn how to code in CSharp",
                        ProgrammingLanguages = new[] { csharp },
                        AvgRating = 3, 
                        Body = "Learning",
                        ImageURL = _mockImageURL
                    },
                
                new Article("Javascript", ArticleType.Video, DateTime.Today.ToUniversalTime(), DifficultyLevel.Novice) 
                    {
                        Description= "Description",
                        ProgrammingLanguages = new[] { javascript },
                        AvgRating = 2, 
                        Body = "Read this article",
                        ImageURL = _mockImageURL,
                        VideoURL = _mockVideoURL
                    },

                new Article("Golang", ArticleType.Video, DateTime.Today.ToUniversalTime(), DifficultyLevel.Novice) {
                    Description= "This is a description",
                    ProgrammingLanguages = new[] { golang },
                    AvgRating = 1, 
                    Body = "Learn how to write in go",
                    ImageURL = _mockImageURL,
                    VideoURL = _mockVideoURL

                    },
                
                new Article("Learn how to write Go or FSharp", ArticleType.Written, DateTime.Today.ToUniversalTime(), DifficultyLevel.Intermediate) {
                    Description= "Description",
                    ProgrammingLanguages = new[] { golang, fsharp },
                    AvgRating = 3, 
                    Body = "Read this article",
                    ImageURL = _mockImageURL,
                    
                    },
                 new Article("Python", ArticleType.Video, DateTime.Today.ToUniversalTime(), DifficultyLevel.Intermediate) {
                    Description= "This is python",
                    ProgrammingLanguages = new[] { python },
                    AvgRating = 1, 
                    Body = "Learn how to write in the very popular language Python",
                    ImageURL = _mockImageURL,
                    VideoURL = _mockVideoURL
                    },
                    new Article("Testing with python", ArticleType.Written, DateTime.Today.ToUniversalTime(), DifficultyLevel.Expert) {
                    Description= "This a written article",
                    ProgrammingLanguages = new[] { python },
                    AvgRating = 3, 
                    Body = "Very interesting",
                    ImageURL = _mockImageURL

                    }

            );


        context.SaveChanges();

        _context = context;
        _repository = new ArticleRepository(_context);
        
    }

    [Fact]
    public async void Create_new_Article_generates_id()
    {
        //Arrange 
        var programmingLangs = new List<string>() {"Java", "Go"};
        var toCreate = new ArticleCreateDTO{Title ="Introduction to Go", Body = "Article", ProgrammingLanguages = programmingLangs};
        //Act
        var created = await _repository.CreateArticleAsync(toCreate);
        
        //Assert
        Assert.Equal(10, created.Id);
        Assert.Equal("Introduction to Go", created.Title);
        Assert.Equal(programmingLangs,created.ProgrammingLanguages);
    }

    [Fact]
    public async Task Read_returns_all()
    {
        //Arrange 
        var expected = new List<ArticlePreviewDTO>()
        {
            new(1, "Introduction to Java", ArticleType.Written, DateTime.Today.ToUniversalTime(), "Description",
                new string[] {"Java"}, DifficultyLevel.Expert, 2, _mockImageURL),
            new(2, "Introduction to CSharp", ArticleType.Written, DateTime.Today.ToUniversalTime(), null,
                new string[] {"CSharp"}, DifficultyLevel.Expert, 1, _mockImageURL),
            new(3, "Introduction to Java", ArticleType.Written, DateTime.Today.ToUniversalTime(), "Description",
                new string[] {"Java"}, DifficultyLevel.Expert, 3, _mockImageURL),
            new(4, "Introduction to CSharp", ArticleType.Written, DateTime.Today.ToUniversalTime(),
                "Learn how to code in CSharp", new string[] {"CSharp"}, DifficultyLevel.Expert, 3, _mockImageURL),
            new(5, "Javascript", ArticleType.Video, DateTime.Today.ToUniversalTime(), "Description",
                new string[] {"Javascript"}, DifficultyLevel.Novice, 2, _mockImageURL),
            new(6, "Golang", ArticleType.Video, DateTime.Today.ToUniversalTime(), "This is a description",
                new string[] {"Golang"}, DifficultyLevel.Novice, 1, _mockImageURL),
            new(7, "Learn how to write Go or FSharp", ArticleType.Written, DateTime.Today.ToUniversalTime(), "Description",
                new string[] {"FSharp","Golang"}, DifficultyLevel.Intermediate, 3, _mockImageURL),
            new(8, "Python", ArticleType.Video, DateTime.Today.ToUniversalTime(), "This is python",
                new string[] {"Python"}, DifficultyLevel.Intermediate, 1, _mockImageURL),
            new(9, "Testing with python", ArticleType.Written, DateTime.Today.ToUniversalTime(),
                "This a written article", new string[] {"Python"}, DifficultyLevel.Expert, 3, _mockImageURL)
        };
        
        //Act 
        var actual = (await _repository.ReadAllArticlesAsync()).Value.ToList();
        
        //Assert
        for (var i = 0; i < actual.Count; i++)
        {
            Assert.Equal(expected[i].Id, actual[i].Id);
            Assert.Equal(expected[i].Title, actual[i].Title);
            Assert.Equal(expected[i].Type, actual[i].Type);
            Assert.Equal(expected[i].Created.Date, actual[i].Created.Date);
            Assert.Equal(expected[i].Description, actual[i].Description);
            Assert.Equal(expected[i].ProgrammingLanguages, actual[i].ProgrammingLanguages);
            Assert.Equal(expected[i].Difficulty, actual[i].Difficulty);
            Assert.Equal(expected[i].AvgRating, actual[i].AvgRating);
            Assert.Equal(expected[i].ImageURL, actual[i].ImageURL);
        }

    }
    
    [Fact]
    public async Task Read_given_non_existing_id_returns_null()
    {
        var actual = await _repository.ReadArticleFromIdAsync(99);
        Assert.True(actual.IsNone);
    }

     [Fact]
    public async void Read_given_non_existing_title_returns_null()
    {
        var actual = await _repository.ReadArticlesFromTitleAsync("THISISNOTWORKING");
        Assert.True(actual.IsNone);
    }
    
    [Fact]
    public async Task Read_given_existing_id_returns_Article()
    {
        //Arrange
        var expected = new ArticleDTO(1, "Introduction to Java", ArticleType.Written, DateTime.Today,"Test", null, DifficultyLevel.Expert, null, "Article", null, null);
        
        //Act
        var actual = (await _repository.ReadArticleFromIdAsync(1)).Value;
        
        //Assert
        Assert.Equal(expected.Id, actual.Id);
        Assert.Equal(expected.Title, actual.Title);
    }


   
     [Fact]
    public async void Read_given_exsiting_title_returns_ArticleList()
    {
        //Arrange
        var expected_1 = new ArticlePreviewDTO(2,"Introduction to CSharp", ArticleType.Written, DateTime.Today, null, new string[]{"CSharp"}, DifficultyLevel.Expert, 1,  null);
        var expected_2 = new ArticlePreviewDTO(4, "Introduction to CSharp", ArticleType.Written,DateTime.Today, "Learn how to code in CSharp", new string[]{"CSharp"}, DifficultyLevel.Expert, 3,  null);

        //Act
        var actual = await _repository.ReadArticlesFromTitleAsync("CSharp");
        IEnumerable<ArticlePreviewDTO> actualValue = actual.Value;

        var actualArray = actualValue.ToArray();


        Assert.Equal(expected_1.Id, actualArray[0].Id);
        Assert.Equal(expected_1.Title, actualArray[0].Title);
        Assert.Equal(expected_1.Description, actualArray[0].Description);
        Assert.Equal(expected_1.ProgrammingLanguages, actualArray[0].ProgrammingLanguages);
        Assert.Equal(expected_1.Difficulty, actualArray[0].Difficulty);
        Assert.Equal(expected_1.AvgRating, actualArray[0].AvgRating);


        Assert.Equal(expected_2.Id, actualArray[1].Id);
        Assert.Equal(expected_2.Title, actualArray[1].Title);
        Assert.Equal(expected_2.Description, actualArray[1].Description);
        Assert.Equal(expected_2.ProgrammingLanguages, actualArray[1].ProgrammingLanguages);
        Assert.Equal(expected_2.Difficulty, actualArray[1].Difficulty);
        Assert.Equal(expected_2.AvgRating, actualArray[1].AvgRating);
    }
    
    [Fact]
    public async Task UpdateAsync_given_non_existing_id_returns_NotFound()
    {
        var content = new ArticleUpdateDTO
        {
            Title ="Introduction to Java", 
            Description = "Description", 
            ProgrammingLanguages = new List<string>(){"Java 4", "Java 5"}, 
            Difficulty = DifficultyLevel.Expert, 
            AvgRating = null,
            Body = "Article"
        };
        
        var updated = await _repository.UpdateArticleAsync(42, content);
        
        Assert.Equal(Status.NotFound, updated);
        
    }

    [Fact]
    public async Task  Update_updates_existing_Content()
    {
        var content = new ArticleUpdateDTO
        {
            Title ="Introduction to Java2", 
            Description = "This is updated", 
            ProgrammingLanguages = new List<string>(){}, 
            Difficulty = DifficultyLevel.Expert, 
            AvgRating = 20,
            Body = "Article"
        };
        
        var updated = await _repository.UpdateArticleAsync(1, content);
        
        Assert.Equal(Status.Updated, updated);
        
        var option = await _repository.ReadArticleFromIdAsync(1);
        var UpdatedContent = option.Value;
        
        Assert.Equal("This is updated", UpdatedContent.Description);
        Assert.Equal(20, UpdatedContent.AvgRating);
        Assert.Equal("Introduction to Java2", UpdatedContent.Title);
        Assert.Empty(UpdatedContent.ProgrammingLanguages);
    }
    
    [Fact]
    public async Task  Delete_given_non_existing_id_returns_NotFound()
    {
        var actual = await _repository.DeleteArticleAsync(44);
        Assert.Equal(Status.NotFound, actual);
    }
    
    [Fact]
    public async Task Delete_given_existing_id_deletes()
    {
        var actual = await _repository.DeleteArticleAsync(2);

        Assert.Equal(Status.Deleted, actual);
        Assert.Null(await _context.Articles.FindAsync(2));
    }

    [Fact]
    public async Task Get_given_non_existing_title_and_empty_difficulty_and_language_returns_null(){
        var actual = await _repository.ReadAllArticlesFromParametersAsync("THISDOESNOOOOOOOOTEXIIIIST", "", null);   
        Assert.False(actual.IsSome);
    }

    [Fact]
    public async Task Get_given_non_existing_difficulty_and_empty_title_and_language_returns_null(){
        var actual = await _repository.ReadAllArticlesFromParametersAsync("", "100000", null);   
        Assert.False(actual.IsSome);
    }

    [Fact]
    public async Task Get_given_non_existing_language_and_empty_difficulty_and_title_returns_null(){
        var actual = await _repository.ReadAllArticlesFromParametersAsync("", "", new string[]{"NOTALANGUAGE"});   
        Assert.False(actual.IsSome);
    }


    [Fact]
    public async Task Get_given_existing_title_AND_no_matching_parameter_on_difficulty_and_language_returns_article(){
        //Arrange
        var expected1 = new ArticlePreviewDTO(1, "Introduction to Java", ArticleType.Written, DateTime.Today.ToUniversalTime(), "Description", new string[]{"Java"}, DifficultyLevel.Expert, 2, _mockImageURL);      
        var expected2 = new ArticlePreviewDTO(3, "Introduction to Java", ArticleType.Written, DateTime.Today.ToUniversalTime(), "Description", new string[]{"Java"}, DifficultyLevel.Expert, 3,  _mockImageURL);        
        var expected3 = new ArticlePreviewDTO(5, "Javascript", ArticleType.Video, DateTime.Today.ToUniversalTime(), "Description", new string[]{"Javascript"}, DifficultyLevel.Novice, 2,  _mockImageURL);        

        //Act
        var actual = await _repository.ReadAllArticlesFromParametersAsync("Java", "", null);   
        IEnumerable<ArticlePreviewDTO> actualValue = actual.Value;
        var actualArray = actualValue.ToArray();

        // foreach(var a in actualArray){
        //     Console.WriteLine("SKRIVER HER!!!" + a.Title);
        // }

        //Assert
        Assert.Equal(expected1.Id, actualArray[0].Id);
        Assert.Equal(expected1.Title, actualArray[0].Title);
        Assert.Equal(expected1.Type, actualArray[0].Type);
        Assert.Equal(expected1.Created.Date, actualArray[0].Created.Date);
        Assert.Equal(expected1.Description, actualArray[0].Description);
        Assert.Equal(expected1.ProgrammingLanguages, actualArray[0].ProgrammingLanguages);
        Assert.Equal(expected1.Difficulty, actualArray[0].Difficulty);
        Assert.Equal(expected1.AvgRating, actualArray[0].AvgRating);
        Assert.Equal(expected1.ImageURL, actualArray[0].ImageURL);
       
        Assert.Equal(expected2.Id, actualArray[1].Id);
        Assert.Equal(expected2.Title, actualArray[1].Title);
        Assert.Equal(expected2.Type, actualArray[1].Type);
        Assert.Equal(expected2.Created.Date, actualArray[1].Created.Date);
        Assert.Equal(expected2.Description, actualArray[1].Description);
        Assert.Equal(expected2.ProgrammingLanguages, actualArray[1].ProgrammingLanguages);
        Assert.Equal(expected2.Difficulty, actualArray[1].Difficulty);
        Assert.Equal(expected2.AvgRating, actualArray[1].AvgRating);
        Assert.Equal(expected2.ImageURL, actualArray[1].ImageURL);
        
        Assert.Equal(expected3.Id, actualArray[2].Id);
        Assert.Equal(expected3.Title, actualArray[2].Title);
        Assert.Equal(expected3.Type, actualArray[2].Type);
        Assert.Equal(expected3.Created.Date, actualArray[2].Created.Date);
        Assert.Equal(expected3.Description, actualArray[2].Description);
        Assert.Equal(expected3.ProgrammingLanguages, actualArray[2].ProgrammingLanguages);
        Assert.Equal(expected3.Difficulty, actualArray[2].Difficulty);
        Assert.Equal(expected3.AvgRating, actualArray[2].AvgRating);
        Assert.Equal(expected3.ImageURL, actualArray[2].ImageURL);
    }

    [Fact]
    public async Task Get_given_difficulty_AND_no_matching_parameter_on_title_and_language_returns_article(){
        //Arrange
        var expected1 = new ArticlePreviewDTO(5, "Javascript", ArticleType.Video, DateTime.Today.ToUniversalTime(), "Description", new string[]{"Javascript"}, DifficultyLevel.Novice, 2,  _mockImageURL);
        var expected2 = new ArticlePreviewDTO(6, "Golang", ArticleType.Video, DateTime.Today.ToUniversalTime(), "This is a description", new string[]{"Golang"}, DifficultyLevel.Novice, 1,  _mockImageURL);


        //Act
        var actual = await _repository.ReadAllArticlesFromParametersAsync("", "1", null);   
        IEnumerable<ArticlePreviewDTO> actualValue = actual.Value;
        var actualArray = actualValue.ToArray();
        

        //Assert
        Assert.Equal(expected1.Id, actualArray[0].Id);
        Assert.Equal(expected1.Title, actualArray[0].Title);
        Assert.Equal(expected1.Type, actualArray[0].Type);
        Assert.Equal(expected1.Created.Date, actualArray[0].Created.Date);
        Assert.Equal(expected1.Description, actualArray[0].Description);
        Assert.Equal(expected1.ProgrammingLanguages, actualArray[0].ProgrammingLanguages);
        Assert.Equal(expected1.Difficulty, actualArray[0].Difficulty);
        Assert.Equal(expected1.AvgRating, actualArray[0].AvgRating);
        Assert.Equal(expected1.ImageURL, actualArray[0].ImageURL);
       
        Assert.Equal(expected2.Id, actualArray[1].Id);
        Assert.Equal(expected2.Title, actualArray[1].Title);
        Assert.Equal(expected2.Type, actualArray[1].Type);
        Assert.Equal(expected2.Created.Date, actualArray[1].Created.Date);
        Assert.Equal(expected2.Description, actualArray[1].Description);
        Assert.Equal(expected2.ProgrammingLanguages, actualArray[1].ProgrammingLanguages);
        Assert.Equal(expected2.Difficulty, actualArray[1].Difficulty);
        Assert.Equal(expected2.AvgRating, actualArray[1].AvgRating);
        Assert.Equal(expected2.ImageURL, actualArray[1].ImageURL);
    }
       
    [Fact]
    public async Task Get_given_language_AND_no_matching_parameter_on_title_and_difficulty_returns_article(){
        //Arrange
        var expected1 = new ArticlePreviewDTO(2,"Introduction to CSharp", ArticleType.Written, DateTime.Today.ToUniversalTime(), null, new string[]{"CSharp"}, DifficultyLevel.Expert, 1,  _mockImageURL);
        var expected2 = new ArticlePreviewDTO(4, "Introduction to CSharp", ArticleType.Written,DateTime.Today.ToUniversalTime(), "Learn how to code in CSharp", new string[]{"CSharp"}, DifficultyLevel.Expert, 3, _mockImageURL);
    
        //Act
        var actual = await _repository.ReadAllArticlesFromParametersAsync("", "", new string[]{"CSharp"});   
        IEnumerable<ArticlePreviewDTO> actualValue = actual.Value;
        var actualArray = actualValue.ToArray();
        

        //Assert
        Assert.Equal(expected1.Id, actualArray[0].Id);
        Assert.Equal(expected1.Title, actualArray[0].Title);
        Assert.Equal(expected1.Type, actualArray[0].Type);
        Assert.Equal(expected1.Created.Date, actualArray[0].Created.Date);
        Assert.Equal(expected1.Description, actualArray[0].Description);
        Assert.Equal(expected1.ProgrammingLanguages, actualArray[0].ProgrammingLanguages);
        Assert.Equal(expected1.Difficulty, actualArray[0].Difficulty);
        Assert.Equal(expected1.AvgRating, actualArray[0].AvgRating);
        Assert.Equal(expected1.ImageURL, actualArray[0].ImageURL);
       
        Assert.Equal(expected2.Id, actualArray[1].Id);
        Assert.Equal(expected2.Title, actualArray[1].Title);
        Assert.Equal(expected2.Type, actualArray[1].Type);
        Assert.Equal(expected2.Created.Date, actualArray[1].Created.Date);
        Assert.Equal(expected2.Description, actualArray[1].Description);
        Assert.Equal(expected2.ProgrammingLanguages, actualArray[1].ProgrammingLanguages);
        Assert.Equal(expected2.Difficulty, actualArray[1].Difficulty);
        Assert.Equal(expected2.AvgRating, actualArray[1].AvgRating);
        Assert.Equal(expected2.ImageURL, actualArray[1].ImageURL);
    }
    

    [Fact]
    public async Task Get_given_title_and_difficulty_AND_no_matching_parameter_on_language_returns_article(){
        //Arrange
        var expected1 = new ArticlePreviewDTO(8, "Python", ArticleType.Video, DateTime.Today.ToUniversalTime(), "This is python", new string[]{"Python"}, DifficultyLevel.Intermediate, 1,  _mockImageURL);  

        //Act
        var actual = await _repository.ReadAllArticlesFromParametersAsync("Python", "2", null);   
        IEnumerable<ArticlePreviewDTO> actualValue = actual.Value;
        var actualArray = actualValue.ToArray();


        //Assert
        Assert.Equal(expected1.Id, actualArray[0].Id);
        Assert.Equal(expected1.Title, actualArray[0].Title);
        Assert.Equal(expected1.Type, actualArray[0].Type);
        Assert.Equal(expected1.Created.Date, actualArray[0].Created.Date);
        Assert.Equal(expected1.Description, actualArray[0].Description);
        Assert.Equal(expected1.ProgrammingLanguages, actualArray[0].ProgrammingLanguages);
        Assert.Equal(expected1.Difficulty, actualArray[0].Difficulty);
        Assert.Equal(expected1.AvgRating, actualArray[0].AvgRating);
        Assert.Equal(expected1.ImageURL, actualArray[0].ImageURL);
   }


    [Fact]
    public async Task Get_given_title_and_language_AND_no_matching_parameter_on_difficulty_returns_article(){
        //Arrange
        var expected1 = new ArticlePreviewDTO(1, "Introduction to Java", ArticleType.Written, DateTime.Today.ToUniversalTime(), "Description", new string[]{"Java"}, DifficultyLevel.Expert, 2,  _mockImageURL );      
        var expected2 = new ArticlePreviewDTO(3, "Introduction to Java", ArticleType.Written, DateTime.Today.ToUniversalTime(), "Description", new string[]{"Java"}, DifficultyLevel.Expert, 3,  _mockImageURL );        

        //Act
        var actual = await _repository.ReadAllArticlesFromParametersAsync("Java", "", new string[]{"Java"});   
        IEnumerable<ArticlePreviewDTO> actualValue = actual.Value;
        var actualArray = actualValue.ToArray();

        //Assert
        Assert.Equal(expected1.Id, actualArray[0].Id);
        Assert.Equal(expected1.Title, actualArray[0].Title);
        Assert.Equal(expected1.Type, actualArray[0].Type);
        Assert.Equal(expected1.Created.Date, actualArray[0].Created.Date);
        Assert.Equal(expected1.Description, actualArray[0].Description);
        Assert.Equal(expected1.ProgrammingLanguages, actualArray[0].ProgrammingLanguages);
        Assert.Equal(expected1.Difficulty, actualArray[0].Difficulty);
        Assert.Equal(expected1.AvgRating, actualArray[0].AvgRating);
        Assert.Equal(expected1.ImageURL, actualArray[0].ImageURL);
       
        Assert.Equal(expected2.Id, actualArray[1].Id);
        Assert.Equal(expected2.Title, actualArray[1].Title);
        Assert.Equal(expected2.Type, actualArray[1].Type);
        Assert.Equal(expected2.Created.Date, actualArray[1].Created.Date);
        Assert.Equal(expected2.Description, actualArray[1].Description);
        Assert.Equal(expected2.ProgrammingLanguages, actualArray[1].ProgrammingLanguages);
        Assert.Equal(expected2.Difficulty, actualArray[1].Difficulty);
        Assert.Equal(expected2.AvgRating, actualArray[1].AvgRating);
        Assert.Equal(expected2.ImageURL, actualArray[1].ImageURL);
    }


    [Fact]
    public async Task Get_given_difficulty_and_language_AND_no_matching_parameter_on_title_returns_article(){
        //Arrange
         var expected1 = new ArticlePreviewDTO(9, "Testing with python", ArticleType.Written, DateTime.Today.ToUniversalTime(), "This a written article", new string[]{"Python"}, DifficultyLevel.Expert, 3,  _mockImageURL); 

        //Act
        var actual = await _repository.ReadAllArticlesFromParametersAsync("", "3", new string[]{"Python"});   
        IEnumerable<ArticlePreviewDTO> actualValue = actual.Value;
        var actualArray = actualValue.ToArray();
        

        //Assert
        Assert.Equal(expected1.Id, actualArray[0].Id);
        Assert.Equal(expected1.Title, actualArray[0].Title);
        Assert.Equal(expected1.Type, actualArray[0].Type);
        Assert.Equal(expected1.Created.Date, actualArray[0].Created.Date);
        Assert.Equal(expected1.Description, actualArray[0].Description);
        Assert.Equal(expected1.ProgrammingLanguages, actualArray[0].ProgrammingLanguages);
        Assert.Equal(expected1.Difficulty, actualArray[0].Difficulty);
        Assert.Equal(expected1.AvgRating, actualArray[0].AvgRating);
        Assert.Equal(expected1.ImageURL, actualArray[0].ImageURL);
       
    }

    [Fact]
    public async Task Get_given_title_difficulty_languages_returns_article(){
        //Arrange
        var expected1 = new ArticlePreviewDTO(7, "Learn how to write Go or FSharp", ArticleType.Written, DateTime.Today.ToUniversalTime(), "Description", new string[]{"FSharp", "Golang"}, DifficultyLevel.Intermediate, 3, _mockImageURL);

        //Act
        var actual = await _repository.ReadAllArticlesFromParametersAsync("Go", "2", new string[]{"Golang"});   
        IEnumerable<ArticlePreviewDTO> actualValue = actual.Value;
        var actualArray = actualValue.ToArray();

        //Assert
        Assert.Equal(expected1.Id, actualArray[0].Id);
        Assert.Equal(expected1.Title, actualArray[0].Title);
        Assert.Equal(expected1.Type, actualArray[0].Type);
        Assert.Equal(expected1.Created.Date, actualArray[0].Created.Date);
        Assert.Equal(expected1.Description, actualArray[0].Description);
        Assert.Equal(expected1.ProgrammingLanguages, actualArray[0].ProgrammingLanguages);
        Assert.Equal(expected1.Difficulty, actualArray[0].Difficulty);
        Assert.Equal(expected1.AvgRating, actualArray[0].AvgRating);
        Assert.Equal(expected1.ImageURL, actualArray[0].ImageURL);
    }



    //Her er bare alle ArticleDTO's så kan genbruges i testene!!
    //var expected1 = new ArticleDTO(1, "Introduction to Java", ArticleType.Written, DateTime.Today.ToUniversalTime(), "Description", new string[]{"Java"}, DifficultyLevel.Expert, 2, "Text", null);      
    //var expected1 = new ArticleDTO(2,"Introduction to CSharp", ArticleType.Written, DateTime.Today.ToUniversalTime(), null, new string[]{"CSharp"}, DifficultyLevel.Expert, 1, "Coding is fun", null);
    //var expected2 = new ArticleDTO(3, "Introduction to Java", ArticleType.Written, DateTime.Today.ToUniversalTime(), "Description", new string[]{"Java"}, DifficultyLevel.Expert, 3, "Text", null);        
    //var expected2 = new ArticleDTO(4, "Introduction to CSharp", ArticleType.Written,DateTime.Today.ToUniversalTime(), "Learn how to code in CSharp", new string[]{"CSharp"}, DifficultyLevel.Expert, 3, "Learning", null);
    //var expected3 = new ArticleDTO(5, "Javascript", ArticleType.Video, DateTime.Today.ToUniversalTime(), "Description", new string[]{"Javascript"}, DifficultyLevel.Novice, 2, "Read this article", null);        
    //var expected2 = new ArticleDTO(6, "Golang", ArticleType.Video, DateTime.Today.ToUniversalTime(), "This is a description", new string[]{"Golang"}, DifficultyLevel.Novice, 1, "Learn how to write in go", null);
    //var expected2 = new ArticleDTO(7, "Learn how to write Go", ArticleType.Written, DateTime.Today.ToUniversalTime(), "Description", new string[]{"Golang"}, DifficultyLevel.Intermediate, 3, "Read this article", null);
    //var expected2 = new ArticleDTO(8, "Python", ArticleType.Video, DateTime.Today.ToUniversalTime(), "This is python", new string[]{"Python"}, DifficultyLevel.Intermediate, 1, "Learn how to write in the very popular language Python", null);      
    //var expected2 = new ArticleDTO(9, "Testing with python", ArticleType.Written, DateTime.Today.ToUniversalTime(), "This a written article", new string[]{"Python"}, DifficultyLevel.Expert, 3, "Very interesting", null); 

  

 


    
    public void Dispose()
    {
        _context.Dispose();
    } 
}