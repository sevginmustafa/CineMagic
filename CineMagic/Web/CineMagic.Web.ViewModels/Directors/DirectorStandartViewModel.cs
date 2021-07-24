namespace CineMagic.Web.ViewModels.Directors
{
    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class DirectorStandartViewModel : IMapFrom<Director>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        public string ProfilePicPath { get; set; }

        public double Popularity { get; set; }
    }
}
