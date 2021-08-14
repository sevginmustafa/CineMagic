namespace CineMagic.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CineMagic.Web.ViewModels.Countries;
    using CineMagic.Web.ViewModels.InputModels.Administration;

    public interface ICountriesService
    {
        Task<IEnumerable<T>> GetPopularCountriesAsync<T>();

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task CreateAsync(CountryCreateInputModel inputModel);

        Task DeleteAsync(int id);

        Task EditAsync(CountryEditViewModel directorEditViewModel);

        Task<T> GetViewModelByIdAsync<T>(int id);

        IQueryable<T> GetAllCountriesAsQueryable<T>();
    }
}
