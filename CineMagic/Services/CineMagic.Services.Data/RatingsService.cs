namespace CineMagic.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using CineMagic.Data.Common.Repositories;
    using CineMagic.Data.Models;
    using CineMagic.Services.Data.Contracts;
    using Microsoft.EntityFrameworkCore;

    public class RatingsService : IRatingsService
    {
        private readonly IRepository<Rating> ratingsRepository;

        public RatingsService(IRepository<Rating> ratingsRepository)
        {
            this.ratingsRepository = ratingsRepository;
        }

        public async Task<double> GetAverageRatingAsync(int movieId)
        {
            var rating = await this.ratingsRepository
            .AllAsNoTracking()
            .Where(x => x.MovieId == movieId)
            .ToListAsync();

            if (rating.Count > 0)
            {
                return rating.Average(x => x.Rate);
            }

            return 0;
        }

        public async Task<double> GetUserRatingAsync(int movieId, string userId)
        {
            var rating = await this.ratingsRepository
            .AllAsNoTracking()
            .FirstOrDefaultAsync(x => x.MovieId == movieId && x.UserId == userId);

            if (rating != null)
            {
                return rating.Rate;
            }

            return await this.GetAverageRatingAsync(movieId);
        }

        public async Task SetRateAsync(double rate, int movieId, string userId)
        {
            var rating = await this.ratingsRepository
                .All()
                .FirstOrDefaultAsync(x => x.MovieId == movieId && x.UserId == userId);

            if (rating == null)
            {
                rating = new Rating
                {
                    MovieId = movieId,
                    UserId = userId,
                };

                await this.ratingsRepository.AddAsync(rating);
            }

            rating.Rate = rate;

            await this.ratingsRepository.SaveChangesAsync();
        }
    }
}
