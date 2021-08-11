namespace CineMagic.Web.ViewModels.Movies
{
    using System.Collections.Generic;

    public class MovieSinglePageListViewModel
    {
        public MovieSinglePageViewModel Movie { get; set; }

        public IEnumerable<SimilarMoviesViewModel> SimilarMovies { get; set; }
    }
}
