namespace CineMagic.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using CineMagic.Common;
    using CineMagic.Services.Data.Contracts;
    using CineMagic.Web.ViewModels;
    using CineMagic.Web.ViewModels.Directors;
    using CineMagic.Web.ViewModels.InputModels.Administration;
    using Microsoft.AspNetCore.Mvc;

    public class DirectorsController : AdministrationController
    {
        private readonly IDirectorsService directorsService;

        public DirectorsController(IDirectorsService directorsService)
        {
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
            var viewModel = await this.directorsService
                .GetViewModelByIdAsync<DirectorEditViewModel>(id);

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

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.directorsService.DeleteAsync(id);

            return this.RedirectToAction("GetAll", "Directors", new { area = "Administration" });
        }

        public async Task<IActionResult> GetAll(string searchByName, int page = 1)
        {
            var directors = Enumerable.Empty<DirectorsAdministrationViewModel>().AsQueryable();

            if (string.IsNullOrWhiteSpace(searchByName))
            {
                directors = this.directorsService
                .GetAllDirectorsAsQueryableOrderedByCreatedOn<DirectorsAdministrationViewModel>();
            }
            else
            {
                directors = this.directorsService
                .SearchDirectorsByNameAsQueryable<DirectorsAdministrationViewModel>(searchByName);
            }

            var paginatedList = await PaginatedList<DirectorsAdministrationViewModel>
                .CreateAsync(directors, page, GlobalConstants.AdministrationItemsPerPage);

            var viewModel = new DirectorsAdministrationPaginationViewModel
            {
                Directors = paginatedList,
                SearchString = searchByName,
            };

            return this.View(viewModel);
        }
    }
}
