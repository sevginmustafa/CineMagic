namespace CineMagic.Web.ViewModels.Actors
{
    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class ActorMostPopularMoviesViewModel : IMapFrom<Movie>
    {
        public int Id { get; set; }

        public string PosterPath { get; set; }

        public string Title { get; set; }

        public string Tagline { get; set; }

        public double Popularity { get; set; }
    }
}
