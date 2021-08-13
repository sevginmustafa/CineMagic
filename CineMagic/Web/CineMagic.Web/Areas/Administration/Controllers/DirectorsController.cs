namespace CineMagic.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using CineMagic.Common;
    using CineMagic.Data.Common.Repositories;
    using CineMagic.Data.Models;
    using CineMagic.Services.Data.Contracts;
    using CineMagic.Web.ViewModels;
    using CineMagic.Web.ViewModels.Directors;
    using CineMagic.Web.ViewModels.InputModels.Administration;
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    public class DirectorsController : Controller
    {
        private readonly IDeletableEntityRepository<Director> directorsRepository;
        private readonly IDirectorsService directorsService;

        public DirectorsController(
            IDeletableEntityRepository<Director> directorsRepository,
            IDirectorsService directorsService)
        {
            this.directorsRepository = directorsRepository;
            this.directorsService = directorsService;
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
        public async Task<IActionResult> Create(DirectorCreateInpuModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            await this.directorsService.CreateAsync(inputModel);

            return this.RedirectToAction("GetAll", "Directors", new { area = "Administration" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await this.directorsService.GetViewModelByIdAsync<DirectorEditViewModel>(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DirectorEditViewModel directorEditViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(directorEditViewModel);
            }

            await this.directorsService.EditAsync(directorEditViewModel);

            return this.RedirectToAction("GetAll", "Directors", new { area = "Administration" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.directorsService.DeleteAsync(id);

            return this.RedirectToAction("GetAll", "Directors", new { area = "Administration" });
        }

        public async Task<IActionResult> GetAll(int page = 1)
        {
            var directors = this.directorsService.GetAllDirectorsAsQueryable<DirectorsAdministrationViewModel>();

            var paginatedList = await PaginatedList<DirectorsAdministrationViewModel>
                .CreateAsync(directors, page, GlobalConstants.AdministrationItemsPerPage);

            return this.View(paginatedList);
        }
    }
}
