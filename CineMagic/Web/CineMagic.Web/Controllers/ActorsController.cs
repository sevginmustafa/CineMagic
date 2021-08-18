namespace CineMagic.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using CineMagic.Common;
    using CineMagic.Data.Models.Enums;
    using CineMagic.Services.Data.Contracts;
    using CineMagic.Web.ViewModels;
    using CineMagic.Web.ViewModels.Actors;
    using Microsoft.AspNetCore.Mvc;

    public class ActorsController : Controller
    {
        private readonly IActorsService actorsService;
        private readonly IMoviesService moviesService;

        public ActorsController(
            IActorsService actorsService,
            IMoviesService moviesService)
        {
            this.actorsService = actorsService;
            this.moviesService = moviesService;
        }

        public async Task<IActionResult> All(string letter, string searchByName, int page = 1)
        {
            var actors = Enumerable.Empty<ActorDetailedViewModel>().AsQueryable();

            if (letter != null)
            {
                actors = this.actorsService
                    .GetActorsByLetterAsQueryable<ActorDetailedViewModel>(letter);
            }
            else
            {
                actors = this.actorsService
                    .SearchActorsByTitleAsQueryable<ActorDetailedViewModel>(searchByName);
            }

            var actorsPaginated = await PaginatedList<ActorDetailedViewModel>
                .CreateAsync(actors, page, GlobalConstants.PaginatedTableItemsPerPageCount);

            var alphabetPagingViewModel = new AlphabetPagingViewModel
            {
                SelectedLetter = letter,
            };

            var viewModel = new ActorsAlphabetPaginationViewModel
            {
                Actors = actorsPaginated,
                AlphabetPagingViewModel = alphabetPagingViewModel,
                SearchString = searchByName,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> BornToday(int gender, int page = 1)
        {
            var actors = this.actorsService
                .GetActorsBornTodayAsQueryable<ActorStandartViewModel>(gender);

            var actorsPaginated = await PaginatedList<ActorStandartViewModel>
                .CreateAsync(actors, page, GlobalConstants.ItemsStandartCountPagination);

            var model = new ActorsGenderFilterPagingViewModel
            {
                Actors = actorsPaginated,
                Gender = (Gender)gender,
            };

            return this.View(model);
        }

        public async Task<IActionResult> MostPopularActors(int gender, int page = 1)
        {
            var actors = this.actorsService
                .GetMostPopularActorsAsQueryable<ActorStandartViewModel>(gender, GlobalConstants.MostPopularPeopleCount);

            var actorsPaginated = await PaginatedList<ActorStandartViewModel>
                .CreateAsync(actors, page, GlobalConstants.ItemsStandartCountPagination);

            var model = new ActorsGenderFilterPagingViewModel
            {
                Actors = actorsPaginated,
                Gender = (Gender)gender,
            };

            return this.View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var actor = await this.actorsService
                .GetActorByIdAsync<ActorSinglePageViewModel>(id);

            var movies = await this.moviesService
                .GetActorMostPopularMoviesAsync<ActorMostPopularMoviesViewModel>(id, GlobalConstants.SinglePageRighSectionMoviesCount);

            var viewModel = new ActorSinglePageListViewModel
            {
                Actor = actor,
                Movies = movies,
            };

            return this.View(viewModel);
        }
    }
}
