namespace CineMagic.Services.Data.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    using CineMagic.Web.ViewModels.Actors;

    using CineMagic.Web.ViewModels.InputModels.Administration;

    public interface IActorsService
    {
        IQueryable<T> GetActorsBornTodayAsQueryable<T>(int gender);

        IQueryable<T> GetMostPopularActorsAsQueryable<T>(int gender, int count);

        Task<T> GetActorByIdAsync<T>(int id);

        IQueryable<T> GetActorsByLetterAsQueryable<T>(string letter);

        IQueryable<T> SearchActorsByNameAsQueryable<T>(string name);

        Task CreateAsync(ActorCreateInputModel inputModel);

        Task DeleteAsync(int id);

        Task EditAsync(ActorEditViewModel actorEditViewModel);

        Task<T> GetViewModelByIdAsync<T>(int id);

        IQueryable<T> GetAllActorsAsQueryableOrderedByName<T>();

        IQueryable<T> GetAllActorsAsQueryableOrderedByCreatedOn<T>();
    }
}
