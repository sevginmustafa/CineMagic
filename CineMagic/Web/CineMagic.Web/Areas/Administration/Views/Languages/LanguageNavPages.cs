namespace CineMagic.Web.Areas.Administration.Views.Languages
{
    using CineMagic.Web.Areas.Administration.Views.Shared;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class LanguageNavPages : AdminNavPages
    {
        public static string CreateLanguage => "CreateLanguage";

        public static string GetAll => "GetAll";

        public static string CreateLanguageNavClass(ViewContext viewContext) => PageNavClass(viewContext, CreateLanguage);

        public static string GetAllNavClass(ViewContext viewContext) => PageNavClass(viewContext, GetAll);
    }
}
