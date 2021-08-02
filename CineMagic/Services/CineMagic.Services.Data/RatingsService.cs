namespace CineMagic.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using CineMagic.Data.Common.Repositories;
    using CineMagic.Data.Models;
    using CineMagic.Services.Data.Contracts;

    public class RatingsService : IRatingsService
    {
        private readonly IRepository<Rating> ratingsRepository;

        public RatingsService(IRepository<Rating> ratingsRepository)
        {
            this.ratingsRepository = ratingsRepository;
        }

        public double GetAverageRating(int movieId)
        => this.ratingsRepository
            .AllAsNoTracking()
            .Where(x => x.MovieId == movieId)
            .Average(x => x.Rate);

        public async Task SetRateAsync(int rate, int movieId, string userId)
        {
            var rating = this.ratingsRepository
                .All()
                .FirstOrDefault(x => x.MovieId == movieId && x.UserId == userId);

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
