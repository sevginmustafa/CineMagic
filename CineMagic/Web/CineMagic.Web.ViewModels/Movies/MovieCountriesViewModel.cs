namespace CineMagic.Web.ViewModels.Movies
{
    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class MovieCountriesViewModel : IMapFrom<MovieCountry>
    {
        public int MovieId { get; set; }

        public int CountryId { get; set; }

        public string CountryName { get; set; }
    }
}
