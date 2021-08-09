namespace CineMagic.Web.ViewModels.Movies
{
    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class MovieCommentUserViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string Username { get; set; }
    }
}
