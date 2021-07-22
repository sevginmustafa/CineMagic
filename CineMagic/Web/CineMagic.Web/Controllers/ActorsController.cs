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
        private readonly IActorsService actorsService;

        public ActorsController(IActorsService actorsService)
        {
            this.actorsService = actorsService;
        }

        public async Task<IActionResult> BornToday()
        {
            var actors = await this.actorsService.GetActorsBornToday<PersonStandartViewModel>();

            return this.View(actors);
        }
    }
}
