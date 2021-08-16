namespace CineMagic.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using CineMagic.Data;
    using CineMagic.Data.Common.Repositories;
    using CineMagic.Data.Models;
    using CineMagic.Data.Repositories;
    using CineMagic.Services.Mapping;
    using CineMagic.Web.ViewModels.Settings;
    using Microsoft.EntityFrameworkCore;

    using Moq;

    using Xunit;

    public class SettingsServiceTests
    {
        public SettingsServiceTests()
        {
            this.InitializeMapper();
        }

        [Fact]
        public async Task GetCountShouldReturnCorrectNumber()
        {
            using var dbContext = this.InitializeDatabase();

            await dbContext.Settings.AddAsync(new Setting { });
            await dbContext.Settings.AddAsync(new Setting { });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Setting>(dbContext);

            var service = new SettingsService(repository);

            Assert.Equal(2, service.GetCount());
        }

        [Fact]
        public async Task GetCountShouldReturnCorrectNumberUsingDbContext()
        {
            using var dbContext = this.InitializeDatabase();

            dbContext.Settings.Add(new Setting());
            dbContext.Settings.Add(new Setting());
            dbContext.Settings.Add(new Setting());
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Setting>(dbContext);
            var service = new SettingsService(repository);
            Assert.Equal(3, service.GetCount());
        }

        [Fact]
        public async Task GetGetAllShouldWorkCorrectly()
        {
            using var dbContext = this.InitializeDatabase();

            await dbContext.AddAsync(new Setting { });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Setting>(dbContext);
            var service = new SettingsService(repository);

            var settings = service.GetAll<SettingViewModel>();
            Assert.Single(settings);
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
