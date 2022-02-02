namespace CineMagic.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using CineMagic.Common;
    using CineMagic.Services.Data.Contracts;
    using CineMagic.Web.ViewModels;
    using CineMagic.Web.ViewModels.Countries;
    using CineMagic.Web.ViewModels.Directors;
    using CineMagic.Web.ViewModels.Genres;
    using CineMagic.Web.ViewModels.InputModels.Administration;
    using CineMagic.Web.ViewModels.Languages;
    using CineMagic.Web.ViewModels.Movies;
    using Microsoft.AspNetCore.Mvc;

    public class MoviesController : AdministrationController
    {
        private readonly IMoviesService moviesService;
        private readonly IDirectorsService directorsService;
        private readonly IGenresService genresService;
        private readonly ICountriesService countriesService;
        private readonly ILanguagesService languagesService;

        public MoviesController(
            IMoviesService moviesService,
            IDirectorsService directorsService,
            IGenresService genresService,
            ICountriesService countriesService,
            ILanguagesService languagesService)
        {
            this.moviesService = moviesService;
            this.directorsService = directorsService;
            this.genresService = genresService;
            this.countriesService = countriesService;
            this.languagesService = languagesService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> Create()
        {
            var allDirectors = await this.directorsService
                   .GetAllAsync<DirectorSimpleViewModel>();
            var allGenres = await this.genresService
                .GetAllAsync<GenreSimpleViewModel>();
            var allCountries = await this.countriesService
                .GetAllAsync<CountrySimpleViewModel>();
            var allLanguages = await this.languagesService
                 .GetAllAsync<LanguageSimpleViewModel>();

            var model = new MovieCreateInputModel
            {
                AllDirectors = allDirectors,
                AllGenres = allGenres,
                AllCountries = allCountries,
                AllLanguages = allLanguages,
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieCreateInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                var allDirectors = await this.directorsService
                    .GetAllAsync<DirectorSimpleViewModel>();
                var allGenres = await this.genresService
                    .GetAllAsync<GenreSimpleViewModel>();
                var allCountries = await this.countriesService
                    .GetAllAsync<CountrySimpleViewModel>();
                var allLanguages = await this.languagesService
                     .GetAllAsync<LanguageSimpleViewModel>();

                inputModel.AllDirectors = allDirectors;
                inputModel.AllGenres = allGenres;
                inputModel.AllCountries = allCountries;
                inputModel.AllLanguages = allLanguages;

                return this.View(inputModel);
            }

            await this.moviesService.CreateAsync(inputModel);
            return this.RedirectToAction("GetAll", "Movies", new { area = "Administration" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await this.moviesService
                .GetViewModelByIdAsync<MovieEditViewModel>(id);

            var allDirectors = await this.directorsService
                .GetAllAsync<DirectorSimpleViewModel>();
            var alllGenres = await this.genresService
                .GetAllAsync<GenreSimpleViewModel>();
            var allCountries = await this.countriesService
                .GetAllAsync<CountrySimpleViewModel>();
            var allLanguages = await this.languagesService
                .GetAllAsync<LanguageSimpleViewModel>();
            var genres = await this.moviesService
                .GetAllMovieGenresAsync<MovieGenresViewModel>(id);
            var countries = await this.moviesService
                .GetAllMovieCountriesAsync<MovieCountriesViewModel>(id);
            var languages = await this.moviesService
                .GetAllMovieLanguagesAsync<MovieLanguagesViewModel>(id);

            viewModel.AllDirectors = allDirectors;
            viewModel.AllGenres = alllGenres;
            viewModel.AllCountries = allCountries;
            viewModel.AllLanguages = allLanguages;
            viewModel.Genres = genres;
            viewModel.ProductionCountries = countries;
            viewModel.Languages = languages;

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MovieEditViewModel movieEditViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                var allDirectors = await this.directorsService
                    .GetAllAsync<DirectorSimpleViewModel>();
                var allGenres = await this.genresService
                    .GetAllAsync<GenreSimpleViewModel>();
                var allCountries = await this.countriesService
                    .GetAllAsync<CountrySimpleViewModel>();
                var allLanguages = await this.languagesService
                    .GetAllAsync<LanguageSimpleViewModel>();

                var genres = await this.moviesService
                    .GetAllMovieGenresAsync<MovieGenresViewModel>(movieEditViewModel.Id);
                var countries = await this.moviesService
                    .GetAllMovieCountriesAsync<MovieCountriesViewModel>(movieEditViewModel.Id);
                var languages = await this.moviesService
                    .GetAllMovieLanguagesAsync<MovieLanguagesViewModel>(movieEditViewModel.Id);

                movieEditViewModel.AllDirectors = allDirectors;
                movieEditViewModel.AllGenres = allGenres;
                movieEditViewModel.AllCountries = allCountries;
                movieEditViewModel.AllLanguages = allLanguages;
                movieEditViewModel.Genres = genres;
                movieEditViewModel.ProductionCountries = countries;
                movieEditViewModel.Languages = languages;

                return this.View(movieEditViewModel);
            }

            await this.moviesService.EditAsync(movieEditViewModel);

            return this.RedirectToAction("GetAll", "Movies", new { area = "Administration" });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.moviesService.DeleteAsync(id);

            return this.RedirectToAction("GetAll", "Movies", new { area = "Administration" });
        }

        public async Task<IActionResult> GetAll(string searchByTitle, int page = 1)
        {
            var movies = Enumerable.Empty<MoviesAdministrationViewModel>().AsQueryable();

            if (string.IsNullOrWhiteSpace(searchByTitle))
            {
                movies = this.moviesService
                   .GetAllMoviesAsQueryableOrderedByCreatedOn<MoviesAdministrationViewModel>();
            }
            else
            {
                movies = this.moviesService
                   .SearchMoviesByTitleAsQueryable<MoviesAdministrationViewModel>(searchByTitle);
            }

            var moviesPaginated = await PaginatedList<MoviesAdministrationViewModel>
                .CreateAsync(movies, page, GlobalConstants.AdministrationItemsPerPage);

            var viewModel = new MoviesAdministrationPaginationViewModel
            {
                Movies = moviesPaginated,
                SearchString = searchByTitle,
            };

            return this.View(viewModel);
        }
    }
}
