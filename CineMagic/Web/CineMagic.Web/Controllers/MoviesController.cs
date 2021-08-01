namespace CineMagic.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CineMagic.Services.Data.Contracts;
    using CineMagic.Web.Infrastructure;
    using CineMagic.Web.ViewModels;
    using CineMagic.Web.ViewModels.Movies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class MoviesController : Controller
    {
        private readonly IMoviesService moviesService;
    
        public MoviesController(IMoviesService moviesService)
        {
            this.moviesService = moviesService;
        }

        public async Task<IActionResult> All(string letter, int page = 1)
        {
            const int ItemsPerPage = 20;

            var movies = this.moviesService.GetMoviesByLetterAsQueryable<MovieDetailedViewModel>(letter);

            var moviesPaginated = await PaginatedList<MovieDetailedViewModel>.CreateAsync(movies, page, ItemsPerPage);

            var alphabetPagingViewModel = new AlphabetPagingViewModel
            {
                SelectedLetter = letter,
            };

            var viewModel = new MoviesAlphabetPaginationViewModel
            {
                Movies = moviesPaginated,
                AlphabetPagingViewModel = alphabetPagingViewModel,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var movie = await this.moviesService.GetMovieByIdAsync<MovieSinglePageViewModel>(id);

            return this.View(movie);
        }

        [Authorize]
        public async Task<IActionResult> AddToWatchlist(int movieId)
        {
            var userId = this.User.GetId();

            await this.moviesService.AddToUserWatchlistAsync(movieId, userId);

            return this.RedirectToAction(nameof(this.Details), new { id = movieId });
        }

        [Authorize]
        public async Task<IActionResult> RemoveFromWatchlist(int movieId)
        {
            var userId = this.User.GetId();

            await this.moviesService.RemoveFromUserWatchlistAsync(movieId, userId);

            return this.RedirectToAction(nameof(this.Details), new { id = movieId });
        }
    }
}
