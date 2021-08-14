namespace CineMagic.Services.Data.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    using CineMagic.Web.ViewModels.InputModels.Administration;
    using CineMagic.Web.ViewModels.Privacies;

    public interface IPrivaciesService
    {
        Task<T> GetPrivacyContentAsync<T>();

        Task CreateAsync(PrivacyCreateInputModel inputModel);

        Task DeleteAsync(int id);

        Task EditAsync(PrivacyEditViewModel directorEditViewModel);

        Task<T> GetViewModelAsync<T>();
    }
}
