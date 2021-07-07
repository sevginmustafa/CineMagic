namespace InTheAction.Web.Controllers
{
    using System.Threading.Tasks;

    using InTheAction.Services.Scraping;
    using Microsoft.AspNetCore.Mvc;

    // TODO: Move in administration area
    public class GatherMoviesController : BaseController
    {
        private readonly ITheMovieDbOrgScraperService theMovieDbOrgScraperService;

        public GatherMoviesController(ITheMovieDbOrgScraperService theMovieDbOrgScraperService)
        {
            this.theMovieDbOrgScraperService = theMovieDbOrgScraperService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> GatherData()
        {
            await this.theMovieDbOrgScraperService.GetMovieData(11, 110);

            return this.View();
        }
    }
}
