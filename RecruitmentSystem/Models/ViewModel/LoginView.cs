
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace RecruitmentSystem.Models.ViewModel
{
    public class LoginView
    {
        [Required(ErrorMessage = "Please enter username.")]
        public string Username{get; set;}

        [Required(ErrorMessage = "Please enter password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}