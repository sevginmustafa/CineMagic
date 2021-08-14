namespace CineMagic.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CineMagic.Data.Common.Repositories;
    using CineMagic.Data.Models;
    using CineMagic.Services.Data.Contracts;
    using CineMagic.Services.Mapping;
    using CineMagic.Web.ViewModels.Countries;
    using CineMagic.Web.ViewModels.InputModels.Administration;
    using Microsoft.EntityFrameworkCore;

    public class CountriesService : ICountriesService
    {
        private const int MostPopularCountries = 10;

        private readonly IDeletableEntityRepository<Country> countriesRepository;

        public CountriesService(IDeletableEntityRepository<Country> countriesRepository)
        {
            this.countriesRepository = countriesRepository;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
            => await this.countriesRepository
            .AllAsNoTracking()
            .OrderBy(x => x.Name)
            .To<T>()
            .ToListAsync();

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

        public async Task CreateAsync(CountryCreateInputModel inputModel)
        {
            bool findCountry = await this.countriesRepository
                   .AllAsNoTracking()
                   .AnyAsync(x => x.Name == inputModel.Name);

            //if (findDirector)
            //{
            //    throw new ArgumentException(
            //        string.Format(ExceptionMessages.DirectorAlreadyExists, actor.FirstName, actor.LastName));
            //}

            var country = new Country
            {
                Name = inputModel.Name,
            };

            await this.countriesRepository.AddAsync(country);
            await this.countriesRepository.SaveChangesAsync();
        }

        public IQueryable<T> GetAllCountriesAsQueryable<T>()
        => this.countriesRepository
            .AllAsNoTracking()
            .OrderByDescending(x => x.CreatedOn)
            .To<T>();

        public async Task DeleteAsync(int id)
        {
            var findCountry = await this.countriesRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            // if (findDirector == null)
            // {
            //     throw new ArgumentException(
            //string.Format(ExceptionMessages.DirectorAlreadyExists, actor.FirstName, actor.LastName));
            // }

            findCountry.IsDeleted = true;
            findCountry.DeletedOn = DateTime.UtcNow;

            this.countriesRepository.Update(findCountry);
            await this.countriesRepository.SaveChangesAsync();
        }

        public async Task EditAsync(CountryEditViewModel countryEditViewModel)
        {
            var findCountry = await this.countriesRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == countryEditViewModel.Id);

            // if (findActor == null)
            // {
            //     throw new ArgumentException(
            //string.Format(ExceptionMessages.DirectorAlreadyExists, actor.FirstName, actor.LastName));
            // }

            findCountry.Name = countryEditViewModel.Name;

            this.countriesRepository.Update(findCountry);
            await this.countriesRepository.SaveChangesAsync();
        }

        public async Task<T> GetViewModelByIdAsync<T>(int id)
        {
            var country = await this.countriesRepository
                 .AllAsNoTracking()
                 .Where(m => m.Id == id)
                 .To<T>()
                 .FirstOrDefaultAsync();

            //if (director == null)
            //{
            //    throw new NullReferenceException(string.Format(ExceptionMessages.MovieNotFound, id));
            //}

            return country;
        }
    }
}
