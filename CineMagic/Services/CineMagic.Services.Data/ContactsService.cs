namespace CineMagic.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using CineMagic.Common;
    using CineMagic.Data.Common.Repositories;
    using CineMagic.Data.Models;
    using CineMagic.Services.Data.Contracts;
    using CineMagic.Services.Messaging;
    using CineMagic.Web.ViewModels.InputModels.Contacts;

    public class ContactsService : IContactsService
    {
        private readonly IRepository<ContactFormEntry> contactsRepository;
        private readonly IEmailSender emailSender;

        public ContactsService(
            IRepository<ContactFormEntry> contactsRepository,
            IEmailSender emailSender)
        {
            this.contactsRepository = contactsRepository;
            this.emailSender = emailSender;
        }

        public async Task GetEnquiryFromUser(ContactFormInputModel inputModel)
        {
            var enquiry = new ContactFormEntry
            {
                Name = inputModel.Name,
                Email = inputModel.Email,
                Subject = inputModel.Subject,
                Message = inputModel.Message,
            };

            await this.contactsRepository.AddAsync(enquiry);
            await this.contactsRepository.SaveChangesAsync();

            await this.emailSender.SendEmailAsync(
                enquiry.Email,
                enquiry.Name,
                GlobalConstants.SystemEmail,
                enquiry.Subject,
                enquiry.Message);
        }

        public Task SendEnquiryToUser(ContactFormInputModel inputModel)
        {
            throw new NotImplementedException();
        }
    }
}
