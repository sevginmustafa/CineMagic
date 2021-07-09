namespace InTheAction.Services.GetDataFromTMDB
{
    using System.Threading.Tasks;

    public interface IFillDatabaseService
    {
        Task AddDataToDBAsync(int startIndex, int endIndex);
    }
}
