namespace CineMagic.Web.Areas.Administration.Views.Contacts
{
    using CineMagic.Web.Areas.Administration.Views.Shared;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class ContactNavPages:AdminNavPages
    {
        public static string CreateContact => "CreateContact";

        public static string GetAll => "GetAll";

        public static string CreateContactNavClass(ViewContext viewContext) => PageNavClass(viewContext, CreateContact);

        public static string GetAllNavClass(ViewContext viewContext) => PageNavClass(viewContext, GetAll);
    }
}
