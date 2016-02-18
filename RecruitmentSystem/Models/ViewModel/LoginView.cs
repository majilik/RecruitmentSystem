
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

/// <summary>
/// Represents the ViewModel of login
/// </summary>
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