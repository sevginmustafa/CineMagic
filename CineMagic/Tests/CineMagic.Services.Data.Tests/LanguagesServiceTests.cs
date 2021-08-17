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
    using CineMagic.Web.ViewModels.InputModels.Administration;
    using CineMagic.Web.ViewModels.Languages;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class LanguagesServiceTests
    {
        public LanguagesServiceTests()
        {
            this.InitializeMapper();
        }

        [Fact]
        public async Task GetAllAsyncShouldReturnCorrectCount()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Languages.AddAsync(new Language { Name = "English" });
            await dbContext.Languages.AddAsync(new Language { Name = "German" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Language>(dbContext);

            var service = new LanguagesService(repository);

            // Act
            var actualResult = await service.GetAllAsync<LanguageSimpleViewModel>();

            // Assert
            Assert.Equal(2, actualResult.Count());
        }

        [Fact]
        public async Task CreateAsyncShouldWorkCorrectly()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            var languageCreateInputModel = new LanguageCreateInputModel { Name = "French" };

            using var repository = new EfDeletableEntityRepository<Language>(dbContext);

            var service = new LanguagesService(repository);

            // Act
            await service.CreateAsync(languageCreateInputModel);

            var actualResult = repository.AllAsNoTracking().Count();

            // Assert
            Assert.Equal(1, actualResult);
        }

        [Fact]
        public async Task CreateAsyncShouldThrowExceptionWhenLanguageAlreadyExists()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Languages.AddAsync(new Language { Name = "Bulgarian" });

            await dbContext.SaveChangesAsync();

            var languageCreateInputModel = new LanguageCreateInputModel { Name = "Bulgarian" };

            using var repository = new EfDeletableEntityRepository<Language>(dbContext);

            var service = new LanguagesService(repository);

            // Act - Assert
            await Assert.ThrowsAsync<ArgumentException>(()
                => service.CreateAsync(languageCreateInputModel));
        }

        [Fact]
        public async Task GetAllLanguagesAsQueryableShouldReturnCorrectCount()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Languages.AddAsync(new Language { Name = "Turkish" });
            await dbContext.Languages.AddAsync(new Language { Name = "German" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Language>(dbContext);

            var service = new LanguagesService(repository);

            // Act
            var actualResult = service.GetAllLanguagesAsQueryable<LanguageSimpleViewModel>();

            // Assert
            Assert.Equal(2, actualResult.Count());
        }

        [Fact]
        public async Task DeleteAsyncShouldDecreaseCount()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Languages.AddAsync(new Language { Name = "English" });
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Language>(dbContext);

            var service = new LanguagesService(repository);

            var entity = await dbContext.Languages.FindAsync(1);
            dbContext.Entry(entity).State = EntityState.Detached;

            // Act
            await service.DeleteAsync(1);

            var actualResult = repository.AllAsNoTracking().Count();

            // Assert
            Assert.Equal(0, actualResult);
        }

        [Fact]
        public async Task DeleteAsyncShouldThrowExceptionWhenInvalidIdIsGiven()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Languages.AddAsync(new Language { Name = "Bulgarian" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Language>(dbContext);

            var service = new LanguagesService(repository);

            // Act - Assert
            await Assert.ThrowsAsync<NullReferenceException>(()
                => service.DeleteAsync(7));
        }

        [Fact]
        public async Task EditAsyncShouldWorkCorrectly()
        {
            // Arrange
            var languageEditViewModel = new LanguageEditViewModel
            {
                Id = 1,
                Name = "Spanish",
            };

            using var dbContext = this.InitializeDatabase();

            await dbContext.Languages.AddAsync(new Language { Name = "English" });
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Language>(dbContext);

            var service = new LanguagesService(repository);

            var entity = await dbContext.Languages.FindAsync(1);
            dbContext.Entry(entity).State = EntityState.Detached;

            // Act
            await service.EditAsync(languageEditViewModel);

            var actualResult = await repository.AllAsNoTracking().FirstOrDefaultAsync();

            // Assert
            Assert.Equal("Spanish", actualResult.Name);
        }

        [Fact]
        public async Task EditAsyncShouldThrowExceptionWhenInvalidIdIsGiven()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            var languageEditViewModel = new LanguageEditViewModel
            {
                Id = 5,
                Name = "Punjabi",
            };

            await dbContext.Languages.AddAsync(new Language { Name = "Mandarin" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Language>(dbContext);

            var service = new LanguagesService(repository);

            // Act - Assert
            await Assert.ThrowsAsync<NullReferenceException>(()
                => service.EditAsync(languageEditViewModel));
        }

        [Fact]
        public async Task GetViewModelByIdAsyncShouldReturnCorrectViewModel()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Languages.AddAsync(new Language { Name = "English" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Language>(dbContext);

            var service = new LanguagesService(repository);

            // Act
            var actualResult = await service.GetViewModelByIdAsync<LanguageEditViewModel>(1);

            // Assert
            Assert.IsType<LanguageEditViewModel>(actualResult);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncShouldThrowExceptionWhenInvalidIdIsGiven()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Languages.AddAsync(new Language { Name = "Hindi" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Language>(dbContext);

            var service = new LanguagesService(repository);

            // Act - Assert
            await Assert.ThrowsAsync<NullReferenceException>(()
                => service.GetViewModelByIdAsync<LanguageEditViewModel>(5));
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
