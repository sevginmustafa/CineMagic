//namespace CineMagic.Services.Scraping
//{
//    using System;
//    using System.Globalization;
//    using System.Linq;
//    using System.Text.RegularExpressions;
//    using System.Threading.Tasks;

//    using AngleSharp;
//    using CineMagic.Data.Common.Repositories;
//    using CineMagic.Data.Models;
//    using CineMagic.Data.Models.Enums;
//    using CineMagic.Services.Scraping.DTOs;

//    public class TheMovieDbOrgScraperService : ITheMovieDbOrgScraperService
//    {
//        private readonly IConfiguration config;
//        private readonly IBrowsingContext context;
//        private readonly IDeletableEntityRepository<Movie> moviesRepository;
//        private readonly IDeletableEntityRepository<Director> directorsRepository;
//        private readonly IDeletableEntityRepository<Actor> actorsRepository;
//        private readonly IDeletableEntityRepository<Genre> genresRepository;
//        private readonly IDeletableEntityRepository<Country> countriesRepository;

//        public TheMovieDbOrgScraperService(
//            IDeletableEntityRepository<Movie> moviesRepository,
//            IDeletableEntityRepository<Director> directorsRepository,
//            IDeletableEntityRepository<Actor> actorsRepository,
//            IDeletableEntityRepository<Genre> genresRepository,
//            IDeletableEntityRepository<Country> countriesRepository)
//        {
//            this.config = Configuration.Default.WithDefaultLoader();
//            this.context = BrowsingContext.New(this.config);

//            this.moviesRepository = moviesRepository;
//            this.directorsRepository = directorsRepository;
//            this.actorsRepository = actorsRepository;
//            this.genresRepository = genresRepository;
//            this.countriesRepository = countriesRepository;
//        }

//        public async Task GetMovieData(int startIndex, int endIndex)
//        {
//            for (int i = startIndex; i <= endIndex; i++)
//            {
//                await this.GetMovieData(i);
//            }
//        }

//        private async Task GetMovieData(int id)
//        {
//            try
//            {
//                var link = $"https://www.themoviedb.org/movie/{id}";

//                var document = this.context.OpenAsync(link)
//                    .GetAwaiter()
//                    .GetResult();

//                var languageBudgetRevenue = document.QuerySelector(".facts.left_column").TextContent
//                   .Trim()
//                   .Split("\n")
//                   .Where(x => x.Contains("Budget") || x.Contains("Language") || x.Contains("Revenue"))
//                   .ToArray();

//                // Get Language
//                var language = languageBudgetRevenue[0].Split().LastOrDefault();

//                if (language != "English")
//                {
//                    return;
//                }

//                // Get Budget
//                var budget = double.Parse(languageBudgetRevenue[1].Split()
//                    .LastOrDefault()
//                    .Replace("$", string.Empty)
//                    .Replace(",", string.Empty));

//                var revenue = double.Parse(languageBudgetRevenue[2].Split()
//                    .LastOrDefault()
//                    .Replace("$", string.Empty)
//                    .Replace(",", string.Empty));

//                // Get Title
//                var tryTitle = document.QuerySelector("div.title.ott_false > h2 > a");

//                if (tryTitle == null)
//                {
//                    tryTitle = document.QuerySelector("div.title.ott_true > h2 > a");
//                }

//                var title = tryTitle.TextContent;

//                // Get ReleaseYear
//                var releaseYear = short.Parse(document.QuerySelector("span.tag.release_date").TextContent
//                    .Replace("(", string.Empty)
//                    .Replace(")", string.Empty));

//                // Get Genres
//                var genres = document.QuerySelectorAll("span.genres > a")
//                    .Select(x => x.TextContent)
//                    .ToArray();

//                // Get Runtime
//                var runtime = document.QuerySelector("span.runtime").TextContent.Trim();

