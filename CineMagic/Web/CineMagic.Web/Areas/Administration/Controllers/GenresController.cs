namespace CineMagic.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using CineMagic.Common;
    using CineMagic.Data.Common.Repositories;
    using CineMagic.Data.Models;
    using CineMagic.Services.Data.Contracts;
    using CineMagic.Web.ViewModels;
    using CineMagic.Web.ViewModels.Genres;
    using CineMagic.Web.ViewModels.InputModels.Administration;
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    public class GenresController : Controller
    {
        private readonly IDeletableEntityRepository<Genre> genresRepository;
        private readonly IGenresService genresService;

        public GenresController(
            IDeletableEntityRepository<Genre> genresRepository,
            IGenresService genresService)
        {
            this.genresRepository = genresRepository;
            this.genresService = genresService;
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
        public async Task<IActionResult> Create(GenreCreateInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            await this.genresService.CreateAsync(inputModel);

            return this.RedirectToAction("GetAll", "Genres", new { area = "Administration" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await this.genresService.GetViewModelByIdAsync<GenreEditViewModel>(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GenreEditViewModel genreEditViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(genreEditViewModel);
            }

            await this.genresService.EditAsync(genreEditViewModel);

            return this.RedirectToAction("GetAll", "Genres", new { area = "Administration" });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.genresService.DeleteAsync(id);

            return this.RedirectToAction("GetAll", "Genres", new { area = "Administration" });
        }

        public async Task<IActionResult> GetAll(int page = 1)
        {
            var genres = this.genresService.GetAllGenresAsQueryable<GenreSimpleViewModel>();

            var paginatedList = await PaginatedList<GenreSimpleViewModel>
                .CreateAsync(genres, page, GlobalConstants.AdministrationItemsPerPage);

            return this.View(paginatedList);
        }
    }
}
