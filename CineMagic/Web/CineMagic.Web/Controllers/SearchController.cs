namespace CineMagic.Web.Controllers
{
    using System.Threading.Tasks;

    using CineMagic.Common;
    using CineMagic.Services.Data.Contracts;
    using CineMagic.Web.ViewModels;
    using CineMagic.Web.ViewModels.Actors;
    using CineMagic.Web.ViewModels.Directors;
    using CineMagic.Web.ViewModels.Movies;
    using Microsoft.AspNetCore.Mvc;

    public class SearchController : Controller
    {
        private readonly ISearchService searchService;

        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        public async Task<IActionResult> MoviesSearch(string title, int page = 1)
        {
            this.TempData["SearchString"] = title;

            var movies = this.searchService
                .SearchMoviesAsQueryable<MovieStandartViewModel>(title);

            var moviesPaginated = await PaginatedList<MovieStandartViewModel>
                .CreateAsync(movies, page, GlobalConstants.ItemsStandartCountPagination);

            return this.View(moviesPaginated);
        }

        public async Task<IActionResult> ActorsSearch(string name, int page = 1)
        {
            this.TempData["SearchString"] = name;

            var actors = this.searchService
                .SearchActorsAsQueryable<ActorStandartViewModel>(name);

            var actorsPaginated = await PaginatedList<ActorStandartViewModel>
                .CreateAsync(actors, page, GlobalConstants.ItemsStandartCountPagination);

            return this.View(actorsPaginated);
        }

        public async Task<IActionResult> DirectorsSearch(string name, int page = 1)
        {
            this.TempData["SearchString"] = name;

            var directors = this.searchService
                .SearchDirectorsAsQueryable<DirectorStandartViewModel>(name);

            var directorsPaginated = await PaginatedList<DirectorStandartViewModel>
                .CreateAsync(directors, page, GlobalConstants.ItemsStandartCountPagination);

            return this.View(directorsPaginated);
        }
    }
}
