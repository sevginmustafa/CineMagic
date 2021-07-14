namespace CineMagic.Services.GetDataFromTMDB.DTOs
{
    using Newtonsoft.Json;

    public class CastDTO
    {
        [JsonProperty("id")]
        public int ActorId { get; set; }

        [JsonProperty("character")]
        public string CharacterName { get; set; }

        [JsonProperty("known_for_department")]
        public string Department { get; set; }
    }
}
