namespace CineMagic.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using CineMagic.Data;
    using CineMagic.Data.Models;
    using CineMagic.Data.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class UsersServiceTests
    {
        [Fact]
        public async Task IsEmailAvailableShouldWorkReturnTrueIfGivenEmailDoesNotExists()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Users.AddAsync(new ApplicationUser
            {
                UserName = Guid.NewGuid().ToString(),
                Email = "pesho@gmail.com",
            });

            await dbContext.SaveChangesAsync();

            // Act - Assert
            using var repository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);

            var service = new UsersService(repository);

            Assert.True(service.IsEmailAvailable("gosho@gmail.com"));
        }

        [Fact]
        public async Task IsEmailAvailableShouldWorkReturnFalseIfGivenEmailAlreadyExists()
        {
            // Arrange
            using var dbContext = this.InitializeDatabase();

            await dbContext.Users.AddAsync(new ApplicationUser
            {
                UserName = Guid.NewGuid().ToString(),
                Email = "pesho@gmail.com",
            });

            await dbContext.SaveChangesAsync();

            // Act - Assert
            using var repository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);

            var service = new UsersService(repository);

            Assert.False(service.IsEmailAvailable("pesho@gmail.com"));
        }

        private CineMagicDbContext InitializeDatabase()
        {
            var options = new DbContextOptionsBuilder<CineMagicDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            return new CineMagicDbContext(options);
        }
    }
}
