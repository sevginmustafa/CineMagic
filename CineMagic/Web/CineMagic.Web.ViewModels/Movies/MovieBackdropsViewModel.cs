namespace CineMagic.Web.ViewModels.Movies
{
    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class MovieBackdropsViewModel : IMapFrom<MovieBackdrop>
    {
        public string Path { get; set; }
    }
}
