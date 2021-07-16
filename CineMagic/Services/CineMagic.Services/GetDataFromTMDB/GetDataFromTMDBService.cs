namespace CineMagic.Services.GetDataFromTMDB
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    using CineMagic.Services.GetDataFromTMDB.DTOs;
    using Newtonsoft.Json;

    public class GetDataFromTMDBService : IGetDataFromTMDBService
    {
        private readonly WebClient client = new WebClient();

        public MovieDTO GetMovieDataAsJSON(int movieId)
        {
            try
            {
                string result = this.client.DownloadString($"https://api.themoviedb.org/3/movie/{movieId}?api_key=bc76d9675394b601c098e4b5c540a75d");

                var movie = JsonConvert.DeserializeObject<MovieDTO>(result);

                return movie;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public BackdropsDTO GetMovieBackdropsDataAsJSON(int movieId)
        {
            string result = this.client.DownloadString($"https://api.themoviedb.org/3/movie/{movieId}/images?api_key=bc76d9675394b601c098e4b5c540a75d");

            var backdrops = JsonConvert.DeserializeObject<BackdropsDTO>(result);

            return backdrops;
        }

        public string GetMovieTrailerPathDataAsJSON(int movieId)
        {
            string result = this.client.DownloadString($"https://api.themoviedb.org/3/movie/{movieId}/videos?api_key=bc76d9675394b601c098e4b5c540a75d");

            var trailer = JsonConvert.DeserializeObject<TrailerDTO>(result);

            return trailer.Results.FirstOrDefault()?.Path;
        }

        public CastAndCrewDTO GetMovieCastAndCrewDataAsJSON(int movieId)
        {
            string result = this.client.DownloadString($"https://api.themoviedb.org/3/movie/{movieId}/credits?api_key=bc76d9675394b601c098e4b5c540a75d");

            var castAndCrew = JsonConvert.DeserializeObject<CastAndCrewDTO>(result);

            return castAndCrew;
        }

        public PersonDTO GetMovieActorDataAsJSON(int actorId)
        {
            string result = this.client.DownloadString($"https://api.themoviedb.org/3/person/{actorId}?api_key=bc76d9675394b601c098e4b5c540a75d");

            var actor = JsonConvert.DeserializeObject<PersonDTO>(result);

            return actor;
        }

        public PersonDTO GetMovieDirectorDataAsJSON(CastAndCrewDTO castAndCrew)
        {
            var directorId = castAndCrew.Crew.FirstOrDefault(x => x.Job == "Director").DirectorId;

            string result = this.client.DownloadString($"https://api.themoviedb.org/3/person/{directorId}?api_key=bc76d9675394b601c098e4b5c540a75d");

            var director = JsonConvert.DeserializeObject<PersonDTO>(result);

            return director;
        }
    }
}
