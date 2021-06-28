namespace InTheAction.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task SeedAsync(InTheActionDbContext dbContext, IServiceProvider serviceProvider);
    }
}
