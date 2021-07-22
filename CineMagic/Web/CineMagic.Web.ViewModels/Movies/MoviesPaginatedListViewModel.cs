namespace CineMagic.Web.ViewModels.Movies
{
    using System;
    using System.Collections.Generic;

    public class MoviesPaginatedListViewModel
    {
        public IEnumerable<MovieStandartViewModel> Movies { get; set; }

        public int PageNumber { get; set; }

        public int MoviesCount { get; set; }

        public int ItemsPerPage { get; set; }

        public int PagesCount => (int)Math.Ceiling((double)this.MoviesCount / this.ItemsPerPage);

        public bool HasPreviousPage => this.PageNumber > 1;

        public int PreviousPage => this.PageNumber - 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int NextPage => this.PageNumber + 1;
    }
}
