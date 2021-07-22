namespace CineMagic.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CineMagic.Common;
    using CineMagic.Services.Data.Contracts;
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
            var viewModel = new MoviesPaginatedListViewModel
            {
                Movies = await this.moviesService.GetMoviesByReleaseYear<MovieStandartViewModel>(year, page, GlobalConstants.ItemsPerPagePagination),
                PageNumber = page,
                MoviesCount = this.moviesService.GetMoviesByReleaseYearCount(year),
                ItemsPerPage = GlobalConstants.ItemsPerPagePagination,
            };

            this.TempData["ReleaseYear"] = year;

            return this.View(viewModel);
        }
    }
}
