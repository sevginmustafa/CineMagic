namespace CineMagic.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using CineMagic.Data.Common.Repositories;
    using CineMagic.Data.Models;
    using CineMagic.Services.Data.Contracts;
    using CineMagic.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class DirectorsService : IDirectorsService
    {
        private readonly IDeletableEntityRepository<Director> directorsRepository;

        public DirectorsService(IDeletableEntityRepository<Director> directorsRepository)
        {
            this.directorsRepository = directorsRepository;
        }

        public async Task<T> GetDirectorByIdAsync<T>(int id)
        => await this.directorsRepository
            .AllAsNoTracking()
            .Where(x => x.Id == id)
            .To<T>()
            .FirstOrDefaultAsync();

        public IQueryable<T> GetDirectorsBornTodayAsQueryable<T>(int gender)
        {
            var directors = Enumerable.Empty<T>().AsQueryable();

            if (gender == 0)
            {
                directors = this.directorsRepository
                    .AllAsNoTracking()
                    .Where(x => x.Birthday.Value.DayOfYear == DateTime.Now.DayOfYear)
                    .OrderBy(x => x.Name)
                    .To<T>();
            }
            else
            {
                directors = this.directorsRepository
                    .AllAsNoTracking()
                    .Where(x => x.Birthday.Value.DayOfYear == DateTime.Now.DayOfYear && (int)x.Gender == gender)
                    .OrderBy(x => x.Name)
                    .To<T>();
            }

            return directors;
        }

        public IQueryable<T> GetMostPopularDirectorsAsQueryable<T>(int gender, int count)
        {
            var directors = Enumerable.Empty<T>().AsQueryable();

            if (gender == 0)
            {
                directors = this.directorsRepository
                    .AllAsNoTracking()
                    .OrderByDescending(x => x.Popularity)
                    .Take(count)
                    .To<T>();
            }
            else
            {
                directors = this.directorsRepository
                    .AllAsNoTracking()
                    .Where(x => (int)x.Gender == gender)
                    .OrderByDescending(x => x.Popularity)
                    .Take(count)
                    .To<T>();
            }

            return directors;
        }
    }
}
