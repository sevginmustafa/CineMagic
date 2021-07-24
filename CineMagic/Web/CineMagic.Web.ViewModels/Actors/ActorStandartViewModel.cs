namespace CineMagic.Web.ViewModels.Actors
{
    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class ActorStandartViewModel : IMapFrom<Actor>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        public string ProfilePicPath { get; set; }

        public double Popularity { get; set; }
    }
}
