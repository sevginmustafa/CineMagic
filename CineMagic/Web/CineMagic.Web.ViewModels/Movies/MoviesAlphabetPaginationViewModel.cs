namespace CineMagic.Web.ViewModels.Movies
{
    public class MoviesAlphabetPaginationViewModel
    {
        public PaginatedList<MovieDetailedViewModel> Movies { get; set; }

        public AlphabetPagingViewModel AlphabetPagingViewModel { get; set; }
    }
}
