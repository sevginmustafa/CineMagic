namespace CineMagic.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CineMagic.Services.Data.Contracts;
    using CineMagic.Web.ViewModels.Movies;
    using Microsoft.AspNetCore.Mvc;

    public class CountriesController : Controller
    {
        private readonly IMoviesService moviesService;

        public CountriesController(IMoviesService moviesService)
        {
            this.moviesService = moviesService;
        }

        public async Task<IActionResult> ByName(string name)
        {
            var movies = await this.moviesService.GetMoviesByCountryName<MovieStandartViewModel>(name);

            return this.View(movies);
        }
    }
}
