namespace InTheAction.Services.GetDataFromTMDB
{
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    using InTheAction.Services.GetDataFromTMDB.DTOs;
    using Newtonsoft.Json;

    public class GetDataFromTMDBService : IGetDataFromTMDBService
    {
        public async Task GetMovieDataAsJSON(int startIndex, int endIndex)
        {
            WebClient client = new WebClient();
            StringBuilder sb = new StringBuilder();

            for (int i = startIndex; i <= endIndex; i++)
            {
                sb.Append(client.DownloadString($"https://api.themoviedb.org/3/movie/{i}?api_key=bc76d9675394b601c098e4b5c540a75d") + ',');
            }

            string result = '[' + sb.ToString().TrimEnd(',') + ']';

            var movies = JsonConvert.DeserializeObject<MovieDTO[]>(result);

            for (int i = startIndex; i <= endIndex; i++)
            {
                sb.Append(client.DownloadString($"https://api.themoviedb.org/3/movie/{i}/videos?api_key=bc76d9675394b601c098e4b5c540a75d") + ',');
            }

           result = '[' + sb.ToString().TrimEnd(',') + ']';

            var trailer = JsonConvert.DeserializeObject<MovieDTO[]>(result);
        }
    }
}
