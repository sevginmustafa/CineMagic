namespace CineMagic.Web.Areas.Administration.Views.Shared
{
    using System;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class AdminNavPages
    {
        public static string Movies => "Movies";

        public static string Actors => "Actors";

        public static string Directors => "Directors";

        public static string Genres => "Genres";

        public static string Countries => "Countries";

        public static string Languages => "Languages";

        public static string Privacy => "Privacy";

        public static string Contacts => "Contacts";

        public static string GatherMovies => "GatherMovies";

        public static string MovieNavClass(ViewContext viewContext) => PageNavClass(viewContext, Movies);

        public static string ActorNavClass(ViewContext viewContext) => PageNavClass(viewContext, Actors);

        public static string DirectorNavClass(ViewContext viewContext) => PageNavClass(viewContext, Directors);

        public static string GenreNavClass(ViewContext viewContext) => PageNavClass(viewContext, Genres);

        public static string CountryNavClass(ViewContext viewContext) => PageNavClass(viewContext, Countries);

        public static string LanguageNavClass(ViewContext viewContext) => PageNavClass(viewContext, Languages);

        public static string PrivacyNavClass(ViewContext viewContext) => PageNavClass(viewContext, Privacy);

        public static string ContactNavClass(ViewContext viewContext) => PageNavClass(viewContext, Contacts);

        public static string GatherMoviesNavClass(ViewContext viewContext) => PageNavClass(viewContext, GatherMovies);

        protected static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
