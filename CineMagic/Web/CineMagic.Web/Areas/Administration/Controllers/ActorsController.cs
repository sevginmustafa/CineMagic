namespace CineMagic.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using CineMagic.Common;
    using CineMagic.Data.Common.Repositories;
    using CineMagic.Data.Models;
    using CineMagic.Services.Data.Contracts;
    using CineMagic.Web.ViewModels;
    using CineMagic.Web.ViewModels.Actors;
    using CineMagic.Web.ViewModels.InputModels.Administration;
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    public class ActorsController : Controller
    {
        private readonly IDeletableEntityRepository<Actor> actorsRepository;
        private readonly IActorsService actorsService;

        public ActorsController(
            IDeletableEntityRepository<Actor> actorsRepository,
            IActorsService actorsService)
        {
            this.actorsRepository = actorsRepository;
            this.actorsService = actorsService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ActorCreateInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            await this.actorsService.CreateAsync(inputModel);

            return this.RedirectToAction("GetAll", "Actors", new { area = "Administration" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await this.actorsService.GetViewModelByIdAsync<ActorEditViewModel>(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ActorEditViewModel actorEditViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(actorEditViewModel);
            }

            await this.actorsService.EditAsync(actorEditViewModel);

            return this.RedirectToAction("GetAll", "Actors", new { area = "Administration" });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.actorsService.DeleteAsync(id);

            return this.RedirectToAction("GetAll", "Actors", new { area = "Administration" });
        }

        public async Task<IActionResult> GetAll(int page = 1)
        {
            var actors = this.actorsService.GetAllActorsAsQueryable<ActorsAdministrationViewModel>();

            var paginatedList = await PaginatedList<ActorsAdministrationViewModel>
                .CreateAsync(actors, page, GlobalConstants.AdministrationItemsPerPage);

            return this.View(paginatedList);
        }
    }
}
