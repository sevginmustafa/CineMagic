namespace CineMagic.Web.ViewModels.Movies
{
    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class MovieLanguagesViewModel : IMapFrom<MovieLanguage>
    {
        public int MovieId { get; set; }

        public int LanguageId { get; set; }

        public string LanguageName { get; set; }
    }
}
