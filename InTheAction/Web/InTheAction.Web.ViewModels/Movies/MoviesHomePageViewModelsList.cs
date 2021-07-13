namespace InTheAction.Web.ViewModels.Movies
{
    using System.Collections.Generic;

    public class MoviesHomePageViewModelsList
    {
        public IEnumerable<RecentMoviesViewModel> RecentMovies { get; set; }

        public IEnumerable<MostRecentMovieViewModel> MostRecentMovie { get; set; }

        public IEnumerable<RecentMoviesViewModel> TopRatedMovies { get; set; }
    }
}
