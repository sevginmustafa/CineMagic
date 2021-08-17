namespace CineMagic.Services.Data.Tests
{
    using System.Net;

    using CineMagic.Services.GetDataFromTMDB;
    using CineMagic.Services.GetDataFromTMDB.DTOs;
    using Xunit;

    public class GetDataFromTMDBServiceTests
    {
        [Fact]
        public void GetMovieDataAsJSONShouldReturnMovieDTOWithCorrectProperties()
        {
            var movieDTO = new MovieDTO
            {
                Title = "Gladiator",
            };

            var service = new GetDataFromTMDBService();

            var actualResult = service.GetMovieDataAsJSON(98);

            Assert.Equal(movieDTO.Title, actualResult.Title);
        }

        [Fact]
        public void GetMovieDataAsJSONShouldReturnNullIfInvaliIdIsGiven()
        {
            var service = new GetDataFromTMDBService();

            var actualResult = service.GetMovieDataAsJSON(-98);

            Assert.Null(actualResult);
        }

        [Fact]
        public void GetMovieBackdropsDataAsJSONShouldReturnCorrectCount()
        {
            var service = new GetDataFromTMDBService();

            var actualResult = service.GetMovieBackdropsDataAsJSON(98);

            Assert.Equal(21, actualResult.Backdrops.Count);
        }

        [Fact]
        public void GetMovieBackdropsDataAsJSONShouldThrowExceptionWhenInvalidIdIsGiven()
        {
            var service = new GetDataFromTMDBService();

            Assert.Throws<WebException>(()
                => service.GetMovieBackdropsDataAsJSON(-98));
        }

        [Fact]
        public void GetMovieTrailerPathDataAsJSONhouldReturnCorrectUrl()
        {
            var service = new GetDataFromTMDBService();

            var trailerDTO = new TrailerPathDTO
            {
                Path = "owK1qxDselE",
            };

            var actualResult = service.GetMovieTrailerPathDataAsJSON(98);

            Assert.Equal(trailerDTO.Path, actualResult);
        }

        [Fact]
        public void GetMovieTrailerPathDataAsJSONShouldThrowExceptionWhenInvalidIdIsGiven()
        {
            var service = new GetDataFromTMDBService();

            Assert.Throws<WebException>(()
                => service.GetMovieTrailerPathDataAsJSON(-98));
        }

        [Fact]
        public void GetMovieCastAndCrewDataAsJSONShouldReturnCorrectCollectionCount()
        {
            var service = new GetDataFromTMDBService();

            var trailerDTO = new TrailerPathDTO
            {
                Path = "owK1qxDselE",
            };

            var actualResultCastCount = service.GetMovieCastAndCrewDataAsJSON(98).Cast.Count;
            var actualResultCrewCount = service.GetMovieCastAndCrewDataAsJSON(98).Crew.Count;

            Assert.Equal(40, actualResultCastCount);
            Assert.Equal(199, actualResultCrewCount);
        }

        [Fact]
        public void GetMovieCastAndCrewDataAsJSONShouldThrowExceptionWhenInvalidIdIsGiven()
        {
            var service = new GetDataFromTMDBService();

            Assert.Throws<WebException>(()
                => service.GetMovieCastAndCrewDataAsJSON(-98));
        }

        [Fact]
        public void GetMovieActorDataAsJSONShouldReturnCorrectCollectionCount()
        {
            var service = new GetDataFromTMDBService();

            var personDTO = new PersonDTO
            {
                Name = "Sarah Polley",
            };

            var actualResult = service.GetMovieActorDataAsJSON(98).Name;

            Assert.Equal(personDTO.Name, actualResult);
        }

        [Fact]
        public void GetMovieActorDataAsJSONShouldThrowExceptionWhenInvalidIdIsGiven()
        {
            var service = new GetDataFromTMDBService();

            Assert.Throws<WebException>(()
                => service.GetMovieActorDataAsJSON(-98));
        }

        [Fact]
        public void GetMovieDirectorDataAsJSONShouldReturnCorrectResult()
        {
            var service = new GetDataFromTMDBService();

            var personDTO = new PersonDTO
            {
                Name = "Ridley Scott",
            };

            var castAndCrew = service.GetMovieCastAndCrewDataAsJSON(98);
            var actualResult = service.GetMovieDirectorDataAsJSON(castAndCrew).Name;

            Assert.Equal(personDTO.Name, actualResult);
        }
    }
}
