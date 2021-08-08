namespace CineMagic.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using CineMagic.Web.ViewModels.InputModels.Contacts;

    public interface IContactsService
    {
        Task GetEnquiryFromUser(ContactFormInputModel inputModel);

        Task SendEnquiryToUser(ContactFormInputModel inputModel);
    }
}
