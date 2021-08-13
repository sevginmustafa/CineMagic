namespace CineMagic.Web.ViewModels.Languages
{
    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class LanguageSimpleViewModel : IMapFrom<Language>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
