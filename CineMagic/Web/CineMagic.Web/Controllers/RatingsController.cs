namespace CineMagic.Web.Controllers
{
    using System.Threading.Tasks;

    using CineMagic.Services.Data.Contracts;
    using CineMagic.Web.Infrastructure;
    using CineMagic.Web.ViewModels.Ratings;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class RatingsController : Controller
    {
        private readonly IRatingsService ratingsService;

        public RatingsController(IRatingsService ratingsService)
        {
            this.ratingsService = ratingsService;
        }

        [Authorize]
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<RatingResponseModel>> Rate(RateInputModel inputModel)
        {
            var userId = this.User.GetId();

            await this.ratingsService.SetRateAsync(inputModel.Rate, inputModel.MovieId, userId);
            var averageRating = await this.ratingsService.GetAverageRatingAsync(inputModel.MovieId);

            return new RatingResponseModel { AverageRating = averageRating };
        }
    }
}
