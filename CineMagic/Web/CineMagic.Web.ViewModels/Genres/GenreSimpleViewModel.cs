namespace CineMagic.Web.ViewModels.Genres
{
    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class GenreSimpleViewModel : IMapFrom<Genre>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
