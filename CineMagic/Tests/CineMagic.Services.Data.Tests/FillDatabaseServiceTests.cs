namespace CineMagic.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CineMagic.Data;
    using CineMagic.Data.Models;
    using CineMagic.Data.Repositories;
    using CineMagic.Services.GetDataFromTMDB;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class FillDatabaseServiceTests
    {
        [Fact]
        public async Task AddDataToDBAsyncShouldFillDatabaseWithCorrectData()
        {
            using var dbContext = this.InitializeDatabase();

            using var moviesRepository = new EfDeletableEntityRepository<Movie>(dbContext);
            using var directorsRepository = new EfDeletableEntityRepository<Director>(dbContext);
            using var actorsRepository = new EfDeletableEntityRepository<Actor>(dbContext);
            using var genresRepository = new EfDeletableEntityRepository<Genre>(dbContext);
            using var countriesRepository = new EfDeletableEntityRepository<Country>(dbContext);
            using var languagesRepository = new EfDeletableEntityRepository<Language>(dbContext);
            var getDataFromTMDBService = new GetDataFromTMDBService();

            var service = new FillDatabaseService(
                moviesRepository,
                directorsRepository,
                actorsRepository,
                genresRepository,
                countriesRepository,
                languagesRepository,
                getDataFromTMDBService);

            await service.AddDataToDBAsync(98, 100);

            var actualResult = moviesRepository.AllAsNoTracking().Count();

            Assert.Equal(3, actualResult);
        }

        [Fact]
        public async Task GetLastMovieAddedTmdbIdShouldReturnCorrectResult()
        {
            using var dbContext = this.InitializeDatabase();

            using var moviesRepository = new EfDeletableEntityRepository<Movie>(dbContext);
            using var directorsRepository = new EfDeletableEntityRepository<Director>(dbContext);
            using var actorsRepository = new EfDeletableEntityRepository<Actor>(dbContext);
            using var genresRepository = new EfDeletableEntityRepository<Genre>(dbContext);
            using var countriesRepository = new EfDeletableEntityRepository<Country>(dbContext);
            using var languagesRepository = new EfDeletableEntityRepository<Language>(dbContext);
            var getDataFromTMDBService = new GetDataFromTMDBService();

            var service = new FillDatabaseService(
                moviesRepository,
                directorsRepository,
                actorsRepository,
                genresRepository,
                countriesRepository,
                languagesRepository,
                getDataFromTMDBService);

            await dbContext.AddAsync(new Movie { TMDBId = 1 });
            await dbContext.SaveChangesAsync();

            var actualResult = service.GetLastMovieAddedTmdbId();

            Assert.Equal(1, actualResult);
        }

        [Fact]
        public void GetLastMovieAddedTmdbIdShouldReturnZeroIfDBIsEmpty()
        {
            using var dbContext = this.InitializeDatabase();

            using var moviesRepository = new EfDeletableEntityRepository<Movie>(dbContext);
            using var directorsRepository = new EfDeletableEntityRepository<Director>(dbContext);
            using var actorsRepository = new EfDeletableEntityRepository<Actor>(dbContext);
            using var genresRepository = new EfDeletableEntityRepository<Genre>(dbContext);
            using var countriesRepository = new EfDeletableEntityRepository<Country>(dbContext);
            using var languagesRepository = new EfDeletableEntityRepository<Language>(dbContext);
            var getDataFromTMDBService = new GetDataFromTMDBService();

            var service = new FillDatabaseService(
                moviesRepository,
                directorsRepository,
                actorsRepository,
                genresRepository,
                countriesRepository,
                languagesRepository,
                getDataFromTMDBService);

            var actualResult = service.GetLastMovieAddedTmdbId();

            Assert.Equal(0, actualResult);
        }

        private CineMagicDbContext InitializeDatabase()
        {
            var options = new DbContextOptionsBuilder<CineMagicDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            return new CineMagicDbContext(options);
        }
    }
}
