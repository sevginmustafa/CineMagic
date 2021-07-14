namespace InTheAction.Web.ViewModels.Countries
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using InTheAction.Data.Models;
    using InTheAction.Services.Mapping;

    public class CountryNavbarViewModel : IMapFrom<Country>
    {
        public string Name { get; set; }
    }
}
