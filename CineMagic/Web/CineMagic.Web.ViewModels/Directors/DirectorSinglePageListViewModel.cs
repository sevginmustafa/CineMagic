namespace CineMagic.Web.ViewModels.Directors
{
    using System.Collections.Generic;

    public class DirectorSinglePageListViewModel
    {
        public DirectorSinglePageViewModel Director { get; set; }

        public IEnumerable<DirectorBestProfitMoviesViewModel> Movies { get; set; }
    }
}
