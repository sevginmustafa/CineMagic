namespace CineMagic.Web.Areas.Administration.Views.Actors
{
    using CineMagic.Web.Areas.Administration.Views.Shared;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class ActorNavPages : AdminNavPages
    {
        public static string CreateActor => "CreateActor";

        public static string GetAll => "GetAll";

        public static string CreateActorNavClass(ViewContext viewContext) => PageNavClass(viewContext, CreateActor);

        public static string GetAllNavClass(ViewContext viewContext) => PageNavClass(viewContext, GetAll);
    }
}
