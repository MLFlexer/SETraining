﻿using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjTest2.Shared.Models;
using ProjTest2.Server.MockData;

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

        if(!context.Content.Any())
        {
            context.Content.AddRange(

                PreBuildSeedData.EmptyVideo,
                PreBuildSeedData.JavaVideo,
                PreBuildSeedData.CSharpVideo,
                PreBuildSeedData.JavascriptVideo,
                PreBuildSeedData.EmptyArticle,
                PreBuildSeedData.JavaArticle,
                PreBuildSeedData.CSharpArticle,
                PreBuildSeedData.JavascriptArticle

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
                PreBuildSeedData.History
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
                PreBuildSeedData.Moderator
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
                PreBuildSeedData.Learner
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
                PreBuildSeedData.Image
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
                PreBuildSeedData.Rating
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
                PreBuildSeedData.Java,
                PreBuildSeedData.CSharp,
                PreBuildSeedData.JavaScript
            );

            context.SaveChanges();
        }
    }


}