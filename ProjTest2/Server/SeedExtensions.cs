using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

            SeedContents(context);
        }
        return host;
    }

    private static void SeedContents(KhanContext context)
    {
        context.Database.Migrate();

        if(!context.Content.Any()) {
            var Java = new ProgrammingLanguage("java");
            var JavaList = new List<ProgrammingLanguage>();
            JavaList.Add(Java);
            var creator = new Moderator("Jens");

            context.Content.AddRange(
                new Video("Boring Video", new byte[0]/* , creator */) {Creator = creator },
                new Article("TestArticle1", "textbody"/* , creator */) {Creator = creator, ProgrammingLanguages = JavaList, Description = "Description for Article 1",  Difficulty = DifficultyLevel.Novice, AvgRating = 4.8f },
                new Video("Java Tutorial", new byte[0]/* , creator */) {Creator = creator, ProgrammingLanguages = JavaList, Description = "Quick Java tutorial",  Difficulty = DifficultyLevel.Novice, AvgRating = 2.5f, Length = 1069 },
                new Video("Great Video", new byte[0]/* , creator */) {Creator = creator, ProgrammingLanguages = JavaList, Description = "This is an even better video!",  Difficulty = DifficultyLevel.Novice, AvgRating = 3.2f, Length = 724 },
                new Article("Better Article", "textbody"/* , creator */) {Creator = creator, ProgrammingLanguages = JavaList, Description = "This is a better article",  Difficulty = DifficultyLevel.Novice, AvgRating = 5.0f },
                new Article("Article 2", "textbody"/* , creator */) {Creator = creator, ProgrammingLanguages = JavaList, Difficulty = DifficultyLevel.Novice, AvgRating = 2.5f },
                new Article("Boring Article", "textbody"/* , creator */) {Creator = creator },
                new Video("TestVideo1", new byte[0]/* , creator */) {Creator = creator, ProgrammingLanguages = JavaList, Description = "Description for Video 1",  Difficulty = DifficultyLevel.Novice, AvgRating = 4.9f, Length = 253 }
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
                
            );

            context.SaveChanges();
        }
    }
}
