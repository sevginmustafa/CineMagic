namespace CineMagic.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IMoviesService
    {
        Task<IEnumerable<T>> GetRecentMoviesAsync<T>(int count);

        Task<IEnumerable<T>> GetPopularMoviesAsync<T>(int count);

        Task<IEnumerable<T>> GetTopRatedMoviesAsync<T>(int count);
    }
}
