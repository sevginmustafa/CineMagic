namespace CineMagic.Web.ViewModels.Actors
{
    public class ActorsAlphabetPaginationViewModel
    {
        public PaginatedList<ActorDetailedViewModel> Actors { get; set; }

        public AlphabetPagingViewModel AlphabetPagingViewModel { get; set; }

        public string SearchString { get; set; }
    }
}
