using System.ComponentModel.DataAnnotations;

/// <summary>
/// Represents the ViewModel of login.
/// </summary>
namespace RecruitmentSystem.Models.ViewModel
{
    /// <summary>
    /// Login view model
    /// </summary>
    public class LoginView
    {
        /// <summary>
        /// GET/SET. 
        /// Username is required.
        /// </summary>
        [Required(ErrorMessageResourceName = "UsernameRequiredErrorMessage", 
            ErrorMessageResourceType = typeof(Localization.Models.ViewModels.LoginView))]
        [Display(Name = "UsernameDisplayName", ResourceType = typeof(Localization.Models.ViewModels.LoginView))]
        public string Username { get; set; }

        /// <summary>
        /// GET/SET
        /// Password i required.
        /// </summary>
        [Required(ErrorMessageResourceName = "PasswordRequiredErrorMessage", 
            ErrorMessageResourceType = typeof(Localization.Models.ViewModels.LoginView))]
        [Display(Name = "PasswordDisplayName", ResourceType = typeof(Localization.Models.ViewModels.LoginView))]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}