namespace CineMagic.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using CineMagic.Data.Models;
    using CineMagic.Services.Data.Contracts;
    using CineMagic.Web.ViewModels;
    using CineMagic.Web.ViewModels.Movies;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        private const int HomePageFeaturedMoviesTabCount = 9;
        private const int HomePageLatestMoviesCount = 11;
        private const int HomePageWatchlistMoviesCount = 10;

        private readonly IMoviesService moviesService;
        private readonly UserManager<ApplicationUser> usermanager;

        public HomeController(IMoviesService moviesService, UserManager<ApplicationUser> usermanager)
        {
            this.moviesService = moviesService;
            this.usermanager = usermanager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await this.usermanager.GetUserAsync(this.User);

            var mostRecentMovie = await this.moviesService.GetRecentMoviesAsync<MovieHomePageRespTabsViewModel>(1);
            var recentMovies = await this.moviesService.GetRecentMoviesAsync<MovieHomePageViewModel>(HomePageFeaturedMoviesTabCount);
            var mostPopularMovie = await this.moviesService.GetPopularMoviesAsync<MovieHomePageRespTabsViewModel>(1);
            var popularMovies = await this.moviesService.GetPopularMoviesAsync<MovieHomePageViewModel>(HomePageFeaturedMoviesTabCount);
            var bestRatedMovie = await this.moviesService.GetTopRatedMoviesAsync<MovieHomePageRespTabsViewModel>(1);
            var topRatedMovies = await this.moviesService.GetTopRatedMoviesAsync<MovieHomePageViewModel>(HomePageFeaturedMoviesTabCount);
            var latestMovie = await this.moviesService.GetLatestMoviesAsync<MovieHomePageBannerViewModel>(1);
            var latestMovies = await this.moviesService.GetLatestMoviesAsync<MovieHomePageViewModel>(HomePageLatestMoviesCount);
            //var watchlistMovies = await this.moviesService.GetWatchlistMovies<MovieHomePageViewModel>(user.Id, HomePageWatchlistMoviesCount);

            var viewModels = new HomePageViewModelsList
            {
                MostRecentMovie = mostRecentMovie,
                RecentMovies = recentMovies.Skip(1),
                MostPopularMovie = mostPopularMovie,
                PopularMovies = popularMovies.Skip(1),
                BestRatedMovie = bestRatedMovie,
                TopRatedMovies = topRatedMovies.Skip(1),
                LatestMovie = latestMovie,
                LatestMovies = latestMovies.Skip(1),
                //Watchlist = watchlistMovies,
            };

            return this.View(viewModels);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
