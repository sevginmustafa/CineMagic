namespace CineMagic.Web.ViewModels.Movies
{
    using System.Collections.Generic;

    public class MoviesHomePageViewModelsList
    {
        public IEnumerable<MostRecentMovieViewModel> MostRecentMovie { get; set; }

        public IEnumerable<RecentMoviesViewModel> RecentMovies { get; set; }

        public IEnumerable<MostRecentMovieViewModel> MostPopularMovie { get; set; }

        public IEnumerable<RecentMoviesViewModel> PopularMovies { get; set; }

        public IEnumerable<MostRecentMovieViewModel> BestRatedMovie { get; set; }

        public IEnumerable<RecentMoviesViewModel> TopRatedMovies { get; set; }
    }
}
