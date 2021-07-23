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

        public async Task<IEnumerable<T>> GetActorsBornToday<T>(int gender)
        => await this.actorsRepository
                .AllAsNoTracking()
                .Where(x => x.Birthday.Value.DayOfYear == DateTime.UtcNow.DayOfYear && (int)x.Gender != gender)
                .OrderBy(x => x.Name)
                .To<T>()
                .ToListAsync();

        public async Task<IEnumerable<T>> GetMostPopularActors<T>(int count)
        => await this.actorsRepository
            .AllAsNoTracking()
            .OrderByDescending(x => x.Popularity)
            .Take(count)
            .To<T>()
            .ToListAsync();
    }
}
