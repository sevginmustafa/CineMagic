using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(CineMagic.Web.Areas.Identity.IdentityHostingStartup))]

namespace CineMagic.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}
