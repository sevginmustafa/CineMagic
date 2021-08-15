namespace CineMagic.Services.Data.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    using CineMagic.Data.Models;
    using CineMagic.Web.ViewModels.InputModels.Administration;
    using CineMagic.Web.ViewModels.InputModels.Contacts;

    public interface IContactsService
    {
        Task GetEnquiryFromUserAsync(ContactFormInputModel inputModel);

        Task SendAnswerToUserAsync(AdminContactFormInputModel inputModel);

        Task DeleteAsync(int id);

        IQueryable<T> GetAllEnquiriesFromUsersAsQueryable<T>();

        ContactFormEntry GetEnquiryById(int id);

        Task<T> GetViewModelByIdAsync<T>(int id);
    }
}
