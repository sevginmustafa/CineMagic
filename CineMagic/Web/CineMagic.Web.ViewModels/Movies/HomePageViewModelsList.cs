namespace CineMagic.Web.ViewModels.Movies
{
    using System.Collections.Generic;

    public class HomePageViewModelsList
    {
        public IEnumerable<MovieHomePageRespTabsViewModel> MostRecentMovie { get; set; }

        public IEnumerable<MovieHomePageViewModel> RecentMovies { get; set; }

        public IEnumerable<MovieHomePageRespTabsViewModel> MostPopularMovie { get; set; }

        public IEnumerable<MovieHomePageViewModel> PopularMovies { get; set; }

        public IEnumerable<MovieHomePageRespTabsViewModel> BestRatedMovie { get; set; }

        public IEnumerable<MovieHomePageViewModel> TopRatedMovies { get; set; }

        public IEnumerable<MovieHomePageViewModel> LatestMovies { get; set; }

        public IEnumerable<MovieHomePageBannerViewModel> LatestMovie { get; set; }

        public IEnumerable<MovieHomePageViewModel> Watchlist { get; set; }
    }
}
