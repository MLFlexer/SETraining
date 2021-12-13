using System.Linq;
using Microsoft.EntityFrameworkCore;
using SETraining.Shared.Models;
using SETraining.Server.Contexts;
using SETraining.Server.Repositories;

namespace SETraining.Server;

// Most of this class was written by our lecturer Rasmus Lystrøm

public static class SeedExtensions
{

    private static IArticleRepository _articleRepository;

    public static IHost Seed(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<SETrainingContext>();
            _articleRepository = new ArticleRepository(context);

            SeedArticles(context);
            SeedLearners(context);
            SeedArticleRatings(context);
            SeedHistoryEntries(context);
        }
        return host;
    }

    private static void SeedArticles(SETrainingContext context)
    {
        context.Database.Migrate();
        //context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        if (context.Articles.Any()) return;
        var java = new ProgrammingLanguage("Java");
        var csharp = new ProgrammingLanguage("C#");
        var javascript = new ProgrammingLanguage("Javascript");
        var fsharp = new ProgrammingLanguage("F#");

        // Text below was copied from Wikipedia: https://en.wikipedia.org/wiki/Java_(programming_language)
        var javaArticleText1 = "Java is a high-level, class-based, object-oriented programming language that is designed to have as few implementation dependencies as possible. It is a general-purpose programming language intended to let programmers write once, run anywhere (WORA), meaning that compiled Java code can run on all platforms that support Java without the need for recompilation. Java applications are typically compiled to bytecode that can run on any Java virtual machine (JVM) regardless of the underlying computer architecture.";
        var videolink = "https://www.youtube.com/watch?v=eIrMbAQSU34";
        var wikipedia = new Moderator("Wikipedia");
        var jkof = new Moderator("jkof");

        var invalidFilePath = "*invalid filepath, used for testing*";

        context.Articles.AddRange(
            new Article("Java", ArticleType.Written, DateTime.Today.ToUniversalTime(), DifficultyLevel.Expert) {
                Description = "The Wikipedia page about the programming language Java",
                Difficulty = DifficultyLevel.Novice,
                Creator = wikipedia,
                ProgrammingLanguages = new[] { java }
            },
            new Article("C# Article", ArticleType.Written, DateTime.Today.ToUniversalTime(), DifficultyLevel.Expert) {
                Description = "An Article about C#",
                Difficulty = DifficultyLevel.Intermediate,
                Creator = jkof,
                ProgrammingLanguages = new[] { csharp },
                AvgRating = 4
                    
            },
            new Article("Better Article", ArticleType.Written, DateTime.Today.ToUniversalTime(), DifficultyLevel.Expert) {
                ProgrammingLanguages = new[] { java, csharp, fsharp },
                Description = "An Article about Java and C#"
            },
            new Article("Javascript Introduction", ArticleType.Written, DateTime.Today.ToUniversalTime(), DifficultyLevel.Expert) {
                ProgrammingLanguages = new[] { javascript },
                AvgRating = 1,
                Description = "An introduction to the Javascript language"
            },
            new Article("Java Video", ArticleType.Video, DateTime.Now.ToUniversalTime(), DifficultyLevel.Novice) {
                Description = "The Wikipedia page about the programming language Java",
                Difficulty = DifficultyLevel.Novice,
                Creator = wikipedia,
                ProgrammingLanguages = new[] { java }
            },
            new Article("C# Video",  ArticleType.Video, DateTime.Now.ToUniversalTime(), DifficultyLevel.Expert ) {
                Description = "An Video about C#",
                Difficulty = DifficultyLevel.Intermediate,
                Creator = jkof,
                ProgrammingLanguages = new[] { csharp },
                AvgRating = 4
                    
            },
            new Article("Better Video", ArticleType.Video, DateTime.Now.ToUniversalTime(), DifficultyLevel.Intermediate) {
                ProgrammingLanguages = new[] { java, csharp },
                Description = "A Video about Java and C#"
            },
            new Article("Javascript Introduction Video", ArticleType.Video, DateTime.Now.ToUniversalTime(), DifficultyLevel.Novice) {
                ProgrammingLanguages = new[] { javascript, fsharp },
                AvgRating = 1,
                Description = "An introduction to the Javascript language"
            }
            
        );
        
        context.SaveChanges();
    }

    private static void SeedLearners(SETrainingContext context)
    {
        context.Database.Migrate();
        if (context.Learners.Any()) return;
        
        var joachimak = new Learner("Joachim Alexander Kofoed") { Level = DifficultyLevel.Expert };
        var joachimdf = new Learner("Joachim de Fries") { Level = DifficultyLevel.Novice };
        var testLearner = new Learner("Test Learner") { Level = DifficultyLevel.Intermediate };
        var testLearner2 = new Learner("Another Test Learner");

        context.Learners.AddRange(
            testLearner,
            testLearner2,
            joachimdf,
            joachimak
        );

        context.SaveChanges();
    }

    private static void SeedArticleRatings(SETrainingContext context)
    {
        context.Database.Migrate();
        if (context.ArticleRatings.Any()) return;
        
        var articles = context.Articles.ToList();
    
        var learners = context.Learners.ToList();
    
        for (var i = 0; i < articles.Count/2; i++)
        {
            var rand = new Random();
            int randomRating = rand.Next(1, 6);
    
            context.ArticleRatings.Add(new ArticleRating(
                randomRating,
                learners.ElementAtOrDefault(Math.Min(i, learners.Count - 1)) ?? new Learner(""),
                articles.ElementAtOrDefault(i) ?? new Article("", ArticleType.Written, DateTime.Today.ToUniversalTime(), DifficultyLevel.Expert)
            ));
        }
    
        context.SaveChanges();
    }
    
    
    private static void SeedHistoryEntries(SETrainingContext context)
    {
        context.Database.Migrate();

        if (!context.ArticleHistoryEntries.Any())
        {
            var contents = context.Articles.ToList();

            var learners = context.Learners.ToList();

            for (var i = 0; i < contents.Count; i++)
            {
                context.ArticleHistoryEntries.Add(new ArticleHistoryEntry(
                    DateTime.UtcNow.ToUniversalTime(),
                    contents.ElementAtOrDefault(i) ?? new Article("", ArticleType.Written, DateTime.Today.ToUniversalTime(), DifficultyLevel.Expert),
                    learners.ElementAtOrDefault(Math.Min(i, learners.Count - 1)) ?? new Learner("")
                ));
            }

            context.SaveChanges();
        }
   
    }
}
