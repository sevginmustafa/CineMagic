namespace CineMagic.Services.Data.Contracts
{
    using System.Linq;

    public interface ISearchService
    {
        IQueryable<T> SearchMoviesAsQueryable<T>(string title);

        IQueryable<T> SearchActorsAsQueryable<T>(string name);

        IQueryable<T> SearchDirectorsAsQueryable<T>(string name);
    }
}
