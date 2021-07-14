namespace InTheAction.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using InTheAction.Data.Common.Repositories;
    using InTheAction.Data.Models;
    using InTheAction.Services.Data.Contracts;
    using InTheAction.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class MoviesService : IMoviesService
    {
        private readonly IDeletableEntityRepository<Movie> moviesRepository;

        public MoviesService(IDeletableEntityRepository<Movie> moviesRepository)
        {
            this.moviesRepository = moviesRepository;
        }

        public async Task<IEnumerable<T>> GetPopularMoviesAsync<T>(int count)
        {
            var latestMovies = await this.moviesRepository
                .AllAsNoTracking()
                .OrderByDescending(x => x.Popularity)
                .Take(count)
                .To<T>()
                .ToListAsync();

            return latestMovies;
        }

        public async Task<IEnumerable<T>> GetRecentMoviesAsync<T>(int count)
        {
            var latestMovies = await this.moviesRepository
                .AllAsNoTracking()
                .OrderByDescending(x => x.ReleaseDate)
                .Take(count)
                .To<T>()
                .ToListAsync();

            return latestMovies;
        }

        public async Task<IEnumerable<T>> GetTopRatedMoviesAsync<T>(int count)
        {
            var topRatedMovies = await this.moviesRepository
                .AllAsNoTracking()
                .OrderByDescending(x => x.CurrentAverageVote)
                .Take(count)
                .To<T>()
                .ToListAsync();

            return topRatedMovies;
        }
    }
}
