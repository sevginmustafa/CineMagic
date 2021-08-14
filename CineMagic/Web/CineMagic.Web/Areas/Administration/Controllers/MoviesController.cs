namespace CineMagic.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CineMagic.Common;
    using CineMagic.Data;
    using CineMagic.Data.Common.Repositories;
    using CineMagic.Data.Models;
    using CineMagic.Services.Data.Contracts;
    using CineMagic.Web.ViewModels;
    using CineMagic.Web.ViewModels.Countries;
    using CineMagic.Web.ViewModels.Directors;
    using CineMagic.Web.ViewModels.Genres;
    using CineMagic.Web.ViewModels.InputModels.Administration;
    using CineMagic.Web.ViewModels.Languages;
    using CineMagic.Web.ViewModels.Movies;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class MoviesController : Controller
    {
        private readonly IDeletableEntityRepository<Movie> moviesRepository;
        private readonly IMoviesService moviesService;
        private readonly IDirectorsService directorsService;
        private readonly IGenresService genresService;
        private readonly ICountriesService countriesService;
        private readonly ILanguagesService languagesService;

        public MoviesController(
            IDeletableEntityRepository<Movie> moviesRepository,
            IMoviesService moviesService,
            IDirectorsService directorsService,
            IGenresService genresService,
            ICountriesService countriesService,
            ILanguagesService languagesService)
        {
            this.moviesRepository = moviesRepository;
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
            var directors = await this.directorsService
                   .GetAllAsync<DirectorSimpleViewModel>();
            var genres = await this.genresService
                .GetAllAsync<GenreSimpleViewModel>();
            var countries = await this.countriesService
                .GetAllAsync<CountrySimpleViewModel>();
            var languages = await this.languagesService
                 .GetAllAsync<LanguageSimpleViewModel>();

            var model = new MovieCreateInputModel
            {
                Directors = directors,
                Genres = genres,
                ProductionCountries = countries,
                Languages = languages,
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieCreateInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                var directors = await this.directorsService
                    .GetAllAsync<DirectorSimpleViewModel>();
                var genres = await this.genresService
                    .GetAllAsync<GenreSimpleViewModel>();
                var countries = await this.countriesService
                    .GetAllAsync<CountrySimpleViewModel>();
                var languages = await this.languagesService
                     .GetAllAsync<LanguageSimpleViewModel>();

                inputModel.Directors = directors;
                inputModel.Genres = genres;
                inputModel.ProductionCountries = countries;
                inputModel.Languages = languages;

                return this.View(inputModel);
            }

            await this.moviesService.CreateAsync(inputModel);
            return this.RedirectToAction("GetAll", "Movies", new { area = "Administration" });
        }

        //public async Task<IActionResult> Edit(int id)
        //{
        //    var viewModel = await this.actorsService.GetViewModelByIdAsync<ActorEditViewModel>(id);

        //    return this.View(viewModel);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Edit(ActorEditViewModel actorEditViewModel)
        //{
        //    if (!this.ModelState.IsValid)
        //    {
        //        return this.View(actorEditViewModel);
        //    }

        //    await this.actorsService.EditAsync(actorEditViewModel);

        //    return this.RedirectToAction("GetAll", "Actors", new { area = "Administration" });
        //}

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.moviesService.DeleteAsync(id);

            return this.RedirectToAction("GetAll", "Movies", new { area = "Administration" });
        }

        public async Task<IActionResult> GetAll(int page = 1)
        {
            var movies = this.moviesService.GetAllMoviesAsQueryable<MoviesAdministrationViewModel>();

            var paginatedList = await PaginatedList<MoviesAdministrationViewModel>
                .CreateAsync(movies, page, GlobalConstants.AdministrationItemsPerPage);

            return this.View(paginatedList);
        }
    }
}
