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
    using Microsoft.EntityFrameworkCore;

    public class MoviesService : IMoviesService
    {
        private readonly IDeletableEntityRepository<Movie> moviesRepository;

        public MoviesService(IDeletableEntityRepository<Movie> moviesRepository)
        {
            this.moviesRepository = moviesRepository;
        }

        public T GetBannerSectionMovie<T>()
        {
            try
            {
                var movie = this.moviesRepository
                   .AllAsNoTracking()
                   .OrderByDescending(x => x.ReleaseDate)
                   .ThenByDescending(x => x.Popularity)
                   .Take(7)
                   .To<T>()
                   .ToList()[(int)DateTime.UtcNow.DayOfWeek];

                return movie;
            }
            catch (Exception)
            {
                return default;
            }
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

        public async Task<IEnumerable<T>> GetAllMovies<T>(int page, int itemsPerPage)
       => await this.moviesRepository
       .AllAsNoTracking()
       .OrderBy(x => x.Title)
       .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
       .To<T>()
       .ToListAsync();

        public int GetAllMoviesCount()
            => this.moviesRepository
            .AllAsNoTracking()
            .Count();

        public async Task<IEnumerable<T>> GetMoviesByGenreName<T>(string name, int page, int itemsPerPage)
            => await this.moviesRepository
            .AllAsNoTracking()
            .Where(x => x.Genres.Any(x => x.Genre.Name == name))
            .OrderByDescending(x => x.Id)
            .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
            .To<T>()
            .ToListAsync();

        public int GetMoviesByGenreNameCount(string name)
            => this.moviesRepository
            .AllAsNoTracking()
            .Where(x => x.Genres.Any(x => x.Genre.Name == name))
            .Count();

        public async Task<IEnumerable<T>> GetMoviesByCountryName<T>(string name, int page, int itemsPerPage)
            => await this.moviesRepository
            .AllAsNoTracking()
            .Where(x => x.ProductionCountries.Any(x => x.Country.Name == name))
            .OrderByDescending(x => x.Id)
            .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
            .To<T>()
            .ToListAsync();

        public int GetMoviesByCountryNameCount(string name)
            => this.moviesRepository
            .AllAsNoTracking()
            .Where(x => x.ProductionCountries.Any(x => x.Country.Name == name))
            .Count();

        public async Task<IEnumerable<T>> GetMoviesByReleaseYear<T>(int year, int page, int itemsPerPage)
            => await this.moviesRepository
            .AllAsNoTracking()
            .Where(x => x.ReleaseDate.Year == year)
            .OrderByDescending(x => x.Id)
            .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
            .To<T>()
            .ToListAsync();

        public int GetMoviesByReleaseYearCount(int year)
            => this.moviesRepository
            .AllAsNoTracking()
            .Where(x => x.ReleaseDate.Year == year)
            .Count();
    }
}
