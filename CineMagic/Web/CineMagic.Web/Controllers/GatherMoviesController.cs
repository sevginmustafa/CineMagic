namespace CineMagic.Web.Controllers
{
    using System.Threading.Tasks;

    using CineMagic.Services.GetDataFromTMDB;
    using Microsoft.AspNetCore.Mvc;

    // TODO: Move in administration area
    public class GatherMoviesController : Controller
    {
        private readonly IFillDatabaseService fillDatabaseService;

        public GatherMoviesController(IFillDatabaseService fillDatabaseService)
        {
            this.fillDatabaseService = fillDatabaseService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> GatherData()
        {
            await this.fillDatabaseService.AddDataToDBAsync(151, 200);

            return this.View();
        }
    }
}
