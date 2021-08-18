namespace CineMagic.Web.ViewModels.InputModels.Administration
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CineMagic.Web.ViewModels.Countries;
    using CineMagic.Web.ViewModels.Directors;
    using CineMagic.Web.ViewModels.Genres;
    using CineMagic.Web.ViewModels.Languages;

    using static CineMagic.Common.ModelValidation;

    public class MovieCreateInputModel
    {
        [Required]
        [StringLength(Movie.TitleMaxLength, ErrorMessage = Movie.TitleErrorMessage, MinimumLength = Movie.TitleMinLength)]
        public string Title { get; set; }

        [Required]
        [Url]
        [StringLength(Movie.PosterPathMaxLength, ErrorMessage = Movie.PosterPathErrorMessage, MinimumLength = Movie.PosterPathMinLength)]
        [Display(Name = Movie.PosterPathDisplayName)]
        public string PosterPath { get; set; }

        [StringLength(Movie.TrailerPathMaxLength, ErrorMessage = Movie.TrailerPathErrorMessage, MinimumLength = Movie.TrailerPathMinLength)]
        [Display(Name = Movie.TrailerPathDisplayName)]
        public string TrailerPath { get; set; }

        [Required]
        [StringLength(Movie.IMDBLinkMaxLength, ErrorMessage = Movie.IMDBLinkErrorMessage, MinimumLength = Movie.IMDBLinkMinLength)]
        [Display(Name = Movie.IMDBLinkDisplayName)]
        public string IMDBLink { get; set; }

        [Display(Name = Movie.ReleaseDatekDisplayName)]
        public DateTime ReleaseDate { get; set; }

        [Range(Movie.RuntimeMinValue, Movie.RuntimeMaxValue, ErrorMessage = Movie.RuntimeErrorMessage)]
        public int Runtime { get; set; }

        [StringLength(Movie.TaglineMaxLength, ErrorMessage = Movie.TaglineErrorMessage, MinimumLength = Movie.TaglineMinLength)]
        public string Tagline { get; set; }

        [Required]
        [StringLength(Movie.OverviewMaxLength, ErrorMessage = Movie.OverviewErrorMessage, MinimumLength = Movie.OverviewMinLength)]
        public string Overview { get; set; }

        public double Budget { get; set; }

        public double Revenue { get; set; }

        [Range(0, Movie.PopularityMaxValue, ErrorMessage = Movie.PopularityErrorMessage)]
        public double Popularity { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = Person.DirectorIdError)]
        [Display(Name = Person.DirectorDisplayName)]
        public int DirectorId { get; set; }

        [Required(ErrorMessage = Genre.GenreIdError)]
        [Display(Name = Genre.GenresDisplayName)]
        public IList<int> SelectedGenres { get; set; }

        [Required(ErrorMessage = Country.CountryIdError)]
        [Display(Name = Country.CountriesDisplayName)]
        public IList<int> SelectedCountries { get; set; }

        [Required(ErrorMessage = Language.LanguageIdError)]
        [Display(Name = Language.LanguagesDisplayName)]
        public IList<int> SelectedLanguages { get; set; }

        public IEnumerable<DirectorSimpleViewModel> AllDirectors { get; set; }

        public IEnumerable<GenreSimpleViewModel> AllGenres { get; set; }

        public IEnumerable<CountrySimpleViewModel> AllCountries { get; set; }

        public IEnumerable<LanguageSimpleViewModel> AllLanguages { get; set; }
    }
}
