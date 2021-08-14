namespace CineMagic.Web.ViewModels.Privacies
{
    using System.ComponentModel.DataAnnotations;

    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    using static CineMagic.Common.ModelValidation.Privacy;

    public class PrivacyEditViewModel : IMapFrom<Privacy>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(ContentMaxLength, ErrorMessage = ContentErrorMessage, MinimumLength = ContentMinLength)]
        public string Content { get; set; }
    }
}
