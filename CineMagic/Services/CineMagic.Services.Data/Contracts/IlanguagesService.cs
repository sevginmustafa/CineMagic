namespace CineMagic.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CineMagic.Web.ViewModels.InputModels.Administration;
    using CineMagic.Web.ViewModels.Languages;

    public interface ILanguagesService
    {
        Task<IEnumerable<T>> GetAllAsync<T>();

        Task CreateAsync(LanguageCreateInputModel inputModel);

        Task DeleteAsync(int id);

        Task EditAsync(LanguageEditViewModel languageEditViewModel);

        Task<T> GetViewModelByIdAsync<T>(int id);

        IQueryable<T> GetAllLanguagesAsQueryable<T>();
    }
}
