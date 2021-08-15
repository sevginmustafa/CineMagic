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
            => await this.moviesRepository
            .AllAsNoTracking()
            .Where(x => x.Id == id)
            .To<T>()
            .FirstOrDefaultAsync();

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

            // TODO: Make 404 Not Found
            if (movie == null)
            {
                return Enumerable.Empty<T>();
            }

            var genresIds = movie.Genres.Select(x => x.GenreId).ToList();

            var movies = this.moviesRepository.All().AsQueryable();

            foreach (var genreId in genresIds)
            {
                movies = movies.Where(x => x.Genres.Any(x => x.GenreId == genreId));
            }

            return await movies
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
            var findMovie = await this.moviesRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Title == inputModel.Title);

            if (findMovie)
            {
                // throw error
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
            var findMovie = await this.moviesRepository
               .AllAsNoTracking()
               .FirstOrDefaultAsync(x => x.Id == id);

            // if (findDirector == null)
            // {
            //     throw new ArgumentException(
            //string.Format(ExceptionMessages.DirectorAlreadyExists, actor.FirstName, actor.LastName));
            // }

            findMovie.IsDeleted = true;
            findMovie.DeletedOn = DateTime.UtcNow;

            this.moviesRepository.Update(findMovie);
            await this.moviesRepository.SaveChangesAsync();
        }

        public async Task EditAsync(MovieEditViewModel movieEditViewModel)
        {
            var findMovie = await this.moviesRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == movieEditViewModel.Id);

            // if (findActor == null)
            // {
            //     throw new ArgumentException(
            //string.Format(ExceptionMessages.DirectorAlreadyExists, actor.FirstName, actor.LastName));
            // }

            findMovie.Title = movieEditViewModel.Title;
            findMovie.PosterPath = movieEditViewModel.PosterPath;
            findMovie.TrailerPath = movieEditViewModel.TrailerPath;
            findMovie.IMDBLink = movieEditViewModel.IMDBLink;
            findMovie.ReleaseDate = movieEditViewModel.ReleaseDate;
            findMovie.Runtime = movieEditViewModel.Runtime;
            findMovie.Tagline = movieEditViewModel.Tagline;
            findMovie.Overview = movieEditViewModel.Overview;
            findMovie.Budget = movieEditViewModel.Budget;
            findMovie.Revenue = movieEditViewModel.Revenue;
            findMovie.Popularity = movieEditViewModel.Popularity;
            findMovie.DirectorId = movieEditViewModel.DirectorId;

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
                await this.EditMovieGenres(movieEditViewModel, findMovie);
            }
            else
            {
                await this.EditMovieGenres(movieEditViewModel, findMovie);
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
                await this.EditMovieCountries(movieEditViewModel, findMovie);
            }
            else
            {
                await this.EditMovieCountries(movieEditViewModel, findMovie);
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
                await this.EditMovieLanguages(movieEditViewModel, findMovie);
            }
            else
            {
                await this.EditMovieLanguages(movieEditViewModel, findMovie);
            }

            this.moviesRepository.Update(findMovie);
            await this.moviesRepository.SaveChangesAsync();
        }

        public async Task<T> GetViewModelByIdAsync<T>(int id)
        {
            var movie = await this.moviesRepository
                 .All()
                 .Where(m => m.Id == id)
                 .To<T>()
                 .FirstOrDefaultAsync();

            //if (director == null)
            //{
            //    throw new NullReferenceException(string.Format(ExceptionMessages.MovieNotFound, id));
            //}

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
