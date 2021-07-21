namespace CineMagic.Web.ViewModels.Movies
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class MovieFooterViewModel : IMapFrom<Movie>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string PosterPath { get; set; }
    }
}
