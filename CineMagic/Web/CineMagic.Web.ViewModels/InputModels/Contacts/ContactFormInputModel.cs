namespace CineMagic.Web.ViewModels.InputModels.Contacts
{
    using System.ComponentModel.DataAnnotations;

    using static CineMagic.Data.Common.DataValidation.ContactForm;

    public class ContactFormInputModel
    {
        [Required(ErrorMessage = "- The Name field is required.")]
        [MinLength(NameMinLength, ErrorMessage = "- Name should be more than 3 characters long!")]
        [MaxLength(NameMaxLength, ErrorMessage = "- Name should be less than 100 characters long!")]
        public string Name { get; set; }

        [Required(ErrorMessage ="- The Email field is required.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "- The Subject field is required.")]
        [MinLength(SubjectMinLength, ErrorMessage = "- Subject should be more than 5 characters long!")]
        [MaxLength(SubjectMaxLength, ErrorMessage = "- Subject should be less than 100 characters long!")]
        public string Subject { get; set; }

        [Required]
        [MinLength(MessageMinLength, ErrorMessage = "Message should be more than 5 characters long!")]
        [MaxLength(MessageMaxLength, ErrorMessage = "Message should be less than 100 characters long!")]
        public string Message { get; set; }
    }
}
