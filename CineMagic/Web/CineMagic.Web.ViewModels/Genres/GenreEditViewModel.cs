namespace CineMagic.Web.ViewModels.Genres
{
    using System.ComponentModel.DataAnnotations;

    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    using static CineMagic.Common.ModelValidation.Genre;

    public class GenreEditViewModel : IMapFrom<Genre>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, ErrorMessage = NameErrorMessage, MinimumLength = NameMinLength)]
        public string Name { get; set; }
    }
}