//                // Get CoverImageUrl
//                var coverImageUrl = "https://www.themoviedb.org/" +
//                    document.QuerySelector("div.image_content.backdrop > img.poster.lazyload")
//                    .Attributes.FirstOrDefault(x => x.Name == "data-src").Value;

//                // Get Description
//                var description = document.QuerySelector(".overview").TextContent.Trim();

//                // Get TrailerUrl
//                var trailerUrl = "https://www.youtube.com/watch?v=" +
//                    document.QuerySelector("li.video.none > a")
//                    .Attributes.FirstOrDefault(x => x.Name == "data-id").Value;

//                // Get Actors
//                var actorsLinks = document.QuerySelectorAll(".people.scroller > .card > p > a");
//                var characters = document.QuerySelectorAll(".people.scroller > .card > .character");

//                var movie = new Movie
//                {
//                    Title = title,
//                    ReleaseDate = releaseYear,
//                    Runtime = runtime,
//                    Overview = description,
//                    Language = language,
//                    Budget = budget,
//                    Revenue = revenue,
//                    PosterPath = coverImageUrl,
//                    TrailerPath = trailerUrl,
//                    IMDBLink = link,
//                };

//                for (int i = 0; i < genres.Length; i++)
//                {
//                    var findGenre = this.genresRepository.All().FirstOrDefault(x => x.Name == genres[i]);

//                    if (findGenre == null)
//                    {
//                        findGenre = new Genre { Name = genres[i] };
//                        await this.genresRepository.AddAsync(findGenre);
//                        await this.genresRepository.SaveChangesAsync();
//                    }

//                    movie.Genres.Add(new MovieGenre { Genre = findGenre });
//                }

//                for (int i = 0; i < actorsLinks.Length; i++)
//                {
//                    var actorLink = actorsLinks[i].Attributes.FirstOrDefault().Value;

//                    try
//                    {
//                        var actor = this.GetPersonData(actorLink);

//                        var findActor = this.actorsRepository.All().FirstOrDefault(x => x.Name == actor.Name);

//                        if (findActor == null)
//                        {
//                            findActor = new Actor
//                            {
//                                Name = actor.Name,
//                                Biography = actor.Biography,
//                                Gender = (Gender)Enum.Parse(typeof(Gender), actor.Gender),
//                                Birthday = DateTime.ParseExact(actor.Birthday, "yyyy-MM-dd", CultureInfo.InvariantCulture),
//                                Deathday = actor.Deathday != null ? DateTime.ParseExact(actor.Deathday, "yyyy-MM-dd", CultureInfo.InvariantCulture) : null,
//                                ProfilePath = actor.CoverImageUrl,
//                            };

//                            var findCountry = this.countriesRepository.All().FirstOrDefault(x => x.Name == actor.Country);

//                            if (findCountry == null)
//                            {
//                                findCountry = new Country { Name = actor.Country };
//                                await this.countriesRepository.AddAsync(findCountry);
//                                await this.countriesRepository.SaveChangesAsync();
//                            }

//                            var findCity = findCountry.Cities.FirstOrDefault(x => x.Name == actor.Birthplace);

//                            if (findCity == null)
//                            {
//                                findCity = new City { Name = actor.Birthplace };
//                                findCountry.Cities.Add(findCity);
//                            }

//                            findActor.City = findCity;

//                            await this.actorsRepository.AddAsync(findActor);
//                            await this.actorsRepository.SaveChangesAsync();
//                        }

//                        movie.Cast.Add(new MovieActor { Actor = findActor, CharacterName = characters[i].TextContent });
//                    }
//                    catch (Exception)
//                    {
//                    }
//                }

//                // Get Dirctor
//                var directorLink = document.QuerySelectorAll(".people.no_image > li > p > a")
//                    .FirstOrDefault()
//                    .Attributes.FirstOrDefault().Value;

//                var director = this.GetPersonData(directorLink);

//                var findDirector = this.directorsRepository.All().FirstOrDefault(x => x.Name == director.Name);

