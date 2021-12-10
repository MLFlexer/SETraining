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

public class VideoRepositoriesTest : IDisposable
{
    private readonly SETrainingContext _context;
    private readonly VideoRepository _repository;
    private bool disposedValue;
    
    public VideoRepositoriesTest()
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
                
                new Video("Introduction to Java",  "<b>Test<b/>"),
                new Video("Introduction to CSharp", "<b>Test<b/>"),
                new Video("Introduction to Java",  "<b>Test<b/>") {ProgrammingLanguages = new List<ProgrammingLanguage>(){new("Java 4"), new("Java 5")}},
                new Video("Introduction to CSharp",  "<b>Test<b/>")
            );

        context.SaveChanges();

        _context = context;
        _repository = new VideoRepository(_context);
        
    }

    [Fact]
    public async void Create_new_Video_with_generated_id()
    {
        //Arrange 
        var programmingLangs = new List<string>() {"Java", "Go"};
        var toCreate = new VideoCreateDTO{Title = "Introduction to Go", Path = "Video", ProgrammingLanguages = programmingLangs};
        //Act
        var created = await _repository.CreateAsync(toCreate);
        
        //Assert
        Assert.Equal(5, created.Id);
        Assert.Equal("Introduction to Go", created.Title);
        Assert.Equal(programmingLangs,created.ProgrammingLanguages);
    }

    [Fact]
    public async Task Read_returns_all()
    {
        //TODO: Hvorfor bliver der sorteret på Video her?
        var contentsFromDB = await _repository.ReadAllAsync(null);
        var listContents = contentsFromDB.ToList();
        var expected_1 = new VideoDTO(1, "Introduction to Java", null, new List<string>(), null, null, "<b>Test<b/>");
        var expected_2 = new VideoDTO(2,"Introduction to CSharp", null, new List<string>(), null, null, "<b>Test<b/>");
        var expected_3 = new VideoDTO(3, "Introduction to Java", null, new List<string>(){"Java 4", "Java 5"}, null, null, "<b>Test<b/>");
        var expected_4 = new VideoDTO(4, "Introduction to CSharp", null, new List<string>(), null, null, "<b>Test<b/>");
  
        //Using string equality, because record equality does not seem to work somehow...
        Assert.Equal(expected_1.ToString(), listContents[0].ToString());
        Assert.Equal(expected_2.ToString(), listContents[1].ToString());
        Assert.Equal(expected_3.ToString(), listContents[2].ToString());
        Assert.Equal(expected_4.ToString(), listContents[3].ToString());
        Assert.Equal(expected_3.ProgrammingLanguages.First(), listContents[2].ProgrammingLanguages.First());
        Assert.Equal(expected_3.ProgrammingLanguages.Last(), listContents[2].ProgrammingLanguages.Last());
        
    }
    
    [Fact]
    public async Task Read_given_id_on_video_does_not_exist_returns_null()
    {
        var contentsFromDB = await _repository.ReadFromIdAsync(99, null);
        Assert.True(contentsFromDB.IsNone);
    }

    //Todo: er lidt usikker på om denne test er okay??
     [Fact]
    public async void Read_given_title_does_not_exist_returns_emptyList()
    {
        var contentsFromDB = await _repository.ReadFromTitleAsync("THISISNOTWORKING", null);

        Assert.Empty(contentsFromDB.Value);
    }
    
    [Fact]
    public async Task Read_given_id_exists_returns_Video()
    {
        //Arrange
        var expected = new VideoDTO(1, "Introduction to Java", null, null, null, null, "Video");
        
        //Act
        var contentFromDB = await _repository.ReadFromIdAsync(1, null);
        var actual = contentFromDB.Value;
        
        //Assert
        Assert.Equal(expected.Id, actual.Id);
        Assert.Equal(expected.Title, actual.Title);
    }


   
     [Fact]
    public async void Read_given_title_exists_returns_VideoList()
    {
        //Arrange
        var expected_1 = new VideoDTO(2,"Introduction to CSharp", null, new List<string>(), null, null, "Video");
        var expected_2 = new VideoDTO(4, "Introduction to CSharp", null, new List<string>(), null, null, "Video");
  
        //Act
        var actual = await _repository.ReadFromTitleAsync("CSharp", null);
        IEnumerable<VideoDTO> actualValue = actual.Value; 
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
    public async Task UpdateAsync_on_video_given_non_existing_id_returns_NotFound()
    {
        var content = new VideoUpdateDTO
        {
            Title ="Introduction to Java", 
            Description = null, 
            ProgrammingLanguages = new List<string>(){"Java 4", "Java 5"}, 
            Difficulty = null, 
            AvgRating = null,
            Path = "Video"
        };
        
        var updated = await _repository.UpdateAsync(42, content);
        
        Assert.Equal(Status.NotFound, updated);
        
    }

    [Fact]
    public async Task  Update_updates_existing_video()
    {
        var content = new VideoUpdateDTO
        {
            Title ="Introduction to Java2", 
            Description = "This is updated", 
            ProgrammingLanguages = new List<string>(){}, 
            Difficulty = null, 
            AvgRating = 20,
            Path = "Video"
            
        };
        
        var updated = await _repository.UpdateAsync(1, content);
        
        Assert.Equal(Status.Updated, updated);
        
        var option = await _repository.ReadFromIdAsync(1, null);
        var UpdatedContent = option.Value;
        
        Assert.Equal("This is updated", UpdatedContent.Description);
        Assert.Equal(20, UpdatedContent.AvgRating);
        Assert.Equal("Introduction to Java2", UpdatedContent.Title);
        Assert.Empty(UpdatedContent.ProgrammingLanguages);
    }
    
    [Fact]
    public async Task  Delete_given_non_existing_id_on_video_returns_NotFound()
    {
        var actual = await _repository.DeleteAsync(44);
        Assert.Equal(Status.NotFound, actual);
    }
    
    [Fact]
    public async Task Delete_given_existing_id_on_video_deletes()
    {
        var actual = await _repository.DeleteAsync(2);

        Assert.Equal(Status.Deleted, actual);
        Assert.Null(await _context.Videos.FindAsync(2));
    }
    
    public void Dispose()
    {
        _context.Dispose();
    } 
}