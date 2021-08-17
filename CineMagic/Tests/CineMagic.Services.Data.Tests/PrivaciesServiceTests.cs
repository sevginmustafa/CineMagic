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
    using CineMagic.Web.ViewModels.Privacies;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class PrivaciesServiceTests
    {
        public PrivaciesServiceTests()
        {
            this.InitializeMapper();
        }

        [Fact]
        public async Task GetPrivacyContentAsyncShouldWorkProperly()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Privacies.AddAsync(new Privacy { Content = "This is the Privacy Page Content!" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Privacy>(dbContext);

            var service = new PrivaciesService(repository);

            // Act
            var actualResult = await service.GetPrivacyContentAsync<PrivacyContentViewModel>();

            // Assert
            Assert.Equal("This is the Privacy Page Content!", actualResult.Content);
        }

        [Fact]
        public async Task CreateAsyncShouldWorkCorrectly()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            var privacyCreateInputModel = new PrivacyCreateInputModel { Content = "This is the Privacy Page Content!" };

            using var repository = new EfDeletableEntityRepository<Privacy>(dbContext);

            var service = new PrivaciesService(repository);

            // Act
            await service.CreateAsync(privacyCreateInputModel);

            var actualResult = repository.AllAsNoTracking().Count();

            // Assert
            Assert.Equal(1, actualResult);
        }

        [Fact]
        public async Task CreateAsyncShouldThrowExceptionWhenPrivacyAlreadyExists()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Privacies.AddAsync(new Privacy { Content = "This is the Privacy Page Content!" });

            await dbContext.SaveChangesAsync();

            var privacyCreateInputModel = new PrivacyCreateInputModel { Content = "This is the Privacy Page Content!" };

            using var repository = new EfDeletableEntityRepository<Privacy>(dbContext);

            var service = new PrivaciesService(repository);

            // Act - Assert
            await Assert.ThrowsAsync<ArgumentException>(()
                => service.CreateAsync(privacyCreateInputModel));
        }

        [Fact]
        public async Task DeleteAsyncShouldDecreaseCount()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Privacies.AddAsync(new Privacy { Content = "This is the Privacy Page Content!" });
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Privacy>(dbContext);

            var service = new PrivaciesService(repository);

            var entity = await dbContext.Privacies.FindAsync(1);
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

            await dbContext.Privacies.AddAsync(new Privacy { Content = "This is the Privacy Page Content!" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Privacy>(dbContext);

            var service = new PrivaciesService(repository);

            // Act - Assert
            await Assert.ThrowsAsync<NullReferenceException>(()
                => service.DeleteAsync(2));
        }

        [Fact]
        public async Task EditAsyncShouldWorkCorrectly()
        {
            // Arrange
            var privacyEditViewModel = new PrivacyEditViewModel
            {
                Id = 1,
                Content = "This is the Privacy Page Content - Test!",
            };

            using var dbContext = this.InitializeDatabase();

            await dbContext.Privacies.AddAsync(new Privacy { Content = "This is the Privacy Page Content!" });
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Privacy>(dbContext);

            var service = new PrivaciesService(repository);

            var entity = await dbContext.Privacies.FindAsync(1);
            dbContext.Entry(entity).State = EntityState.Detached;

            // Act
            await service.EditAsync(privacyEditViewModel);

            var actualResult = await repository.AllAsNoTracking().FirstOrDefaultAsync();

            // Assert
            Assert.Equal("This is the Privacy Page Content - Test!", actualResult.Content);
        }

        [Fact]
        public async Task EditAsyncShouldThrowExceptionWhenInvalidIdIsGiven()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            var privacyEditViewModel = new PrivacyEditViewModel
            {
                Id = 4,
                Content = "This is the Privacy Page Content!",
            };

            await dbContext.Privacies.AddAsync(new Privacy { Content = "This is the Privacy Page Content - Test!" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Privacy>(dbContext);

            var service = new PrivaciesService(repository);

            // Act - Assert
            await Assert.ThrowsAsync<NullReferenceException>(()
                => service.EditAsync(privacyEditViewModel));
        }

        [Fact]
        public async Task GetViewModelByIdAsyncShouldReturnCorrectViewModel()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Privacies.AddAsync(new Privacy { Content = "This is the Privacy Page Content!" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Privacy>(dbContext);

            var service = new PrivaciesService(repository);

            // Act
            var actualResult = await service.GetViewModelAsync<PrivacyEditViewModel>();

            // Assert
            Assert.IsType<PrivacyEditViewModel>(actualResult);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncShouldThrowExceptionWhenInvalidIdIsGiven()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            using var repository = new EfDeletableEntityRepository<Privacy>(dbContext);

            var service = new PrivaciesService(repository);

            // Act - Assert
            await Assert.ThrowsAsync<NullReferenceException>(()
                => service.GetViewModelAsync<PrivacyEditViewModel>());
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
