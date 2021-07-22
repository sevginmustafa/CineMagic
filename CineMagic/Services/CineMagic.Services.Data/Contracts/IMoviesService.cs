namespace CineMagic.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMoviesService
    {
        T GetBannerSectionMovie<T>();

        Task<IEnumerable<T>> GetRecentMoviesAsync<T>(int count);

        Task<IEnumerable<T>> GetPopularMoviesAsync<T>(int count);

        Task<IEnumerable<T>> GetTopRatedMoviesAsync<T>(int count);

        Task<IEnumerable<T>> GetLatestMoviesAsync<T>(int count);

        Task<IEnumerable<T>> GetWatchlistMovies<T>(string userId, int count);

        Task<IEnumerable<T>> GetMoviesByGenreName<T>(string name, int page, int itemsPerPage);

        int GetMoviesByGenreNameCount(string name);

        Task<IEnumerable<T>> GetMoviesByCountryName<T>(string name, int page, int itemsPerPage);

        int GetMoviesByCountryNameCount(string name);

        Task<IEnumerable<T>> GetMoviesByReleaseYear<T>(int year, int page, int itemsPerPage);

        int GetMoviesByReleaseYearCount(int year);
    }
}
