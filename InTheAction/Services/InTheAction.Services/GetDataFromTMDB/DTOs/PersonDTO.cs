namespace InTheAction.Services.GetDataFromTMDB.DTOs
{
    using Newtonsoft.Json;

    public class PersonDTO
    {
        public string Name { get; set; }

        [JsonProperty("profile_path")]
        public string ProfilePath { get; set; }

        public string Biography { get; set; }

        public int Gender { get; set; }

        public string Birthday { get; set; }

        public string Deathday { get; set; }

        [JsonProperty("place_of_birth")]
        public string Birthplace { get; set; }
    }
}
