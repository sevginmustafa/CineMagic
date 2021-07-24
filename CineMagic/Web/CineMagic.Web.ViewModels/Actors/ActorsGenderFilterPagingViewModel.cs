namespace CineMagic.Web.ViewModels.Actors
{
    using CineMagic.Data.Models.Enums;

    public class ActorsGenderFilterPagingViewModel
    {
        public PaginatedList<ActorStandartViewModel> Actors { get; set; }

        public Gender Gender { get; set; }
    }
}
