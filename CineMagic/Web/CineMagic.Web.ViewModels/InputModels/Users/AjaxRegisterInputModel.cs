namespace CineMagic.Web.ViewModels.InputModels.Users
{
    using System.ComponentModel.DataAnnotations;

    using static CineMagic.Common.ModelValidation.User;

    public class AjaxRegisterInputModel
    {
        [Required]
        [StringLength(UsernameMaxLength, ErrorMessage = UsernameErrorMessage, MinimumLength = UsernameMinLength)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
