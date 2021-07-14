namespace CineMagic.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task SeedAsync(CineMagicDbContext dbContext, IServiceProvider serviceProvider);
    }
}
