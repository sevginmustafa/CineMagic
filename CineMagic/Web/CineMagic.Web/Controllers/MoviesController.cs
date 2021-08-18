namespace CineMagic.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using CineMagic.Common;
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

        public async Task<IActionResult> All(string letter, string searchByTitle, int page = 1)
        {
            var movies = Enumerable.Empty<MovieDetailedViewModel>().AsQueryable();

            if (letter != null)
            {
                movies = this.moviesService
                    .GetMoviesByLetterAsQueryable<MovieDetailedViewModel>(letter);
            }
            else
            {
                movies = this.moviesService
                    .SearchMoviesByTitleAsQueryable<MovieDetailedViewModel>(searchByTitle);
            }

            var moviesPaginated = await PaginatedList<MovieDetailedViewModel>
                .CreateAsync(movies, page, GlobalConstants.PaginatedTableItemsPerPageCount);

            var alphabetPagingViewModel = new AlphabetPagingViewModel
            {
                SelectedLetter = letter,
            };

            var viewModel = new MoviesAlphabetPaginationViewModel
            {
                Movies = moviesPaginated,
                AlphabetPagingViewModel = alphabetPagingViewModel,
                SearchString = searchByTitle,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var movie = await this.moviesService
                .GetMovieByIdAsync<MovieSinglePageViewModel>(id);

            var similarMovies = await this.moviesService
                .GetSimilarMoviesAsync<SimilarMoviesViewModel>(id, GlobalConstants.SinglePageRighSectionMoviesCount);

            var viewModel = new MovieSinglePageListViewModel
            {
                Movie = movie,
                SimilarMovies = similarMovies,
            };

            return this.View(viewModel);
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
