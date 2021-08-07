namespace CineMagic.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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

        IQueryable<T> GetAllMoviesAsQueryable<T>();

        Task<T> GetMovieByIdAsync<T>(int id);

        Task AddToUserWatchlistAsync(int movieId, string userId);

        Task RemoveFromUserWatchlistAsync(int movieId, string userId);

        IQueryable<T> SearchMoviesByTitleAsQueryable<T>(string title);
    }
}
