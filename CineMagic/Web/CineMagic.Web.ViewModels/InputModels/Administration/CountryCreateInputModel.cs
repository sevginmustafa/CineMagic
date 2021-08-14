namespace CineMagic.Web.ViewModels.InputModels.Administration
{
    using System.ComponentModel.DataAnnotations;

    using static CineMagic.Common.ModelValidation.Country;

    public class CountryCreateInputModel
    {
        [Required]
        [StringLength(NameMaxLength, ErrorMessage = NameErrorMessage, MinimumLength = NameMinLength)]
        public string Name { get; set; }
    }
}
