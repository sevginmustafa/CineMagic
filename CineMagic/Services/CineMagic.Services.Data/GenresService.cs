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

    public class GenresService : IGenresService
    {
        private readonly IDeletableEntityRepository<Genre> genresRepository;

        public GenresService(IDeletableEntityRepository<Genre> genresRepository)
        {
            this.genresRepository = genresRepository;
        }

        public async Task<IEnumerable<T>> GetAllGenresAsync<T>()
        {
            var genres = await this.genresRepository
                .AllAsNoTracking()
                .OrderBy(x => x.Name)
                .To<T>()
                .ToListAsync();

            return genres;
        }

        public async Task<IEnumerable<T>> GetPopularGenresAsync<T>()
        {
            var popularGenres = await this.genresRepository
                .AllAsNoTracking()
                .OrderByDescending(x => x.Movies.Count)
                .To<T>()
                .ToListAsync();

            return popularGenres;
        }
    }
}
