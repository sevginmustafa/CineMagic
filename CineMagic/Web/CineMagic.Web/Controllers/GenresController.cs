namespace CineMagic.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CineMagic.Services.Data.Contracts;
    using CineMagic.Web.ViewModels.Movies;
    using Microsoft.AspNetCore.Mvc;

    public class GenresController : Controller
    {
        private readonly IMoviesService moviesService;

        public GenresController(IMoviesService moviesService)
        {
            this.moviesService = moviesService;
        }

        public async Task<IActionResult> ByName(string name, int pageNumber = 1)
        {
            const int ItemsPerPage = 10;

            var viewModel = new MoviesPagingListViewModel
            {
                Movies = await this.moviesService.GetMoviesByGenreName<MovieStandartViewModel>(name, pageNumber, ItemsPerPage),
                GenreName=name,
                PageNumber = pageNumber,
                MoviesCount = this.moviesService.GetMoviesByGenreNameCount(name),
                ItemsPerPage = ItemsPerPage,
            };

            return this.View(viewModel);
        }
    }
}
