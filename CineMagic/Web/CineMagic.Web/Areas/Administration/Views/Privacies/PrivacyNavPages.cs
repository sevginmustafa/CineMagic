namespace CineMagic.Web.Areas.Administration.Views.Privacies
{
    using CineMagic.Web.Areas.Administration.Views.Shared;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class PrivacyNavPages : AdminNavPages
    {
        public static string CreatePrivacy => "CreatePrivacy";

        public static string EditPrivacy => "EditPrivacy";

        public static string CreatePrivacyNavClass(ViewContext viewContext) => PageNavClass(viewContext, CreatePrivacy);

        public static string EditPrivacyNavClass(ViewContext viewContext) => PageNavClass(viewContext, EditPrivacy);
    }
}
