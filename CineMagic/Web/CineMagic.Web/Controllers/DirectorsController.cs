namespace CineMagic.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CineMagic.Common;
    using CineMagic.Data.Models.Enums;
    using CineMagic.Services.Data.Contracts;
    using CineMagic.Web.ViewModels;
    using CineMagic.Web.ViewModels.Directors;
    using Microsoft.AspNetCore.Mvc;

    public class DirectorsController : Controller
    {
        private readonly IDirectorsService directorsService;
        private readonly IMoviesService moviesService;

        public DirectorsController(
            IDirectorsService directorsService,
            IMoviesService moviesService)
        {
            this.directorsService = directorsService;
            this.moviesService = moviesService;
        }

        public async Task<IActionResult> BornToday(int gender, int page = 1)
        {
            var directors = this.directorsService.GetDirectorsBornTodayAsQueryable<DirectorStandartViewModel>(gender);

            var directorsPaginated = await PaginatedList<DirectorStandartViewModel>
                .CreateAsync(directors, page, GlobalConstants.ItemsStandartCountPagination);

            var model = new DirectorsGenderFilterPagingViewModel
            {
                Directors = directorsPaginated,
                Gender = (Gender)gender,
            };

            return this.View(model);
        }

        public async Task<IActionResult> MostPopularDirectors(int gender, int page = 1)
        {
            var directors = this.directorsService
                .GetMostPopularDirectorsAsQueryable<DirectorStandartViewModel>(gender, GlobalConstants.MostPopularPeopleCount);

            var directorsPaginated = await PaginatedList<DirectorStandartViewModel>
                .CreateAsync(directors, page, GlobalConstants.ItemsStandartCountPagination);

            var model = new DirectorsGenderFilterPagingViewModel
            {
                Directors = directorsPaginated,
                Gender = (Gender)gender,
            };

            return this.View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var director = await this.directorsService
                .GetDirectorByIdAsync<DirectorSinglePageViewModel>(id);

            var movies = await this.moviesService
                .GetDirectorBestProfitMoviesAsync<DirectorBestProfitMoviesViewModel>(id, GlobalConstants.SinglePageRighSectionMoviesCount);

            var viewModel = new DirectorSinglePageListViewModel
            {
                Director = director,
                Movies = movies,
            };

            return this.View(viewModel);
        }
    }
}
