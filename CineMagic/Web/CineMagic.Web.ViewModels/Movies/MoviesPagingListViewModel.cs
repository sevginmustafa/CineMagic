namespace CineMagic.Web.ViewModels.Movies
{
    using System;
    using System.Collections.Generic;

    public class MoviesPagingListViewModel
    {
        public IEnumerable<MovieStandartViewModel> Movies { get; set; }

        public string GenreName { get; set; }

        public int PageNumber { get; set; }

        public int MoviesCount { get; set; }

        public int ItemsPerPage { get; set; }

        public int PagesCount => (int)Math.Ceiling(this.MoviesCount / this.ItemsPerPage * 1.0);

        public bool HasPreviousPage => this.PageNumber > 1;

        public int PreviousPage => this.PageNumber - 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int NextPage => this.PageNumber + 1;
    }
}
