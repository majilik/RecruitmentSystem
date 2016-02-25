using System.ComponentModel.DataAnnotations;

/// <summary>
/// Represents the ViewModel of login.
/// </summary>
namespace RecruitmentSystem.Models.ViewModel
{
    public class LoginView
    {
        [Required(ErrorMessageResourceName = "UsernameRequiredErrorMessage", 
            ErrorMessageResourceType = typeof(Localization.Models.ViewModels.LoginView))]
        [Display(Name = "UsernameDisplayName", ResourceType = typeof(Localization.Models.ViewModels.LoginView))]
        public string Username { get; set; }

        [Required(ErrorMessageResourceName = "PasswordRequiredErrorMessage", 
            ErrorMessageResourceType = typeof(Localization.Models.ViewModels.LoginView))]
        [Display(Name = "PasswordDisplayName", ResourceType = typeof(Localization.Models.ViewModels.LoginView))]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}