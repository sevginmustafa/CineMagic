namespace CineMagic.Web.ViewModels.Directors
{
    public class DirectorsAlphabetPaginationViewModel
    {
        public PaginatedList<DirectorDetailedViewModel> Directors { get; set; }

        public AlphabetPagingViewModel AlphabetPagingViewModel { get; set; }

        public string SearchString { get; set; }
    }
}
