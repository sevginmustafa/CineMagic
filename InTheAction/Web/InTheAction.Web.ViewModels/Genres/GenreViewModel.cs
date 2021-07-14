namespace InTheAction.Web.ViewModels.Genres
{
    using InTheAction.Data.Models;
    using InTheAction.Services.Mapping;

    public class GenreViewModel : IMapFrom<Genre>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
