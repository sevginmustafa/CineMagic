namespace CineMagic.Web.ViewModels.InputModels.Users
{
    using System.ComponentModel.DataAnnotations;

    public class AjaxLoginInputModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
