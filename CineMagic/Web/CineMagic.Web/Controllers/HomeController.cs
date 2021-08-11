namespace CineMagic.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using CineMagic.Services.Data.Contracts;
    using CineMagic.Web.Infrastructure;
    using CineMagic.Web.ViewModels;
    using CineMagic.Web.ViewModels.Movies;
    using CineMagic.Web.ViewModels.Privacies;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        private const int HomePageLeadMovieCount = 1;
        private const int HomePageTabFeaturedMoviesCount = 13;
        private const int HomePageLatestMoviesCount = 10;
        private const int HomePageWatchlistMoviesCount = 10;

        private readonly IMoviesService moviesService;
        private readonly IPrivaciesService privaciesService;

        public HomeController(
            IMoviesService moviesService,
            IPrivaciesService privaciesService)
        {
            this.moviesService = moviesService;
            this.privaciesService = privaciesService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = this.User.GetId();

            var bannerSectionMovie = await this.moviesService.GetBannerSectionMovieAsync<MovieHomePageBannerViewModel>();
            var mostRecentMovie = await this.moviesService.GetRecentMoviesAsync<MovieRespTabsViewModel>(HomePageLeadMovieCount);
            var recentMovies = await this.moviesService.GetRecentMoviesAsync<MovieStandartViewModel>(HomePageTabFeaturedMoviesCount);
            var mostPopularMovie = await this.moviesService.GetPopularMoviesAsync<MovieRespTabsViewModel>(HomePageLeadMovieCount);
            var popularMovies = await this.moviesService.GetPopularMoviesAsync<MovieStandartViewModel>(HomePageTabFeaturedMoviesCount);
            var bestRatedMovie = await this.moviesService.GetTopRatedMoviesAsync<MovieRespTabsViewModel>(HomePageLeadMovieCount);
            var topRatedMovies = await this.moviesService.GetTopRatedMoviesAsync<MovieStandartViewModel>(HomePageTabFeaturedMoviesCount);
            var latestMovies = await this.moviesService.GetLatestMoviesAsync<MovieStandartViewModel>(HomePageLatestMoviesCount);
            var watchlistMovies = await this.moviesService.GetWatchlistMoviesAsync<MovieWatchlistViewModel>(userId, HomePageWatchlistMoviesCount);

            var viewModels = new HomePageViewModelsList
            {
                BannerSectionMovie = bannerSectionMovie,
                MostRecentMovie = mostRecentMovie,
                RecentMovies = recentMovies.Skip(1),
                MostPopularMovie = mostPopularMovie,
                PopularMovies = popularMovies.Skip(1),
                BestRatedMovie = bestRatedMovie,
                TopRatedMovies = topRatedMovies.Skip(1),
                LatestMovies = latestMovies,
                Watchlist = watchlistMovies,
            };

            return this.View(viewModels);
        }

        public async Task<IActionResult> Privacy()
        {
            var privacy = await this.privaciesService.GetPrivacyContentAsync<PrivacyContentViewModel>();

            return this.View(privacy);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
