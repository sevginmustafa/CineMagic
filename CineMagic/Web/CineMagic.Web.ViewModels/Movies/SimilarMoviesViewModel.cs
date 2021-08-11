namespace CineMagic.Web.ViewModels.Movies
{
    using System;

    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class SimilarMoviesViewModel : IMapFrom<Movie>
    {
        public int Id { get; set; }

        public string PosterPath { get; set; }

        public string Title { get; set; }

        public string Tagline { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
