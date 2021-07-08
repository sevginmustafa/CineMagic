namespace InTheAction.Web.Controllers
{
    using System.Threading.Tasks;

    using InTheAction.Services.GetDataFromTMDB;
    using InTheAction.Services.Scraping;
    using Microsoft.AspNetCore.Mvc;

    // TODO: Move in administration area
    public class GatherMoviesController : BaseController
    {
        private readonly IGetDataFromTMDBService getDataFromTMDBService;

        public GatherMoviesController(IGetDataFromTMDBService getDataFromTMDBService)
        {
            this.getDataFromTMDBService = getDataFromTMDBService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> GatherData()
        {
            await this.getDataFromTMDBService.GetMovieDataAsJSON(98,99);

            return this.View();
        }
    }
}
