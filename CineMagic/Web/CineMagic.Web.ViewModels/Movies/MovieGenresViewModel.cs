namespace CineMagic.Web.ViewModels.Movies
{
    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class MovieGenresViewModel : IMapFrom<MovieGenre>
    {
        public int GenreId { get; set; }

        public string GenreName { get; set; }
    }
}
