namespace CineMagic.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CineMagic.Data.Common.Repositories;
    using CineMagic.Data.Models;
    using CineMagic.Services.Data.Common;
    using CineMagic.Services.Data.Contracts;
    using CineMagic.Services.Mapping;
    using CineMagic.Web.ViewModels.InputModels.Administration;
    using CineMagic.Web.ViewModels.Movies;
    using Microsoft.EntityFrameworkCore;

    public class MoviesService : IMoviesService
    {
        private const string AllPaginationFilter = "All";
        private const string DigitPaginationFilter = "0 - 9";
        private const int BannerSectionMoviesCount = 10;

        private readonly IDeletableEntityRepository<Movie> moviesRepository;
        private readonly IDeletableEntityRepository<MovieGenre> movieGenresRepository;
        private readonly IDeletableEntityRepository<MovieCountry> movieCountriesRepository;
        private readonly IDeletableEntityRepository<MovieLanguage> movieLanguagesRepository;
        private readonly IRepository<Watchlist> watchlistRepository;

        public MoviesService(
            IDeletableEntityRepository<Movie> moviesRepository,
            IDeletableEntityRepository<MovieGenre> movieGenresRepository,
            IDeletableEntityRepository<MovieCountry> movieCountriesRepository,
            IDeletableEntityRepository<MovieLanguage> movieLanguagesRepository,
            IRepository<Watchlist> watchlistRepository)
        {
            this.moviesRepository = moviesRepository;
            this.movieGenresRepository = movieGenresRepository;
            this.movieCountriesRepository = movieCountriesRepository;
            this.movieLanguagesRepository = movieLanguagesRepository;
            this.watchlistRepository = watchlistRepository;
        }

        public async Task<T> GetBannerSectionMovieAsync<T>()
            => await this.moviesRepository
            .AllAsNoTracking()
            .OrderByDescending(x => x.ReleaseDate)
            .Take(BannerSectionMoviesCount)
            .OrderBy(x => Guid.NewGuid())
            .To<T>()
            .FirstOrDefaultAsync();

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

        public async Task<IEnumerable<T>> GetWatchlistMoviesAsync<T>(string userId, int count)
            => await this.watchlistRepository
            .AllAsNoTracking()
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedOn)
            .Take(count)
            .To<T>()
            .ToListAsync();

        public IQueryable<T> GetMoviesByLetterAsQueryable<T>(string letter)
        {
            var movies = Enumerable.Empty<T>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(letter) && letter != AllPaginationFilter && letter != DigitPaginationFilter)
            {
                movies = this.moviesRepository
                    .AllAsNoTracking()
                    .Where(x => x.Title.ToLower().StartsWith(letter.ToLower()))
                    .OrderBy(x => x.Title)
                    .To<T>();
            }
            else if (letter == DigitPaginationFilter)
            {
                var digits = Enumerable.Range(0, 10).Select(x => x.ToString()).ToList();

                movies = this.moviesRepository
                    .AllAsNoTracking()
                    .Where(x => digits.Contains(x.Title.Substring(0, 1)))
                    .OrderBy(x => x.Title)
                    .To<T>();
            }
            else
            {
                movies = this.GetAllMoviesAsQueryable<T>();
            }

            return movies;
        }

        public IQueryable<T> SearchMoviesByTitleAsQueryable<T>(string title)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                return this.moviesRepository
                     .AllAsNoTracking()
                     .Where(x => x.Title.ToLower().Contains(title.ToLower()))
                     .To<T>();
            }

            return this.GetAllMoviesAsQueryable<T>();
        }

        public IQueryable<T> GetMoviesByGenreNameAsQueryable<T>(string name)
            => this.moviesRepository
            .AllAsNoTracking()
            .Where(x => x.Genres.Any(x => x.Genre.Name == name))
            .OrderByDescending(x => x.Id)
            .To<T>();

        public IQueryable<T> GetMoviesByCountryNameAsQueryable<T>(string name)
            => this.moviesRepository
            .AllAsNoTracking()
            .Where(x => x.ProductionCountries.Any(x => x.Country.Name == name))
            .OrderByDescending(x => x.Id)
            .To<T>();

        public IQueryable<T> GetMoviesByReleaseYearAsQueryable<T>(int year)
            => this.moviesRepository
            .AllAsNoTracking()
            .Where(x => x.ReleaseDate.Year == year)
            .OrderByDescending(x => x.Id)
            .To<T>();

        public IQueryable<T> GetAllMoviesAsQueryable<T>()
            => this.moviesRepository
            .AllAsNoTracking()
            .OrderBy(x => x.Title)
            .To<T>();

        public async Task<T> GetMovieByIdAsync<T>(int id)
        {
            var movie = await this.moviesRepository
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            if (movie == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.MovieNotFound, id));
            }

            return movie;
        }

        public async Task AddToUserWatchlistAsync(int movieId, string userId)
        {
            if (!this.watchlistRepository.AllAsNoTracking().Any(x => x.MovieId == movieId && x.UserId == userId))
            {
                await this.watchlistRepository.AddAsync(new Watchlist { MovieId = movieId, UserId = userId });

                await this.watchlistRepository.SaveChangesAsync();
            }
        }

        public async Task RemoveFromUserWatchlistAsync(int movieId, string userId)
        {
            var watchlist = this.watchlistRepository.AllAsNoTracking()
                .FirstOrDefault(x => x.MovieId == movieId && x.UserId == userId);

            if (watchlist != null)
            {
                this.watchlistRepository.Delete(watchlist);

                await this.watchlistRepository.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> GetSimilarMoviesAsync<T>(int movieId, int count)
        {
            var movie = await this.moviesRepository
                .AllAsNoTracking().Include(x => x.Genres)
                .FirstOrDefaultAsync(x => x.Id == movieId);

            var genresIds = movie.Genres.Select(x => x.GenreId).ToList();

            var movies = this.moviesRepository.All().AsQueryable();

            foreach (var genreId in genresIds)
            {
                movies = movies.Where(x => x.Genres.Any(x => x.GenreId == genreId));
            }

            return await movies.
                Where(x => x.Id != movieId)
                .Take(count)
                .To<T>()
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetActorMostPopularMoviesAsync<T>(int actorId, int count)
            => await this.moviesRepository
            .AllAsNoTracking()
            .Where(x => x.Cast.Any(x => x.ActorId == actorId))
            .OrderByDescending(x => x.Popularity)
            .Take(count)
            .To<T>()
            .ToListAsync();

        public async Task<IEnumerable<T>> GetDirectorBestProfitMoviesAsync<T>(int directorId, int count)
            => await this.moviesRepository
            .AllAsNoTracking()
            .Where(x => x.DirectorId == directorId)
            .OrderByDescending(x => x.Revenue)
            .Take(count)
            .To<T>()
            .ToListAsync();

        public async Task CreateAsync(MovieCreateInputModel inputModel)
        {
            bool findMovie = await this.moviesRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Title == inputModel.Title);

            if (findMovie)
            {
                throw new ArgumentException(
                    string.Format(ExceptionMessages.MovieAlreadyExists, inputModel.Title));
            }

            var movie = new Movie
            {
                Title = inputModel.Title,
                PosterPath = inputModel.PosterPath,
                TrailerPath = inputModel.TrailerPath,
                IMDBLink = inputModel.IMDBLink,
                ReleaseDate = inputModel.ReleaseDate,
                Runtime = inputModel.Runtime,
                Tagline = inputModel.Tagline,
                Overview = inputModel.Overview,
                Budget = inputModel.Budget,
                Revenue = inputModel.Revenue,
                Popularity = inputModel.Popularity,
                DirectorId = inputModel.DirectorId,
            };

            foreach (var genreId in inputModel.SelectedGenres)
            {
                movie.Genres.Add(new MovieGenre { GenreId = genreId });
            }

            foreach (var countryId in inputModel.SelectedCountries)
            {
                movie.ProductionCountries.Add(new MovieCountry { CountryId = countryId });
            }

            foreach (var languageId in inputModel.SelectedGenres)
            {
                movie.Languages.Add(new MovieLanguage { LanguageId = languageId });
            }

            await this.moviesRepository.AddAsync(movie);
            await this.moviesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var movie = await this.moviesRepository
               .AllAsNoTracking()
               .FirstOrDefaultAsync(x => x.Id == id);

            if (movie == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.MovieNotFound, id));
            }

            movie.IsDeleted = true;
            movie.DeletedOn = DateTime.UtcNow;

            this.moviesRepository.Update(movie);
            await this.moviesRepository.SaveChangesAsync();

            var movieGenres = await this.movieGenresRepository
                .All()
                .Where(m => m.MovieId == id)
                .ToListAsync();

            var movieCountries = await this.movieCountriesRepository
                .All()
                .Where(m => m.MovieId == id)
                .ToListAsync();

            var movieLanguages = await this.movieLanguagesRepository
                 .All()
                 .Where(m => m.MovieId == id)
                 .ToListAsync();

            foreach (var movieGenre in movieGenres)
            {
                movieGenre.IsDeleted = true;
                movieGenre.DeletedOn = DateTime.UtcNow;
                this.movieGenresRepository.Update(movieGenre);
            }

            foreach (var movieCountry in movieCountries)
            {
                movieCountry.IsDeleted = true;
                movieCountry.DeletedOn = DateTime.UtcNow;
                this.movieCountriesRepository.Update(movieCountry);
            }

            foreach (var movieLanguage in movieLanguages)
            {
                movieLanguage.IsDeleted = true;
                movieLanguage.DeletedOn = DateTime.UtcNow;
                this.movieLanguagesRepository.Update(movieLanguage);
            }

            await this.movieGenresRepository.SaveChangesAsync();
            await this.movieCountriesRepository.SaveChangesAsync();
            await this.movieLanguagesRepository.SaveChangesAsync();
        }

        public async Task EditAsync(MovieEditViewModel movieEditViewModel)
        {
            var movie = await this.moviesRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == movieEditViewModel.Id);

            if (movie == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.MovieNotFound, movieEditViewModel.Id));
            }

            movie.Title = movieEditViewModel.Title;
            movie.PosterPath = movieEditViewModel.PosterPath;
            movie.TrailerPath = movieEditViewModel.TrailerPath;
            movie.IMDBLink = movieEditViewModel.IMDBLink;
            movie.ReleaseDate = movieEditViewModel.ReleaseDate;
            movie.Runtime = movieEditViewModel.Runtime;
            movie.Tagline = movieEditViewModel.Tagline;
            movie.Overview = movieEditViewModel.Overview;
            movie.Budget = movieEditViewModel.Budget;
            movie.Revenue = movieEditViewModel.Revenue;
            movie.Popularity = movieEditViewModel.Popularity;
            movie.DirectorId = movieEditViewModel.DirectorId;

            var movieGenres = await this.movieGenresRepository
                .AllAsNoTracking()
                .Where(x => x.MovieId == movieEditViewModel.Id)
                .ToListAsync();

            if (movieGenres.Count > 0)
            {
                foreach (var movieGenre in movieGenres)
                {
                    this.movieGenresRepository.HardDelete(movieGenre);
                }

                await this.movieGenresRepository.SaveChangesAsync();
                await this.EditMovieGenres(movieEditViewModel, movie);
            }
            else
            {
                await this.EditMovieGenres(movieEditViewModel, movie);
            }

            var movieCountries = await this.movieCountriesRepository
                .AllAsNoTracking()
                .Where(x => x.MovieId == movieEditViewModel.Id)
                .ToListAsync();

            if (movieCountries.Count > 0)
            {
                foreach (var movieCountry in movieCountries)
                {
                    this.movieCountriesRepository.HardDelete(movieCountry);
                }

                await this.movieCountriesRepository.SaveChangesAsync();
                await this.EditMovieCountries(movieEditViewModel, movie);
            }
            else
            {
                await this.EditMovieCountries(movieEditViewModel, movie);
            }

            var movieLanguages = await this.movieLanguagesRepository
              .AllAsNoTracking()
              .Where(x => x.MovieId == movieEditViewModel.Id)
              .ToListAsync();

            if (movieLanguages.Count > 0)
            {
                foreach (var movieLanguage in movieLanguages)
                {
                    this.movieLanguagesRepository.HardDelete(movieLanguage);
                }

                await this.movieLanguagesRepository.SaveChangesAsync();
                await this.EditMovieLanguages(movieEditViewModel, movie);
            }
            else
            {
                await this.EditMovieLanguages(movieEditViewModel, movie);
            }

            this.moviesRepository.Update(movie);
            await this.moviesRepository.SaveChangesAsync();
        }

        public async Task<T> GetViewModelByIdAsync<T>(int id)
        {
            var movie = await this.moviesRepository
                 .All()
                 .Where(m => m.Id == id)
                 .To<T>()
                 .FirstOrDefaultAsync();

            if (movie == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.MovieNotFound, id));
            }

            return movie;
        }

        public async Task<IEnumerable<T>> GetAllMovieGenresAsync<T>(int movieId)
            => await this.movieGenresRepository
            .AllAsNoTracking()
            .Where(x => x.MovieId == movieId)
            .To<T>()
            .ToListAsync();

        public async Task<IEnumerable<T>> GetAllMovieCountriesAsync<T>(int movieId)
            => await this.movieCountriesRepository
            .AllAsNoTracking()
            .Where(x => x.MovieId == movieId)
            .To<T>()
            .ToListAsync();

        public async Task<IEnumerable<T>> GetAllMovieLanguagesAsync<T>(int movieId)
            => await this.movieLanguagesRepository
            .AllAsNoTracking()
            .Where(x => x.MovieId == movieId)
            .To<T>()
            .ToListAsync();

        private async Task EditMovieGenres(MovieEditViewModel movieEditViewModel, Movie movie)
        {
            foreach (var genreId in movieEditViewModel.SelectedGenres)
            {
                movie.Genres.Add(new MovieGenre { GenreId = genreId });
            }

            await this.moviesRepository.SaveChangesAsync();
        }

        private async Task EditMovieCountries(MovieEditViewModel movieEditViewModel, Movie movie)
        {
            foreach (var countryId in movieEditViewModel.SelectedCountries)
            {
                movie.ProductionCountries.Add(new MovieCountry { CountryId = countryId });
            }

            await this.moviesRepository.SaveChangesAsync();
        }

        private async Task EditMovieLanguages(MovieEditViewModel movieEditViewModel, Movie movie)
        {
            foreach (var languageId in movieEditViewModel.SelectedLanguages)
            {
                movie.Languages.Add(new MovieLanguage { LanguageId = languageId });
            }

            await this.moviesRepository.SaveChangesAsync();
        }
    }
}
