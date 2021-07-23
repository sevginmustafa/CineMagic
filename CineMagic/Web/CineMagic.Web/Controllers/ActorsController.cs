namespace CineMagic.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CineMagic.Services.Data.Contracts;
    using CineMagic.Web.ViewModels.People;
    using Microsoft.AspNetCore.Mvc;

    public class ActorsController : Controller
    {
        private const int MostPopularActorsCount = 50;

        private readonly IActorsService actorsService;

        public ActorsController(IActorsService actorsService)
        {
            this.actorsService = actorsService;
        }

        public async Task<IActionResult> BornToday(int gender)
        {
            var actors = await this.actorsService.GetActorsBornToday<PersonStandartViewModel>(gender);

            return this.View(actors);
        }

        public async Task<IActionResult> MostPopularActors()
        {
            var actors = await this.actorsService.GetMostPopularActors<PersonStandartViewModel>(MostPopularActorsCount);

            return this.View(actors);
        }
    }
}
