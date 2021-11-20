using System;
using ProjTest2.Shared.Models;

namespace ProjTest2.Server.MockData
{

    public static class PreBuildSeedData
    {
        //Raw classes
        public static ProgrammingLanguage Java = new ProgrammingLanguage("java");
        public static ProgrammingLanguage CSharp = new ProgrammingLanguage("csharp");
        public static ProgrammingLanguage JavaScript = new ProgrammingLanguage("javascript");

        public static Video EmptyVideo = new Video("Empty video", new byte[0]);
        public static Video JavaVideo = new Video("Java video", new byte[0]);
        public static Video CSharpVideo = new Video("CSharp video", new byte[0]);
        public static Video JavascriptVideo = new Video("JavaScript video", new byte[0]);

        public static Article EmptyArticle = new Article("Empty Article", "");
        public static Article JavaArticle = new Article("Java Article", "An Article about Java");
        public static Article CSharpArticle = new Article("CSharp Article", "An Article about CSharp");
        public static Article JavascriptArticle = new Article("JavaScript Article", "An Article about JavaScript");


        public static Moderator Moderator = new Moderator("Jim the Iniative Man");

        public static Learner Learner = new Learner("Bob the Builder");

        public static Rating Rating = new Rating(10);

        public static DifficultyLevel DifficultyLevelNovice = DifficultyLevel.Novice;
        public static DifficultyLevel DifficultyLevelIntermediate = DifficultyLevel.Intermediate;
        public static DifficultyLevel DifficultyLevelExpert = DifficultyLevel.Expert;

        public static Image Image = new Image(new byte[0]);

        public static HistoryEntry History = new HistoryEntry(new DateTime());

        //Collections of Data

        public static ICollection<ProgrammingLanguage> JavaList = new List<ProgrammingLanguage> { Java };
        public static ICollection<ProgrammingLanguage> AllLangsList = new List<ProgrammingLanguage> { Java, CSharp, JavaScript };
 
    };
}

