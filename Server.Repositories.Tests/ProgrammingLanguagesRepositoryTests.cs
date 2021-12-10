using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SETraining.Server.Contexts;
using SETraining.Server.Repositories;
using SETraining.Shared.DTOs;
using SETraining.Shared.Models;
using Xunit;
using SETraining.Shared;

namespace Server.Repositories.Tests;

public class ProgrammingLanguagesRepositoryTests : IDisposable
{
    private readonly SETrainingContext _context;
    private readonly ProgrammingLanguagesRepository _repository;
    
    public ProgrammingLanguagesRepositoryTests()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<SETrainingContext>();
        builder.UseSqlite(connection);
        var context = new SETrainingContext(builder.Options);
        context.Database.EnsureCreated();
        
        context.AddRange(
            new ProgrammingLanguage("C#"),
            new ProgrammingLanguage("Java"),
            new ProgrammingLanguage("F#"),
            new ProgrammingLanguage("JavaScript"),
            new ProgrammingLanguage("Go")
        );

        context.SaveChanges();

        _context = context;
        _repository = new ProgrammingLanguagesRepository(_context);
    }

    [Fact]
    public async void Create_new_ProgrammingLanguage_with_generated_id()
    {
        //Arrange
        var toCreate = new ProgrammingLanguageCreateDTO{Name = "SourcePawn"};

        //Act
        var created = await _repository.CreateAsync(toCreate);
        
        //Assert
        Assert.Equal("SourcePawn", created.Name);
    }
    
    [Fact]
    public async Task Read_returns_all_ProgrammingLanguages()
    {
        var actual = await _repository.ReadAsync();
        var expected = new[] { 
            new ProgrammingLanguageDTO("C#"),
            new ProgrammingLanguageDTO("F#"),
            new ProgrammingLanguageDTO("Go"),
            new ProgrammingLanguageDTO("Java"),
            new ProgrammingLanguageDTO("JavaScript")
        };

        var enumerator = expected.GetEnumerator();

        foreach (var item in actual)
        {
            enumerator.MoveNext();
            Assert.Equal(enumerator.Current, item);
        }
    }
    
    [Fact]
    public async Task Read_given_non_existing_name_returns_OptionIsNone()
    {
        var actual = await _repository.ReadAsync("NotALanguage");
        Assert.True(actual.IsNone);
    }
    
    [Fact]
    public async Task Read_given_existing_name_returns_ProgrammingLanguage()
    {
        //Arrange
        var expected = new ProgrammingLanguageDTO("C#");
        
        //Act
        var actual = await _repository.ReadAsync("C#");

        //Assert
        Assert.Equal(expected.Name, actual.Value.Name);
    }
    
    public void Dispose() => _context.Dispose();
}
