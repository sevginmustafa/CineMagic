namespace InTheAction.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using InTheAction.Services.Data.Contracts;
    using InTheAction.Web.ViewModels;
    using InTheAction.Web.ViewModels.Movies;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IMoviesService moviesService;

        public HomeController(IMoviesService moviesService)
        {
            this.moviesService = moviesService;
        }

        public async Task<IActionResult> Index()
        {
            var mostRecentMovie = await this.moviesService.GetRecentMovies<MostRecentMovieViewModel>(1);
            var recentMovies = await this.moviesService.GetRecentMovies<RecentMoviesViewModel>(8);
            var topRatedMovies = await this.moviesService.GetTopRatedMovies<RecentMoviesViewModel>(8);

            var viewModels = new MoviesHomePageViewModelsList
            {
                RecentMovies = recentMovies,
                MostRecentMovie = mostRecentMovie,
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
