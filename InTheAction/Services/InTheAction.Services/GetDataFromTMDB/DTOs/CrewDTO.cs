namespace InTheAction.Services.GetDataFromTMDB.DTOs
{
    using Newtonsoft.Json;

    public class CrewDTO
    {
        [JsonProperty("id")]
        public int DirectorId { get; set; }

        [JsonProperty("known_for_department")]
        public string Department { get; set; }
    }
}
