namespace CineMagic.Web.ViewModels.InputModels.Administration
{
    using System.ComponentModel.DataAnnotations;

    using static CineMagic.Common.ModelValidation.ContactForm;

    public class AdminContactFormInputModel
    {
        [Required]
        [StringLength(NameMaxLength, ErrorMessage = NameErrorMessage, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string To { get; set; }

        [Required]
        [StringLength(SubjectMaxLength, ErrorMessage = SubjectErrorMessage, MinimumLength = SubjectMinLength)]
        public string Subject { get; set; }

        [Required]
        [MaxLength(MessageMaxLength, ErrorMessage = MessageErrorMessage)]
        public string Message { get; set; }
    }
}
