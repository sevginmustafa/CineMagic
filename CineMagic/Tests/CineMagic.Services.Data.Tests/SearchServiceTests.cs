namespace CineMagic.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using CineMagic.Data;
    using CineMagic.Data.Common.Repositories;
    using CineMagic.Data.Models;
    using CineMagic.Data.Repositories;
    using CineMagic.Services.Mapping;
    using CineMagic.Web.ViewModels.Actors;
    using CineMagic.Web.ViewModels.Directors;
    using CineMagic.Web.ViewModels.Movies;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class SearchServiceTests
    {
        public SearchServiceTests()
        {
            this.InitializeMapper();
        }

        [Theory]
        [InlineData("A", 3)]
        [InlineData("a QuIet PLAce", 1)]
        [InlineData("The", 1)]
        [InlineData("           ", 3)]
        [InlineData("Z", 0)]
        [InlineData("2", 1)]
        public async Task SearchMoviesAsQueryableShouldReturnCorrectCount(string title, int expectedResult)
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Movies.AddAsync(new Movie { Title = "Avatar" });
            await dbContext.Movies.AddAsync(new Movie { Title = "A Quiet Place 2" });
            await dbContext.Movies.AddAsync(new Movie { Title = "The Avengers" });

            await dbContext.SaveChangesAsync();

            using var moviesRepository = new EfDeletableEntityRepository<Movie>(dbContext);
            var fakeActorsRepository = new Moq.Mock<IDeletableEntityRepository<Actor>>();
            var fakeDirectorsRepository = new Moq.Mock<IDeletableEntityRepository<Director>>();

            var service = new SearchService(
                moviesRepository,
                fakeActorsRepository.Object,
                fakeDirectorsRepository.Object);

            // Act
            var actualResult = service.SearchMoviesAsQueryable<MovieStandartViewModel>(title).Count();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData("Scarlett Johansson", 1)]
        [InlineData("dW", 1)]
        [InlineData("", 3)]
        [InlineData("yNe j", 1)]
        [InlineData("R", 2)]
        [InlineData("Angelina Jolie", 0)]
        public async Task SearchActorsAsQueryableShouldReturnCorrectCount(string name, int expectedResult)
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Actors.AddAsync(new Actor { Name = "Scarlett Johansson" });
            await dbContext.Actors.AddAsync(new Actor { Name = "Robert De Niro" });
            await dbContext.Actors.AddAsync(new Actor { Name = "Dwayne Johnson" });

            await dbContext.SaveChangesAsync();

            var fakeMoviesRepository = new Moq.Mock<IDeletableEntityRepository<Movie>>();
            using var actorsRepository = new EfDeletableEntityRepository<Actor>(dbContext);
            var fakeDirectorsRepository = new Moq.Mock<IDeletableEntityRepository<Director>>();

            var service = new SearchService(
                fakeMoviesRepository.Object,
                actorsRepository,
                fakeDirectorsRepository.Object);

            // Act
            var actualResult = service.SearchActorsAsQueryable<ActorStandartViewModel>(name).Count();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData("StevenSpielberg", 0)]
        [InlineData("1", 0)]
        [InlineData("Qu", 1)]
        [InlineData("M", 1)]
        [InlineData(null, 3)]
        [InlineData("TarTino", 0)]
        public async Task SearchDirectorsAsQueryableShouldReturnCorrectCount(string name, int expectedResult)
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Directors.AddAsync(new Director { Name = "Quentin Tarantino" });
            await dbContext.Directors.AddAsync(new Director { Name = "James Cameron" });
            await dbContext.Directors.AddAsync(new Director { Name = "Steven Spielberg" });

            await dbContext.SaveChangesAsync();

            var fakeMoviesRepository = new Moq.Mock<IDeletableEntityRepository<Movie>>();
            var fakeActorsRepository = new Moq.Mock<IDeletableEntityRepository<Actor>>();
            using var directorsRepository = new EfDeletableEntityRepository<Director>(dbContext);

            var service = new SearchService(
                fakeMoviesRepository.Object,
                fakeActorsRepository.Object,
                directorsRepository);

            // Act
            var actualResult = service.SearchDirectorsAsQueryable<DirectorStandartViewModel>(name).Count();

            // Assert
            Assert.Equal(expectedResult, actualResult);
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
