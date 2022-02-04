namespace CineMagic.Web.ViewModels.Directors
{
    public class DirectorsAdministrationPaginationViewModel
    {
        public PaginatedList<DirectorsAdministrationViewModel> Directors { get; set; }

        public string SearchString { get; set; }
    }
}
