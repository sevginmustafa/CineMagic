namespace CineMagic.Web.ViewModels.InputModels.Administration
{
    using System.ComponentModel.DataAnnotations;

    using static CineMagic.Common.ModelValidation.Privacy;

    public class PrivacyCreateInputModel
    {
        [Required]
        [StringLength(ContentMaxLength, ErrorMessage = ContentErrorMessage, MinimumLength = ContentMinLength)]
        public string Content { get; set; }
    }
}
