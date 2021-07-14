namespace CineMagic.Web.Controllers
{
    using System.Diagnostics;
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
            var mostRecentMovie = await this.moviesService.GetRecentMoviesAsync<MostRecentMovieViewModel>(1);
            var recentMovies = await this.moviesService.GetRecentMoviesAsync<RecentMoviesViewModel>(8);
            var mostPopularMovie = await this.moviesService.GetPopularMoviesAsync<MostRecentMovieViewModel>(1);
            var popularMovies = await this.moviesService.GetPopularMoviesAsync<RecentMoviesViewModel>(8);
            var bestRatedMovie = await this.moviesService.GetTopRatedMoviesAsync<MostRecentMovieViewModel>(1);
            var topRatedMovies = await this.moviesService.GetTopRatedMoviesAsync<RecentMoviesViewModel>(8);

            var viewModels = new MoviesHomePageViewModelsList
            {
                MostRecentMovie = mostRecentMovie,
                RecentMovies = recentMovies,
                MostPopularMovie=mostPopularMovie,
                PopularMovies=popularMovies,
                BestRatedMovie=bestRatedMovie,
                TopRatedMovies = topRatedMovies,
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
