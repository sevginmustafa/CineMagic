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

    public class ActorsService : IActorsService
    {
        private readonly IDeletableEntityRepository<Actor> actorsRepository;

        public ActorsService(IDeletableEntityRepository<Actor> actorsRepository)
        {
            this.actorsRepository = actorsRepository;
        }

        public async Task<T> GetActorByIdAsync<T>(int id)
         => await this.actorsRepository
            .AllAsNoTracking()
            .Where(x => x.Id == id)
            .To<T>()
            .FirstOrDefaultAsync();

        public IQueryable<T> GetActorsBornTodayAsQueryable<T>(int gender)
        {
            var actors = Enumerable.Empty<T>().AsQueryable();

            if (gender == 0)
            {
                actors = this.actorsRepository
                    .AllAsNoTracking()
                    .Where(x => x.Birthday.Value.DayOfYear == DateTime.Now.DayOfYear)
                    .OrderBy(x => x.Name)
                    .To<T>();
            }
            else
            {
                actors = this.actorsRepository
                    .AllAsNoTracking()
                    .Where(x => x.Birthday.Value.DayOfYear == DateTime.Now.DayOfYear && (int)x.Gender == gender)
                    .OrderBy(x => x.Name)
                    .To<T>();
            }

            return actors;
        }

        public IQueryable<T> GetMostPopularActorsAsQueryable<T>(int gender, int count)
        {
            var actors = Enumerable.Empty<T>().AsQueryable();

            if (gender == 0)
            {
                actors = this.actorsRepository
                    .AllAsNoTracking()
                    .OrderByDescending(x => x.Popularity)
                    .Take(count)
                    .To<T>();
            }
            else
            {
                actors = this.actorsRepository
                    .AllAsNoTracking()
                    .Where(x => (int)x.Gender == gender)
                    .OrderByDescending(x => x.Popularity)
                    .Take(count)
                    .To<T>();
            }

            return actors;
        }
    }
}
