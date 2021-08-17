namespace CineMagic.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using CineMagic.Data;
    using CineMagic.Data.Models;
    using CineMagic.Data.Repositories;
    using CineMagic.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class RatingsServiceTests
    {
        [Theory]
        [InlineData(1, 4.5)]
        [InlineData(2, 0)]
        public async Task GetAverageRatingAsyncShouldReturnCorrectValue(int movieId, double expectedResult)
        {
            using var dbContext = this.InitializeDatabase();

            await dbContext.Ratings.AddAsync(new Rating
            {
                MovieId = 1,
                UserId = Guid.NewGuid().ToString(),
                Rate = 5,
            });

            await dbContext.Ratings.AddAsync(new Rating
            {
                MovieId = 1,
                UserId = Guid.NewGuid().ToString(),
                Rate = 4,
            });

            await dbContext.SaveChangesAsync();

            using var repository = new EfRepository<Rating>(dbContext);

            var service = new RatingsService(repository);

            // Act
            var actualResult = await service.GetAverageRatingAsync(movieId);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(1, "third", 3.5)]
        [InlineData(1, "first", 5)]
        [InlineData(2, "first", 4)]
        public async Task GetUserRatingAsyncShouldReturnCorrectValue(int movieId, string userId, double expectedResult)
        {
            using var dbContext = this.InitializeDatabase();

            await dbContext.Ratings.AddAsync(new Rating
            {
                MovieId = 1,
                UserId = "first",
                Rate = 5,
            });

            await dbContext.Ratings.AddAsync(new Rating
            {
                MovieId = 2,
                UserId = "first",
                Rate = 4,
            });

            await dbContext.Ratings.AddAsync(new Rating
            {
                MovieId = 1,
                UserId = "second",
                Rate = 2,
            });

            await dbContext.SaveChangesAsync();

            using var repository = new EfRepository<Rating>(dbContext);

            var service = new RatingsService(repository);

            // Act
            var actualResult = await service.GetUserRatingAsync(movieId, userId);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task SetRateAsyncShouldCreateNewRatingIfMovieIdAndUserIdAsACombinationDoesNotExist()
        {
            using var dbContext = this.InitializeDatabase();

            using var repository = new EfRepository<Rating>(dbContext);

            var service = new RatingsService(repository);

            // Act
            await service.SetRateAsync(2, 1, "first");

            var actualResult = await repository.AllAsNoTracking().FirstOrDefaultAsync();

            // Assert
            Assert.Equal(2, actualResult.Rate);
        }

        [Fact]
        public async Task SetRateAsyncShouldChangeOnlyTheRateIfMovieIdAndUserIdAsACombinationExist()
        {
            using var dbContext = this.InitializeDatabase();

            using var repository = new EfRepository<Rating>(dbContext);

            var rating = new Rating
            {
                MovieId = 1,
                UserId = "first",
                Rate = 5,
            };

            var service = new RatingsService(repository);

            // Act
            await service.SetRateAsync(1.5, 1, "first");

            var actualResult = await repository.AllAsNoTracking().FirstOrDefaultAsync();

            // Assert
            Assert.Equal(1.5, actualResult.Rate);
        }

        private CineMagicDbContext InitializeDatabase()
        {
            var options = new DbContextOptionsBuilder<CineMagicDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            return new CineMagicDbContext(options);
        }
    }
}