//                if (findDirector == null)
//                {
//                    findDirector = new Director
//                    {
//                        Name = director.Name,
//                        Biography = director.Biography,
//                        Gender = (Gender)Enum.Parse(typeof(Gender), director.Gender),
//                        Birthday = DateTime.ParseExact(director.Birthday, "yyyy-MM-dd", CultureInfo.InvariantCulture),
//                        Deathday = director.Deathday != null ? DateTime.ParseExact(director.Deathday, "yyyy-MM-dd", CultureInfo.InvariantCulture) : null,
//                        ProfilePath = director.CoverImageUrl,
//                    };

//                    var findCountry = this.countriesRepository.All().FirstOrDefault(x => x.Name == director.Country);

//                    if (findCountry == null)
//                    {
//                        findCountry = new Country { Name = director.Country };
//                        await this.countriesRepository.AddAsync(findCountry);
//                        await this.countriesRepository.SaveChangesAsync();
//                    }

//                    var findCity = findCountry.Cities.FirstOrDefault(x => x.Name == director.Birthplace);

//                    if (findCity == null)
//                    {
//                        findCity = new City { Name = director.Birthplace };
//                        findCountry.Cities.Add(findCity);
//                    }

//                    findDirector.City = findCity;

//                    movie.Director = findDirector;

//                    await this.moviesRepository.AddAsync(movie);
//                    await this.moviesRepository.SaveChangesAsync();
//                }
//            }
//            catch (Exception)
//            {
//            }
//        }

//        private ScrapePersonDTO GetPersonData(string personLink)
//        {
//            // Get PersonLink
//            var link = "https://www.themoviedb.org" + personLink;

//            var personInfo = this.context.OpenAsync(link)
//                .GetAwaiter()
//                .GetResult();

//            // Get Name
//            var name = personInfo.QuerySelector(".title > h2 > a").TextContent;

//            // Get Biography
//            var biography = personInfo.QuerySelectorAll(".text.initial > p")
//                .FirstOrDefault().TextContent;

//            // Get Birthday and Birthplace
//            var birthdayAndBirthplace = personInfo.QuerySelectorAll(".full")
//                .Select(x => x.TextContent.Replace("  ", string.Empty))
//                .ToArray();

//            var birthday = Regex.Match(birthdayAndBirthplace[0], @"[0-9]{4}-[0-9]{2}-[0-9]{2}").ToString();

//            string deathday = null;
//            string[] placeAndCountry = null;

//            if (birthdayAndBirthplace[1].Contains("Day of Death"))
//            {
//                deathday = Regex.Match(birthdayAndBirthplace[1], @"[0-9]{4}-[0-9]{2}-[0-9]{2}").ToString();
//                placeAndCountry = birthdayAndBirthplace[2].Replace("Place of Birth ", string.Empty)
//                    .Split(", ")
//                    .ToArray();
//            }
//            else
//            {
//                placeAndCountry = birthdayAndBirthplace[1].Replace("Place of Birth ", string.Empty)
//                    .Split(", ")
//                    .ToArray();
//            }

//            var birthplace = placeAndCountry.FirstOrDefault();
//            var country = placeAndCountry.LastOrDefault();

//            // Get Gender
//            var gender = personInfo.QuerySelector("#media_v4 > div > div > div.grey_column > div > section > section > p:nth-child(3)")
//                .TextContent.Replace("Gender ", string.Empty);

//            // Get CoverImageUrl
//            var coverImageUrl = "https://www.themoviedb.org" +
//                personInfo.QuerySelector("#original_header > div > div.image_content > img")
//                .Attributes.FirstOrDefault(x => x.Name == "data-src").Value;

//            var person = new ScrapePersonDTO
//            {
//                Name = name,
//                Biography = biography,
//                Gender = gender,
//                Birthday = birthday,
//                Deathday = deathday,
//                Birthplace = birthplace,
//                Country = country,
//                CoverImageUrl = coverImageUrl,
//            };

//            return person;
//        }
//    }
//}
