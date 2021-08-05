namespace CineMagic.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IDirectorsService
    {
        IQueryable<T> GetDirectorsBornTodayAsQueryable<T>(int gender);

        IQueryable<T> GetMostPopularDirectorsAsQueryable<T>(int gender, int count);

        Task<T> GetDirectorByIdAsync<T>(int id);
    }
}
