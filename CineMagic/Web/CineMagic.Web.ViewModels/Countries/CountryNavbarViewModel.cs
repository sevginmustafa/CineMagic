namespace CineMagic.Web.ViewModels.Countries
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class CountryNavbarViewModel : IMapFrom<Country>
    {
        public string Name { get; set; }
    }
}
