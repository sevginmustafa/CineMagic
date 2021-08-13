namespace CineMagic.Web.ViewModels.Directors
{
    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class DirectorSimpleViewModel : IMapFrom<Director>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
