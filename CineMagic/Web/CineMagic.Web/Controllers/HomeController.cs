namespace CineMagic.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using CineMagic.Services.Data.Contracts;
    using CineMagic.Web.ViewModels;
    using CineMagic.Web.ViewModels.Movies;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        private readonly IMoviesService moviesService;

        public HomeController(IMoviesService moviesService)
        {
            this.moviesService = moviesService;
        }

        public async Task<IActionResult> Index()
        {
            var mostRecentMovie = await this.moviesService.GetRecentMoviesAsync<MovieHomePageViewModel>(1);
            var recentMovies = await this.moviesService.GetRecentMoviesAsync<MoviesHomePageSliderViewModel>(9);
            var mostPopularMovie = await this.moviesService.GetPopularMoviesAsync<MovieHomePageViewModel>(1);
            var popularMovies = await this.moviesService.GetPopularMoviesAsync<MoviesHomePageSliderViewModel>(9);
            var bestRatedMovie = await this.moviesService.GetTopRatedMoviesAsync<MovieHomePageViewModel>(1);
            var topRatedMovies = await this.moviesService.GetTopRatedMoviesAsync<MoviesHomePageSliderViewModel>(9);

            var viewModels = new MoviesHomePageViewModelsList
            {
                MostRecentMovie = mostRecentMovie,
                RecentMovies = recentMovies.Skip(1),
                MostPopularMovie = mostPopularMovie,
                PopularMovies = popularMovies.Skip(1),
                BestRatedMovie = bestRatedMovie,
                TopRatedMovies = topRatedMovies.Skip(1),
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
