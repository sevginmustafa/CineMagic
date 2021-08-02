namespace CineMagic.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface IRatingsService
    {
        Task SetRateAsync(double rate, int movieId, string userId);

        Task<double> GetAverageRatingAsync(int movieId);

        Task<double> GetUserRatingAsync(int movieId, string userId);
    }
}
