namespace CineMagic.Web.ViewModels.Movies
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CineMagic.Common;
    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;
    using CineMagic.Web.ViewModels.Countries;
    using CineMagic.Web.ViewModels.Directors;
    using CineMagic.Web.ViewModels.Genres;
    using CineMagic.Web.ViewModels.Languages;

    public class MovieEditViewModel : IMapFrom<Movie>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(ModelValidation.Movie.TitleMaxLength, ErrorMessage = ModelValidation.Movie.TitleErrorMessage, MinimumLength = ModelValidation.Movie.TitleMinLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(ModelValidation.Movie.PosterPathMaxLength, ErrorMessage = ModelValidation.Movie.PosterPathErrorMessage, MinimumLength = ModelValidation.Movie.PosterPathMinLength)]
        [Display(Name = ModelValidation.Movie.PosterPathDisplayName)]
        public string PosterPath { get; set; }

        [StringLength(ModelValidation.Movie.TrailerPathMaxLength, ErrorMessage = ModelValidation.Movie.TrailerPathErrorMessage, MinimumLength = ModelValidation.Movie.TrailerPathMinLength)]
        [Display(Name = ModelValidation.Movie.TrailerPathDisplayName)]
        public string TrailerPath { get; set; }

        [StringLength(ModelValidation.Movie.IMDBLinkMaxLength, ErrorMessage = ModelValidation.Movie.IMDBLinkErrorMessage, MinimumLength = ModelValidation.Movie.IMDBLinkMinLength)]
        [Display(Name = ModelValidation.Movie.IMDBLinkDisplayName)]
        public string IMDBLink { get; set; }

        [Display(Name = ModelValidation.Movie.ReleaseDatekDisplayName)]
        public DateTime ReleaseDate { get; set; }

        [Range(ModelValidation.Movie.RuntimeMinValue, ModelValidation.Movie.RuntimeMaxValue, ErrorMessage = ModelValidation.Movie.RuntimeErrorMessage)]
        public int Runtime { get; set; }

        [StringLength(ModelValidation.Movie.TaglineMaxLength, ErrorMessage = ModelValidation.Movie.TaglineErrorMessage, MinimumLength = ModelValidation.Movie.TaglineMinLength)]
        public string Tagline { get; set; }

        [Required]
        [StringLength(ModelValidation.Movie.OverviewMaxLength, ErrorMessage = ModelValidation.Movie.OverviewErrorMessage, MinimumLength = ModelValidation.Movie.OverviewMinLength)]
        public string Overview { get; set; }

        public double Budget { get; set; }

        public double Revenue { get; set; }

        [Range(0, ModelValidation.Movie.PopularityMaxValue, ErrorMessage = ModelValidation.Movie.PopularityErrorMessage)]
        public double Popularity { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = ModelValidation.Person.DirectorIdError)]
        [Display(Name = ModelValidation.Person.DirectorDisplayName)]
        public int DirectorId { get; set; }

        [Required(ErrorMessage = ModelValidation.Genre.GenreIdError)]
        [Display(Name = ModelValidation.Genre.GenresDisplayName)]
        public IList<int> SelectedGenres { get; set; }

        [Required(ErrorMessage = ModelValidation.Country.CountryIdError)]
        [Display(Name = ModelValidation.Country.CountriesDisplayName)]
        public IList<int> SelectedCountries { get; set; }

        [Required(ErrorMessage = ModelValidation.Country.CountryIdError)]
        [Display(Name = ModelValidation.Language.LanguagesDisplayName)]
        public IList<int> SelectedLanguages { get; set; }

        public IEnumerable<DirectorSimpleViewModel> AllDirectors { get; set; }

        public IEnumerable<GenreSimpleViewModel> AllGenres { get; set; }

        public IEnumerable<CountrySimpleViewModel> AllCountries { get; set; }

        public IEnumerable<LanguageSimpleViewModel> AllLanguages { get; set; }

        public IEnumerable<MovieGenresViewModel> Genres { get; set; }

        public IEnumerable<MovieCountriesViewModel> ProductionCountries { get; set; }

        public IEnumerable<MovieLanguagesViewModel> Languages { get; set; }
    }
}
