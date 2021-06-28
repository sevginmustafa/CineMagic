namespace InTheAction.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using InTheAction.Services.Data;
    using InTheAction.Web.ViewModels;
    using InTheAction.Web.ViewModels.Movies;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IGetBestRatedMoviesService getBestRatedMoviesService;

        public HomeController(IGetBestRatedMoviesService getBestRatedMoviesService)
        {
            this.getBestRatedMoviesService = getBestRatedMoviesService;
        }

        public IActionResult Index()
        {
            var movies = this.getBestRatedMoviesService.Get();

            var model = movies
                .Select(x => new GetBestRatedMoviesOutputModel
                {
                    Title = x.Title,
                    CoverImageUrl = x.CoverImageUrl,
                    ReleaseYear = x.ReleaseYear,
                    Runtime = x.Runtime,
                    Description = x.Description,
                    Rating = x.Rating,
                    NumberOfVotes = x.NumberOfVotes,
                    Language = x.Language,
                    Budget = x.Budget,
                })
                .ToList();

            return this.View(model);
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
