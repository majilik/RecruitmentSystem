
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace RecruitmentSystem.Models.ViewModel
{
    public class LoginView
    {
        [Required(ErrorMessage = "Please enter username.")]
        public string Username{get; set;}

        [Required(ErrorMessage = "Please enter password.")]
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}