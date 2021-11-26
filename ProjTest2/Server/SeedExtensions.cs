using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjTest2.Shared.Models;

namespace ProjTest2.Server;

// Most of this class was written by our lecturer Rasmus Lystrøm

public static class SeedExtensions
{
    public static IHost Seed(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<KhanContext>();

            SeedContent(context);
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

            var joachim = new Learner("Joachim");

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
                    ProgrammingLanguages = new[] { csharp }
                },
                new Article("Better Article", "An Article about Java and C#") {
                    ProgrammingLanguages = new[] { java, csharp }
                },
                new Article("Javascript Introduction", "An introduction to the Javascript language") {
                    ProgrammingLanguages = new[] { javascript }
                },
                new Video("Some Video", new RawVideo(new byte[1])) {
                    Description = "This is content of type video",
                    Difficulty = DifficultyLevel.Expert,
                    Creator = jkof,
                    ProgrammingLanguages = new[] { fsharp }
                },
                new Video("Another video", new RawVideo(new byte[1]))
            );

            context.SaveChanges();
        }
    }
}
