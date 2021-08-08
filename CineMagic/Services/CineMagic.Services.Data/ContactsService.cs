namespace CineMagic.Services.Data
{
    using System.Threading.Tasks;

    using CineMagic.Common;
    using CineMagic.Data.Common.Repositories;
    using CineMagic.Data.Models;
    using CineMagic.Services.Data.Contracts;
    using CineMagic.Services.Messaging;
    using CineMagic.Web.ViewModels.InputModels.Contacts;

    public class ContactsService : IContactsService
    {
        private readonly IRepository<ContactFormEntry> userContactsRepository;
        private readonly IRepository<AdminContactFormEntry> adminContactsRepository;
        private readonly IEmailSender emailSender;

        public ContactsService(
            IRepository<ContactFormEntry> userContactsRepository,
            IRepository<AdminContactFormEntry> adminContactsRepository,
            IEmailSender emailSender)
        {
            this.userContactsRepository = userContactsRepository;
            this.adminContactsRepository = adminContactsRepository;
            this.emailSender = emailSender;
        }

        public async Task GetEnquiryFromUserAsync(ContactFormInputModel inputModel)
        {
            var enquiry = new ContactFormEntry
            {
                Name = inputModel.Name,
                Email = inputModel.Email,
                Subject = inputModel.Subject,
                Message = inputModel.Message,
            };

            await this.userContactsRepository.AddAsync(enquiry);
            await this.userContactsRepository.SaveChangesAsync();

            // await this.emailSender.SendEmailAsync(
            //    enquiry.Email,
            //    enquiry.Name,
            //    GlobalConstants.SystemEmail,
            //    enquiry.Subject,
            //    enquiry.Message);
        }

        public async Task SendEnquiryToUserAsync(ContactFormInputModel inputModel)
        {
            var enquiry = new AdminContactFormEntry
            {
                Name = inputModel.Name,
                Email = inputModel.Email,
                Subject = inputModel.Subject,
                Message = inputModel.Message,
            };

            await this.adminContactsRepository.AddAsync(enquiry);
            await this.adminContactsRepository.SaveChangesAsync();

            await this.emailSender.SendEmailAsync(
                GlobalConstants.SystemEmail,
                enquiry.Name,
                enquiry.Email,
                enquiry.Subject,
                enquiry.Message);
        }
    }
}
