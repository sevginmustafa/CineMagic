namespace InTheAction.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InTheAction.Data.Common.Repositories;
    using InTheAction.Data.Models;
    using InTheAction.Services.Data.Contracts;
    using InTheAction.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class CountriesService : ICountriesService
    {
        private const int MostPopularCountries = 10;

        private readonly IDeletableEntityRepository<Country> countriesRepository;

        public CountriesService(IDeletableEntityRepository<Country> countriesRepository)
        {
            this.countriesRepository = countriesRepository;
        }

        public async Task<IEnumerable<T>> GetPopularCountriesAsync<T>()
        {
            var countries = await this.countriesRepository
                .AllAsNoTracking()
                .OrderByDescending(x => x.Movies.Count)
                .Take(MostPopularCountries)
                .To<T>()
                .ToListAsync();

            return countries;
        }
    }
}
