namespace InTheAction.Web.ViewModels.Movies
{
    using InTheAction.Data.Models;
    using InTheAction.Services.Mapping;

    public class MovieGenresViewModel : IMapFrom<MovieGenre>
    {
        public int GenreId { get; set; }

        public string GenreName { get; set; }
    }
}
