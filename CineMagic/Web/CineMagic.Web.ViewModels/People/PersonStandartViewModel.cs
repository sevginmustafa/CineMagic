namespace CineMagic.Web.ViewModels.People
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class PersonStandartViewModel : IMapFrom<Actor>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        public string ProfilePicPath { get; set; }

        public double Popularity { get; set; }
    }
}
