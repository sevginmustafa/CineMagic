namespace CineMagic.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using CineMagic.Common;
    using CineMagic.Services.Data.Contracts;
    using CineMagic.Web.ViewModels;
    using CineMagic.Web.ViewModels.InputModels.Administration;
    using CineMagic.Web.ViewModels.Languages;
    using Microsoft.AspNetCore.Mvc;

    public class LanguagesController : AdministrationController
    {
        private readonly ILanguagesService languagesService;

        public LanguagesController(ILanguagesService languagesService)
        {
            this.languagesService = languagesService;
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
        public async Task<IActionResult> Create(LanguageCreateInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            await this.languagesService.CreateAsync(inputModel);

            return this.RedirectToAction("GetAll", "Languages", new { area = "Administration" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await this.languagesService
                .GetViewModelByIdAsync<LanguageEditViewModel>(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(LanguageEditViewModel languageEditViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(languageEditViewModel);
            }

            await this.languagesService.EditAsync(languageEditViewModel);

            return this.RedirectToAction("GetAll", "Languages", new { area = "Administration" });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.languagesService.DeleteAsync(id);

            return this.RedirectToAction("GetAll", "Languages", new { area = "Administration" });
        }

        public async Task<IActionResult> GetAll(int page = 1)
        {
            var languages = this.languagesService
                .GetAllLanguagesAsQueryable<LanguageSimpleViewModel>();

            var paginatedList = await PaginatedList<LanguageSimpleViewModel>
                .CreateAsync(languages, page, GlobalConstants.AdministrationItemsPerPage);

            return this.View(paginatedList);
        }
    }
}
