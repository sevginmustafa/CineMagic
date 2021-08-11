namespace CineMagic.Web.ViewModels.Directors
{
    using System.Collections.Generic;

    public class DirectorSinglePageListViewModel
    {
        public DirectorSinglePageViewModel Director { get; set; }

        public IEnumerable<DirectorHighestGrossingMoviesViewModel> Movies { get; set; }
    }
}
