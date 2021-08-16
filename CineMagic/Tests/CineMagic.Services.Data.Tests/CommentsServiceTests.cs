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
    using CineMagic.Web.ViewModels.InputModels.Comments;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class CommentsServiceTests
    {
        public CommentsServiceTests()
        {
            this.InitializeMapper();
        }

        [Fact]
        public async Task CreateMovieCommentAsyncShouldWorkProperly()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            using var movieCommentsRepository = new EfRepository<MovieComment>(dbContext);
            using var actorCommentsRepository = new EfRepository<ActorComment>(dbContext);
            using var directorsCommentsRepository = new EfRepository<DirectorComment>(dbContext);

            var service = new CommentsService(
                movieCommentsRepository,
                actorCommentsRepository,
                directorsCommentsRepository);

            // Act
            var movieCommentInputModel = new MovieCommentInputModel
            {
                Content = "TestContent",
            };

            await service.CreateMovieCommentAsync(movieCommentInputModel);

            var actualResult = movieCommentsRepository.AllAsNoTracking().Count();

            // Assert
            Assert.Equal(1, actualResult);
        }

        [Fact]
        public async Task CreateActorCommentAsyncShouldWorkProperly()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            using var movieCommentsRepository = new EfRepository<MovieComment>(dbContext);
            using var actorCommentsRepository = new EfRepository<ActorComment>(dbContext);
            using var directorsCommentsRepository = new EfRepository<DirectorComment>(dbContext);

            var service = new CommentsService(
                movieCommentsRepository,
                actorCommentsRepository,
                directorsCommentsRepository);

            // Act
            var actorCommentInputModel = new ActorCommentInputModel
            {
                Content = "TestContent",
            };

            await service.CreateActorCommentAsync(actorCommentInputModel);

            var actualResult = actorCommentsRepository.AllAsNoTracking().Count();

            // Assert
            Assert.Equal(1, actualResult);
        }

        [Fact]
        public async Task CreateDirectorCommentAsyncShouldWorkProperly()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            using var movieCommentsRepository = new EfRepository<MovieComment>(dbContext);
            using var actorCommentsRepository = new EfRepository<ActorComment>(dbContext);
            using var directorsCommentsRepository = new EfRepository<DirectorComment>(dbContext);

            var service = new CommentsService(
                movieCommentsRepository,
                actorCommentsRepository,
                directorsCommentsRepository);

            // Act
            var directorCommentInputModel = new DirectorCommentInputModel
            {
                Content = "TestContent",
            };

            await service.CreateDirectorCommentAsync(directorCommentInputModel);

            var actualResult = directorsCommentsRepository.AllAsNoTracking().Count();

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
