namespace CineMagic.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using CineMagic.Web.ViewModels.InputModels.Administration;

    public interface IActorsService
    {
        IQueryable<T> GetActorsBornTodayAsQueryable<T>(int gender);

        IQueryable<T> GetMostPopularActorsAsQueryable<T>(int gender, int count);

        Task<T> GetActorByIdAsync<T>(int id);

        Task CreateAsync(ActorCreateInputModel inputModel);

        IQueryable<T> GetAllActorsAsQueryable<T>();
    }
}
