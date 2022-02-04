namespace CineMagic.Web.Controllers
{
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

        public async Task<IActionResult> All(string letter, string searchByName, int page = 1)
        {
            var directors = Enumerable.Empty<DirectorDetailedViewModel>().AsQueryable();

            if (letter != null)
            {
                directors = this.directorsService
                    .GetDirectorsByLetterAsQueryable<DirectorDetailedViewModel>(letter);
            }
            else
            {
                directors = this.directorsService
                    .SearchDirectorsByNameAsQueryable<DirectorDetailedViewModel>(searchByName);
            }

            var directorsPaginated = await PaginatedList<DirectorDetailedViewModel>
                .CreateAsync(directors, page, GlobalConstants.PaginatedTableItemsPerPageCount);

            var alphabetPagingViewModel = new AlphabetPagingViewModel
            {
                SelectedLetter = letter,
            };

            var viewModel = new DirectorsAlphabetPaginationViewModel
            {
                Directors = directorsPaginated,
                AlphabetPagingViewModel = alphabetPagingViewModel,
                SearchString = searchByName,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> BornToday(int gender, int page = 1)
        {
            var directors = this.directorsService
                .GetDirectorsBornTodayAsQueryable<DirectorStandartViewModel>(gender);

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
                .GetDirectorBestProfitMoviesAsync<DirectorHighestGrossingMoviesViewModel>(id, GlobalConstants.SinglePageRighSectionMoviesCount);

            var viewModel = new DirectorSinglePageListViewModel
            {
                Director = director,
                Movies = movies,
            };

            return this.View(viewModel);
        }
    }
}
