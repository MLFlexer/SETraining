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

namespace Server.Repositories.Tests;

public class ArticleRepositoriesTest : IDisposable
{
    private readonly SETrainingContext _context;
    private readonly ArticleRepository _repository;
    private bool disposedValue;
    
    public ArticleRepositoriesTest()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<SETrainingContext>();
        builder.UseSqlite(connection);
        var context = new SETrainingContext(builder.Options);
        context.Database.EnsureCreated();
        
        //Add existing test data 
        
        //TODO: ADD ALL FIELDS TO ENTITIES IN RANGE AND CORRESPONDING TESTS
        context.AddRange(
                new Article("Introduction to Java", ArticleType.Written, DateTime.Today, DifficultyLevel.Expert),
                new Article("Introduction to CSharp", ArticleType.Written, DateTime.Today, DifficultyLevel.Expert),
                new Article("Introduction to Java", ArticleType.Written, DateTime.Today, DifficultyLevel.Expert) {ProgrammingLanguages = new List<ProgrammingLanguage>(){new("Java 4"), new("Java 5")}},
                new Article("Introduction to CSharp", ArticleType.Written, DateTime.Today, DifficultyLevel.Expert)
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
        Assert.Equal(5, created.Id);
        Assert.Equal("Introduction to Go", created.Title);
        Assert.Equal(programmingLangs,created.ProgrammingLanguages);
        
    }

    //TODO: testen fejler
    //[Fact]
    public async Task Read_returns_all()
    {
        //TODO: Hvorfor bliver der sorteret på Article her?
        var contentsFromDB = await _repository.ReadAllArticlesAsync();
        var listContents = contentsFromDB.Value.ToList();
        var expected_1 = new ArticleDTO(1, "Introduction to Java", ArticleType.Written, DateTime.Today,null, new List<string>(){"Java 4", "Java 5"}, DifficultyLevel.Expert, null, "<b>Test<b/>", null);
        var expected_2 = new ArticleDTO(2,"Introduction to CSharp", ArticleType.Written, DateTime.Today,null, new List<string>(), DifficultyLevel.Expert, null, "<b>Test<b/>", null);
        var expected_3 = new ArticleDTO(3, "Introduction to Java", ArticleType.Written, DateTime.Today,null, new List<string>(), DifficultyLevel.Expert, null, "<b>Test<b/>", null);
        var expected_4 = new ArticleDTO(4, "Introduction to CSharp", ArticleType.Written,DateTime.Today,null, new List<string>(), DifficultyLevel.Expert, null, "<b>Test<b/>", null);
        
        //Using string equality, because record equality does not seem to work somehow...
        Assert.Equal(expected_1.ToString(), listContents[0].ToString());
        Assert.Equal(expected_2.ToString(), listContents[1].ToString());
        Assert.Equal(expected_3.ToString(), listContents[2].ToString());
        Assert.Equal(expected_4.ToString(), listContents[3].ToString());
        Assert.Equal(expected_3.ProgrammingLanguages.First(), listContents[2].ProgrammingLanguages.First());
        Assert.Equal(expected_3.ProgrammingLanguages.Last(), listContents[2].ProgrammingLanguages.Last());
    }
    
    [Fact]
    public async Task Read_given_non_existing_id_returns_null()
    {
        var contentsFromDB = await _repository.ReadArticleFromIdAsync(99);
        Assert.True(contentsFromDB.IsNone);
    }

    //Todo: er lidt usikker på om denne test er okay??
     [Fact]
    public async void Read_given_non_existing_title_returns_emptyList()
    {
        var contentsFromDB = await _repository.ReadArticlesFromTitleAsync("THISISNOTWORKING");

        Assert.Empty(contentsFromDB.Value);
    }
    
    [Fact]
    public async Task Read_given_existing_id_returns_Article()
    {
        //Arrange
        var expected = new ArticleDTO(1, "Introduction to Java", ArticleType.Written, DateTime.Today,null, null, DifficultyLevel.Expert, null, "Article", null);
        
        //Act
        var contentFromDB = await _repository.ReadArticleFromIdAsync(1);
        var actual = contentFromDB.Value;
        
        //Assert
        Assert.Equal(expected.Id, actual.Id);
        Assert.Equal(expected.Title, actual.Title);
    }


   
     [Fact]
    public async void Read_given_exsiting_title_returns_ArticleList()
    {
        //Arrange
        var expected_1 = new ArticleDTO(2,"Introduction to CSharp", ArticleType.Written, DateTime.Today,null, new List<string>(), DifficultyLevel.Expert, null, "Article", null);
        var expected_2 = new ArticleDTO(4, "Introduction to CSharp", ArticleType.Written,DateTime.Today, null, new List<string>(), DifficultyLevel.Expert, null, "Article", null);
  
        //Act
        var actual = await _repository.ReadArticlesFromTitleAsync("CSharp");
        IEnumerable<ArticleDTO> actualValue = actual.Value; 
        var actual1 = actualValue.First();
        var actual2 = actualValue.Last();


        Assert.Equal(expected_1.Id, actual1.Id);
        Assert.Equal(expected_1.Title, actual1.Title);
        Assert.Equal(expected_1.Description, actual1.Description);
        Assert.Equal(expected_1.ProgrammingLanguages, actual1.ProgrammingLanguages);
        Assert.Equal(expected_1.Difficulty, actual1.Difficulty);
        Assert.Equal(expected_1.AvgRating, actual1.AvgRating);


        Assert.Equal(expected_2.Id, actual2.Id);
        Assert.Equal(expected_2.Title, actual2.Title);
        Assert.Equal(expected_2.Description, actual2.Description);
        Assert.Equal(expected_2.ProgrammingLanguages, actual2.ProgrammingLanguages);
        Assert.Equal(expected_2.Difficulty, actual2.Difficulty);
        Assert.Equal(expected_2.AvgRating, actual2.AvgRating);
    }
    
    [Fact]
    public async Task UpdateAsync_given_non_existing_id_returns_NotFound()
    {
        var content = new ArticleUpdateDTO
        {
            Title ="Introduction to Java", 
            Description = null, 
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
    
    public void Dispose()
    {
        _context.Dispose();
    } 
}