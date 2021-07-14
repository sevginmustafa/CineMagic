namespace CineMagic.Services.GetDataFromTMDB
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using CineMagic.Data.Common.Repositories;
    using CineMagic.Data.Models;
    using CineMagic.Data.Models.Enums;

    public class FillDatabaseService : IFillDatabaseService
    {
        private readonly IDeletableEntityRepository<Movie> moviesRepository;
        private readonly IDeletableEntityRepository<Director> directorsRepository;
        private readonly IDeletableEntityRepository<Actor> actorsRepository;
        private readonly IDeletableEntityRepository<Genre> genresRepository;
        private readonly IDeletableEntityRepository<Country> countriesRepository;
        private readonly IGetDataFromTMDBService getDataFromTMDBService;

        public FillDatabaseService(
            IDeletableEntityRepository<Movie> moviesRepository,
            IDeletableEntityRepository<Director> directorsRepository,
            IDeletableEntityRepository<Actor> actorsRepository,
            IDeletableEntityRepository<Genre> genresRepository,
            IDeletableEntityRepository<Country> countriesRepository,
            IGetDataFromTMDBService getDataFromTMDBService)
        {
            this.moviesRepository = moviesRepository;
            this.directorsRepository = directorsRepository;
            this.actorsRepository = actorsRepository;
            this.genresRepository = genresRepository;
            this.countriesRepository = countriesRepository;
            this.getDataFromTMDBService = getDataFromTMDBService;
        }

        public async Task AddDataToDBAsync(int startIndex, int endIndex)
        {
            for (int i = startIndex; i <= endIndex; i++)
            {
                var movieDTO = this.getDataFromTMDBService.GetMovieDataAsJSON(i);

                if (movieDTO != null &&
                    movieDTO.PosterPath != null &&
                    movieDTO.IMDBId != null &&
                    movieDTO.Runtime.HasValue &&
                    movieDTO.Overview != null &&
                    movieDTO.Runtime > 70 &&
                    DateTime.ParseExact(movieDTO.ReleaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture).Year >= 1970 &&
                    movieDTO.NumberOfVotes > 300)
                {
                    var trailer = this.getDataFromTMDBService.GetMovieTrailerPathDataAsJSON(movieDTO.Id);

                    var movie = new Movie
                    {
                        Title = movieDTO.Title,
                        PosterPath = "https://www.themoviedb.org/t/p/w600_and_h900_bestv2" + movieDTO.PosterPath,
                        TrailerPath = trailer,
                        IMDBLink = "https://www.imdb.com/title/" + movieDTO.IMDBId,
                        ReleaseDate = DateTime.ParseExact(movieDTO.ReleaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                        Runtime = movieDTO.Runtime.Value,
                        Overview = movieDTO.Overview,
                        Language = movieDTO.Language.Select(x => x.Name).FirstOrDefault(),
                        Budget = movieDTO.Budget,
                        Revenue = movieDTO.Revenue,
                        Popularity=movieDTO.Popularity,
                        CurrentAverageVote = movieDTO.AverageVote,
                        CurrentNumberOfVotes = movieDTO.NumberOfVotes,
                    };

                    foreach (var genre in movieDTO.Genres)
                    {
                        var findGenre = this.genresRepository.AllAsNoTracking().FirstOrDefault(x => x.Name == genre.Name);

                        if (findGenre == null)
                        {
                            findGenre = new Genre { Name = genre.Name };

                            await this.genresRepository.AddAsync(findGenre);

                            await this.genresRepository.SaveChangesAsync();
                        }

                        movie.Genres.Add(new MovieGenre { GenreId = findGenre.Id });
                    }

                    foreach (var country in movieDTO.ProductionCountries)
                    {
                        var findCountry = this.countriesRepository.AllAsNoTracking().FirstOrDefault(x => x.Name == country.Name);

                        if (findCountry == null)
                        {
                            findCountry = new Country { Name = country.Name };

                            await this.countriesRepository.AddAsync(findCountry);

                            await this.countriesRepository.SaveChangesAsync();
                        }

                        movie.ProductionCountries.Add(new MovieCountry { CountryId = findCountry.Id });
                    }

                    var castAndCrew = this.getDataFromTMDBService.GetMovieCastAndCrewDataAsJSON(movieDTO.Id);

                    var director = this.getDataFromTMDBService.GetMovieDirectorDataAsJSON(castAndCrew);

                    var findDirector = this.directorsRepository.AllAsNoTracking().FirstOrDefault(x => x.Name == director.Name);

                    if (findDirector == null)
                    {
                        findDirector = new Director
                        {
                            Name = director.Name,
                            ProfilePicPath = director.ProfilePicPath != null ? "https://www.themoviedb.org/t/p/w600_and_h900_bestv2" + director.ProfilePicPath : null,
                            Biography = director.Biography,
                            Gender = (Gender)director.Gender,
                            Birthday = director.Birthday != null ? DateTime.ParseExact(director.Birthday, "yyyy-MM-dd", CultureInfo.InvariantCulture) : null,
                            Deathday = director.Deathday != null ? DateTime.ParseExact(director.Deathday, "yyyy-MM-dd", CultureInfo.InvariantCulture) : null,
                            Birthplace = director.Birthplace,
                        };

                        await this.directorsRepository.AddAsync(findDirector);

                        await this.directorsRepository.SaveChangesAsync();
                    }

                    movie.DirectorId = findDirector.Id;

                    foreach (var cast in castAndCrew.Cast.Take(10))
                    {
                        var actor = this.getDataFromTMDBService.GetMovieActorDataAsJSON(cast.ActorId);

                        var findActor = this.actorsRepository.AllAsNoTracking().FirstOrDefault(x => x.Name == actor.Name);

                        if (findActor == null)
                        {
                            findActor = new Actor
                            {
                                Name = actor.Name,
                                ProfilePicPath = actor.ProfilePicPath != null ? "https://www.themoviedb.org/t/p/w600_and_h900_bestv2" + actor.ProfilePicPath : null,
                                Biography = actor.Biography,
                                Gender = (Gender)actor.Gender,
                                Birthday = actor.Birthday != null ? DateTime.ParseExact(actor.Birthday, "yyyy-MM-dd", CultureInfo.InvariantCulture) : null,
                                Deathday = actor.Deathday != null ? DateTime.ParseExact(actor.Deathday, "yyyy-MM-dd", CultureInfo.InvariantCulture) : null,
                                Birthplace = actor.Birthplace,
                            };

                            await this.actorsRepository.AddAsync(findActor);

                            await this.actorsRepository.SaveChangesAsync();
                        }

                        movie.Cast.Add(new MovieActor { ActorId = findActor.Id, CharacterName = cast.CharacterName });
                    }

                    await this.moviesRepository.AddAsync(movie);

                    await this.moviesRepository.SaveChangesAsync();
                }
            }
        }
    }
}
