namespace InTheAction.Services.GetDataFromTMDB
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;

    using InTheAction.Services.GetDataFromTMDB.DTOs;
    using Newtonsoft.Json;

    public class GetDataFromTMDBService : IGetDataFromTMDBService
    {
        private readonly WebClient client = new WebClient();

        public IEnumerable<MovieDTO> GetMovieDataAsJSON(int startIndex, int endIndex)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = startIndex; i <= endIndex; i++)
            {
                sb.Append(this.client.DownloadString($"https://api.themoviedb.org/3/movie/{i}?api_key=bc76d9675394b601c098e4b5c540a75d") + ',');
            }

            string result = '[' + sb.ToString().TrimEnd(',') + ']';

            var movies = JsonConvert.DeserializeObject<MovieDTO[]>(result);

            return movies;
        }

        public string GetMovieTrailerPathDataAsJSON(int movieId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(this.client.DownloadString($"https://api.themoviedb.org/3/movie/{movieId}/videos?api_key=bc76d9675394b601c098e4b5c540a75d"));

            var trailer = JsonConvert.DeserializeObject<TrailerDTO>(sb.ToString());

            return trailer.Results.FirstOrDefault().Path;
        }

        public CastAndCrewDTO GetMovieCastAndCrewDataAsJSON(int movieId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(this.client.DownloadString($"https://api.themoviedb.org/3/movie/{movieId}/credits?api_key=bc76d9675394b601c098e4b5c540a75d"));

            var castAndCrew = JsonConvert.DeserializeObject<CastAndCrewDTO>(sb.ToString());

            return castAndCrew;
        }

        public PersonDTO GetMovieActorDataAsJSON(int actorId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(this.client.DownloadString($"https://api.themoviedb.org/3/person/{actorId}?api_key=bc76d9675394b601c098e4b5c540a75d"));

            var actor = JsonConvert.DeserializeObject<PersonDTO>(sb.ToString());

            return actor;
        }

        public PersonDTO GetMovieDirectorDataAsJSON(CastAndCrewDTO castAndCrew)
        {
            var directorId = castAndCrew.Crew.FirstOrDefault(x => x.Department == "Directing").DirectorId;

            StringBuilder sb = new StringBuilder();

            sb.Append(this.client.DownloadString($"https://api.themoviedb.org/3/person/{directorId}?api_key=bc76d9675394b601c098e4b5c540a75d"));

            var director = JsonConvert.DeserializeObject<PersonDTO>(sb.ToString());

            return director;
        }
    }
}
