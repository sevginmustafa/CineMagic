namespace CineMagic.Web.ViewModels.Movies
{
    public class MoviesAdministrationPaginationViewModel
    {
        public PaginatedList<MoviesAdministrationViewModel> Movies { get; set; }

        public string SearchString { get; set; }
    }
}
