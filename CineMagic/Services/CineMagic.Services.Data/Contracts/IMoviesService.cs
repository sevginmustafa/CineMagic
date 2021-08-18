namespace CineMagic.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CineMagic.Web.ViewModels.InputModels.Administration;
    using CineMagic.Web.ViewModels.Movies;

    public interface IMoviesService
    {
        Task<T> GetBannerSectionMovieAsync<T>();

        Task<IEnumerable<T>> GetRecentMoviesAsync<T>(int count);

        Task<IEnumerable<T>> GetPopularMoviesAsync<T>(int count);

        Task<IEnumerable<T>> GetTopRatedMoviesAsync<T>(int count);

        Task<IEnumerable<T>> GetLatestMoviesAsync<T>(int count);

        Task<IEnumerable<T>> GetWatchlistMoviesAsync<T>(string userId, int count);

        IQueryable<T> GetMoviesByLetterAsQueryable<T>(string letter);

        IQueryable<T> GetMoviesByGenreNameAsQueryable<T>(string name);

        IQueryable<T> GetMoviesByCountryNameAsQueryable<T>(string name);

        IQueryable<T> GetMoviesByReleaseYearAsQueryable<T>(int year);

        Task<T> GetMovieByIdAsync<T>(int id);

        Task AddToUserWatchlistAsync(int movieId, string userId);

        Task RemoveFromUserWatchlistAsync(int movieId, string userId);

        IQueryable<T> SearchMoviesByTitleAsQueryable<T>(string title);

        Task<IEnumerable<T>> GetSimilarMoviesAsync<T>(int movieId, int count);

        Task<IEnumerable<T>> GetActorMostPopularMoviesAsync<T>(int actorId, int count);

        Task<IEnumerable<T>> GetDirectorBestProfitMoviesAsync<T>(int directorId, int count);

        Task CreateAsync(MovieCreateInputModel inputModel);

        Task DeleteAsync(int id);

        Task EditAsync(MovieEditViewModel movieEditViewModel);

        IQueryable<T> GetAllMoviesAsQueryableOrderedByTitle<T>();

        IQueryable<T> GetAllMoviesAsQueryableOrderedByCreatedOn<T>();

        Task<T> GetViewModelByIdAsync<T>(int id);

        Task<IEnumerable<T>> GetAllMovieGenresAsync<T>(int movieId);

        Task<IEnumerable<T>> GetAllMovieCountriesAsync<T>(int movieId);

        Task<IEnumerable<T>> GetAllMovieLanguagesAsync<T>(int movieId);
    }
}
