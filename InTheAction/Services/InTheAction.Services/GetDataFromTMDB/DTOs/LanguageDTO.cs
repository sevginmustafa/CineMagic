namespace InTheAction.Services.GetDataFromTMDB.DTOs
{
    using Newtonsoft.Json;

    public class LanguageDTO
    {
        [JsonProperty("english_name")]
        public string Name { get; set; }
    }
}
