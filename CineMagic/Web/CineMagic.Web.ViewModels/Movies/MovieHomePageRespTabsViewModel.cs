namespace CineMagic.Web.ViewModels.Movies
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class MovieHomePageRespTabsViewModel : MovieHomePageViewModel
    {
        public string Overview { get; set; }

        public ICollection<MovieGenresViewModel> Genres { get; set; }
    }
}
