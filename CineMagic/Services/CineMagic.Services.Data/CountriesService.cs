namespace CineMagic.Services.Data
{
    using CineMagic.Data.Common.Repositories;
    using CineMagic.Data.Models;
    using CineMagic.Services.Data.Contracts;
    using CineMagic.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
