namespace InTheAction.Services.GetDataFromTMDB.DTOs
{
    using Newtonsoft.Json;

    public class TrailerPathDTO
    {
        [JsonProperty("key")]
        public string Path { get; set; }
    }
}
