namespace CineMagic.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IRatingsService
    {
        Task SetRateAsync(int rate, int movieId, string userId);

        double GetAverageRating(int movieId);
    }
}
