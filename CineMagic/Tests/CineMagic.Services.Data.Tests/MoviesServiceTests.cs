namespace CineMagic.Services.Data.Tests
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using CineMagic.Data;
    using CineMagic.Data.Common.Repositories;
    using CineMagic.Data.Models;
    using CineMagic.Data.Models.Enums;
    using CineMagic.Data.Repositories;
    using CineMagic.Services.Data.Common;
    using CineMagic.Services.Mapping;
    using CineMagic.Web.ViewModels.Actors;
    using CineMagic.Web.ViewModels.InputModels.Administration;
    using CineMagic.Web.ViewModels.Movies;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class MoviesServiceTests
    {
        public MoviesServiceTests()
        {
            this.InitializeMapper();
        }

        [Fact]
        public async Task GetBannerSectionMovieAsyncShoudReturnCorrectCount()
        {
            using var dbContext = this.InitializeDatabase();

            await dbContext.Movies.AddAsync(new Movie
            {
                Title = "A Quiet Place 2",
                ReleaseDate = DateTime.UtcNow,
            });

            await dbContext.SaveChangesAsync();

            using var moviesRepository = new EfDeletableEntityRepository<Movie>(dbContext);
            var fakeMovieGenresRepository = new Moq.Mock<IDeletableEntityRepository<MovieGenre>>();
            var fakeMovieCountriesRepository = new Moq.Mock<IDeletableEntityRepository<MovieCountry>>();
            var fakeMovieLanguagesRepository = new Moq.Mock<IDeletableEntityRepository<MovieLanguage>>();
            var fakeWatchlistsRepository = new Moq.Mock<IRepository<Watchlist>>();

            var service = new MoviesService(
                moviesRepository,
                fakeMovieGenresRepository.Object,
                fakeMovieCountriesRepository.Object,
                fakeMovieLanguagesRepository.Object,
                fakeWatchlistsRepository.Object);

            // Act
            var actualResult = await service.GetBannerSectionMovieAsync<MovieStandartViewModel>();

            // Assert
            Assert.Equal("A Quiet Place 2", actualResult.Title);
        }

        [Fact]
        public async Task GetRecentMoviesAsyncShoudReturnCorrectCountAndInCorrectOrder()
        {
            using var dbContext = this.InitializeDatabase();

            await dbContext.Movies.AddAsync(new Movie
            {
                Title = "Avatar",
            });

            await dbContext.Movies.AddAsync(new Movie
            {
                Title = "A Quiet Place 2",
            });

            await dbContext.SaveChangesAsync();

            using var moviesRepository = new EfDeletableEntityRepository<Movie>(dbContext);
            var fakeMovieGenresRepository = new Moq.Mock<IDeletableEntityRepository<MovieGenre>>();
            var fakeMovieCountriesRepository = new Moq.Mock<IDeletableEntityRepository<MovieCountry>>();
            var fakeMovieLanguagesRepository = new Moq.Mock<IDeletableEntityRepository<MovieLanguage>>();
            var fakeWatchlistsRepository = new Moq.Mock<IRepository<Watchlist>>();

            var service = new MoviesService(
                moviesRepository,
                fakeMovieGenresRepository.Object,
                fakeMovieCountriesRepository.Object,
                fakeMovieLanguagesRepository.Object,
                fakeWatchlistsRepository.Object);

            // Act
            var movies = await service.GetRecentMoviesAsync<MovieStandartViewModel>(2);
            var actualResult = movies.FirstOrDefault().Title;

            // Assert
            Assert.Equal(2, movies.Count());
            Assert.Equal("A Quiet Place 2", actualResult);
        }

        [Fact]
        public async Task GetPopularMoviesAsyncShoudReturnCorrectCountAndInCorrectOrder()
        {
            using var dbContext = this.InitializeDatabase();

            await dbContext.Movies.AddAsync(new Movie
            {
                Title = "Avatar",
                Popularity = 56,
            });

            await dbContext.Movies.AddAsync(new Movie
            {
                Title = "A Quiet Place 2",
                Popularity = 55.94,
            });

            await dbContext.SaveChangesAsync();

            using var moviesRepository = new EfDeletableEntityRepository<Movie>(dbContext);
            var fakeMovieGenresRepository = new Moq.Mock<IDeletableEntityRepository<MovieGenre>>();
            var fakeMovieCountriesRepository = new Moq.Mock<IDeletableEntityRepository<MovieCountry>>();
            var fakeMovieLanguagesRepository = new Moq.Mock<IDeletableEntityRepository<MovieLanguage>>();
            var fakeWatchlistsRepository = new Moq.Mock<IRepository<Watchlist>>();

            var service = new MoviesService(
                moviesRepository,
                fakeMovieGenresRepository.Object,
                fakeMovieCountriesRepository.Object,
                fakeMovieLanguagesRepository.Object,
                fakeWatchlistsRepository.Object);

            // Act
            var movies = await service.GetPopularMoviesAsync<MovieStandartViewModel>(100);
            var actualResult = movies.FirstOrDefault().Title;

            // Assert
            Assert.Equal(2, movies.Count());
            Assert.Equal("Avatar", actualResult);
        }

        [Fact]
        public async Task GetTopRatedMoviesAsyncShoudReturnCorrectCountAndInCorrectOrder()
        {
            using var dbContext = this.InitializeDatabase();

            await dbContext.Movies.AddAsync(new Movie
            {
                Title = "Avatar",
                CurrentAverageVote = 49,
            });

            await dbContext.Movies.AddAsync(new Movie
            {
                Title = "A Quiet Place 2",
                CurrentAverageVote = 44,
            });

            await dbContext.SaveChangesAsync();

            using var moviesRepository = new EfDeletableEntityRepository<Movie>(dbContext);
            var fakeMovieGenresRepository = new Moq.Mock<IDeletableEntityRepository<MovieGenre>>();
            var fakeMovieCountriesRepository = new Moq.Mock<IDeletableEntityRepository<MovieCountry>>();
            var fakeMovieLanguagesRepository = new Moq.Mock<IDeletableEntityRepository<MovieLanguage>>();
            var fakeWatchlistsRepository = new Moq.Mock<IRepository<Watchlist>>();

            var service = new MoviesService(
                moviesRepository,
                fakeMovieGenresRepository.Object,
                fakeMovieCountriesRepository.Object,
                fakeMovieLanguagesRepository.Object,
                fakeWatchlistsRepository.Object);

            // Act
            var movies = await service.GetTopRatedMoviesAsync<MovieStandartViewModel>(1);
            var actualResult = movies.FirstOrDefault().Title;

            // Assert
            Assert.Single(movies);
            Assert.Equal("Avatar", actualResult);
        }

        [Fact]
        public async Task GetLatestMoviesAsyncShoudReturnCorrectCountAndInCorrectOrder()
        {
            using var dbContext = this.InitializeDatabase();

            await dbContext.Movies.AddAsync(new Movie
            {
                Title = "Avatar",
                ReleaseDate = DateTime.UtcNow.AddYears(-10),
            });
            await dbContext.Movies.AddAsync(new Movie
            {
                Title = "A Quiet Place 2",
                ReleaseDate = DateTime.UtcNow,
            });

            await dbContext.SaveChangesAsync();

            using var moviesRepository = new EfDeletableEntityRepository<Movie>(dbContext);
            var fakeMovieGenresRepository = new Moq.Mock<IDeletableEntityRepository<MovieGenre>>();
            var fakeMovieCountriesRepository = new Moq.Mock<IDeletableEntityRepository<MovieCountry>>();
            var fakeMovieLanguagesRepository = new Moq.Mock<IDeletableEntityRepository<MovieLanguage>>();
            var fakeWatchlistsRepository = new Moq.Mock<IRepository<Watchlist>>();

            var service = new MoviesService(
                moviesRepository,
                fakeMovieGenresRepository.Object,
                fakeMovieCountriesRepository.Object,
                fakeMovieLanguagesRepository.Object,
                fakeWatchlistsRepository.Object);

            // Act
            var movies = await service.GetLatestMoviesAsync<MovieStandartViewModel>(3);
            var actualResult = movies.FirstOrDefault().Title;

            // Assert
            Assert.Equal(2, movies.Count());
            Assert.Equal("A Quiet Place 2", actualResult);
        }

        [Fact]
        public async Task GetWatchlistMoviesAsyncShoudReturnCorrectCountAndInCorrectOrder()
        {
            using var dbContext = this.InitializeDatabase();

            await dbContext.Movies.AddAsync(new Movie { Title = "Avatar" });
            await dbContext.Movies.AddAsync(new Movie { Title = "Avatar 2" });

            await dbContext.SaveChangesAsync();

            await dbContext.Watchlists.AddAsync(new Watchlist
            {
                MovieId = 1,
                UserId = "second",
            });

            await dbContext.Watchlists.AddAsync(new Watchlist
            {
                MovieId = 2,
                UserId = "first",
            });

            await dbContext.Watchlists.AddAsync(new Watchlist
            {
                MovieId = 2,
                UserId = "second",
            });

            await dbContext.SaveChangesAsync();

            var fakeMoviesRepository = new Moq.Mock<IDeletableEntityRepository<Movie>>();
            var fakeMovieGenresRepository = new Moq.Mock<IDeletableEntityRepository<MovieGenre>>();
            var fakeMovieCountriesRepository = new Moq.Mock<IDeletableEntityRepository<MovieCountry>>();
            var fakeMovieLanguagesRepository = new Moq.Mock<IDeletableEntityRepository<MovieLanguage>>();
            using var watchlistsRepository = new EfRepository<Watchlist>(dbContext);

            var service = new MoviesService(
                fakeMoviesRepository.Object,
                fakeMovieGenresRepository.Object,
                fakeMovieCountriesRepository.Object,
                fakeMovieLanguagesRepository.Object,
                watchlistsRepository);

            // Act
            var watchlistMovies = await service.GetWatchlistMoviesAsync<MovieWatchlistViewModel>("second", 3);
            var actualresult = watchlistMovies.FirstOrDefault().MovieId;

            // Assert
            Assert.Equal(2, watchlistMovies.Count());
            Assert.Equal(2, actualresult);
        }

        [Theory]
        [InlineData("a", 2)]
        [InlineData("2", 1)]
        [InlineData("z", 0)]
        [InlineData("L", 1)]
        [InlineData("                        ", 4)]
        [InlineData(null, 4)]
        [InlineData("", 4)]
        public async Task GetMoviesByLetterAsQueryableShoudReturnCorrectCount(string letter, int expectedResult)
        {
            using var dbContext = this.InitializeDatabase();

            await dbContext.Movies.AddAsync(new Movie { Title = "Avatar" });
            await dbContext.Movies.AddAsync(new Movie { Title = "2012" });
            await dbContext.Movies.AddAsync(new Movie { Title = "Lights Out" });
            await dbContext.Movies.AddAsync(new Movie { Title = "Avatar 2" });

            await dbContext.SaveChangesAsync();

            using var moviesRepository = new EfDeletableEntityRepository<Movie>(dbContext);
            var fakeMovieGenresRepository = new Moq.Mock<IDeletableEntityRepository<MovieGenre>>();
            var fakeMovieCountriesRepository = new Moq.Mock<IDeletableEntityRepository<MovieCountry>>();
            var fakeMovieLanguagesRepository = new Moq.Mock<IDeletableEntityRepository<MovieLanguage>>();
            var fakeWatchlistsRepository = new Moq.Mock<IRepository<Watchlist>>();

            var service = new MoviesService(
                moviesRepository,
                fakeMovieGenresRepository.Object,
                fakeMovieCountriesRepository.Object,
                fakeMovieLanguagesRepository.Object,
                fakeWatchlistsRepository.Object);

            // Act
            var actualResult = service.GetMoviesByLetterAsQueryable<MovieDetailedViewModel>(letter).Count();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData("i", 1)]
        [InlineData("2", 2)]
        [InlineData("2012     ", 0)]
        [InlineData("aVataR", 2)]
        [InlineData(" oUt", 1)]
        [InlineData(null, 4)]
        [InlineData("r 2", 1)]
        public async Task SearchMoviesByTitleAsQueryableShoudReturnCorrectCount(string title, int expectedResult)
        {
            using var dbContext = this.InitializeDatabase();

            await dbContext.Movies.AddAsync(new Movie { Title = "Avatar" });
            await dbContext.Movies.AddAsync(new Movie { Title = "2012" });
            await dbContext.Movies.AddAsync(new Movie { Title = "Lights Out" });
            await dbContext.Movies.AddAsync(new Movie { Title = "Avatar 2" });

            await dbContext.SaveChangesAsync();

            using var moviesRepository = new EfDeletableEntityRepository<Movie>(dbContext);
            var fakeMovieGenresRepository = new Moq.Mock<IDeletableEntityRepository<MovieGenre>>();
            var fakeMovieCountriesRepository = new Moq.Mock<IDeletableEntityRepository<MovieCountry>>();
            var fakeMovieLanguagesRepository = new Moq.Mock<IDeletableEntityRepository<MovieLanguage>>();
            var fakeWatchlistsRepository = new Moq.Mock<IRepository<Watchlist>>();

            var service = new MoviesService(
                moviesRepository,
                fakeMovieGenresRepository.Object,
                fakeMovieCountriesRepository.Object,
                fakeMovieLanguagesRepository.Object,
                fakeWatchlistsRepository.Object);

            // Act
            var actualResult = service.SearchMoviesByTitleAsQueryable<MovieDetailedViewModel>(title).Count();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task GetMoviesByGenreNameAsQueryableShoudReturnCorrectCountAndInCorrectOrder()
        {
            using var dbContext = this.InitializeDatabase();

            await dbContext.Genres.AddAsync(new Genre { Name = "Horror" });

            await dbContext.SaveChangesAsync();

            var movieFirst = new Movie
            {
                Title = "Avatar",
            };

            var movieSecond = new Movie
            {
                Title = "A Quiet Place 2",
            };

            var movieThird = new Movie
            {
                Title = "Lights Out",
            };

            movieSecond.Genres.Add(new MovieGenre { GenreId = 1 });
            movieThird.Genres.Add(new MovieGenre { GenreId = 1 });

            await dbContext.Movies.AddAsync(movieFirst);
            await dbContext.Movies.AddAsync(movieSecond);
            await dbContext.Movies.AddAsync(movieThird);

            await dbContext.SaveChangesAsync();

            using var moviesRepository = new EfDeletableEntityRepository<Movie>(dbContext);
            var fakeMovieGenresRepository = new Moq.Mock<IDeletableEntityRepository<MovieGenre>>();
            var fakeMovieCountriesRepository = new Moq.Mock<IDeletableEntityRepository<MovieCountry>>();
            var fakeMovieLanguagesRepository = new Moq.Mock<IDeletableEntityRepository<MovieLanguage>>();
            var fakeWatchlistsRepository = new Moq.Mock<IRepository<Watchlist>>();

            var service = new MoviesService(
                moviesRepository,
                fakeMovieGenresRepository.Object,
                fakeMovieCountriesRepository.Object,
                fakeMovieLanguagesRepository.Object,
                fakeWatchlistsRepository.Object);

            // Act
            var movies = service.GetMoviesByGenreNameAsQueryable<MovieStandartViewModel>("Horror");
            var actualResult = movies.FirstOrDefault().Title;


            // Assert
            Assert.Equal(2, movies.Count());
            Assert.Equal("Lights Out", actualResult);
        }

        [Fact]
        public async Task GetMoviesByCountryNameAsQueryableShoudReturnCorrectCountAndInCorrectOrder()
        {
            using var dbContext = this.InitializeDatabase();

            await dbContext.Countries.AddAsync(new Country { Name = "USA" });

            await dbContext.SaveChangesAsync();

            var movieFirst = new Movie
            {
                Title = "Avatar",
            };

            var movieSecond = new Movie
            {
                Title = "A Quiet Place 2",
            };

            var movieThird = new Movie
            {
                Title = "Avatar 2",
            };

            movieFirst.ProductionCountries.Add(new MovieCountry { CountryId = 1 });
            movieSecond.ProductionCountries.Add(new MovieCountry { CountryId = 1 });
            movieThird.ProductionCountries.Add(new MovieCountry { CountryId = 1 });

            await dbContext.Movies.AddAsync(movieFirst);
            await dbContext.Movies.AddAsync(movieSecond);
            await dbContext.Movies.AddAsync(movieThird);

            await dbContext.SaveChangesAsync();

            using var moviesRepository = new EfDeletableEntityRepository<Movie>(dbContext);
            var fakeMovieGenresRepository = new Moq.Mock<IDeletableEntityRepository<MovieGenre>>();
            var fakeMovieCountriesRepository = new Moq.Mock<IDeletableEntityRepository<MovieCountry>>();
            var fakeMovieLanguagesRepository = new Moq.Mock<IDeletableEntityRepository<MovieLanguage>>();
            var fakeWatchlistsRepository = new Moq.Mock<IRepository<Watchlist>>();

            var service = new MoviesService(
                moviesRepository,
                fakeMovieGenresRepository.Object,
                fakeMovieCountriesRepository.Object,
                fakeMovieLanguagesRepository.Object,
                fakeWatchlistsRepository.Object);

            // Act
            var movies = service.GetMoviesByCountryNameAsQueryable<MovieStandartViewModel>("USA");
            var actualResult = movies.FirstOrDefault().Title;

            // Assert
            Assert.Equal(3, movies.Count());
            Assert.Equal("Avatar 2", actualResult);
        }

        [Fact]
        public async Task GetMoviesByReleaseYearAsQueryableShoudReturnCorrectCountAndInCorrectOrder()
        {
            using var dbContext = this.InitializeDatabase();

            var movieFirst = new Movie
            {
                Title = "Avatar",
                ReleaseDate = DateTime.ParseExact("2009-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            };

            var movieSecond = new Movie
            {
                Title = "A Quiet Place 2",
                ReleaseDate = DateTime.ParseExact("2021-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            };

            var movieThird = new Movie
            {
                Title = "2012",
                ReleaseDate = DateTime.ParseExact("2009-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            };

            await dbContext.Movies.AddAsync(movieFirst);
            await dbContext.Movies.AddAsync(movieSecond);
            await dbContext.Movies.AddAsync(movieThird);

            await dbContext.SaveChangesAsync();

            using var moviesRepository = new EfDeletableEntityRepository<Movie>(dbContext);
            var fakeMovieGenresRepository = new Moq.Mock<IDeletableEntityRepository<MovieGenre>>();
            var fakeMovieCountriesRepository = new Moq.Mock<IDeletableEntityRepository<MovieCountry>>();
            var fakeMovieLanguagesRepository = new Moq.Mock<IDeletableEntityRepository<MovieLanguage>>();
            var fakeWatchlistsRepository = new Moq.Mock<IRepository<Watchlist>>();

            var service = new MoviesService(
                moviesRepository,
                fakeMovieGenresRepository.Object,
                fakeMovieCountriesRepository.Object,
                fakeMovieLanguagesRepository.Object,
                fakeWatchlistsRepository.Object);

            // Act
            var movies = service.GetMoviesByReleaseYearAsQueryable<MovieStandartViewModel>(2009);
            var actualResult = movies.FirstOrDefault().Title;

            // Assert
            Assert.Equal(2, movies.Count());
            Assert.Equal("2012", actualResult);
        }

        [Fact]
        public async Task GetAllMoviesAsQueryableShoudReturnCorrectCountAndInCorrectOrder()
        {
            using var dbContext = this.InitializeDatabase();

            var movieFirst = new Movie
            {
                Title = "Lights Out",
            };

            var movieSecond = new Movie
            {
                Title = "A Quiet Place 2",
            };

            await dbContext.Movies.AddAsync(movieFirst);
            await dbContext.Movies.AddAsync(movieSecond);

            await dbContext.SaveChangesAsync();

            using var moviesRepository = new EfDeletableEntityRepository<Movie>(dbContext);
            var fakeMovieGenresRepository = new Moq.Mock<IDeletableEntityRepository<MovieGenre>>();
            var fakeMovieCountriesRepository = new Moq.Mock<IDeletableEntityRepository<MovieCountry>>();
            var fakeMovieLanguagesRepository = new Moq.Mock<IDeletableEntityRepository<MovieLanguage>>();
            var fakeWatchlistsRepository = new Moq.Mock<IRepository<Watchlist>>();

            var service = new MoviesService(
                moviesRepository,
                fakeMovieGenresRepository.Object,
                fakeMovieCountriesRepository.Object,
                fakeMovieLanguagesRepository.Object,
                fakeWatchlistsRepository.Object);

            // Act
            var movies = service.GetAllMoviesAsQueryable<MovieStandartViewModel>();
            var actualResult = movies.FirstOrDefault().Title;

            // Assert
            Assert.Equal(2, movies.Count());
            Assert.Equal("A Quiet Place 2", actualResult);
        }

        [Fact]
        public async Task GetMovieByIdAsyncShouldReturnCorrectMovie()
        {
            using var dbContext = this.InitializeDatabase();

            var movieFirst = new Movie
            {
                Title = "Lights Out",
            };

            var movieSecond = new Movie
            {
                Title = "A Quiet Place 2",
            };

            await dbContext.Movies.AddAsync(movieFirst);
            await dbContext.Movies.AddAsync(movieSecond);

            await dbContext.SaveChangesAsync();

            using var moviesRepository = new EfDeletableEntityRepository<Movie>(dbContext);
            var fakeMovieGenresRepository = new Moq.Mock<IDeletableEntityRepository<MovieGenre>>();
            var fakeMovieCountriesRepository = new Moq.Mock<IDeletableEntityRepository<MovieCountry>>();
            var fakeMovieLanguagesRepository = new Moq.Mock<IDeletableEntityRepository<MovieLanguage>>();
            var fakeWatchlistsRepository = new Moq.Mock<IRepository<Watchlist>>();

            var service = new MoviesService(
                moviesRepository,
                fakeMovieGenresRepository.Object,
                fakeMovieCountriesRepository.Object,
                fakeMovieLanguagesRepository.Object,
                fakeWatchlistsRepository.Object);

            // Act
            var actualResult = await service.GetMovieByIdAsync<MovieSinglePageViewModel>(2);

            // Assert
            Assert.Equal("A Quiet Place 2", actualResult.Title);
        }


        private CineMagicDbContext InitializeDatabase()
        {
            var options = new DbContextOptionsBuilder<CineMagicDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            return new CineMagicDbContext(options);
        }

        private void InitializeMapper() => AutoMapperConfig.
             RegisterMappings(Assembly.Load("CineMagic.Web.ViewModels"));
    }
}
