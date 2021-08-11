namespace CineMagic.Web.ViewModels.Actors
{
    using System.Collections.Generic;

    public class ActorSinglePageListViewModel
    {
        public ActorSinglePageViewModel Actor { get; set; }

        public IEnumerable<ActorMostPopularMoviesViewModel> Movies { get; set; }
    }
}
