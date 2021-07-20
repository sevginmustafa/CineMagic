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

    public class MoviesService : IMoviesService
    {
        private readonly IDeletableEntityRepository<Movie> moviesRepository;

        public MoviesService(IDeletableEntityRepository<Movie> moviesRepository)
        {
            this.moviesRepository = moviesRepository;
        }

        public async Task<IEnumerable<T>> GetRecentMoviesAsync<T>(int count)
        => await this.moviesRepository
            .AllAsNoTracking()
            .OrderByDescending(x => x.CreatedOn)
            .Take(count)
            .To<T>()
            .ToListAsync();

        public async Task<IEnumerable<T>> GetPopularMoviesAsync<T>(int count)
            => await this.moviesRepository
            .AllAsNoTracking()
            .OrderByDescending(x => x.Popularity)
            .Take(count)
            .To<T>()
            .ToListAsync();

        public async Task<IEnumerable<T>> GetTopRatedMoviesAsync<T>(int count)
            => await this.moviesRepository
            .AllAsNoTracking()
            .OrderByDescending(x => x.CurrentAverageVote)
            .Take(count)
            .To<T>()
            .ToListAsync();

        public async Task<IEnumerable<T>> GetLatestMoviesAsync<T>(int count)
        => await this.moviesRepository
            .AllAsNoTracking()
            .OrderByDescending(x => x.ReleaseDate)
            .Take(count)
            .To<T>()
            .ToListAsync();

        // TODO
        public async Task<IEnumerable<T>> GetWatchlistMovies<T>(string userId, int count)
        => await this.moviesRepository
            .AllAsNoTracking()
            .OrderByDescending(x => x.ReleaseDate)
            .Take(count)
            .To<T>()
            .ToListAsync();

        public async Task<IEnumerable<T>> GetMoviesByGenreName<T>(string genreName)
        => await this.moviesRepository
            .AllAsNoTracking()
            .Where(x => x.Genres.Any(x => x.Genre.Name == genreName))
            .To<T>()
            .ToListAsync();
    }
}
