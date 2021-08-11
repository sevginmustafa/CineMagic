namespace CineMagic.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface IPrivaciesService
    {
        Task<T> GetPrivacyContentAsync<T>();
    }
}
