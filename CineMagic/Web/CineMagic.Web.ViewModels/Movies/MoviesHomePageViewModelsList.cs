namespace CineMagic.Web.ViewModels.Movies
{
    using System.Collections.Generic;

    public class MoviesHomePageViewModelsList
    {
        public IEnumerable<MovieHomePageViewModel> MostRecentMovie { get; set; }

        public IEnumerable<MoviesHomePageSliderViewModel> RecentMovies { get; set; }

        public IEnumerable<MovieHomePageViewModel> MostPopularMovie { get; set; }

        public IEnumerable<MoviesHomePageSliderViewModel> PopularMovies { get; set; }

        public IEnumerable<MovieHomePageViewModel> BestRatedMovie { get; set; }

        public IEnumerable<MoviesHomePageSliderViewModel> TopRatedMovies { get; set; }

        public IEnumerable<MoviesHomePageSliderViewModel> LatestMovies { get; set; }

        public IEnumerable<MovieHomePageBannerViewModel> LatestMovie { get; set; }
    }
}
