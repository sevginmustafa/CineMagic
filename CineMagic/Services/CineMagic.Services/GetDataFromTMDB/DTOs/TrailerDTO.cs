namespace CineMagic.Services.GetDataFromTMDB.DTOs
{
    using System.Collections.Generic;

    public class TrailerDTO
    {
        public ICollection<TrailerPathDTO> Results { get; set; }
    }
}
