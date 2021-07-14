namespace CineMagic.Web.ViewModels.Genres
{
    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class GenreNavbarViewModel : IMapFrom<Genre>
    {
        public string Name { get; set; }
    }
}
