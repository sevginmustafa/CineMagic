namespace CineMagic.Web.ViewModels.Movies
{
    using System.Collections.Generic;

    public class HomePageViewModelsList
    {
        public MovieHomePageBannerViewModel BannerSectionMovie { get; set; }

        public IEnumerable<MovieRespTabsViewModel> MostRecentMovie { get; set; }

        public IEnumerable<MovieStandartViewModel> RecentMovies { get; set; }

        public IEnumerable<MovieRespTabsViewModel> MostPopularMovie { get; set; }

        public IEnumerable<MovieStandartViewModel> PopularMovies { get; set; }

        public IEnumerable<MovieRespTabsViewModel> BestRatedMovie { get; set; }

        public IEnumerable<MovieStandartViewModel> TopRatedMovies { get; set; }

        public IEnumerable<MovieStandartViewModel> LatestMovies { get; set; }

        public IEnumerable<MovieStandartViewModel> Watchlist { get; set; }
    }
}
