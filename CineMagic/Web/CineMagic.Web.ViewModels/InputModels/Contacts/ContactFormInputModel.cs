namespace CineMagic.Web.ViewModels.InputModels.Contacts
{
    using System.ComponentModel.DataAnnotations;

    using static CineMagic.Common.ModelValidation.ContactForm;

    public class ContactFormInputModel
    {
        [Required(ErrorMessage = "- The Name field is required.")]
        [StringLength(NameMaxLength, ErrorMessage = NameErrorMessage, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Required(ErrorMessage ="- The Email field is required.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "- The Subject field is required.")]
        [StringLength(SubjectMaxLength, ErrorMessage = SubjectErrorMessage, MinimumLength = SubjectMinLength)]
        public string Subject { get; set; }

        [Required]
        [StringLength(MessageMaxLength, ErrorMessage = MessageErrorMessage, MinimumLength = MessageMinLength)]
        public string Message { get; set; }
    }
}
