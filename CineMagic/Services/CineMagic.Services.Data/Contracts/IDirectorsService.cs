namespace CineMagic.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CineMagic.Web.ViewModels.Directors;
    using CineMagic.Web.ViewModels.InputModels.Administration;

    public interface IDirectorsService
    {
        IQueryable<T> GetDirectorsBornTodayAsQueryable<T>(int gender);

        IQueryable<T> GetMostPopularDirectorsAsQueryable<T>(int gender, int count);

        IQueryable<T> GetDirectorsByLetterAsQueryable<T>(string letter);

        IQueryable<T> SearchDirectorsByNameAsQueryable<T>(string name);

        Task<T> GetDirectorByIdAsync<T>(int id);

        Task CreateAsync(DirectorCreateInpuModel inputModel);

        Task DeleteAsync(int id);

        Task EditAsync(DirectorEditViewModel directorEditViewModel);

        Task<T> GetViewModelByIdAsync<T>(int id);

        Task<IEnumerable<T>> GetAllAsync<T>();

        IQueryable<T> GetAllDirectorsAsQueryableOrderedByName<T>();

        IQueryable<T> GetAllDirectorsAsQueryableOrderedByCreatedOn<T>();
    }
}
