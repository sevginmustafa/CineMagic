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
    using CineMagic.Web.ViewModels.Actors;
    using CineMagic.Web.ViewModels.InputModels.Administration;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class ActorsServiceTests
    {
        public ActorsServiceTests()
        {
            this.InitializeMapper();
        }

        [Fact]
        public async Task GetActorByIdAsyncShouldReturnCorrectActor()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Actors.AddAsync(new Actor { Name = "Test1" });
            await dbContext.Actors.AddAsync(new Actor { Name = "Test2" });
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Actor>(dbContext);

            var service = new ActorsService(repository);

            // Act
            var actualResult = await service.GetActorByIdAsync<ActorSinglePageViewModel>(2);

            // Assert
            Assert.Equal(2, actualResult.Id);
        }

        [Fact]
        public async Task GetActorByIdAsyncShouldThrowExceptionWhenInvalidIdIsGiven()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            dbContext.Actors.Add(new Actor { });
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Actor>(dbContext);

            var service = new ActorsService(repository);

            // Act - Assert
            var exception = await Assert.ThrowsAsync<NullReferenceException>(()
                 => service.GetActorByIdAsync<ActorSinglePageViewModel>(2));

            Assert.Equal(string.Format(ExceptionMessages.ActorNotFound, 2), exception.Message);
        }

        [Theory]
        [InlineData("i", 4)]
        [InlineData("2", 0)]
        [InlineData("  ", 4)]
        [InlineData("BraD p", 1)]
        [InlineData("ly Blu", 1)]
        [InlineData(null, 4)]
        [InlineData("j", 2)]
        public async Task SearchActorsByTitleAsQueryableShoudReturnCorrectCount(string title, int expectedResult)
        {
            using var dbContext = this.InitializeDatabase();

            await dbContext.Actors.AddAsync(new Actor { Name = "Brad Pitt" });
            await dbContext.Actors.AddAsync(new Actor { Name = "Angelina Jolie" });
            await dbContext.Actors.AddAsync(new Actor { Name = "Jackie Chan" });
            await dbContext.Actors.AddAsync(new Actor { Name = "Emily Blunt" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Actor>(dbContext);

            var service = new ActorsService(repository);

            // Act
            var actualResult = service.SearchActorsByTitleAsQueryable<ActorDetailedViewModel>(title).Count();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData("i", 0)]
        [InlineData("0 - 9", 0)]
        [InlineData("All", 4)]
        [InlineData("   ", 4)]
        [InlineData("j", 1)]
        [InlineData(null, 4)]
        [InlineData("B", 1)]
        public async Task GetActorsByLetterAsQueryableShoudReturnCorrectCount(string letter, int expectedResult)
        {
            using var dbContext = this.InitializeDatabase();

            await dbContext.Actors.AddAsync(new Actor { Name = "Brad Pitt" });
            await dbContext.Actors.AddAsync(new Actor { Name = "Angelina Jolie" });
            await dbContext.Actors.AddAsync(new Actor { Name = "Jackie Chan" });
            await dbContext.Actors.AddAsync(new Actor { Name = "Emily Blunt" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Actor>(dbContext);

            var service = new ActorsService(repository);

            // Act
            var actualResult = service.GetActorsByLetterAsQueryable<ActorDetailedViewModel>(letter).Count();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        [InlineData(2, 0)]
        [InlineData(5, 0)]
        public async Task GetActorsBornTodayAsQueryableShouldReturnTheCorrectActorsWithCorrectGender(int gender, int expected)
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            dbContext.Actors.Add(new Actor
            {
                Birthday = DateTime.UtcNow,
                Gender = Gender.Female,
            });

            dbContext.Actors.Add(new Actor
            {
                Birthday = DateTime.UtcNow.AddDays(-5),
                Gender = Gender.Female,
            });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Actor>(dbContext);

            var service = new ActorsService(repository);

            // Act
            var actualResult = service.GetActorsBornTodayAsQueryable<ActorStandartViewModel>(gender).Count();

            // Assert
            Assert.Equal(expected, actualResult);
        }

        [Fact]
        public async Task GetActorsBornTodayAsQueryableShouldReturnActorsInCorrectOrder()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            dbContext.Actors.Add(new Actor
            {
                Name = "CTest",
                Birthday = DateTime.UtcNow,
                Gender = Gender.Female,
            });

            dbContext.Actors.Add(new Actor
            {
                Name = "BTest",
                Birthday = DateTime.UtcNow.AddDays(-5),
                Gender = Gender.Female,
            });

            dbContext.Actors.Add(new Actor
            {
                Name = "ATest",
                Birthday = DateTime.UtcNow,
                Gender = Gender.Female,
            });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Actor>(dbContext);

            var service = new ActorsService(repository);

            // Act
            var actualResult = service.GetActorsBornTodayAsQueryable<ActorStandartViewModel>(1).FirstOrDefault().Name;

            // Assert
            Assert.Equal("ATest", actualResult);
        }

        [Theory]
        [InlineData(0, 3, 3)]
        [InlineData(1, 0, 0)]
        [InlineData(1, 1, 1)]
        [InlineData(3, 5, 0)]
        [InlineData(2, 1, 1)]
        public async Task GetMostPopularActorsAsQueryableShouldReturnTheCorrectActorsWithCorrectGenderAndCount(int gender, int count, int expected)
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            dbContext.Actors.Add(new Actor
            {
                Name = "ATest",
                Gender = Gender.Female,
                Popularity = 1,
            });

            dbContext.Actors.Add(new Actor
            {
                Name = "BTest",
                Gender = Gender.Male,
                Popularity = 2,
            });

            dbContext.Actors.Add(new Actor
            {
                Name = "CTest",
                Gender = Gender.Male,
                Popularity = 3,
            });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Actor>(dbContext);

            var service = new ActorsService(repository);

            // Act
            var actualResult = service.GetMostPopularActorsAsQueryable<ActorStandartViewModel>(gender, count).Count();

            // Assert
            Assert.Equal(expected, actualResult);
        }

        [Fact]
        public async Task GetMostPopularActorsAsQueryableShouldReturnActorsInCorrectOrder()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            dbContext.Actors.Add(new Actor
            {
                Gender = Gender.Female,
                Popularity = 1,
            });

            dbContext.Actors.Add(new Actor
            {
                Gender = Gender.Female,
                Popularity = 2,
            });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Actor>(dbContext);

            var service = new ActorsService(repository);

            // Act
            var actualResult = service.GetMostPopularActorsAsQueryable<ActorStandartViewModel>(1, 5).FirstOrDefault().Popularity;

            // Assert
            Assert.Equal(2, actualResult);
        }

        [Fact]
        public async Task CreateAsyncShouldWorkCorrectly()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            var actor = new ActorCreateInputModel
            {
                Gender = Gender.Female,
                Popularity = 1,
            };

            using var repository = new EfDeletableEntityRepository<Actor>(dbContext);

            var service = new ActorsService(repository);

            // Act
            await service.CreateAsync(actor);

            var actualResult = repository.AllAsNoTracking().Count();

            // Assert
            Assert.Equal(1, actualResult);
        }

        [Fact]
        public async Task CreateAsyncShouldThrowExceptionWhenActorWithGivenNameAlreadyExists()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            dbContext.Actors.Add(new Actor
            {
                Name = "Test",
            });

            await dbContext.SaveChangesAsync();

            var testActor = new ActorCreateInputModel
            {
                Name = "Test",
            };

            using var repository = new EfDeletableEntityRepository<Actor>(dbContext);

            var service = new ActorsService(repository);

            // Act - Assert
            await Assert.ThrowsAsync<ArgumentException>(()
                => service.CreateAsync(testActor));
        }

        [Fact]
        public async Task CreateAsyncShouldCreateActorWithCorrectProperties()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            var actor = new ActorCreateInputModel
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

            using var repository = new EfDeletableEntityRepository<Actor>(dbContext);

            var service = new ActorsService(repository);

            // Act
            await service.CreateAsync(actor);

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
        public async Task GetAllActorsAsQueryableOrderedByNameShouldReturnCorrectCountAndInCorrectOrder()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Actors.AddAsync(new Actor { Name = "Test" });
            await dbContext.Actors.AddAsync(new Actor { Name = "Test2" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Actor>(dbContext);

            var service = new ActorsService(repository);

            // Act
            var actors = service.GetAllActorsAsQueryableOrderedByName<ActorStandartViewModel>();
            var actualResult = actors.FirstOrDefault().Name;

            // Assert
            Assert.Equal(2, actors.Count());
            Assert.Equal("Test", actualResult);
        }

        [Fact]
        public async Task GetAllActorsAsQueryableOrderedByCreatedOnShouldReturnCorrectCountAndInCorrectOrder()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Actors.AddAsync(new Actor { Name = "Test" });
            await dbContext.Actors.AddAsync(new Actor { Name = "Test2" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Actor>(dbContext);

            var service = new ActorsService(repository);

            // Act
            var actors = service.GetAllActorsAsQueryableOrderedByCreatedOn<ActorStandartViewModel>();
            var actualResult = actors.FirstOrDefault().Name;

            // Assert
            Assert.Equal(2, actors.Count());
            Assert.Equal("Test2", actualResult);
        }

        [Fact]
        public async Task DeleteAsyncShouldWorkProperly()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Actors.AddAsync(new Actor { Name = "Test" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Actor>(dbContext);

            var service = new ActorsService(repository);

            var entity = await dbContext.Actors.FindAsync(1);
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

            await dbContext.Actors.AddAsync(new Actor { Name = "Test" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Actor>(dbContext);

            var service = new ActorsService(repository);

            // Act - Assert
            await Assert.ThrowsAsync<NullReferenceException>(()
                => service.DeleteAsync(5));
        }

        [Fact]
        public async Task EditAsyncShouldChangeActorProperties()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            var actor = new Actor
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

            await dbContext.Actors.AddAsync(actor);
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Actor>(dbContext);

            var service = new ActorsService(repository);

            var entity = await dbContext.Actors.FindAsync(1);
            dbContext.Entry(entity).State = EntityState.Detached;

            // Act
            var actorEditViewModel = new ActorEditViewModel
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

            await service.EditAsync(actorEditViewModel);

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

            var actor = new ActorEditViewModel
            {
                Id = 5,
                Name = "Test",
            };

            await dbContext.Actors.AddAsync(new Actor { Name = "Test" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Actor>(dbContext);

            var service = new ActorsService(repository);

            // Act - Assert
            await Assert.ThrowsAsync<NullReferenceException>(()
                => service.EditAsync(actor));
        }

        [Fact]
        public async Task GetViewModelByIdAsyncShouldReturnCorrectViewModel()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Actors.AddAsync(new Actor { Name = "Test" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Actor>(dbContext);

            var service = new ActorsService(repository);

            // Act
            var actualResult = await service.GetViewModelByIdAsync<ActorEditViewModel>(1);

            // Assert
            Assert.IsType<ActorEditViewModel>(actualResult);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncShouldThrowExceptionWhenInvalidIdIsGiven()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Actors.AddAsync(new Actor { Name = "Test" });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Actor>(dbContext);

            var service = new ActorsService(repository);

            // Act - Assert
            await Assert.ThrowsAsync<NullReferenceException>(()
                => service.GetViewModelByIdAsync<ActorStandartViewModel>(5));
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
