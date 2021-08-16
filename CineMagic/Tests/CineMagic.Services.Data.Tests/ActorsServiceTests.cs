namespace CineMagic.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;
    using CineMagic.Data;
    using CineMagic.Data.Common.Repositories;
    using CineMagic.Data.Models;
    using CineMagic.Data.Models.Enums;
    using CineMagic.Data.Repositories;
    using CineMagic.Services.Mapping;
    using CineMagic.Web.ViewModels.Actors;
    using CineMagic.Web.ViewModels.InputModels.Administration;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class ActorsServiceTests
    {
        public ActorsServiceTests()
        {
            this.InitializeMapper();
        }

        [Fact]
        public async Task MethodGetActorByIdAsyncShouldReturnCorrectActor()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            dbContext.Actors.Add(new Actor { });
            dbContext.Actors.Add(new Actor { });
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Actor>(dbContext);

            var service = new ActorsService(repository);

            // Act
            var actualResult = await service.GetActorByIdAsync<ActorSinglePageViewModel>(2);

            // Assert
            Assert.Equal(2, actualResult.Id);
        }

        [Fact]
        public async Task MethodGetActorByIdAsyncShouldThrowExceptionWhenInvalidIdIsGiven()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            dbContext.Actors.Add(new Actor { });
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Actor>(dbContext);

            var service = new ActorsService(repository);

            // Act - Assert
            await Assert.ThrowsAsync<NullReferenceException>(()
                 => service.GetActorByIdAsync<ActorSinglePageViewModel>(2));
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
                Birthday = DateTime.Now,
                Gender = Gender.Female,
            });

            dbContext.Actors.Add(new Actor
            {
                Birthday = DateTime.Now.AddDays(-5),
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
                Birthday = DateTime.Now,
                Gender = Gender.Female,
            });

            dbContext.Actors.Add(new Actor
            {
                Name = "BTest",
                Birthday = DateTime.Now.AddDays(-5),
                Gender = Gender.Female,
            });

            dbContext.Actors.Add(new Actor
            {
                Name = "ATest",
                Birthday = DateTime.Now,
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
                Gender = Gender.Female,
                Popularity = 1,
            });

            dbContext.Actors.Add(new Actor
            {
                Gender = Gender.Male,
                Popularity = 2,
            });

            dbContext.Actors.Add(new Actor
            {
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

            dbContext.Add(new Actor
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
        public async Task GetAllActorsAsQueryableShouldReturnCorrectCount()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            dbContext.Actors.Add(Enumerable.Range(0, 3).Select(x => new Actor { });

            using var repository = new EfDeletableEntityRepository<Actor>(dbContext);

            var service = new ActorsService(repository);

            // Act
            var actualResult = service.GetAllActorsAsQueryable<ActorStandartViewModel>().Count;

            // Assert
            Assert.Equal(1, actualResult);
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
