namespace CineMagic.Web.ViewModels.Languages
{
    using System.ComponentModel.DataAnnotations;

    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    using static CineMagic.Common.ModelValidation.Language;

    public class LanguageEditViewModel : IMapFrom<Language>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, ErrorMessage = NameErrorMessage, MinimumLength = NameMinLength)]
        public string Name { get; set; }
    }
}
