using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjTest2.Server.MockData;
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

            TestSeed(context);

            /* SeedProgrammingLanguage(context);
            SeedContent(context);
            SeedHistoryEntries(context);
            SeedModerator(context);
            SeedLearner(context);
            SeedImage(context);
            SeedRating(context); */
            
        }
        return host;
    }

    private static void TestSeed(KhanContext context)
    {
        context.Database.Migrate();

        if(!context.Content.Any())
        {
            var java = new ProgrammingLanguage("Java");
            var csharp = new ProgrammingLanguage("C#");

            context.Content.AddRange(
                new Article("Java Article", "An Article about Java") { 
                    ProgrammingLanguages = new List<ProgrammingLanguage>(){ java } 
                },
                new Article("C# Article", "An Article about C#") { 
                    ProgrammingLanguages = new List<ProgrammingLanguage>(){ csharp }
                },
                new Article("Better Article", "An Article about Java and C#") {
                    ProgrammingLanguages = new List<ProgrammingLanguage>() { java, csharp }
                },
                new Video("Some Video", new RawVideo(new byte[1])),
                new Video("Another video", new RawVideo(new byte[1]))
            );

            context.SaveChanges();
        }
    }

    private static void SeedContent(KhanContext context)
    {
        context.Database.Migrate();

        if(!context.Content.Any())
        {
            context.Content.AddRange(

                PreBuiltData.EmptyVideo,
                PreBuiltData.JavaVideo,
                PreBuiltData.CSharpVideo,
                PreBuiltData.JavascriptVideo,
                PreBuiltData.EmptyArticle,
                PreBuiltData.JavaArticle,
                PreBuiltData.CSharpArticle,
                PreBuiltData.JavascriptArticle

                );

            context.SaveChanges();
        }
    }

    private static void SeedHistoryEntries(KhanContext context)
    {
        context.Database.Migrate();

        if(!context.HistoryEntry.Any())
        {
            context.HistoryEntry.AddRange(
                PreBuiltData.History
            );

            context.SaveChanges();
        }
    }

    private static void SeedModerator(KhanContext context)
    {
        context.Database.Migrate();

        if (!context.Moderator.Any())
        {
            context.Moderator.AddRange(
                PreBuiltData.Moderator
            );

            context.SaveChanges();
        }
    }

    private static void SeedLearner(KhanContext context)
    {
        context.Database.Migrate();

        if (!context.Learner.Any())
        {
            context.Learner.AddRange(
                PreBuiltData.Learner
            );

            context.SaveChanges();
        }
    }

    private static void SeedImage(KhanContext context)
    {
        context.Database.Migrate();

        if (!context.Image.Any())
        {
            context.Image.AddRange(
                PreBuiltData.Image
            );

            context.SaveChanges();
        }
    }

    private static void SeedRating(KhanContext context)
    {
        context.Database.Migrate();

        if (!context.Rating.Any())
        {
            context.Rating.AddRange(
                PreBuiltData.Rating
            );

            context.SaveChanges();
        }
    }

    private static void SeedProgrammingLanguage(KhanContext context)
    {
        context.Database.Migrate();

        if (!context.ProgrammingLanguage.Any())
        {
            context.ProgrammingLanguage.AddRange(
                PreBuiltData.Java,
                PreBuiltData.CSharp,
                PreBuiltData.JavaScript
            );

            context.SaveChanges();
        }
    }
}
