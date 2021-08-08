namespace CineMagic.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using CineMagic.Web.ViewModels.InputModels.Contacts;

    public interface IContactsService
    {
        Task GetEnquiryFromUserAsync(ContactFormInputModel inputModel);

        Task SendEnquiryToUserAsync(ContactFormInputModel inputModel);
    }
}
