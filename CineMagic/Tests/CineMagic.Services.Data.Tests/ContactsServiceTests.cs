namespace CineMagic.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using CineMagic.Data;
    using CineMagic.Data.Models;
    using CineMagic.Data.Repositories;
    using CineMagic.Services.Mapping;
    using CineMagic.Services.Messaging;
    using CineMagic.Web.ViewModels.Contacts;
    using CineMagic.Web.ViewModels.InputModels.Administration;
    using CineMagic.Web.ViewModels.InputModels.Contacts;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class ContactsServiceTests
    {
        public ContactsServiceTests()
        {
            this.InitializeMapper();
        }

        [Fact]
        public async Task GetAllEnquiriesFromUsersAsQueryableShouldReturnCorrectCount()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            using var contactFormEntryRepository = new EfRepository<ContactFormEntry>(dbContext);
            using var adminContactFormEntryRepository = new EfRepository<AdminContactFormEntry>(dbContext);

            var service = new ContactsService(
                contactFormEntryRepository,
                adminContactFormEntryRepository,
                null);

            // Act
            await contactFormEntryRepository.AddAsync(new ContactFormEntry { });
            await contactFormEntryRepository.SaveChangesAsync();

            var actualResult = service.GetAllEnquiriesFromUsersAsQueryable<ContactsAdministrationViewModel>().Count();

            // Assert
            Assert.Equal(1, actualResult);
        }

        [Fact]
        public async Task GetEnquiryFromUserAsyncShouldIncreaseCount()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            using var contactFormEntryRepository = new EfRepository<ContactFormEntry>(dbContext);
            using var adminContactFormEntryRepository = new EfRepository<AdminContactFormEntry>(dbContext);

            var service = new ContactsService(
                contactFormEntryRepository,
                adminContactFormEntryRepository,
                null);

            var inputModel = new ContactFormInputModel
            {
                Name = "Ivan",
                Email = "ivan@abv.bg",
                Subject = "I have a enquiry?",
                Message = "Please answer my question!",
            };

            // Act
            await service.GetEnquiryFromUserAsync(inputModel);

            var actualResult = contactFormEntryRepository.AllAsNoTracking().Count();

            // Assert
            Assert.Equal(1, actualResult);
        }

        [Fact]
        public async Task SendAnswerToUserAsyncShouldIncreaseCount()
        {
            var emailMock = new Moq.Mock<IEmailSender>();

            // Arrange
            using var dbContext = this.InitializeDatabase();

            using var contactFormEntryRepository = new EfRepository<ContactFormEntry>(dbContext);
            using var adminContactFormEntryRepository = new EfRepository<AdminContactFormEntry>(dbContext);

            var service = new ContactsService(
                contactFormEntryRepository,
                adminContactFormEntryRepository,
                emailMock.Object);

            var inputModel = new AdminContactFormInputModel
            {
                Name = "Admin",
                Email = "admin@abv.bg",
                Subject = "I reply to your enquiry",
                Message = "I can answer after 24 hours.",
            };

            // Act
            await service.SendAnswerToUserAsync(inputModel);

            var actualResult = adminContactFormEntryRepository.AllAsNoTracking().Count();

            // Assert
            Assert.Equal(1, actualResult);
        }

        [Fact]
        public async Task DeleteAsyncShouldDecreaseCount()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            using var contactFormEntryRepository = new EfRepository<ContactFormEntry>(dbContext);
            using var adminContactFormEntryRepository = new EfRepository<AdminContactFormEntry>(dbContext);

            var service = new ContactsService(
                contactFormEntryRepository,
                adminContactFormEntryRepository,
                null);

            await dbContext.ContactFormEntries.AddAsync(new ContactFormEntry { });
            await dbContext.ContactFormEntries.AddAsync(new ContactFormEntry { });
            await dbContext.SaveChangesAsync();

            var entity = await dbContext.ContactFormEntries.FindAsync(1);
            dbContext.Entry(entity).State = EntityState.Detached;

            // Act
            await service.DeleteAsync(1);

            var actualResult = contactFormEntryRepository.AllAsNoTracking().Count();

            // Assert
            Assert.Equal(1, actualResult);
        }

        [Fact]
        public async Task DeleteAsyncShouldThrowExceptionIfInvaliIdIsGiven()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            using var contactFormEntryRepository = new EfRepository<ContactFormEntry>(dbContext);
            using var adminContactFormEntryRepository = new EfRepository<AdminContactFormEntry>(dbContext);

            var service = new ContactsService(
                contactFormEntryRepository,
                adminContactFormEntryRepository,
                null);

            await dbContext.ContactFormEntries.AddAsync(new ContactFormEntry { });
            await dbContext.SaveChangesAsync();

            // Act - Arrange
            await Assert.ThrowsAsync<NullReferenceException>(()
               => service.DeleteAsync(8));
        }

        [Fact]
        public async Task GetViewModelByIdAsyncShouldReturnCorrectViewModel()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            using var contactFormEntryRepository = new EfRepository<ContactFormEntry>(dbContext);
            using var adminContactFormEntryRepository = new EfRepository<AdminContactFormEntry>(dbContext);

            var service = new ContactsService(
                contactFormEntryRepository,
                adminContactFormEntryRepository,
                null);

            await dbContext.ContactFormEntries.AddAsync(new ContactFormEntry
            {
                Name = "Ivan",
                Email = "ivan@abv.bg",
                Subject = "I have a enquiry?",
                Message = "Please answer my question!",
            });

            await dbContext.SaveChangesAsync();

            // Act
            var actualResult = await service.GetViewModelByIdAsync<ContactDetailedViewModel>(1);

            // Assert
            Assert.IsType<ContactDetailedViewModel>(actualResult);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncShouldThrowExceptionWhenInvalidIdIsGiven()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            using var contactFormEntryRepository = new EfRepository<ContactFormEntry>(dbContext);
            using var adminContactFormEntryRepository = new EfRepository<AdminContactFormEntry>(dbContext);

            var service = new ContactsService(
                contactFormEntryRepository,
                adminContactFormEntryRepository,
                null);

            await dbContext.ContactFormEntries.AddAsync(new ContactFormEntry { });

            await dbContext.SaveChangesAsync();

            // Act - Assert
            await Assert.ThrowsAsync<NullReferenceException>(()
                => service.GetViewModelByIdAsync<ContactDetailedViewModel>(5));
        }

        [Fact]
        public async Task GetEnquiryByIdShouldReturnCorrectdContactFormEntryModel()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            using var contactFormEntryRepository = new EfRepository<ContactFormEntry>(dbContext);
            using var adminContactFormEntryRepository = new EfRepository<AdminContactFormEntry>(dbContext);

            var service = new ContactsService(
                contactFormEntryRepository,
                adminContactFormEntryRepository,
                null);

            await dbContext.ContactFormEntries.AddAsync(new ContactFormEntry
            {
                Name = "Ivan",
                Email = "ivan@abv.bg",
                Subject = "I have a enquiry?",
                Message = "Please answer my question!",
            });

            await dbContext.ContactFormEntries.AddAsync(new ContactFormEntry
            {
                Name = "Dragan",
                Email = "dragan@abv.bg",
                Subject = "Question?",
                Message = "How are you, Admin?",
            });

            await dbContext.SaveChangesAsync();

            // Act
            var actualResult = service.GetEnquiryById(2);

            // Assert
            Assert.Equal("Dragan", actualResult.Name);
        }

        [Fact]
        public async Task GetEnquiryByIdShouldThrowExceptionWhenInvalidIdIsGiven()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            using var contactFormEntryRepository = new EfRepository<ContactFormEntry>(dbContext);
            using var adminContactFormEntryRepository = new EfRepository<AdminContactFormEntry>(dbContext);

            var service = new ContactsService(
                contactFormEntryRepository,
                adminContactFormEntryRepository,
                null);

            await dbContext.ContactFormEntries.AddAsync(new ContactFormEntry { });

            await dbContext.SaveChangesAsync();

            // Act - Assert
            Assert.Throws<NullReferenceException>(()
                => service.GetEnquiryById(5));
        }

        private CineMagicDbContext InitializeDatabase()
        {
            var options = new DbContextOptionsBuilder<CineMagicDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            return new CineMagicDbContext(options);
        }

        private void InitializeMapper() => AutoMapperConfig.
             RegisterMappings(Assembly.Load("CineMagic.Web.ViewModels"));
    }
}
