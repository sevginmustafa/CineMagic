namespace CineMagic.Web.ViewModels.Movies
{
    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class MovieCountriesViewModel : IMapFrom<MovieCountry>
    {
        public string CountryName { get; set; }
    }
}
