namespace CineMagic.Web.CustomMiddlewares
{
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using CineMagic.Data.Common.Repositories;
    using CineMagic.Data.Models;
    using Microsoft.AspNetCore.Http;

    public class CreatePrivacyCustomMiddleware
    {
        private readonly RequestDelegate next;

        public CreatePrivacyCustomMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, IDeletableEntityRepository<Privacy> privaciesRepository)
        {
            await this.SeedUserPrivacy(privaciesRepository);
            await this.next(context);
        }

        private async Task SeedUserPrivacy(IDeletableEntityRepository<Privacy> privaciesRepository)
        {
            const string privacyPath = "./wwwroot/privacy.txt";

            if (!privaciesRepository.AllAsNoTracking().Any())
            {
                var privacy = new Privacy
                {
                    Content = await File.ReadAllTextAsync(privacyPath),
                };

                await privaciesRepository.AddAsync(privacy);

                await privaciesRepository.SaveChangesAsync();
            }
        }
    }
}
