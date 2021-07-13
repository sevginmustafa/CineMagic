namespace InTheAction.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IMoviesService
    {
        Task<IEnumerable<T>> GetRecentMovies<T>(int count);

        Task<IEnumerable<T>> GetTopRatedMovies<T>(int count);
    }
}
