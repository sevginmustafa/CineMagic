namespace CineMagic.Web.Areas.Administration.Views.Countries
{
    using CineMagic.Web.Areas.Administration.Views.Shared;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CountryNavPages : AdminNavPages
    {
        public static string CreateCountry => "CreateCountry";

        public static string GetAll => "GetAll";

        public static string CreateCountryNavClass(ViewContext viewContext) => PageNavClass(viewContext, CreateCountry);

        public static string GetAllNavClass(ViewContext viewContext) => PageNavClass(viewContext, GetAll);
    }
}
