using System;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ProjTest2.Server;
using ProjTest2.Server.MockData;
using ProjTest2.Server.Repositories;
using ProjTest2.Shared.DTOs;
using Xunit;

namespace Server.Repositories.Test;

public class ContentRepositoryTests : IDisposable
{
    private readonly KhanContext _context;
    private readonly ContentRepository _repository;
    private bool disposedValue;
    
    public ContentRepositoryTests()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<KhanContext>();
        builder.UseSqlite(connection);
        var context = new KhanContext(builder.Options);
        context.Database.EnsureCreated();
        
        //Add existing test data 
        
        context.AddRange(
            PreBuiltData.CSharpArticle,
            PreBuiltData.EmptyVideo,
            PreBuiltData.JavascriptArticle,
            PreBuiltData.JavaVideo
            );

        context.SaveChanges();

        _context = context;
        _repository = new ContentRepository(_context);
        
    }


    [Fact]
    public async void Create_new_Video_with_generated_id()
    {
        //Arrange 
        var toCreate = PreBuiltData.GOVideoCreateDTO;
        //Act
        var created = await _repository.CreateAsync(toCreate);
        
        //Assert
        Assert.Equal(5, created.Id);
        Assert.Equal("Video", created.Type);
        Assert.Equal("Go", created.Title);
    }
    
    [Fact]
    public async void Create_new_Article_with_generated_id()
    {
        //Arrange 
        var toCreate = PreBuiltData.GOArticleCreateDTO;
        
        //Act
        var created = await _repository.CreateAsync(toCreate);
        
        //Assert
        Assert.Equal(5, created.Id);
        Assert.Equal("Article", created.Type);
        Assert.Equal("Go", created.Title);
    }
    
    [Fact]
    public async void Read_returns_all()
    {
        var contents = await _repository.ReadAsync();
        Console.WriteLine(contents);
        Assert.Collection(contents,
            c => Assert.Equal(new ArticleDTO(1, "CSharp Article"), c),
            c => Assert.Equal(new VideoDTO(2, "Empty Video"), c),
            c => Assert.Equal(new ArticleDTO(3, "Javascript Article"), c),
            c => Assert.Equal(new VideoDTO(4, "Java Video"), c)
        );
    }
    
    [Fact]
    public void Read_given_id_does_not_exist_returns_null()
    {
        throw new NotImplementedException();
    }
    
    [Fact]
    public void Read_given_id_exists_returns_Content()
    {
        throw new NotImplementedException();
    }
    
    [Fact]
    public void  Update_updates_existing_Content()
    {
        throw new NotImplementedException();
    }
    
    [Fact]
    public void  Delete_given_non_existing_id_returns_NotFound()
    {
        throw new NotImplementedException();
    }
    
    [Fact]
    public void  Delete_given_existing_id_deletes()
    {
        throw new NotImplementedException();
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
}