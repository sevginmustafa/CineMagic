namespace CineMagic.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using CineMagic.Data;
    using CineMagic.Data.Models;
    using CineMagic.Data.Models.Enums;
    using CineMagic.Data.Repositories;
    using CineMagic.Services.Data.Common;
    using CineMagic.Services.Mapping;
    using CineMagic.Web.ViewModels.Directors;
    using CineMagic.Web.ViewModels.InputModels.Administration;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class DirectorsServiceTests
    {
        public DirectorsServiceTests()
        {
            this.InitializeMapper();
        }

        [Fact]
        public async Task MethodGetDirectorByIdAsyncShouldReturnCorrectDirector()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Directors.AddAsync(new Director { Name = "Test1" });
            await dbContext.Directors.AddAsync(new Director { Name = "Test2" });
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Director>(dbContext);

            var service = new DirectorsService(repository);

            // Act
            var actualResult = await service.GetDirectorByIdAsync<DirectorSinglePageViewModel>(2);

            // Assert
            Assert.Equal(2, actualResult.Id);
        }

        [Fact]
        public async Task MethodGetDirectorByIdAsyncShouldThrowExceptionWhenInvalidIdIsGiven()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            dbContext.Directors.Add(new Director { });
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Director>(dbContext);

            var service = new DirectorsService(repository);

            // Act - Assert
            var exception = await Assert.ThrowsAsync<NullReferenceException>(()
                 => service.GetDirectorByIdAsync<DirectorSinglePageViewModel>(2));

            Assert.Equal(string.Format(ExceptionMessages.DirectorNotFound, 2), exception.Message);
        }

        [Theory]
        [InlineData("j", 2)]
        [InlineData("2", 0)]
        [InlineData(" ", 4)]
        [InlineData("a", 3)]
        [InlineData("bERg", 1)]
        [InlineData(null, 4)]
        [InlineData("Q", 1)]
        public async Task SearchActorsByTitleAsQueryableShoudReturnCorrectCount(string title, int expectedResult)
        {
            using var dbContext = this.InitializeDatabase();

            await dbContext.Directors.AddAsync(new Director { Name = "John Krasinski" });
            await dbContext.Directors.AddAsync(new Director { Name = "James Cameron" });
            await dbContext.Directors.AddAsync(new Director { Name = "Steven Spielberg" });
            await dbContext.Directors.AddAsync(new Director { Name = "Quentin Tarantino" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Director>(dbContext);

            var service = new DirectorsService(repository);

            // Act
            var actualResult = service.SearchDirectorsByTitleAsQueryable<DirectorDetailedViewModel>(title).Count();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData("i", 0)]
        [InlineData("0 - 9", 0)]
        [InlineData("All", 4)]
        [InlineData("   ", 4)]
        [InlineData("j", 2)]
        [InlineData(null, 4)]
        [InlineData("B", 0)]
        public async Task GetActorsByLetterAsQueryableShoudReturnCorrectCount(string letter, int expectedResult)
        {
            using var dbContext = this.InitializeDatabase();

            await dbContext.Directors.AddAsync(new Director { Name = "John Krasinski" });
            await dbContext.Directors.AddAsync(new Director { Name = "James Cameron" });
            await dbContext.Directors.AddAsync(new Director { Name = "Steven Spielberg" });
            await dbContext.Directors.AddAsync(new Director { Name = "Quentin Tarantino" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Director>(dbContext);

            var service = new DirectorsService(repository);

            // Act
            var actualResult = service.GetDirectorsByLetterAsQueryable<DirectorDetailedViewModel>(letter).Count();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        [InlineData(2, 0)]
        [InlineData(5, 0)]
        public async Task GetDirectorsBornTodayAsQueryableShouldReturnTheCorrectDirectorsWithCorrectGender(int gender, int expected)
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            dbContext.Directors.Add(new Director
            {
                Birthday = DateTime.Now,
                Gender = Gender.Female,
            });

            dbContext.Directors.Add(new Director
            {
                Birthday = DateTime.Now.AddDays(-5),
                Gender = Gender.Female,
            });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Director>(dbContext);

            var service = new DirectorsService(repository);

            // Act
            var actualResult = service.GetDirectorsBornTodayAsQueryable<DirectorStandartViewModel>(gender).Count();

            // Assert
            Assert.Equal(expected, actualResult);
        }

        [Fact]
        public async Task GetDirectorsBornTodayAsQueryableShouldReturnDirectorsInCorrectOrder()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            dbContext.Directors.Add(new Director
            {
                Name = "CTest",
                Birthday = DateTime.Now,
                Gender = Gender.Female,
            });

            dbContext.Directors.Add(new Director
            {
                Name = "BTest",
                Birthday = DateTime.Now.AddDays(-5),
                Gender = Gender.Female,
            });

            dbContext.Directors.Add(new Director
            {
                Name = "ATest",
                Birthday = DateTime.Now,
                Gender = Gender.Female,
            });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Director>(dbContext);

            var service = new DirectorsService(repository);

            // Act
            var actualResult = service.GetDirectorsBornTodayAsQueryable<DirectorStandartViewModel>(1).FirstOrDefault().Name;

            // Assert
            Assert.Equal("ATest", actualResult);
        }

        [Theory]
        [InlineData(0, 3, 3)]
        [InlineData(1, 0, 0)]
        [InlineData(1, 1, 1)]
        [InlineData(3, 5, 0)]
        [InlineData(2, 1, 1)]
        public async Task GetMostPopularDirectorsAsQueryableShouldReturnTheCorrectDirectorsWithCorrectGenderAndCount(int gender, int count, int expected)
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            dbContext.Directors.Add(new Director
            {
                Name = "ATest",
                Gender = Gender.Female,
                Popularity = 1,
            });

            dbContext.Directors.Add(new Director
            {
                Name = "BTest",
                Gender = Gender.Male,
                Popularity = 2,
            });

            dbContext.Directors.Add(new Director
            {
                Name = "CTest",
                Gender = Gender.Male,
                Popularity = 3,
            });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Director>(dbContext);

            var service = new DirectorsService(repository);

            // Act
            var actualResult = service.GetMostPopularDirectorsAsQueryable<DirectorStandartViewModel>(gender, count).Count();

            // Assert
            Assert.Equal(expected, actualResult);
        }

        [Fact]
        public async Task GetMostPopularDirectorsAsQueryableShouldReturnDirectorsInCorrectOrder()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            dbContext.Directors.Add(new Director
            {
                Gender = Gender.Female,
                Popularity = 1,
            });

            dbContext.Directors.Add(new Director
            {
                Gender = Gender.Female,
                Popularity = 2,
            });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Director>(dbContext);

            var service = new DirectorsService(repository);

            // Act
            var actualResult = service.GetMostPopularDirectorsAsQueryable<DirectorStandartViewModel>(1, 5).FirstOrDefault().Popularity;

            // Assert
            Assert.Equal(2, actualResult);
        }

        [Fact]
        public async Task CreateAsyncShouldWorkCorrectly()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            var director = new DirectorCreateInpuModel
            {
                Gender = Gender.Female,
                Popularity = 1,
            };

            using var repository = new EfDeletableEntityRepository<Director>(dbContext);

            var service = new DirectorsService(repository);

            // Act
            await service.CreateAsync(director);

            var actualResult = repository.AllAsNoTracking().Count();

            // Assert
            Assert.Equal(1, actualResult);
        }

        [Fact]
        public async Task CreateAsyncShouldThrowExceptionWhenDirectorWithGivenNameAlreadyExists()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            dbContext.Directors.Add(new Director
            {
                Name = "Test",
            });

            await dbContext.SaveChangesAsync();

            var testDirector = new DirectorCreateInpuModel
            {
                Name = "Test",
            };

            using var repository = new EfDeletableEntityRepository<Director>(dbContext);

            var service = new DirectorsService(repository);

            // Act - Assert
            await Assert.ThrowsAsync<ArgumentException>(()
                => service.CreateAsync(testDirector));
        }

        [Fact]
        public async Task CreateAsyncShouldCreateDirectorWithCorrectProperties()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            var director = new DirectorCreateInpuModel
            {
                Name = "Pesho",
                ProfilePicPath = "www.google.com",
                Biography = "TestBiography",
                Gender = Gender.Male,
                Birthday = DateTime.UtcNow,
                Deathday = null,
                Birthplace = "Varna",
                Popularity = 5,
            };

            using var repository = new EfDeletableEntityRepository<Director>(dbContext);

            var service = new DirectorsService(repository);

            // Act
            await service.CreateAsync(director);

            var actualResult = repository.AllAsNoTracking().FirstOrDefault();

            // Assert
            Assert.Equal("Pesho", actualResult.Name);
            Assert.Equal("www.google.com", actualResult.ProfilePicPath);
            Assert.Equal("TestBiography", actualResult.Biography);
            Assert.Equal(Gender.Male, actualResult.Gender);
            Assert.Equal(2021, actualResult.Birthday.Value.Year);
            Assert.Null(actualResult.Deathday);
            Assert.Equal("Varna", actualResult.Birthplace);
            Assert.Equal(5, actualResult.Popularity);
        }

        [Fact]
        public async Task GetAllDirectorsAsQueryableOrderedByNameShouldReturnCorrectCountAndInCorrectOrder()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Directors.AddAsync(new Director { Name = "Test" });
            await dbContext.Directors.AddAsync(new Director { Name = "Test2" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Director>(dbContext);

            var service = new DirectorsService(repository);

            // Act
            var directors = service.GetAllDirectorsAsQueryableOrderedByName<DirectorStandartViewModel>();
            var actualResult = directors.FirstOrDefault().Name;

            // Assert
            Assert.Equal(2, directors.Count());
            Assert.Equal("Test", actualResult);
        }

        [Fact]
        public async Task GetAllDirectorsAsQueryableOrderedByCreatedOnShouldReturnCorrectCountAndInCorrectOrder()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Directors.AddAsync(new Director { Name = "Test" });
            await dbContext.Directors.AddAsync(new Director { Name = "Test2" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Director>(dbContext);

            var service = new DirectorsService(repository);

            // Act
            var directors = service.GetAllDirectorsAsQueryableOrderedByCreatedOn<DirectorStandartViewModel>();
            var actualResult = directors.FirstOrDefault().Name;

            // Assert
            Assert.Equal(2, directors.Count());
            Assert.Equal("Test2", actualResult);
        }

        [Fact]
        public async Task DeleteAsyncShouldWorkProperly()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Directors.AddAsync(new Director { Name = "Test" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Director>(dbContext);

            var service = new DirectorsService(repository);

            var entity = await dbContext.Directors.FindAsync(1);
            dbContext.Entry(entity).State = EntityState.Detached;

            // Act
            await service.DeleteAsync(1);

            var actualResult = repository.AllAsNoTracking().Count();

            // Assert
            Assert.Equal(0, actualResult);
        }

        [Fact]
        public async Task DeleteAsyncShouldThrowExceptionWhenIdIsInvalid()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Directors.AddAsync(new Director { Name = "Test" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Director>(dbContext);

            var service = new DirectorsService(repository);

            // Act - Assert
            await Assert.ThrowsAsync<NullReferenceException>(()
                => service.DeleteAsync(5));
        }

        [Fact]
        public async Task EditAsyncShouldChangeDirectorProperties()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            var director = new Director
            {
                Name = "Pesho",
                ProfilePicPath = "www.google.com",
                Biography = "TestBiography",
                Gender = Gender.Male,
                Birthday = DateTime.UtcNow,
                Deathday = null,
                Birthplace = "Varna",
                Popularity = 5,
            };

            await dbContext.Directors.AddAsync(director);
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Director>(dbContext);

            var service = new DirectorsService(repository);

            var entity = await dbContext.Directors.FindAsync(1);
            dbContext.Entry(entity).State = EntityState.Detached;

            // Act
            var directorEditViewModel = new DirectorEditViewModel
            {
                Id = 1,
                Name = "PeshoCopy",
                ProfilePicPath = "www.googlecopy.com",
                Biography = "TestBiographyCopy",
                Gender = Gender.Female,
                Birthday = null,
                Deathday = DateTime.UtcNow,
                Birthplace = "Sofia",
                Popularity = 2,
            };

            await service.EditAsync(directorEditViewModel);

            var actualResult = repository.AllAsNoTracking().FirstOrDefault();

            // Assert
            Assert.Equal("PeshoCopy", actualResult.Name);
            Assert.Equal("www.googlecopy.com", actualResult.ProfilePicPath);
            Assert.Equal("TestBiographyCopy", actualResult.Biography);
            Assert.Equal(Gender.Female, actualResult.Gender);
            Assert.Null(actualResult.Birthday);
            Assert.Equal(2021, actualResult.Deathday.Value.Year);
            Assert.Equal("Sofia", actualResult.Birthplace);
            Assert.Equal(2, actualResult.Popularity);
        }

        [Fact]
        public async Task EditAsyncShouldThrowExceptionWhenIdIsInvalid()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            var director = new DirectorEditViewModel
            {
                Id = 5,
                Name = "Test",
            };

            await dbContext.Directors.AddAsync(new Director { Name = "Test" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Director>(dbContext);

            var service = new DirectorsService(repository);

            // Act - Assert
            await Assert.ThrowsAsync<NullReferenceException>(()
                => service.EditAsync(director));
        }

        [Fact]
        public async Task GetViewModelByIdAsyncShouldReturnCorrectViewModel()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Directors.AddAsync(new Director { Name = "Test" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Director>(dbContext);

            var service = new DirectorsService(repository);

            // Act
            var actualResult = await service.GetViewModelByIdAsync<DirectorEditViewModel>(1);

            // Assert
            Assert.IsType<DirectorEditViewModel>(actualResult);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncShouldThrowExceptionWhenInvalidIdIsGiven()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Directors.AddAsync(new Director { Name = "Test" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Director>(dbContext);

            var service = new DirectorsService(repository);

            // Act - Assert
            await Assert.ThrowsAsync<NullReferenceException>(()
                => service.GetViewModelByIdAsync<DirectorStandartViewModel>(5));
        }

        [Fact]
        public async Task GetAllAsyncShouldReturnCorrectCount()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Directors.AddAsync(new Director { Name = "Test" });
            await dbContext.Directors.AddAsync(new Director { Name = "Tet2" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Director>(dbContext);

            var service = new DirectorsService(repository);

            // Act
            var actualResult = await service.GetAllAsync<DirectorStandartViewModel>();

            // Assert
            Assert.Equal(2, actualResult.Count());
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
