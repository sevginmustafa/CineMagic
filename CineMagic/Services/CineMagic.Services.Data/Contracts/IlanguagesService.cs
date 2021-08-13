namespace CineMagic.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILanguagesService
    {
        Task<IEnumerable<T>> GetAllAsync<T>();
    }
}
