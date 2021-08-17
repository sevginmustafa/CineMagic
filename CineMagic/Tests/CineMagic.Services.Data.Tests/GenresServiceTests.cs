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
    using CineMagic.Web.ViewModels.Genres;
    using CineMagic.Web.ViewModels.InputModels.Administration;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class GenresServiceTests
    {
        public GenresServiceTests()
        {
            this.InitializeMapper();
        }

        [Fact]
        public async Task GetAllAsyncShouldReturnCorrectCount()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Genres.AddAsync(new Genre { Name = "Horror" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Comedy" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Genre>(dbContext);

            var service = new GenresService(repository);

            // Act
            var actualResult = await service.GetAllAsync<GenreSimpleViewModel>();

            // Assert
            Assert.Equal(2, actualResult.Count());
        }

        [Fact]
        public async Task GetPopularGenresAsyncShouldReturnCorrectCount()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Genres.AddAsync(new Genre { Name = "Action" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Sci-Fi" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Genre>(dbContext);

            var service = new GenresService(repository);

            // Act
            var actualResult = await service.GetPopularGenresAsync<GenreNavbarViewModel>();

            // Assert
            Assert.Equal(2, actualResult.Count());
        }

        [Fact]
        public async Task CreateAsyncShouldWorkCorrectly()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            var genreCreateInputModel = new GenreCreateInputModel { Name = "Musical" };

            using var repository = new EfDeletableEntityRepository<Genre>(dbContext);

            var service = new GenresService(repository);

            // Act
            await service.CreateAsync(genreCreateInputModel);

            var actualResult = repository.AllAsNoTracking().Count();

            // Assert
            Assert.Equal(1, actualResult);
        }

        [Fact]
        public async Task CreateAsyncShouldThrowExceptionWhenGenreAlreadyExists()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Genres.AddAsync(new Genre { Name = "Drama" });

            await dbContext.SaveChangesAsync();

            var genreCreateInputModel = new GenreCreateInputModel { Name = "Drama" };

            using var repository = new EfDeletableEntityRepository<Genre>(dbContext);

            var service = new GenresService(repository);

            // Act - Assert
            await Assert.ThrowsAsync<ArgumentException>(()
                => service.CreateAsync(genreCreateInputModel));
        }

        [Fact]
        public async Task GetAllGenresAsQueryableShouldReturnCorrectCount()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Genres.AddAsync(new Genre { Name = "Horror" });
            await dbContext.Genres.AddAsync(new Genre { Name = "War" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Genre>(dbContext);

            var service = new GenresService(repository);

            // Act
            var actualResult = service.GetAllGenresAsQueryable<GenreSimpleViewModel>();

            // Assert
            Assert.Equal(2, actualResult.Count());
        }

        [Fact]
        public async Task DeleteAsyncShouldDecreaseCount()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Genres.AddAsync(new Genre { Name = "War" });
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Genre>(dbContext);

            var service = new GenresService(repository);

            var entity = await dbContext.Genres.FindAsync(1);
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

            await dbContext.Genres.AddAsync(new Genre { Name = "Drama" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Genre>(dbContext);

            var service = new GenresService(repository);

            // Act - Assert
            await Assert.ThrowsAsync<NullReferenceException>(()
                => service.DeleteAsync(7));
        }

        [Fact]
        public async Task EditAsyncShouldWorkCorrectly()
        {
            // Arrange
            var genreEditViewModel = new GenreEditViewModel
            {
                Id = 1,
                Name = "Animation",
            };

            using var dbContext = this.InitializeDatabase();

            await dbContext.Genres.AddAsync(new Genre { Name = "Action" });
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Genre>(dbContext);

            var service = new GenresService(repository);

            var entity = await dbContext.Genres.FindAsync(1);
            dbContext.Entry(entity).State = EntityState.Detached;

            // Act
            await service.EditAsync(genreEditViewModel);

            var actualResult = await repository.AllAsNoTracking().FirstOrDefaultAsync();

            // Assert
            Assert.Equal("Animation", actualResult.Name);
        }

        [Fact]
        public async Task EditAsyncShouldThrowExceptionWhenInvalidIdIsGiven()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            var genreEditViewModel = new GenreEditViewModel
            {
                Id = 5,
                Name = "War",
            };

            await dbContext.Genres.AddAsync(new Genre { Name = "Drama" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Genre>(dbContext);

            var service = new GenresService(repository);

            // Act - Assert
            await Assert.ThrowsAsync<NullReferenceException>(()
                => service.EditAsync(genreEditViewModel));
        }

        [Fact]
        public async Task GetViewModelByIdAsyncShouldReturnCorrectViewModel()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Genres.AddAsync(new Genre { Name = "Comedy" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Genre>(dbContext);

            var service = new GenresService(repository);

            // Act
            var actualResult = await service.GetViewModelByIdAsync<GenreEditViewModel>(1);

            // Assert
            Assert.IsType<GenreEditViewModel>(actualResult);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncShouldThrowExceptionWhenInvalidIdIsGiven()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Genres.AddAsync(new Genre { Name = "War" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Genre>(dbContext);

            var service = new GenresService(repository);

            // Act - Assert
            await Assert.ThrowsAsync<NullReferenceException>(()
                => service.GetViewModelByIdAsync<GenreEditViewModel>(5));
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
