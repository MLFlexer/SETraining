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
        }
        return host;
    }

    private static void SeedArticles(SETrainingContext context)
    {
        context.Database.Migrate();
        if (context.Articles.Any()) return;
        
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

        context.Articles.AddRange(
            new Article("New Java trends", ArticleType.Written, DateTime.Today.ToUniversalTime(), DifficultyLevel.Expert) {
                Description = "Essential Java features to learn in 2022",
                Body = JavaArticleHTML1,
                ImageURL = "https://setrainingupload.blob.core.windows.net/setrainingupload/f070405b-f3ce-49a7-a8e7-24c60ff34f05", //credit: https://thehackernews.com/new-images/img/a/AVvXsEgul5prT8igF6QOpUFSIDyQRmLuxGJ2c92UiBBFSTwQokAb-z9IAS8NOTkurRSsXUlWiO594AQKF5F5poW2VwixXqlS-0kR52JN7RdZ_sGdKfylB_GKWjo5-Hz-cVwcHEOlqUsE9doPNzxVSfhN-5l5odfF0Azpw2a7CZI3P1m684txHPPtg3ffHRZI=s728-e1000
                ProgrammingLanguages = new[] { java },
                AvgRating = 3
            },
            
            new Article("Why Log4j won’t go away", ArticleType.Written, DateTime.Now.ToUniversalTime(),DifficultyLevel.Intermediate)
            {
                Description = "Essential Java features to learn in 2022",
                Body = JavaArticleHTML2,
                ProgrammingLanguages = new[] { java },
                ImageURL = "https://setrainingupload.blob.core.windows.net/setrainingupload/428cddad-be1e-4f25-bac9-8e7bd8328661", //credit: https://upload.wikimedia.org/wikipedia/commons/thumb/e/e9/Java-Debugging-Tips-881x441.jpg/800px-Java-Debugging-Tips-881x441.jpg
                AvgRating = 2
            },
            
            new Article("Welcome to CSharp 10", ArticleType.Written, DateTime.Today.ToUniversalTime(), DifficultyLevel.Novice) {
                Description = "An Article about CSharp",
                Body = CSharpArticleHTML,
                ImageURL = "https://setrainingupload.blob.core.windows.net/setrainingupload/77ea183b-c48e-4dc6-83c2-caaba51a5bbf", //credit: https://res.cloudinary.com/practicaldev/image/fetch/s--G8QhPGJ0--/c_limit%2Cf_auto%2Cfl_progressive%2Cq_auto%2Cw_880/https://cdn-images-1.medium.com/max/800/1%2AF5qsSdo2ZFSTn4tPHEZmTg.png
                ProgrammingLanguages = new[] { csharp },
                AvgRating = 5
            },
            new Article("CSharp or Java?", ArticleType.Written, DateTime.Today.ToUniversalTime(), DifficultyLevel.Expert) {
                Description = "A head to head comparison",
                Body = CSharpVsJavaArticleHTML,
                ImageURL = "https://setrainingupload.blob.core.windows.net/setrainingupload/7f58c7d1-fc43-471f-8f03-e45f20ff62d5", //credit: https://hackr.io/blog/c-sharp-vs-java/thumbnail/large
                ProgrammingLanguages = new[] { csharp, java },
                AvgRating = 4
            },
            new Article("Javascript 101", ArticleType.Written, DateTime.Today.ToUniversalTime(), DifficultyLevel.Expert) {
                Description = "A beginners introduction",
                Body = JavasScriptArticleHTML,
                ImageURL = "https://setrainingupload.blob.core.windows.net/setrainingupload/d1d3bb76-95e8-4b04-9bb6-e7c8bf629607", //credit: https://cdn-dk-di-ud.clio.me/user_upload/Javascript-736400_960_720.png
                ProgrammingLanguages = new[] { javascript, typescript },
                AvgRating = 4
            },
            new Article("Rust vs Go", ArticleType.Video, DateTime.Now.ToUniversalTime(), DifficultyLevel.Expert) {
                Description = "Which is Better and Why?",
                ImageURL = "https://setrainingupload.blob.core.windows.net/setrainingupload/b52cd8df-bd50-4416-840c-64dd823beb1d", //credit: https://www.appventurez.com/blog/wp-content/uploads/2020/07/go-vs-rust.jpg
                VideoURL = "https://setrainingupload.blob.core.windows.net/setrainingupload/da2e8fe5-b37a-4de4-a8f5-53e591c8692e", //Video found at https://www.youtube.com/watch?v=ZJ6dVVobjaI all credit to author 
                ProgrammingLanguages = new[] { rust, go },
                AvgRating = 3,
            },
            new Article("What is ASP.NET?",  ArticleType.Video, DateTime.Now.ToUniversalTime(), DifficultyLevel.Novice ) {
                Description = "A brief introduction",
                ImageURL = "https://setrainingupload.blob.core.windows.net/setrainingupload/a354f7df-a3c0-4a9f-9366-cb31fd57b0e5", //credit: https://www.ryadel.com/wp-content/uploads/2018/11/asp-net-core-logo-735x300.png
                VideoURL = "https://setrainingupload.blob.core.windows.net/setrainingupload/7cb5c7f3-b249-48ec-b498-e4c07fff979f", //Found at https://www.youtube.com/watch?v=hsFm1oG2XTg all credit to author 
                ProgrammingLanguages = new[] { csharp },
                AvgRating = 4
            },
            new Article("What is FSharp?", ArticleType.Video, DateTime.Now.ToUniversalTime(), DifficultyLevel.Intermediate) {
                Description = "F for fun?",
                ImageURL = "https://setrainingupload.blob.core.windows.net/setrainingupload/b15b50e0-1610-43b2-add0-a14e73f77921", //credit: https://pbs.twimg.com/profile_images/518069764510330880/yRNL7yTW_400x400.png
                VideoURL = "https://setrainingupload.blob.core.windows.net/setrainingupload/d850895a-5690-4e11-a520-cc68381db056", //Found at https://www.youtube.com/watch?v=9Vk9o9cRZ9I all credit to author 
                ProgrammingLanguages = new[] { fsharp },
                AvgRating = 5
            },
            new Article("JavaScript bind method", ArticleType.Video, DateTime.Now.ToUniversalTime(), DifficultyLevel.Novice) {
                Description = "Bind methods simply explained",
                ImageURL = "https://setrainingupload.blob.core.windows.net/setrainingupload/7146ee23-2096-4e2a-8b51-1694c2b7873d", //credit: https://miro.medium.com/max/1200/1*HRqVf3HxHR4CvzwKnZTXNA.jpeg
                VideoURL = "https://setrainingupload.blob.core.windows.net/setrainingupload/0490df6d-c3e9-4d67-b8d1-ac5d63fe1106", //Found at https://www.youtube.com/watch?v=nYeMGnLdXkw all credit to author 
                ProgrammingLanguages = new[] { javascript },
                AvgRating = 1
            }
        );
        
        context.SaveChanges();
    }
}
