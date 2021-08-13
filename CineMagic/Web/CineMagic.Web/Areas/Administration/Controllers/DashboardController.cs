namespace CineMagic.Web.Areas.Administration.Controllers
{
    using CineMagic.Services.Data.Contracts;

    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;

        public DashboardController(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        [HttpGet("/Administration")]
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
