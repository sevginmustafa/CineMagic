namespace CineMagic.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IGenresService
    {
        Task<IEnumerable<T>> GetAllGenresAsync<T>();

        Task<IEnumerable<T>> GetPopularGenresAsync<T>();
    }
}
