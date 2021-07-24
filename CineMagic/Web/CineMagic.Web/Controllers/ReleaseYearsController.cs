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

    public class ReleaseYearsController : Controller
    {
        private readonly IMoviesService moviesService;

        public ReleaseYearsController(IMoviesService moviesService)
        {
            this.moviesService = moviesService;
        }

        public async Task<IActionResult> ByYear(int year, int page = 1)
        {
            var movies = this.moviesService.GetMoviesByReleaseYearAsQueryable<MovieStandartViewModel>(year);

            var moviesPaginated = await PaginatedList<MovieStandartViewModel>.CreateAsync(movies, page, GlobalConstants.ItemsStandartCountPagination);

            this.TempData["ReleaseYear"] = year;

            return this.View(moviesPaginated);
        }
    }
}
