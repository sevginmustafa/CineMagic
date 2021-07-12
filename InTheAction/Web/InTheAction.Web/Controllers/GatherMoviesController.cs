namespace InTheAction.Web.Controllers
{
    using System.Threading.Tasks;

    using InTheAction.Services.GetDataFromTMDB;
    using Microsoft.AspNetCore.Mvc;

    // TODO: Move in administration area
    public class GatherMoviesController : BaseController
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
            await this.fillDatabaseService.AddDataToDBAsync(175, 175);

            return this.View();
        }
    }
}
