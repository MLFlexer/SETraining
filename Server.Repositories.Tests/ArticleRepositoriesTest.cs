using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SETraining.Server.Contexts;
using SETraining.Server.Repositories;
using SETraining.Shared.DTOs;
using SETraining.Shared.Models;
using Xunit;
using SETraining.Shared;
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
                    Description = "Description",
                    ProgrammingLanguages = new[] { java },
                    Body = "Text",
                    ImageURL = _mockImageURL,
                },

                new Article("Introduction to CSharp", ArticleType.Written, DateTime.Today.ToUniversalTime(), DifficultyLevel.Expert)
                {
                    ProgrammingLanguages = new[] { csharp },
                    Body = "Coding is fun",
                    ImageURL = _mockImageURL
                },

                new Article("Introduction to Java", ArticleType.Written, DateTime.Today.ToUniversalTime(), DifficultyLevel.Expert)
                {
                    Description = "Description",
                    ProgrammingLanguages = new[] { java },
                    Body = "Text",
                    ImageURL = _mockImageURL
                },

                new Article("Introduction to CSharp", ArticleType.Written, DateTime.Today.ToUniversalTime(), DifficultyLevel.Expert)
                {
                    Description = "Learn how to code in CSharp",
                    ProgrammingLanguages = new[] { csharp },
                    Body = "Learning",
                    ImageURL = _mockImageURL
                },

                new Article("Javascript", ArticleType.Video, DateTime.Today.ToUniversalTime(), DifficultyLevel.Novice)
                {
                    Description = "Description",
                    ProgrammingLanguages = new[] { javascript },
                    Body = "Read this article",
                    ImageURL = _mockImageURL,
                    VideoURL = _mockVideoURL
                },

                new Article("Golang", ArticleType.Video, DateTime.Today.ToUniversalTime(), DifficultyLevel.Novice)
                {
                    Description = "This is a description",
                    ProgrammingLanguages = new[] { golang },
                    Body = "Learn how to write in go",
                    ImageURL = _mockImageURL,
                    VideoURL = _mockVideoURL

                },

                new Article("Learn how to write Go or FSharp", ArticleType.Written, DateTime.Today.ToUniversalTime(), DifficultyLevel.Intermediate)
                {
                    Description = "Description",
                    ProgrammingLanguages = new[] { golang, fsharp },
                    Body = "Read this article",
                    ImageURL = _mockImageURL,

                },
                 new Article("Python", ArticleType.Video, DateTime.Today.ToUniversalTime(), DifficultyLevel.Intermediate)
                 {
                     Description = "This is python",
                     ProgrammingLanguages = new[] { python },
                     Body = "Learn how to write in the very popular language Python",
                     ImageURL = _mockImageURL,
                     VideoURL = _mockVideoURL
                 },
                    new Article("Testing with python", ArticleType.Written, DateTime.Today.ToUniversalTime(), DifficultyLevel.Expert)
                    {
                        Description = "This a written article",
                        ProgrammingLanguages = new[] { python },
                        Body = "Very interesting",
                        ImageURL = _mockImageURL
                    }
            );

        context.SaveChanges();

        _context = context;
        _repository = new ArticleRepository(_context);

    }

    [Fact]
    public async Task Create_creates_new_Article()
    {
        //Arrange 
        var programmingLangs = new List<string>() { "Java", "Go" };
        var toCreate = new ArticleCreateDTO { Title = "Introduction to Go", Body = "Article", ProgrammingLanguages = programmingLangs };
        //Act
        var created = await _repository.CreateAsync(toCreate);

        //Assert
        Assert.Equal(10, created.Id);
        Assert.Equal("Introduction to Go", created.Title);
        Assert.Equal(programmingLangs, created.ProgrammingLanguages);
    }

    [Fact]
    public async Task Read_returns_all()
    {
        //Arrange 
        var expected = new List<ArticlePreviewDTO>()
        {
            new(1, "Introduction to Java", ArticleType.Written, DateTime.Today.ToUniversalTime(), "Description",
                new string[] {"Java"}, DifficultyLevel.Expert, _mockImageURL),
            new(2, "Introduction to CSharp", ArticleType.Written, DateTime.Today.ToUniversalTime(), null,
                new string[] {"CSharp"}, DifficultyLevel.Expert, _mockImageURL),
            new(3, "Introduction to Java", ArticleType.Written, DateTime.Today.ToUniversalTime(), "Description",
                new string[] {"Java"}, DifficultyLevel.Expert, _mockImageURL),
            new(4, "Introduction to CSharp", ArticleType.Written, DateTime.Today.ToUniversalTime(),
                "Learn how to code in CSharp", new string[] {"CSharp"}, DifficultyLevel.Expert, _mockImageURL),
            new(5, "Javascript", ArticleType.Video, DateTime.Today.ToUniversalTime(), "Description",
                new string[] {"Javascript"}, DifficultyLevel.Novice, _mockImageURL),
            new(6, "Golang", ArticleType.Video, DateTime.Today.ToUniversalTime(), "This is a description",
                new string[] {"Golang"}, DifficultyLevel.Novice, _mockImageURL),
            new(7, "Learn how to write Go or FSharp", ArticleType.Written, DateTime.Today.ToUniversalTime(), "Description",
                new string[] {"FSharp","Golang"}, DifficultyLevel.Intermediate, _mockImageURL),
            new(8, "Python", ArticleType.Video, DateTime.Today.ToUniversalTime(), "This is python",
                new string[] {"Python"}, DifficultyLevel.Intermediate, _mockImageURL),
            new(9, "Testing with python", ArticleType.Written, DateTime.Today.ToUniversalTime(),
                "This a written article", new string[] {"Python"}, DifficultyLevel.Expert, _mockImageURL)
        };

        //Act 
        var actual = (await _repository.ReadAsync()).Value.ToList();

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
            Assert.Equal(expected[i].ImageURL, actual[i].ImageURL);
        }
    }

    [Fact]
    public async Task ReadArticleFromIdAsync_given_non_existing_id_returns_null()
    {
        var actual = await _repository.ReadFromIdAsync(99);
        Assert.True(actual.IsNone);
    }

    [Fact]
    public async Task ReadArticleFromIdAsync_given_existing_id_returns_Article()
    {
        //Arrange
        var expected = new ArticleDTO(1, "Introduction to Java", ArticleType.Written, DateTime.Today, "Test", null, DifficultyLevel.Expert, null, "Article", null);

        //Act
        var actual = (await _repository.ReadFromIdAsync(1)).Value;

        //Assert
        Assert.Equal(expected.Id, actual.Id);
        Assert.Equal(expected.Title, actual.Title);
    }

    [Fact]
    public async Task ReadArticlesFromTitleAsync_given_non_existing_title_returns_null()
    {
        var actual = await _repository.ReadFromTitleAsync("THISISNOTWORKING");
        Assert.True(actual.IsNone);
    }

    [Fact]
    public async Task ReadArticlesFromTitleAsync_given_existing_title_returns_articles()
    {
        //Arrange
        var expectedArr = new ArticlePreviewDTO[] {
            new ArticlePreviewDTO(2,"Introduction to CSharp", ArticleType.Written, DateTime.Today.ToUniversalTime(), null, new string[]{"CSharp"}, DifficultyLevel.Expert, null),
            new ArticlePreviewDTO(4, "Introduction to CSharp", ArticleType.Written,DateTime.Today.ToUniversalTime(), "Learn how to code in CSharp", new string[]{"CSharp"}, DifficultyLevel.Expert, null)
        };

        //Act
        var actual = await _repository.ReadFromTitleAsync("CSharp");
        IEnumerable<ArticlePreviewDTO> actualValue = actual.Value;

        var actualArray = actualValue.ToArray();

        for (var i = 0; i < expectedArr.Length; i++)
        {
            Assert.Equal(expectedArr[i].Id, actualArray[i].Id);
            Assert.Equal(expectedArr[i].Title, actualArray[i].Title);
            Assert.Equal(expectedArr[i].Type, actualArray[i].Type);
            Assert.Equal(expectedArr[i].Created.Date, actualArray[i].Created.Date);
            Assert.Equal(expectedArr[i].Description, actualArray[i].Description);
            Assert.Equal(expectedArr[i].ProgrammingLanguages, actualArray[i].ProgrammingLanguages);
            Assert.Equal(expectedArr[i].Difficulty, actualArray[i].Difficulty);
        }
    }

    [Fact]
    public async Task UpdateAsync_given_non_existing_id_returns_NotFound()
    {
        var content = new ArticleUpdateDTO
        {
            Id = 42,
            Title = "Introduction to Java",
            Description = "Description",
            ProgrammingLanguages = new List<string>() { "Java 4", "Java 5" },
            Difficulty = DifficultyLevel.Expert,
            Body = "Article"
        };

        var updated = await _repository.UpdateAsync(42, content);

        Assert.Equal(Status.NotFound, updated);
    }

    [Fact]
    public async Task UpdateAsync_given_existing_id_updates_article()
    {
        var content = new ArticleUpdateDTO
        {
            Id = 1,
            Title = "Introduction to Java2",
            Description = "This is updated",
            ProgrammingLanguages = new List<string>() { },
            Difficulty = DifficultyLevel.Expert,
            Body = "Article"
        };

        var updated = await _repository.UpdateAsync(1, content);

        Assert.Equal(Status.Updated, updated);

        var option = await _repository.ReadFromIdAsync(1);
        var UpdatedContent = option.Value;

        Assert.Equal("This is updated", UpdatedContent.Description);
        Assert.Equal("Introduction to Java2", UpdatedContent.Title);
        Assert.Empty(UpdatedContent.ProgrammingLanguages);
    }

    [Fact]
    public async Task Delete_given_non_existing_id_returns_NotFound()
    {
        var actual = await _repository.DeleteAsync(44);
        Assert.Equal(Status.NotFound, actual);
    }

    [Fact]
    public async Task Delete_given_existing_id_deletes_article()
    {
        var actual = await _repository.DeleteAsync(2);

        Assert.Equal(Status.Deleted, actual);
        Assert.Null(await _context.Articles.FindAsync(2));
    }

    [Fact]
    public async Task Read_given_non_existing_title_and_empty_difficulty_and_language_returns_null()
    {
        var actual = await _repository.ReadFromParametersAsync("THISDOESNOOOOOOOOTEXIIIIST", "", new string[1]);
        Assert.True(actual.IsNone);
    }

    [Fact]
    public async Task Read_given_non_existing_difficulty_and_empty_title_and_language_returns_null()
    {
        var actual = await _repository.ReadFromParametersAsync("", "100000", new string[1]);
        Assert.True(actual.IsNone);
    }

    [Fact]
    public async Task Read_given_non_existing_language_and_empty_difficulty_and_title_returns_null()
    {
        var actual = await _repository.ReadFromParametersAsync("", "", new string[] { "NOTALANGUAGE" });
        Assert.True(actual.IsNone);
    }

    [Fact]
    public async Task Read_given_existing_title_AND_no_matching_parameter_on_difficulty_and_language_returns_article()
    {
        //Arrange
        var expectedArr = new ArticlePreviewDTO[] {
            new ArticlePreviewDTO(1, "Introduction to Java", ArticleType.Written, DateTime.Today.ToUniversalTime(), "Description", new string[]{"Java"}, DifficultyLevel.Expert, _mockImageURL),
            new ArticlePreviewDTO(3, "Introduction to Java", ArticleType.Written, DateTime.Today.ToUniversalTime(), "Description", new string[]{"Java"}, DifficultyLevel.Expert, _mockImageURL),
            new ArticlePreviewDTO(5, "Javascript", ArticleType.Video, DateTime.Today.ToUniversalTime(), "Description", new string[]{"Javascript"}, DifficultyLevel.Novice, _mockImageURL)
        };

        //Act
        var actual = await _repository.ReadFromParametersAsync("Java", "", null!);

        IEnumerable<ArticlePreviewDTO> actualValue = actual.Value;
        var actualArray = actualValue.ToArray();

        //Assert
        for (var i = 0; i < expectedArr.Length; i++)
        {
            Assert.Equal(expectedArr[i].Id, actualArray[i].Id);
            Assert.Equal(expectedArr[i].Title, actualArray[i].Title);
            Assert.Equal(expectedArr[i].Type, actualArray[i].Type);
            Assert.Equal(expectedArr[i].Created.Date, actualArray[i].Created.Date);
            Assert.Equal(expectedArr[i].Description, actualArray[i].Description);
            Assert.Equal(expectedArr[i].ProgrammingLanguages, actualArray[i].ProgrammingLanguages);
            Assert.Equal(expectedArr[i].Difficulty, actualArray[i].Difficulty);
            Assert.Equal(expectedArr[i].ImageURL, actualArray[i].ImageURL);
        }
    }

    [Fact]
    public async Task Read_given_difficulty_AND_no_matching_parameter_on_title_and_language_returns_article()
    {
        //Arrange
        var expectedArr = new ArticlePreviewDTO[] {
            new ArticlePreviewDTO(5, "Javascript", ArticleType.Video, DateTime.Today.ToUniversalTime(), "Description", new string[]{"Javascript"}, DifficultyLevel.Novice, _mockImageURL),
            new ArticlePreviewDTO(6, "Golang", ArticleType.Video, DateTime.Today.ToUniversalTime(), "This is a description", new string[]{"Golang"}, DifficultyLevel.Novice, _mockImageURL)
        };

        //Act
        var actual = await _repository.ReadFromParametersAsync("", "1", null!);
        IEnumerable<ArticlePreviewDTO> actualValue = actual.Value;
        var actualArray = actualValue.ToArray();

        //Assert
        for (var i = 0; i < expectedArr.Length; i++)
        {
            Assert.Equal(expectedArr[i].Id, actualArray[i].Id);
            Assert.Equal(expectedArr[i].Title, actualArray[i].Title);
            Assert.Equal(expectedArr[i].Type, actualArray[i].Type);
            Assert.Equal(expectedArr[i].Created.Date, actualArray[i].Created.Date);
            Assert.Equal(expectedArr[i].Description, actualArray[i].Description);
            Assert.Equal(expectedArr[i].ProgrammingLanguages, actualArray[i].ProgrammingLanguages);
            Assert.Equal(expectedArr[i].Difficulty, actualArray[i].Difficulty);
            Assert.Equal(expectedArr[i].ImageURL, actualArray[i].ImageURL);
        }
    }

    [Fact]
    public async Task Read_given_language_AND_no_matching_parameter_on_title_and_difficulty_returns_article()
    {
        //Arrange
        var expectedArr = new ArticlePreviewDTO[] {
            new ArticlePreviewDTO(2,"Introduction to CSharp", ArticleType.Written, DateTime.Today.ToUniversalTime(), null, new string[]{"CSharp"}, DifficultyLevel.Expert, _mockImageURL),
            new ArticlePreviewDTO(4, "Introduction to CSharp", ArticleType.Written,DateTime.Today.ToUniversalTime(), "Learn how to code in CSharp", new string[]{"CSharp"}, DifficultyLevel.Expert, _mockImageURL)
        };

        //Act
        var actual = await _repository.ReadFromParametersAsync("", "", new string[] { "CSharp" });
        IEnumerable<ArticlePreviewDTO> actualValue = actual.Value;
        var actualArray = actualValue.ToArray();

        //Assert
        for (var i = 0; i < expectedArr.Length; i++)
        {
            Assert.Equal(expectedArr[i].Id, actualArray[i].Id);
            Assert.Equal(expectedArr[i].Title, actualArray[i].Title);
            Assert.Equal(expectedArr[i].Type, actualArray[i].Type);
            Assert.Equal(expectedArr[i].Created.Date, actualArray[i].Created.Date);
            Assert.Equal(expectedArr[i].Description, actualArray[i].Description);
            Assert.Equal(expectedArr[i].ProgrammingLanguages, actualArray[i].ProgrammingLanguages);
            Assert.Equal(expectedArr[i].Difficulty, actualArray[i].Difficulty);
            Assert.Equal(expectedArr[i].ImageURL, actualArray[i].ImageURL);
        }
    }

    [Fact]
    public async Task Read_given_title_and_difficulty_AND_no_matching_parameter_on_language_returns_article()
    {
        //Arrange
        var expected1 = new ArticlePreviewDTO(8, "Python", ArticleType.Video, DateTime.Today.ToUniversalTime(), "This is python", new string[] { "Python" }, DifficultyLevel.Intermediate, _mockImageURL);

        //Act
        var actual = await _repository.ReadFromParametersAsync("Python", "2", null!);
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
        Assert.Equal(expected1.ImageURL, actualArray[0].ImageURL);
    }

    [Fact]
    public async Task Read_given_title_and_language_AND_no_matching_parameter_on_difficulty_returns_article()
    {
        //Arrange
        var expectedArr = new ArticlePreviewDTO[] {
            new ArticlePreviewDTO(1, "Introduction to Java", ArticleType.Written, DateTime.Today.ToUniversalTime(), "Description", new string[]{"Java"}, DifficultyLevel.Expert, _mockImageURL),
            new ArticlePreviewDTO(3, "Introduction to Java", ArticleType.Written, DateTime.Today.ToUniversalTime(), "Description", new string[]{"Java"}, DifficultyLevel.Expert, _mockImageURL)
        };

        //Act
        var actual = await _repository.ReadFromParametersAsync("Java", "", new string[] { "Java" });
        IEnumerable<ArticlePreviewDTO> actualValue = actual.Value;
        var actualArray = actualValue.ToArray();

        //Assert
        for (var i = 0; i < expectedArr.Length; i++)
        {
            Assert.Equal(expectedArr[i].Id, actualArray[i].Id);
            Assert.Equal(expectedArr[i].Title, actualArray[i].Title);
            Assert.Equal(expectedArr[i].Type, actualArray[i].Type);
            Assert.Equal(expectedArr[i].Created.Date, actualArray[i].Created.Date);
            Assert.Equal(expectedArr[i].Description, actualArray[i].Description);
            Assert.Equal(expectedArr[i].ProgrammingLanguages, actualArray[i].ProgrammingLanguages);
            Assert.Equal(expectedArr[i].Difficulty, actualArray[i].Difficulty);
            Assert.Equal(expectedArr[i].ImageURL, actualArray[i].ImageURL);
        }
    }

    [Fact]
    public async Task Read_given_difficulty_and_language_AND_no_matching_parameter_on_title_returns_article()
    {
        //Arrange
        var expected1 = new ArticlePreviewDTO(9, "Testing with python", ArticleType.Written, DateTime.Today.ToUniversalTime(), "This a written article", new string[] { "Python" }, DifficultyLevel.Expert, _mockImageURL);

        //Act
        var actual = await _repository.ReadFromParametersAsync("", "3", new string[] { "Python" });
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
        Assert.Equal(expected1.ImageURL, actualArray[0].ImageURL);
    }

    [Fact]
    public async Task Read_given_title_difficulty_languages_returns_article()
    {
        //Arrange
        var expected1 = new ArticlePreviewDTO(7, "Learn how to write Go or FSharp", ArticleType.Written, DateTime.Today.ToUniversalTime(), "Description", new string[] { "FSharp", "Golang" }, DifficultyLevel.Intermediate, _mockImageURL);

        //Act
        var actual = await _repository.ReadFromParametersAsync("Go", "2", new string[] { "Golang" });
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
        Assert.Equal(expected1.ImageURL, actualArray[0].ImageURL);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}