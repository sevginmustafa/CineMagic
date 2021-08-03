namespace CineMagic.Web.ViewModels.Actors
{
    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class ActorMoviesViewModel : IMapFrom<MovieActor>
    {
        public int MovieId { get; set; }

        public string MovieTitle { get; set; }

        public string MoviePosterPath { get; set; }
    }
}
