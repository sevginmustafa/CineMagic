namespace InTheAction.Web.ViewModels.Movies
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class MostRecentMovieViewModel : RecentMoviesViewModel
    {
        public string Overview { get; set; }

        public ICollection<MovieGenresViewModel> Genres { get; set; }
    }
}
