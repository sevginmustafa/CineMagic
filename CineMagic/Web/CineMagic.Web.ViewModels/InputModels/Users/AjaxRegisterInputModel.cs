namespace CineMagic.Web.ViewModels.InputModels.Users
{
    using System.ComponentModel.DataAnnotations;

    using static CineMagic.Data.Common.DataValidation.User;

    public class AjaxRegisterInputModel
    {
        [Required]
        [MinLength(UsernameMinLength, ErrorMessage = "Username should be more than 3 characters long!")]
        [MaxLength(UsernameMaxLength, ErrorMessage = "Username should be less than 30 characters long!")]
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
