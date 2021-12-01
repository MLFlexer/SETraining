using System.Linq;
using Microsoft.EntityFrameworkCore;
using SETraining.Shared.Models;
using SETraining.Server.Contexts;
using SETraining.Server.Repositories;

namespace SETraining.Server;

// Most of this class was written by our lecturer Rasmus Lystrøm

public static class SeedExtensions
{

    private static IContentRepository _repository;

    public static IHost Seed(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<KhanContext>();
            _repository = new ContentRepository(context);

            SeedContent(context);
            SeedLearners(context);
            SeedRatings(context);
            SeedHistoryEntries(context);
        }
        return host;
    }

    private static void SeedContent(KhanContext context)
    {
        context.Database.Migrate();

        if(!context.Contents.Any())
        {
            var java = new ProgrammingLanguage("Java");
            var csharp = new ProgrammingLanguage("C#");
            var javascript = new ProgrammingLanguage("Javascript");
            var fsharp = new ProgrammingLanguage("F#");

            // Text below was copied from Wikipedia: https://en.wikipedia.org/wiki/Java_(programming_language)
            var javaArticleText1 = "Java is a high-level, class-based, object-oriented programming language that is designed to have as few implementation dependencies as possible. It is a general-purpose programming language intended to let programmers write once, run anywhere (WORA), meaning that compiled Java code can run on all platforms that support Java without the need for recompilation. Java applications are typically compiled to bytecode that can run on any Java virtual machine (JVM) regardless of the underlying computer architecture.";

            var wikipedia = new Moderator("Wikipedia");
            var jkof = new Moderator("jkof");

            var invalidFilePath = "*invalid filepath, used for testing*";

            context.Contents.AddRange(
                new Article("Java", javaArticleText1) {
                    Description = "The Wikipedia page about the programming language Java",
                    Difficulty = DifficultyLevel.Novice,
                    Creator = wikipedia,
                    ProgrammingLanguages = new[] { java } 
                },
                new Article("C# Article", "") {
                    Description = "An Article about C#",
                    Difficulty = DifficultyLevel.Intermediate,
                    Creator = jkof,
                    ProgrammingLanguages = new[] { csharp },
                    AvgRating = 4
                    
                },
                new Article("Better Article", "An Article about Java and C#") {
                    ProgrammingLanguages = new[] { java, csharp }
                },
                new Article("Javascript Introduction", "An introduction to the Javascript language") {
                    ProgrammingLanguages = new[] { javascript },
                    AvgRating = 1
                },
                new Video("Some Video", invalidFilePath) {
                    Description = "This is content of type video",
                    Difficulty = DifficultyLevel.Expert,
                    Creator = jkof,
                    ProgrammingLanguages = new[] { fsharp }
                },
                new Video("Another video", invalidFilePath)
            );

            context.SaveChanges();
        }
    }

    private static void SeedLearners(KhanContext context)
    {
        context.Database.Migrate();

        if (!context.Learners.Any())
        {
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
    }

    private static void SeedRatings(KhanContext context)
    {
        context.Database.Migrate();

        if (!context.Ratings.Any())
        {
            var contents = context.Contents.ToList();

            var learners = context.Learners.ToList();

            for (var i = 0; i < contents.Count; i++)
            {
                var rand = new Random();
                int randomRating = rand.Next(1, 6);

                context.Ratings.Add(new Rating(
                    randomRating, 
                    contents.ElementAtOrDefault(i) ?? new Article("", ""),
                    learners.ElementAtOrDefault(Math.Min(i, learners.Count-1)) ?? new Learner("")
                ));
            }

            context.SaveChanges();
        }
    }

    private static void SeedHistoryEntries(KhanContext context)
    {
        context.Database.Migrate();

        if (!context.HistoryEntries.Any())
        {
            var contents = context.Contents.ToList();

            var learners = context.Learners.ToList();

            for (var i = 0; i < contents.Count; i++)
            {
                context.HistoryEntries.Add(new HistoryEntry(
                    DateTime.UtcNow,
                    contents.ElementAtOrDefault(i) ?? new Article("", ""),
                    learners.ElementAtOrDefault(Math.Min(i, learners.Count - 1)) ?? new Learner("")
                ));
            }

            context.SaveChanges();
        }
    }
}
