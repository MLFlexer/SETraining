using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SETraining.Server.Contexts;
using SETraining.Server.Repositories;
using SETraining.Shared.DTOs;
using SETraining.Shared.Models;
using Xunit;

namespace Server.Repositories.Tests;

public class ContentRepositoriesTest : IDisposable
{
    private readonly KhanContext _context;
    private readonly ContentRepository _repository;
    private bool disposedValue;
    
    public ContentRepositoriesTest()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<KhanContext>();
        builder.UseSqlite(connection);
        var context = new KhanContext(builder.Options);
        context.Database.EnsureCreated();
        
        //Add existing test data 
        
        //TODO: ADD ALL FIELDS TO ENTITIES IN RANGE AND CORRESPONDING TESTS
        context.AddRange(
                new Video("Introduction to Java", "no file"),
                new Video("Introduction to CSharp", "no file"),
                new Article("Introduction to Java", "Text body Java Article") {ProgrammingLanguages = new List<ProgrammingLanguage>(){new("Java 4"), new("Java 5")}},
                new Article("Introduction to CSharp", "Text Body CSharp Article")
            );

        context.SaveChanges();

        _context = context;
        _repository = new ContentRepository(_context);
        
    }


    [Fact]
    public async void Create_new_Video_with_generated_id()
    {
        //Arrange 
        var programmingLangs = new List<string>() {"Java", "Go"};
        var toCreate = new ContentCreateDTO("Introduction to Go", "Video") {ProgrammingLanguages = programmingLangs};
        //Act
        var created = await _repository.CreateAsync(toCreate);
        
        //Assert
        Assert.Equal(5, created.Id);
        Assert.Equal("Video", created.Type);
        Assert.Equal("Introduction to Go", created.Title);
        Assert.Equal(programmingLangs,created.ProgrammingLanguages);
    }
    
    [Fact]
    public async void Create_new_Article_with_generated_id()
    {
        //Arrange 
        var programmingLangs = new List<string>() {"Java", "Go"};
        var toCreate = new ContentCreateDTO("Introduction to Go", "Article") {ProgrammingLanguages = programmingLangs};
        
        //Act
        var created = await _repository.CreateAsync(toCreate);
        
        //Assert
        Assert.Equal(5, created.Id);
        Assert.Equal("Article", created.Type);
        Assert.Equal("Introduction to Go", created.Title);
        Assert.Equal(programmingLangs,created.ProgrammingLanguages);
    }
    
    //[Fact]
    public async void Read_returns_all()
    {
        //TODO: Hvorfor bliver der sorteret på Article her?
        var contentsFromDB = await _repository.ReadAsync();
        var expected_1 = new ContentDetailsDTO(1, "Introduction to Java", null, new List<string>(){"Java 4", "Java 5"}, null, null, "Article");
        var expected_2 = new ContentDetailsDTO(2,"Introduction to CSharp", null, new List<string>(), null, null, "Article");
        var expected_3 = new ContentDetailsDTO(3, "Introduction to Java", null, new List<string>(), null, null, "Video");
        var expected_4 = new ContentDetailsDTO(4, "Introduction to CSharp", null, new List<string>(), null, null, "Video");
  
        Assert.Collection(contentsFromDB,
                c => Assert.Equal(expected_1.ProgrammingLanguages, c.ProgrammingLanguages),
            c => Assert.Equal(expected_2, c),
                //TODO: Hvordan tester man c og ikke kun c.ProgrammingLanguages
            c => Assert.Equal(expected_3, c),
            c => Assert.Equal(expected_4, c)
        );
        
    }
    
    [Fact]
    public async void Read_given_id_does_not_exist_returns_null()
    {
        var contentsFromDB = await _repository.ReadAsync(99);
        Assert.True(contentsFromDB.IsNone);
    }
    
    [Fact]
    public async void Read_given_id_exists_returns_Content()
    {
        //Arrange
        var expected = new ContentDetailsDTO(1, "Introduction to Java", null, null, null, null, "Article");
        
        //Act
        var contentFromDB = await _repository.ReadAsync(1);
        var actual = contentFromDB.Value;
        
        //Assert
        Assert.Equal(expected.Id, actual.Id);
        Assert.Equal(expected.Title, actual.Title);
        Assert.Equal(expected.Type, actual.Type);
    }
    
    [Fact]
    public void  Update_updates_existing_Content()
    {
        //throw new NotImplementedException();
        //Arrange 
        
        //Act
        //var contentFromDB = _repository.UpdateAsync(1, new ContentUpdateDTO())
        
        //Delete
    }
    
    //[Fact]
    public void  Delete_given_non_existing_id_returns_NotFound()
    {
        //throw new NotImplementedException();
        
        //TODO: Edit ContentUpdateDTO()!
        //var content = new ContentUpdateDTO();
        // var updated = await _repository.UpdateAsync(42, content);
        // Assert.Equal(Status.NotFound, updated);
    }
    
    [Fact]
    public void  Delete_given_existing_id_deletes()
    {
        //throw new NotImplementedException();
    }
    
    public void Dispose()
    {
        _context.Dispose();
    } 
}