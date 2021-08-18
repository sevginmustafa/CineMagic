namespace CineMagic.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using CineMagic.Data;
    using CineMagic.Data.Common.Repositories;
    using CineMagic.Data.Models;
    using CineMagic.Data.Repositories;
    using CineMagic.Services.Mapping;
    using CineMagic.Web.ViewModels.Actors;
    using CineMagic.Web.ViewModels.Directors;
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
        [InlineData(null, 5)]
        [InlineData("", 5)]
        [InlineData("All", 5)]
        [InlineData("0 - 9", 1)]
        public async Task GetMoviesByLetterAsQueryableShoudReturnCorrectCountDependingOnLetterGiven(string letter, int expectedResult)
        {
            using var dbContext = this.InitializeDatabase();

            await dbContext.Movies.AddAsync(new Movie { Title = "Avatar" });
            await dbContext.Movies.AddAsync(new Movie { Title = "2012" });
            await dbContext.Movies.AddAsync(new Movie { Title = "Lights Out" });
            await dbContext.Movies.AddAsync(new Movie { Title = "Avatar 2" });
            await dbContext.Movies.AddAsync(new Movie { Title = "Escape Room" });

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
        public async Task GetAllMoviesAsQueryableOrderedByTitleShoudReturnCorrectCountAndInCorrectOrder()
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
            var movies = service.GetAllMoviesAsQueryableOrderedByTitle<MovieStandartViewModel>();
            var actualResult = movies.FirstOrDefault().Title;

            // Assert
            Assert.Equal(2, movies.Count());
            Assert.Equal("A Quiet Place 2", actualResult);
        }

        [Fact]
        public async Task GetAllMoviesAsQueryableOrderedByCreatedOnShoudReturnCorrectCountAndInCorrectOrder()
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
            var movies = service.GetAllMoviesAsQueryableOrderedByCreatedOn<MovieStandartViewModel>();
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
            var actualResult = await service.GetMovieByIdAsync<MovieStandartViewModel>(2);

            // Assert
            Assert.Equal("A Quiet Place 2", actualResult.Title);
        }

        [Fact]
        public async Task GetMovieByIdAsyncShouldThrowExceptionWhenInvalidIdIsGiven()
        {
            using var dbContext = this.InitializeDatabase();

            var movieFirst = new Movie
            {
                Title = "Lights Out",
            };

            await dbContext.Movies.AddAsync(movieFirst);

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

            // Act - Assert
            await Assert.ThrowsAsync<NullReferenceException>(()
                => service.GetMovieByIdAsync<MovieStandartViewModel>(20));
        }

        [Fact]
        public async Task AddToUserWatchlistAsyncShoudIncreaseCount()
        {
            using var dbContext = this.InitializeDatabase();

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
            await service.AddToUserWatchlistAsync(1, "first");
            var actualResult = watchlistsRepository.AllAsNoTracking().Count();

            // Assert
            Assert.Equal(1, actualResult);
        }

        [Fact]
        public async Task RemoveFromUserWatchlistAsync()
        {
            using var dbContext = this.InitializeDatabase();
            await dbContext.Movies.AddAsync(new Movie { Title = "Avatar" });

            await dbContext.SaveChangesAsync();

            await dbContext.Watchlists.AddAsync(new Watchlist
            {
                MovieId = 1,
                UserId = "first",
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

            var entity = await dbContext.Watchlists.FirstOrDefaultAsync(x => x.MovieId == 1);
            dbContext.Entry(entity).State = EntityState.Detached;

            // Act
            await service.RemoveFromUserWatchlistAsync(1, "first");
            var actualResult = watchlistsRepository.AllAsNoTracking().Count();

            // Assert
            Assert.Equal(0, actualResult);
        }

        [Fact]
        public async Task GetSimilarMoviesAsyncShoudReturnCorrectCount()
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
            var actualResult = await service.GetSimilarMoviesAsync<SimilarMoviesViewModel>(2, 5);

            // Assert
            Assert.Single(actualResult);
        }

        [Fact]
        public async Task GetActorMostPopularMoviesAsyncShoudReturnCorrectCountInCorrectOrder()
        {
            using var dbContext = this.InitializeDatabase();

            await dbContext.Actors.AddAsync(new Actor { Name = "Emily Blunt" });

            await dbContext.SaveChangesAsync();

            var movieFirst = new Movie
            {
                Title = "Avatar",
                Popularity = 55,
            };

            var movieSecond = new Movie
            {
                Title = "A Quiet Place 2",
                Popularity = 66,
            };

            var movieThird = new Movie
            {
                Title = "A Quiet Place",
                Popularity = 77,
            };

            movieSecond.Cast.Add(new MovieActor { ActorId = 1 });
            movieThird.Cast.Add(new MovieActor { ActorId = 1 });

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
            var movies = await service.GetActorMostPopularMoviesAsync<ActorMostPopularMoviesViewModel>(1, 5);
            var actualResult = movies.FirstOrDefault().Title;

            // Assert
            Assert.Equal(2, movies.Count());
            Assert.Equal("A Quiet Place", actualResult);
        }

        [Fact]
        public async Task GetDirectorBestProfitMoviesAsyncShoudReturnCorrectCountInCorrectOrder()
        {
            using var dbContext = this.InitializeDatabase();

            await dbContext.Directors.AddAsync(new Director { Name = "John Krasinski" });

            await dbContext.SaveChangesAsync();

            var movieFirst = new Movie
            {
                Title = "Avatar",
                Revenue = 55,
            };

            var movieSecond = new Movie
            {
                Title = "A Quiet Place 2",
                Revenue = 166,
            };

            var movieThird = new Movie
            {
                Title = "A Quiet Place",
                Revenue = 77,
            };

            movieSecond.DirectorId = 1;
            movieThird.DirectorId = 1;

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
            var movies = await service.GetDirectorBestProfitMoviesAsync<DirectorHighestGrossingMoviesViewModel>(1, 2);
            var actualResult = movies.FirstOrDefault().Title;

            // Assert
            Assert.Equal(2, movies.Count());
            Assert.Equal("A Quiet Place 2", actualResult);
        }

        [Fact]
        public async Task CreateAsyncShouldIncreaseCount()
        {
            using var dbContext = this.InitializeDatabase();

            var movieCreateInputModel = new MovieCreateInputModel
            {
                Title = "Avatar",
                PosterPath = "www.google.com",
                TrailerPath = null,
                IMDBLink = null,
                ReleaseDate = DateTime.UtcNow,
                Runtime = 120,
                Tagline = null,
                Overview = "No Overview at the moment",
                Budget = 0,
                Revenue = 0,
                Popularity = 1,
                DirectorId = 1,
                SelectedGenres = new List<int> { 1 },
                SelectedCountries = new List<int> { 1 },
                SelectedLanguages = new List<int> { 1 },
            };

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
            await service.CreateAsync(movieCreateInputModel);
            var actualResult = moviesRepository.AllAsNoTracking().Count();

            // Assert
            Assert.Equal(1, actualResult);
        }

        [Fact]
        public async Task CreateAsyncShouldThrowExceptionWhenMovieAlreadyExists()
        {
            using var dbContext = this.InitializeDatabase();

            await dbContext.Movies.AddAsync(new Movie { Title = "Avatar" });
            await dbContext.SaveChangesAsync();

            var movieCreateInputModel = new MovieCreateInputModel
            {
                Title = "Avatar",
                PosterPath = "www.google.com",
                TrailerPath = null,
                IMDBLink = null,
                ReleaseDate = DateTime.UtcNow,
                Runtime = 120,
                Tagline = null,
                Overview = "No Overview at the moment",
                Budget = 0,
                Revenue = 0,
                Popularity = 1,
                DirectorId = 1,
                SelectedGenres = new List<int> { 1 },
                SelectedCountries = new List<int> { 1 },
                SelectedLanguages = new List<int> { 1 },
            };

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

            // Act - Assert
            await Assert.ThrowsAsync<ArgumentException>(()
                => service.CreateAsync(movieCreateInputModel));
        }

        [Fact]
        public async Task DeleteAsyncShouldDecreaseCount()
        {
            using var dbContext = this.InitializeDatabase();

            await dbContext.Movies.AddAsync(new Movie { });
            await dbContext.SaveChangesAsync();

            using var moviesRepository = new EfDeletableEntityRepository<Movie>(dbContext);
            using var movieGenresRepository = new EfDeletableEntityRepository<MovieGenre>(dbContext);
            using var movieCountriesRepository = new EfDeletableEntityRepository<MovieCountry>(dbContext);
            using var movieLanguagesRepository = new EfDeletableEntityRepository<MovieLanguage>(dbContext);
            var fakeWatchlistsRepository = new Moq.Mock<IRepository<Watchlist>>();

            var service = new MoviesService(
                moviesRepository,
                movieGenresRepository,
                movieCountriesRepository,
                movieLanguagesRepository,
                fakeWatchlistsRepository.Object);

            var entity = await dbContext.Movies.FindAsync(1);
            dbContext.Entry(entity).State = EntityState.Detached;

            // Act
            await service.DeleteAsync(1);
            var actualResult = moviesRepository.AllAsNoTracking().Count();

            // Assert
            Assert.Equal(0, actualResult);
        }

        [Fact]
        public async Task DeleteAsyncShouldDecreaseCountOfMappingTables()
        {
            using var dbContext = this.InitializeDatabase();

            await dbContext.MoviesGenres.AddAsync(new MovieGenre { GenreId = 1, MovieId = 1 });
            await dbContext.MoviesCountries.AddAsync(new MovieCountry { CountryId = 1, MovieId = 1 });
            await dbContext.MoviesCountries.AddAsync(new MovieCountry { CountryId = 2, MovieId = 2 });
            await dbContext.MoviesLanguages.AddAsync(new MovieLanguage { LanguageId = 1, MovieId = 1 });

            await dbContext.Movies.AddAsync(new Movie { });
            await dbContext.SaveChangesAsync();

            using var moviesRepository = new EfDeletableEntityRepository<Movie>(dbContext);
            using var movieGenresRepository = new EfDeletableEntityRepository<MovieGenre>(dbContext);
            using var movieCountriesRepository = new EfDeletableEntityRepository<MovieCountry>(dbContext);
            using var movieLanguagesRepository = new EfDeletableEntityRepository<MovieLanguage>(dbContext);
            var fakeWatchlistsRepository = new Moq.Mock<IRepository<Watchlist>>();

            var service = new MoviesService(
                moviesRepository,
                movieGenresRepository,
                movieCountriesRepository,
                movieLanguagesRepository,
                fakeWatchlistsRepository.Object);

            var entity = await dbContext.Movies.FindAsync(1);
            dbContext.Entry(entity).State = EntityState.Detached;

            // Act
            await service.DeleteAsync(1);
            var movieGenresCount = movieGenresRepository.AllAsNoTracking().Count();
            var movieCountriesCount = movieCountriesRepository.AllAsNoTracking().Count();
            var movieLanguagesCount = movieLanguagesRepository.AllAsNoTracking().Count();

            // Assert
            Assert.Equal(0, movieGenresCount);
            Assert.Equal(1, movieCountriesCount);
            Assert.Equal(0, movieLanguagesCount);
        }

        [Fact]
        public async Task DeleteAsyncShouldThrowExceptionWhenInvalidIdIsGiven()
        {
            using var dbContext = this.InitializeDatabase();

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

            // Act - Assert
            await Assert.ThrowsAsync<NullReferenceException>(()
                => service.DeleteAsync(50));
        }

        [Fact]
        public async Task EditAsyncShouldWorkCorrectly()
        {
            using var dbContext = this.InitializeDatabase();

            var movieEditViewModel = new MovieEditViewModel
            {
                Id = 1,
                Title = "Avatar",
                PosterPath = "www.google.com",
                TrailerPath = null,
                IMDBLink = null,
                ReleaseDate = DateTime.UtcNow,
                Runtime = 120,
                Tagline = null,
                Overview = "No Overview at the moment",
                Budget = 0,
                Revenue = 0,
                Popularity = 1,
                DirectorId = 1,
                SelectedGenres = new List<int> { 1 },
                SelectedCountries = new List<int> { 1 },
                SelectedLanguages = new List<int> { 1 },
            };

            await dbContext.Movies.AddAsync(new Movie { Title = "Avatar 2" });
            await dbContext.SaveChangesAsync();

            using var moviesRepository = new EfDeletableEntityRepository<Movie>(dbContext);
            using var movieGenresRepository = new EfDeletableEntityRepository<MovieGenre>(dbContext);
            using var movieCountriesRepository = new EfDeletableEntityRepository<MovieCountry>(dbContext);
            using var movieLanguagesRepository = new EfDeletableEntityRepository<MovieLanguage>(dbContext);
            var fakeWatchlistsRepository = new Moq.Mock<IRepository<Watchlist>>();

            var service = new MoviesService(
                moviesRepository,
                movieGenresRepository,
                movieCountriesRepository,
                movieLanguagesRepository,
                fakeWatchlistsRepository.Object);

            var entity = await dbContext.Movies.FindAsync(1);
            dbContext.Entry(entity).State = EntityState.Detached;

            // Act
            await service.EditAsync(movieEditViewModel);
            var actualResult = await moviesRepository.AllAsNoTracking().FirstOrDefaultAsync();

            // Assert
            Assert.Equal("Avatar", actualResult.Title);
        }

        [Fact]
        public async Task EditAsyncShouldThrowExceptionIfInvalidIdIsGiven()
        {
            using var dbContext = this.InitializeDatabase();

            var movieEditViewModel = new MovieEditViewModel
            {
                Id = 10,
                Title = "Avatar",
                PosterPath = "www.google.com",
                TrailerPath = null,
                IMDBLink = null,
                ReleaseDate = DateTime.UtcNow,
                Runtime = 120,
                Tagline = null,
                Overview = "No Overview at the moment",
                Budget = 0,
                Revenue = 0,
                Popularity = 1,
                DirectorId = 1,
                SelectedGenres = new List<int> { 1 },
                SelectedCountries = new List<int> { 1 },
                SelectedLanguages = new List<int> { 1 },
            };

            await dbContext.Movies.AddAsync(new Movie { Title = "Avatar 2" });
            await dbContext.SaveChangesAsync();

            using var moviesRepository = new EfDeletableEntityRepository<Movie>(dbContext);
            using var movieGenresRepository = new EfDeletableEntityRepository<MovieGenre>(dbContext);
            using var movieCountriesRepository = new EfDeletableEntityRepository<MovieCountry>(dbContext);
            using var movieLanguagesRepository = new EfDeletableEntityRepository<MovieLanguage>(dbContext);
            var fakeWatchlistsRepository = new Moq.Mock<IRepository<Watchlist>>();

            var service = new MoviesService(
                moviesRepository,
                movieGenresRepository,
                movieCountriesRepository,
                movieLanguagesRepository,
                fakeWatchlistsRepository.Object);

            // Act - Assert
            await Assert.ThrowsAsync<NullReferenceException>(()
                => service.EditAsync(movieEditViewModel));
        }

        [Fact]
        public async Task EditAsyncShouldChangeMappingTables()
        {
            using var dbContext = this.InitializeDatabase();

            await dbContext.Genres.AddAsync(new Genre { Name = "Action" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Horror" });

            await dbContext.Countries.AddAsync(new Country { Name = "USA" });
            await dbContext.Countries.AddAsync(new Country { Name = "Bulgaria" });

            await dbContext.Languages.AddAsync(new Language { Name = "English" });
            await dbContext.Languages.AddAsync(new Language { Name = "Bulgarian" });

            await dbContext.SaveChangesAsync();

            await dbContext.MoviesGenres.AddAsync(new MovieGenre { MovieId = 1, GenreId = 2 });

            await dbContext.MoviesCountries.AddAsync(new MovieCountry { MovieId = 1, CountryId = 2 });

            await dbContext.MoviesLanguages.AddAsync(new MovieLanguage { MovieId = 1, LanguageId = 2 });

            await dbContext.SaveChangesAsync();

            var movieEditViewModel = new MovieEditViewModel
            {
                Id = 1,
                Title = "Avatar",
                PosterPath = "www.google.com",
                TrailerPath = null,
                IMDBLink = null,
                ReleaseDate = DateTime.UtcNow,
                Runtime = 120,
                Tagline = null,
                Overview = "No Overview at the moment",
                Budget = 0,
                Revenue = 0,
                Popularity = 1,
                DirectorId = 1,
                SelectedGenres = new List<int> { 2 },
                SelectedCountries = new List<int> { 2 },
                SelectedLanguages = new List<int> { 2 },
            };

            var movieToEdit = new Movie { };

            await dbContext.Movies.AddAsync(movieToEdit);
            await dbContext.SaveChangesAsync();

            using var moviesRepository = new EfDeletableEntityRepository<Movie>(dbContext);
            using var movieGenresRepository = new EfDeletableEntityRepository<MovieGenre>(dbContext);
            using var movieCountriesRepository = new EfDeletableEntityRepository<MovieCountry>(dbContext);
            using var movieLanguagesRepository = new EfDeletableEntityRepository<MovieLanguage>(dbContext);
            var fakeWatchlistsRepository = new Moq.Mock<IRepository<Watchlist>>();

            var service = new MoviesService(
                moviesRepository,
                movieGenresRepository,
                movieCountriesRepository,
                movieLanguagesRepository,
                fakeWatchlistsRepository.Object);

            var entityMovie = await dbContext.Movies.FindAsync(1);
            dbContext.Entry(entityMovie).State = EntityState.Detached;

            var entityMovieGenres = await dbContext.MoviesGenres.FirstOrDefaultAsync(x => x.MovieId == 1);
            dbContext.Entry(entityMovieGenres).State = EntityState.Detached;

            var entityMovieCountries = await dbContext.MoviesCountries.FirstOrDefaultAsync(x => x.MovieId == 1);
            dbContext.Entry(entityMovieCountries).State = EntityState.Detached;

            var entityMovieLanguages = await dbContext.MoviesLanguages.FirstOrDefaultAsync(x => x.MovieId == 1);
            dbContext.Entry(entityMovieLanguages).State = EntityState.Detached;

            // Act
            await service.EditAsync(movieEditViewModel);

            // Assert
            Assert.Contains(movieToEdit.Genres, x => x.GenreId == 2);
            Assert.Contains(movieToEdit.ProductionCountries, x => x.CountryId == 2);
            Assert.Contains(movieToEdit.Languages, x => x.LanguageId == 2);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncShouldReturnCorrectViewModel()
        {
            using var dbContext = this.InitializeDatabase();

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
            var actualResult = await service.GetViewModelByIdAsync<MovieEditViewModel>(1);

            // Assert
            Assert.IsType<MovieEditViewModel>(actualResult);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncShouldThrowExceptionWhenInvalidIdIsGiven()
        {
            using var dbContext = this.InitializeDatabase();

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

            // Act - Assert
            await Assert.ThrowsAsync<NullReferenceException>(()
                => service.GetViewModelByIdAsync<MovieStandartViewModel>(55));
        }

        [Fact]
        public async Task GetAllMovieGenresAsyncShouldReturnCorrectCount()
        {
            using var dbContext = this.InitializeDatabase();

            await dbContext.Genres.AddAsync(new Genre { Name = "Action" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Horror" });

            await dbContext.MoviesGenres.AddAsync(new MovieGenre { MovieId = 1, GenreId = 1 });
            await dbContext.MoviesGenres.AddAsync(new MovieGenre { MovieId = 1, GenreId = 2 });

            await dbContext.SaveChangesAsync();

            var fakeMoviesRepository = new Moq.Mock<IDeletableEntityRepository<Movie>>();
            using var movieGenresRepository = new EfDeletableEntityRepository<MovieGenre>(dbContext);
            var fakeMovieCountriesRepository = new Moq.Mock<IDeletableEntityRepository<MovieCountry>>();
            var fakeMovieLanguagesRepository = new Moq.Mock<IDeletableEntityRepository<MovieLanguage>>();
            var fakeWatchlistsRepository = new Moq.Mock<IRepository<Watchlist>>();

            var service = new MoviesService(
                fakeMoviesRepository.Object,
                movieGenresRepository,
                fakeMovieCountriesRepository.Object,
                fakeMovieLanguagesRepository.Object,
                fakeWatchlistsRepository.Object);

            // Act
            var actualResult = await service.GetAllMovieGenresAsync<MovieGenresViewModel>(1);

            // Assert
            Assert.Equal(2, actualResult.Count());
        }

        [Fact]
        public async Task GetAllMovieCountriesAsyncShouldReturnCorrectCount()
        {
            using var dbContext = this.InitializeDatabase();

            await dbContext.Countries.AddAsync(new Country { Name = "USA" });
            await dbContext.Countries.AddAsync(new Country { Name = "Bulgaria" });

            await dbContext.MoviesCountries.AddAsync(new MovieCountry { MovieId = 1, CountryId = 2 });

            await dbContext.SaveChangesAsync();

            var fakeMoviesRepository = new Moq.Mock<IDeletableEntityRepository<Movie>>();
            var fakeMovieGenresRepository = new Moq.Mock<IDeletableEntityRepository<MovieGenre>>();
            using var movieCountriesRepository = new EfDeletableEntityRepository<MovieCountry>(dbContext);
            var fakeMovieLanguagesRepository = new Moq.Mock<IDeletableEntityRepository<MovieLanguage>>();
            var fakeWatchlistsRepository = new Moq.Mock<IRepository<Watchlist>>();

            var service = new MoviesService(
                fakeMoviesRepository.Object,
                fakeMovieGenresRepository.Object,
                movieCountriesRepository,
                fakeMovieLanguagesRepository.Object,
                fakeWatchlistsRepository.Object);

            // Act
            var actualResult = await service.GetAllMovieCountriesAsync<MovieCountriesViewModel>(1);

            // Assert
            Assert.Single(actualResult);
        }

        [Fact]
        public async Task GetAllMovieLanguagesAsyncShouldReturnCorrectCount()
        {
            using var dbContext = this.InitializeDatabase();

            await dbContext.Languages.AddAsync(new Language { Name = "Bulgarian" });

            await dbContext.MoviesLanguages.AddAsync(new MovieLanguage { MovieId = 1, LanguageId = 1 });

            await dbContext.SaveChangesAsync();

            var fakeMoviesRepository = new Moq.Mock<IDeletableEntityRepository<Movie>>();
            var fakeMovieGenresRepository = new Moq.Mock<IDeletableEntityRepository<MovieGenre>>();
            var fakeMovieCountriesRepository = new Moq.Mock<IDeletableEntityRepository<MovieCountry>>();
            using var movieLanguagesRepository = new EfDeletableEntityRepository<MovieLanguage>(dbContext);
            var fakeWatchlistsRepository = new Moq.Mock<IRepository<Watchlist>>();

            var service = new MoviesService(
                fakeMoviesRepository.Object,
                fakeMovieGenresRepository.Object,
                fakeMovieCountriesRepository.Object,
                movieLanguagesRepository,
                fakeWatchlistsRepository.Object);

            // Act
            var actualResult = await service.GetAllMovieLanguagesAsync<MovieLanguagesViewModel>(1);

            // Assert
            Assert.Single(actualResult);
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
