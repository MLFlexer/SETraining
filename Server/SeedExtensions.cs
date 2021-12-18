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
        //if (context.Articles.Any()) return;
        
        var java = new ProgrammingLanguage("Java");
        var csharp = new ProgrammingLanguage("CSharp");
        var javascript = new ProgrammingLanguage("JavaScript");
        var fsharp = new ProgrammingLanguage("FSharp");
        var go = new ProgrammingLanguage("Go");
        var rust = new ProgrammingLanguage("Rust");
        var typescript = new ProgrammingLanguage("TypeScript");
        
        
        var JavaArticleHTML1 = File.ReadAllText(@"./SeedData/JavaArticle1.txt");
        var JavaArticleHTML2 = File.ReadAllText(@"./SeedData/JavaArticle2.txt");
        var CSharpArticleHTML = File.ReadAllText(@"./SeedData/CSharpArticle.txt");
        var CSharpVsJavaArticleHTML = File.ReadAllText(@"./SeedData/CSharpVsJava.txt");
        var JavasScriptArticleHTML = File.ReadAllText(@"./SeedData/JavaScriptArticle1.txt");

        var wikipedia = new Moderator("Wikipedia");
        var jkof = new Moderator("jkof");

        context.Articles.AddRange(
            new Article("New Java trends", ArticleType.Written, DateTime.Today.ToUniversalTime(), DifficultyLevel.Expert) {
                Description = "Essential Java features to learn in 2022",
                Creator = wikipedia,
                Body = JavaArticleHTML1,
                ProgrammingLanguages = new[] { java },
                AvgRating = 3
            },
            
            new Article("Why Log4j won’t go away", ArticleType.Written, DateTime.Now.ToUniversalTime(),DifficultyLevel.Intermediate)
            {
                Description = "Essential Java features to learn in 2022",
                Creator = wikipedia,
                Body = JavaArticleHTML2,
                ProgrammingLanguages = new[] { java },
                AvgRating = 2
            },
            
            new Article("Welcome to CSharp 10", ArticleType.Written, DateTime.Today.ToUniversalTime(), DifficultyLevel.Novice) {
                Description = "An Article about CSharp",
                Body = CSharpArticleHTML,
                Creator = jkof,
                ProgrammingLanguages = new[] { csharp },
                AvgRating = 5
            },
            new Article("CSharp or Java?", ArticleType.Written, DateTime.Today.ToUniversalTime(), DifficultyLevel.Expert) {
                Description = "A head to head comparison",
                Body = CSharpVsJavaArticleHTML,
                Creator = jkof,
                ProgrammingLanguages = new[] { csharp, java },
                AvgRating = 4
            },
            new Article("Javascript 101", ArticleType.Written, DateTime.Today.ToUniversalTime(), DifficultyLevel.Expert) {
                Description = "A beginners introduction",
                Body = JavasScriptArticleHTML,
                Creator = jkof,
                ProgrammingLanguages = new[] { javascript, typescript },
                AvgRating = 4
            },
            new Article("Rust vs Go", ArticleType.Video, DateTime.Now.ToUniversalTime(), DifficultyLevel.Expert) {
                Description = "Which is Better and Why?",
                Creator = wikipedia,
                VideoULR = "", //Video found at https://www.youtube.com/watch?v=ZJ6dVVobjaI all credit to author 
                ProgrammingLanguages = new[] { rust, go },
                AvgRating = 3,
            },
            new Article("What is ASP.NET?",  ArticleType.Video, DateTime.Now.ToUniversalTime(), DifficultyLevel.Novice ) {
                Description = "A brief introduction",
                Creator = jkof,
                VideoULR = "", //Found at https://www.youtube.com/watch?v=hsFm1oG2XTg all credit to author 
                ProgrammingLanguages = new[] { csharp },
                AvgRating = 4
            },
            new Article("What is FSharp?", ArticleType.Video, DateTime.Now.ToUniversalTime(), DifficultyLevel.Intermediate) {
                Description = "F for fun?",
                Creator = jkof,
                VideoULR = "", //Found at https://www.youtube.com/watch?v=9Vk9o9cRZ9I all credit to author 
                ProgrammingLanguages = new[] { fsharp },
                AvgRating = 5
            },
            new Article("JavaScript bind method", ArticleType.Video, DateTime.Now.ToUniversalTime(), DifficultyLevel.Novice) {
                Description = "Bind methods simply explained",
                Creator = jkof,
                VideoULR = "", //Found at https://www.youtube.com/watch?v=nYeMGnLdXkw all credit to author 
                ProgrammingLanguages = new[] { javascript },
                AvgRating = 1
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
