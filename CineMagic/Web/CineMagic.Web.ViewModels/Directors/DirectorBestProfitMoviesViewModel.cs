namespace CineMagic.Web.ViewModels.Directors
{
    using System;

    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class DirectorBestProfitMoviesViewModel : IMapFrom<Movie>
    {
        public int Id { get; set; }

        public string PosterPath { get; set; }

        public string Title { get; set; }

        public string Tagline { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
