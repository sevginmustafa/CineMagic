namespace InTheAction.Services.GetDataFromTMDB
{
    using System.Threading.Tasks;

    public interface IGetDataFromTMDBService
    {
        Task GetMovieDataAsJSON(int startIndex, int endIndex);
    }
}
