namespace CineMagic.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CineMagic.Common;
    using CineMagic.Services.Data.Contracts;
    using CineMagic.Web.ViewModels;
    using CineMagic.Web.ViewModels.Movies;
    using Microsoft.AspNetCore.Mvc;

    public class GenresController : Controller
    {
        private readonly IMoviesService moviesService;

        public GenresController(IMoviesService moviesService)
        {
            this.moviesService = moviesService;
        }

        public async Task<IActionResult> ByName(string name, int page = 1)
        {
            var movies = this.moviesService.GetMoviesByGenreNameAsQueryable<MovieStandartViewModel>(name);

            var moviesPaginated = await PaginatedList<MovieStandartViewModel>.CreateAsync(movies, page, GlobalConstants.ItemsStandartCountPagination);

            this.TempData["GenreName"] = name;

            return this.View(moviesPaginated);
        }
    }
}
