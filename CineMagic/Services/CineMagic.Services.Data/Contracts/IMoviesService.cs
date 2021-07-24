namespace CineMagic.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IMoviesService
    {
        T GetBannerSectionMovie<T>();

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
    }
}
