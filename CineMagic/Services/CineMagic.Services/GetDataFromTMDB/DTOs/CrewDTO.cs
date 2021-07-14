namespace CineMagic.Services.GetDataFromTMDB.DTOs
{
    using Newtonsoft.Json;

    public class CrewDTO
    {
        [JsonProperty("id")]
        public int DirectorId { get; set; }

        public string Job { get; set; }
    }
}
