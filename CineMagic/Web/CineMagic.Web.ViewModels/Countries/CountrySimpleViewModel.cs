namespace CineMagic.Web.ViewModels.Countries
{
    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class CountrySimpleViewModel : IMapFrom<Country>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
