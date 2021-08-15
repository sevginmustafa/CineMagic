namespace CineMagic.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using CineMagic.Common;
    using CineMagic.Data.Common.Repositories;
    using CineMagic.Data.Models;
    using CineMagic.Services.Data.Contracts;
    using CineMagic.Web.ViewModels;
    using CineMagic.Web.ViewModels.Countries;
    using CineMagic.Web.ViewModels.InputModels.Administration;
    using Microsoft.AspNetCore.Mvc;

    public class CountriesController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Country> countriesRepository;
        private readonly ICountriesService countriesService;

        public CountriesController(
            IDeletableEntityRepository<Country> countriesRepository,
            ICountriesService countriesService)
        {
            this.countriesRepository = countriesRepository;
            this.countriesService = countriesService;
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
        public async Task<IActionResult> Create(CountryCreateInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            await this.countriesService.CreateAsync(inputModel);

            return this.RedirectToAction("GetAll", "Countries", new { area = "Administration" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await this.countriesService.GetViewModelByIdAsync<CountryEditViewModel>(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CountryEditViewModel countryEditViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(countryEditViewModel);
            }

            await this.countriesService.EditAsync(countryEditViewModel);

            return this.RedirectToAction("GetAll", "Countries", new { area = "Administration" });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.countriesService.DeleteAsync(id);

            return this.RedirectToAction("GetAll", "Countries", new { area = "Administration" });
        }

        public async Task<IActionResult> GetAll(int page = 1)
        {
            var countries = this.countriesService.GetAllCountriesAsQueryable<CountrySimpleViewModel>();

            var paginatedList = await PaginatedList<CountrySimpleViewModel>
                .CreateAsync(countries, page, GlobalConstants.AdministrationItemsPerPage);

            return this.View(paginatedList);
        }
    }
}
