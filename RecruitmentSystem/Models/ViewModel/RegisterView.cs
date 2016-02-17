using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentSystem.Models.ViewModel
{
    public class RegisterView
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [RegularExpression(@"^[\u00c0-\u01ffa-zA-Z'-]{2,}( [\u00c0-\u01ffa-zA-Z'-]{2,})*$",
            ErrorMessage = "Invalid characters in name!")]
        [StringLength(50, MinimumLength = 2,
            ErrorMessage = "Name must be between 2 and 50 characters!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required.")]
        [RegularExpression(@"^[\u00c0-\u01ffa-zA-Z'-]{2,}$",
            ErrorMessage = "Invalid characters in surname!")]
        [StringLength(50, MinimumLength = 2,
            ErrorMessage = "Surname must be between 2 and 50 characters!")]
        public string Surname { get; set; }

        [RegularExpression(@"^(19|20)[0-9]{2}(0[1-9]|1[012])(0[1-9]|[12][0-9]|3[01])-[0-9]{4}$",
            ErrorMessage = "Please enter a valid Social Security Number on the form YYYYMMDD-XXXX")]
        [StringLength(13, ErrorMessage = "Social Security Number must be 13 characters!")]
        [DisplayName("Social Security Number")]
        [Index(IsUnique = true)]
        public string Ssn { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
            ErrorMessage = "Invalid e-mail!")]
        [StringLength(100, MinimumLength = 5)]
        [Index(IsUnique = true)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [PasswordPropertyText]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password must be verified.")]
        [DisplayName("Verify Password")]
        [PasswordPropertyText]
        public string PasswordVerify { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(30, MinimumLength = 1,
            ErrorMessage = "Please check that the entered username is between 1 and 30 characters long")]
        [Index(IsUnique = true)]
        public string Username { get; set; }
    }
}