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
    using CineMagic.Web.ViewModels.Actors;
    using Microsoft.AspNetCore.Mvc;

    public class ActorsController : Controller
    {
        private readonly IActorsService actorsService;

        public ActorsController(IActorsService actorsService)
        {
            this.actorsService = actorsService;
        }

        public async Task<IActionResult> BornToday(int gender, int page = 1)
        {
            var actors = this.actorsService.GetActorsBornTodayAsQueryable<ActorStandartViewModel>(gender);

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
            var actors = this.actorsService.GetMostPopularActorsAsQueryable<ActorStandartViewModel>(gender, GlobalConstants.MostPopularPeopleCount);

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
            var actor = await this.actorsService.GetActorByIdAsync<ActorSinglePageViewModel>(id);

            return this.View(actor);
        }
    }
}
