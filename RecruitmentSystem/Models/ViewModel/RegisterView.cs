﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentSystem.Models.ViewModel
{
    /// <summary>
    /// Represents the ViewModel of register view
    /// </summary>
    public class RegisterView
    {
        /// <summary>
        /// GET/SET
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// GET/SET
        /// Name is required.
        /// </summary>

        [Required(ErrorMessageResourceName = "NameRequiredErrorMessage", ErrorMessageResourceType = typeof(Localization.Models.ViewModels.RegisterView))]
        [StringLength(50, MinimumLength = 2,
            ErrorMessageResourceName = "NameLengthErrorMessage", ErrorMessageResourceType = typeof(Localization.Models.ViewModels.RegisterView))]
        [RegularExpression(@"^[\u00c0-\u01ffa-zA-Z'-]{2,}( [\u00c0-\u01ffa-zA-Z'-]{2,})*$",
            ErrorMessageResourceName = "NameRegexErrorMessage", ErrorMessageResourceType = typeof(Localization.Models.ViewModels.RegisterView))]
        [Display(Name = "NameDisplayName", ResourceType = typeof(Localization.Models.ViewModels.RegisterView))]
        public string Name { get; set; }

        /// <summary>
        /// GET/SET
        /// Surname is required.
        /// </summary>
        [Required(ErrorMessageResourceName = "SurnameRequiredErrorMessage", ErrorMessageResourceType = typeof(Localization.Models.ViewModels.RegisterView))]
        [StringLength(50, MinimumLength = 2,
            ErrorMessageResourceName = "SurnameLengthErrorMessage", ErrorMessageResourceType = typeof(Localization.Models.ViewModels.RegisterView))]
        [RegularExpression(@"^[\u00c0-\u01ffa-zA-Z'-]{2,}$",
            ErrorMessageResourceName = "SurnameRegexErrorMessage", ErrorMessageResourceType = typeof(Localization.Models.ViewModels.RegisterView))]
        [Display(Name = "SurnameDisplayName", ResourceType = typeof(Localization.Models.ViewModels.RegisterView))]
        public string Surname { get; set; }

        /// <summary>
        /// GET/SET
        /// </summary>
        [StringLength(13, ErrorMessageResourceName = "SsnLengthErrorMessage", ErrorMessageResourceType = typeof(Localization.Models.ViewModels.RegisterView))]
        [RegularExpression(@"^(19|20)[0-9]{2}(0[1-9]|1[012])(0[1-9]|[12][0-9]|3[01])-[0-9]{4}$",
            ErrorMessageResourceName = "SsnRegexErrorMessage", ErrorMessageResourceType = typeof(Localization.Models.ViewModels.RegisterView))]
        [Display(Name = "SsnDisplayName", ResourceType = typeof(Localization.Models.ViewModels.RegisterView))]
        [Index(IsUnique = true)]
        public string Ssn { get; set; }

        /// <summary>
        /// GET/SET
        /// </summary>
        [EmailAddress(ErrorMessageResourceName = "EmailInvalidErrorMessage", ErrorMessageResourceType = typeof(Localization.Models.ViewModels.RegisterView))]
        [StringLength(100, MinimumLength = 5, 
            ErrorMessageResourceName = "EmailLengthErrorMessage", ErrorMessageResourceType = typeof(Localization.Models.ViewModels.RegisterView))]
        [Display(Name = "EmailDisplayName", ResourceType = typeof(Localization.Models.ViewModels.RegisterView))]
        [Index(IsUnique = true)]
        public string Email { get; set; }

        /// <summary>
        /// GET/SET
        /// Password is required.
        /// </summary>
        [Required(ErrorMessageResourceName = "PasswordRequiredErrorMessage", ErrorMessageResourceType = typeof(Localization.Models.ViewModels.RegisterView))]
        [Display(Name = "PasswordDisplayName", ResourceType = typeof(Localization.Models.ViewModels.RegisterView))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// GET/SET
        /// Password verification is required.
        /// </summary>
        [Required(ErrorMessageResourceName = "VerifyPasswordRequiredErrorMessage", ErrorMessageResourceType = typeof(Localization.Models.ViewModels.RegisterView))]
        [Display(Name = "VerifyPasswordDisplayName", ResourceType = typeof(Localization.Models.ViewModels.RegisterView))]
        [DataType(DataType.Password)]
        public string PasswordVerify { get; set; }

        /// <summary>
        /// GET/SET
        /// Username is required.
        /// </summary>
        [Required(ErrorMessageResourceName = "UsernameRequiredErrorMessage", ErrorMessageResourceType = typeof(Localization.Models.ViewModels.RegisterView))]
        [StringLength(30, MinimumLength = 1,
            ErrorMessageResourceName = "UsernameLengthErrorMessage", ErrorMessageResourceType = typeof(Localization.Models.ViewModels.RegisterView))]
        [Display(Name = "UsernameDisplayName", ResourceType = typeof(Localization.Models.ViewModels.RegisterView))]
        [Index(IsUnique = true)]
        public string Username { get; set; }
    }
}