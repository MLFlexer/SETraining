using System;
using ProjTest2.Shared.DTOs;
using ProjTest2.Shared.Models;

namespace ProjTest2.Server.MockData
{

    public static class PreBuiltData
    {
        //Raw classes
        public static ProgrammingLanguage Java = new ProgrammingLanguage("Java");
        public static ProgrammingLanguage CSharp = new ProgrammingLanguage("Csharp");
        public static ProgrammingLanguage JavaScript = new ProgrammingLanguage("Javascript");

        public static RawVideo RawVideo = new RawVideo(new byte[0]);

        public static Video EmptyVideo = new Video("Empty video", RawVideo) {ProgrammingLanguages = AllLangsList};
        public static Video JavaVideo = new Video("Java video", RawVideo);
        public static Video CSharpVideo = new Video("CSharp video", RawVideo);
        public static Video JavascriptVideo = new Video("JavaScript video", RawVideo);

        public static Article EmptyArticle = new Article("Empty Article", "");
        public static Article JavaArticle = new Article("Java Article", "An Article about Java");
        public static Article CSharpArticle = new Article("CSharp Article", "An Article about CSharp");
        public static Article JavascriptArticle = new Article("JavaScript Article", "An Article about JavaScript");


        public static Moderator Moderator = new Moderator("Jim the Iniative Man");

        public static Learner Learner = new Learner("Bob the Builder");

        public static Rating Rating = new Rating(10, CSharpVideo, Learner);

        public static DifficultyLevel DifficultyLevelNovice = DifficultyLevel.Novice;
        public static DifficultyLevel DifficultyLevelIntermediate = DifficultyLevel.Intermediate;
        public static DifficultyLevel DifficultyLevelExpert = DifficultyLevel.Expert;

        public static Image Image = new Image(new byte[0]);

        public static HistoryEntry History = new HistoryEntry(new DateTime(), JavaVideo, Learner);

        //Collections of Data

        public static ICollection<ProgrammingLanguage> JavaList = new List<ProgrammingLanguage> {Java};

        public static ICollection<ProgrammingLanguage> AllLangsList = new List<ProgrammingLanguage>
            {Java, CSharp, JavaScript};


        //ContentCreateDTO's
        public static ContentCreateDTO GOArticleCreateDTO = new("Go", "Article") {ProgrammingLanguages = AllLangsList.Select(l => l.Language).ToList()};

        public static ContentCreateDTO GOVideoCreateDTO = new ("Go", "Video") {ProgrammingLanguages = AllLangsList.Select(l => l.Language).ToList()};
        
        public static ContentDetailsDTO EmptyVideoDetailsDTO = new (2, "Empty video", null, AllLangsList.Select(p => p.Language).ToList(), null, null, "Video");
        public static ContentDetailsDTO JavaVideoDetailsDTO = new (4, "Java Video", null, null, null, null, "Video");

        public static ContentDetailsDTO CSharpArticleDetailsDTO = new (1, "CSharp Article", null, null, null, null, "Article");
        public static ContentDetailsDTO JavascriptArticleDetailsDTO = new (3, "Javascript Article", null, null, null, null, "Article");

        
    };
}

