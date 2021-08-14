namespace CineMagic.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CineMagic.Web.ViewModels.Genres;
    using CineMagic.Web.ViewModels.InputModels.Administration;

    public interface IGenresService
    {
        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<IEnumerable<T>> GetPopularGenresAsync<T>();

        Task CreateAsync(GenreCreateInputModel inputModel);

        Task DeleteAsync(int id);

        Task EditAsync(GenreEditViewModel directorEditViewModel);

        Task<T> GetViewModelByIdAsync<T>(int id);

        IQueryable<T> GetAllGenresAsQueryable<T>();
    }
}
