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

            SeedCharacters(context);
        }
        return host;
    }

    private static void SeedCharacters(KhanContext context)
    {
        context.Database.Migrate();

        if(!context.Content.Any())
        {
            context.Content.AddRange(
                new Video("Boring Video") { },
                new Article("TestArticle1") { Description = "Description for Article 1", Language = "C#", Difficulty = 5, Rating = 4.8f },
                new Video("Java Tutorial") { Description = "Quick Java tutorial", Language = "Java", Difficulty = 3, Rating = 2.5f, Length = 1069 },
                new Video("Great Video") { Description = "This is an even better video!", Language = "JavaScript", Difficulty = 4, Rating = 3.2f, Length = 724 },
                new Article("Better Article") { Description = "This is a better article", Language = "C#", Difficulty = 3, Rating = 5.0f },
                new Article("Article 2") { Language = "Java", Difficulty = 3, Rating = 2.5f },
                new Article("Boring Article") { },
                new Video("TestVideo1") { Description = "Description for Video 1", Language = "C#", Difficulty = 4, Rating = 4.9f, Length = 253 }
            );

            context.SaveChanges();
        }

        /*if (!context.Articles.Any())
        {
            context.Articles.AddRange(
                new Article("TestArticle1") { Description = "Description for Article 1", Language = "C#", Difficulty = 5, Rating = 4.8f },
                new Article("Article 2") { Language = "Java", Difficulty = 3, Rating = 2.5f },
                new Article("Better Article") { Description = "This is a better article", Language = "C#", Difficulty = 3, Rating = 5.0f },
                new Article("Boring Article") { }
            );

            context.SaveChanges();
        }

        if (!context.Videos.Any())
        {
            context.Videos.AddRange(
                new Video("TestVideo1") { Description = "Description for Video 1", Language = "C#", Difficulty = 4, Rating = 4.9f, Length = 253 },
                new Video("Java Tutorial") { Description = "Quick Java tutorial", Language = "Java", Difficulty = 3, Rating = 2.5f, Length = 1069 },
                new Video("Great Video") { Description = "This is an even better video!", Language = "JavaScript", Difficulty = 4, Rating = 3.2f, Length = 724 },
                new Video("Boring Video") {  }
            );

            context.SaveChanges();
        }*/
    }
}
