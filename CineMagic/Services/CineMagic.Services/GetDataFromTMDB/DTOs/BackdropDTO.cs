namespace CineMagic.Services.GetDataFromTMDB.DTOs
{
    using Newtonsoft.Json;

    public class BackdropDTO
    {
        [JsonProperty("file_path")]
        public string FilePath { get; set; }

        [JsonProperty("iso_639_1")]
        public string ISO { get; set; }
    }
}
