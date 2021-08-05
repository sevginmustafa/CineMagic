namespace CineMagic.Web.ViewModels.Directors
{
    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class DirectorMoviesViewModel : IMapFrom<Movie>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string PosterPath { get; set; }
    }
}
