namespace CineMagic.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CineMagic.Common;
    using CineMagic.Data.Common.Repositories;
    using CineMagic.Data.Models;
    using CineMagic.Services.Data.Common;
    using CineMagic.Services.Data.Contracts;
    using CineMagic.Services.Mapping;
    using CineMagic.Services.Messaging;
    using CineMagic.Web.ViewModels.InputModels.Administration;
    using CineMagic.Web.ViewModels.InputModels.Contacts;
    using Microsoft.EntityFrameworkCore;

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

        public IQueryable<T> GetAllEnquiriesFromUsersAsQueryable<T>()
            => this.userContactsRepository
            .AllAsNoTracking()
            .OrderBy(x => x.CreatedOn)
            .To<T>();

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
        }

        public async Task SendAnswerToUserAsync(AdminContactFormInputModel inputModel)
        {
            var answer = new AdminContactFormEntry
            {
                Name = inputModel.Name,
                Email = inputModel.Email,
                To = inputModel.To,
                Subject = inputModel.Subject,
                Message = inputModel.Message,
            };

            await this.adminContactsRepository.AddAsync(answer);
            await this.adminContactsRepository.SaveChangesAsync();

            await this.emailSender.SendEmailAsync(
                GlobalConstants.SystemEmail,
                answer.Name,
                inputModel.To,
                answer.Subject,
                answer.Message);
        }

        public async Task DeleteAsync(int id)
        {
            var enquiry = await this.userContactsRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (enquiry == null)
            {
                throw new NullReferenceException(
                   string.Format(ExceptionMessages.EnquiryNotFound, id));
            }

            this.userContactsRepository.Delete(enquiry);
            await this.userContactsRepository.SaveChangesAsync();
        }

        public async Task<T> GetViewModelByIdAsync<T>(int id)
        {
            var enquiry = await this.userContactsRepository
                 .AllAsNoTracking()
                 .Where(x => x.Id == id)
                 .To<T>()
                 .FirstOrDefaultAsync();

            if (enquiry == null)
            {
                throw new NullReferenceException(
                   string.Format(ExceptionMessages.EnquiryNotFound, id));
            }

            return enquiry;
        }

        public ContactFormEntry GetEnquiryById(int id)
        {
            var enquiry = this.userContactsRepository
               .AllAsNoTracking()
               .FirstOrDefault(x => x.Id == id);

            if (enquiry == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.EnquiryNotFound, id));
            }

            return enquiry;
        }
    }
}
