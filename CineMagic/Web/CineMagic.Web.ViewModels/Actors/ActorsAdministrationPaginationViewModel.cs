namespace CineMagic.Web.ViewModels.Actors
{
    public class ActorsAdministrationPaginationViewModel
    {
        public PaginatedList<ActorsAdministrationViewModel> Actors { get; set; }

        public string SearchString { get; set; }
    }
}
