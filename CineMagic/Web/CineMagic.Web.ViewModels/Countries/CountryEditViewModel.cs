namespace CineMagic.Web.ViewModels.Countries
{
    using System.ComponentModel.DataAnnotations;

    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    using static CineMagic.Common.ModelValidation.Country;

    public class CountryEditViewModel : IMapFrom<Country>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, ErrorMessage = NameErrorMessage, MinimumLength = NameMinLength)]
        public string Name { get; set; }
    }
}
