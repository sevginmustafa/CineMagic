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
    using CineMagic.Web.ViewModels.Countries;
    using CineMagic.Web.ViewModels.InputModels.Administration;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class CountriesServiceTests
    {
        public CountriesServiceTests()
        {
            this.InitializeMapper();
        }

        [Fact]
        public async Task GetAllAsyncShouldReturnCorrectCount()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Countries.AddAsync(new Country { Name = "Bulgaria" });
            await dbContext.Countries.AddAsync(new Country { Name = "Turkey" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Country>(dbContext);

            var service = new CountriesService(repository);

            // Act
            var actualResult = await service.GetAllAsync<CountrySimpleViewModel>();

            // Assert
            Assert.Equal(2, actualResult.Count());
        }

        [Fact]
        public async Task GetPopularCountriesAsyncShouldReturnCorrectCount()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Countries.AddAsync(new Country { Name = "Bulgaria" });
            await dbContext.Countries.AddAsync(new Country { Name = "Turkey" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Country>(dbContext);

            var service = new CountriesService(repository);

            // Act
            var actualResult = await service.GetPopularCountriesAsync<CountryNavbarViewModel>();

            // Assert
            Assert.Equal(2, actualResult.Count());
        }

        [Fact]
        public async Task CreateAsyncShouldWorkCorrectly()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            var countryCreateInputModel = new CountryCreateInputModel { Name = "Greece" };

            using var repository = new EfDeletableEntityRepository<Country>(dbContext);

            var service = new CountriesService(repository);

            // Act
            await service.CreateAsync(countryCreateInputModel);

            var actualResult = repository.AllAsNoTracking().Count();

            // Assert
            Assert.Equal(1, actualResult);
        }

        [Fact]
        public async Task CreateAsyncShouldThrowExceptionWhenCountryAlreadyExists()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Countries.AddAsync(new Country { Name = "Greece" });

            await dbContext.SaveChangesAsync();

            var countryCreateInputModel = new CountryCreateInputModel { Name = "Greece" };

            using var repository = new EfDeletableEntityRepository<Country>(dbContext);

            var service = new CountriesService(repository);

            // Act - Assert
            await Assert.ThrowsAsync<ArgumentException>(()
                => service.CreateAsync(countryCreateInputModel));
        }

        [Fact]
        public async Task GetAllCountriesAsQueryableShouldReturnCorrectCount()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Countries.AddAsync(new Country { Name = "Bulgaria" });
            await dbContext.Countries.AddAsync(new Country { Name = "Serbia" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Country>(dbContext);

            var service = new CountriesService(repository);

            // Act
            var actualResult = service.GetAllCountriesAsQueryable<CountrySimpleViewModel>();

            // Assert
            Assert.Equal(2, actualResult.Count());
        }

        [Fact]
        public async Task DeleteAsyncShouldDecreaseCount()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Countries.AddAsync(new Country { Name = "USA" });
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Country>(dbContext);

            var service = new CountriesService(repository);

            var entity = await dbContext.Countries.FindAsync(1);
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

            await dbContext.Countries.AddAsync(new Country { Name = "Greece" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Country>(dbContext);

            var service = new CountriesService(repository);

            // Act - Assert
            await Assert.ThrowsAsync<NullReferenceException>(()
                => service.DeleteAsync(7));
        }

        [Fact]
        public async Task EditAsyncShouldWorkCorrectly()
        {
            // Arrange
            var countryEditViewModel = new CountryEditViewModel
            {
                Id = 1,
                Name = "United States Of America",
            };

            using var dbContext = this.InitializeDatabase();

            await dbContext.Countries.AddAsync(new Country { Name = "USA" });
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Country>(dbContext);

            var service = new CountriesService(repository);

            var entity = await dbContext.Countries.FindAsync(1);
            dbContext.Entry(entity).State = EntityState.Detached;

            // Act
            await service.EditAsync(countryEditViewModel);

            var actualResult = await repository.AllAsNoTracking().FirstOrDefaultAsync();

            // Assert
            Assert.Equal("United States Of America", actualResult.Name);
        }

        [Fact]
        public async Task EditAsyncShouldThrowExceptionWhenInvalidIdIsGiven()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            var countryEditViewModel = new CountryEditViewModel
            {
                Id = 5,
                Name = "Iceland",
            };

            await dbContext.Countries.AddAsync(new Country { Name = "Greece" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Country>(dbContext);

            var service = new CountriesService(repository);

            // Act - Assert
            await Assert.ThrowsAsync<NullReferenceException>(()
                => service.EditAsync(countryEditViewModel));
        }

        [Fact]
        public async Task GetViewModelByIdAsyncShouldReturnCorrectViewModel()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Countries.AddAsync(new Country { Name = "Greece" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Country>(dbContext);

            var service = new CountriesService(repository);

            // Act
            var actualResult = await service.GetViewModelByIdAsync<CountryEditViewModel>(1);

            // Assert
            Assert.IsType<CountryEditViewModel>(actualResult);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncShouldThrowExceptionWhenInvalidIdIsGiven()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Countries.AddAsync(new Country { Name = "Greece" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Country>(dbContext);

            var service = new CountriesService(repository);

            // Act - Assert
            await Assert.ThrowsAsync<NullReferenceException>(()
                => service.GetViewModelByIdAsync<CountryEditViewModel>(5));
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
