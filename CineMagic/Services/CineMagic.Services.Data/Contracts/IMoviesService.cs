namespace CineMagic.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IMoviesService
    {
        T GetBannerSectionMovie<T>();

        Task<IEnumerable<T>> GetRecentMoviesAsync<T>(int count);

        Task<IEnumerable<T>> GetPopularMoviesAsync<T>(int count);

        Task<IEnumerable<T>> GetTopRatedMoviesAsync<T>(int count);

        Task<IEnumerable<T>> GetLatestMoviesAsync<T>(int count);

        Task<IEnumerable<T>> GetWatchlistMovies<T>(string userId, int count);

        Task<IEnumerable<T>> GetMoviesByGenreName<T>(string name, int pageNumber, int itemsPerPage);

        int GetMoviesByGenreNameCount(string name);

        Task<IEnumerable<T>> GetMoviesByCountryName<T>(string name);
    }
}
