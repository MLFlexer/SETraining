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

            SeedProgrammingLanguage(context);
            SeedContent(context);
            SeedHistoryEntries(context);
            SeedModerator(context);
            SeedLearner(context);
            SeedImage(context);
            SeedRating(context);
            
        }
        return host;
    }


    private static void SeedContent(KhanContext context)
    {
        context.Database.Migrate();

        if(!context.Contents.Any())
        {
            context.Contents.AddRange(
                

                );
            context.SaveChanges();
        }
    }

    private static void SeedHistoryEntries(KhanContext context)
    {
        context.Database.Migrate();

        if(!context.HistoryEntries.Any())
        {
            context.HistoryEntries.AddRange(
                
            );

            context.SaveChanges();
        }
    }

    private static void SeedModerator(KhanContext context)
    {
        context.Database.Migrate();

        if (!context.Moderators.Any())
        {
            context.Moderators.AddRange(
                
            );

            context.SaveChanges();
        }
    }

    private static void SeedLearner(KhanContext context)
    {
        context.Database.Migrate();

        if (!context.Learners.Any())
        {
            context.Learners.AddRange(
                
            );

            context.SaveChanges();
        }
    }

    private static void SeedImage(KhanContext context)
    {
        context.Database.Migrate();

        if (!context.Images.Any())
        {
            context.Images.AddRange(
                
            );

            context.SaveChanges();
        }
    }

    private static void SeedRating(KhanContext context)
    {
        context.Database.Migrate();

        if (!context.Ratings.Any())
        {
            context.Ratings.AddRange(
                
            );

            context.SaveChanges();
        }
    }


    private static void SeedProgrammingLanguage(KhanContext context)
    {
        context.Database.Migrate();

        if (!context.ProgrammingLanguages.Any())
        {
            context.ProgrammingLanguages.AddRange(
          
                
            );

            context.SaveChanges();
        }
    }


}
