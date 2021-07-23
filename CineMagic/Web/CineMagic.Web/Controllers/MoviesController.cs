namespace CineMagic.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CineMagic.Services.Data.Contracts;
    using CineMagic.Web.ViewModels.Movies;
    using Microsoft.AspNetCore.Mvc;

    public class MoviesController : Controller
    {
        private readonly IMoviesService moviesService;

        public MoviesController(IMoviesService moviesService)
        {
            this.moviesService = moviesService;
        }

        public async Task<IActionResult> All(int page = 1)
        {
            const int ItemsPerPage = 20;

            var model = new AlphabetMoviesPaginatedViewModel
            {
                Movies = await this.moviesService.GetAllMovies<AlphabetMovieViewModel>(page, ItemsPerPage),
                MoviesCount = this.moviesService.GetAllMoviesCount(),
                ItemsPerPage = ItemsPerPage,
            };

            return this.View(model);
        }
    }
}
