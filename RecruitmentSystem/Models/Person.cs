using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentSystem.Models
{
    public class Person
    {
        public long Id { get; set; }

        [Required]
        [RegularExpression(@"^[\u00c0-\u01ffa-zA-Z'-]{2,}( [\u00c0-\u01ffa-zA-Z'-]{2,})*$",
            ErrorMessage = "")]
        [StringLength(50, MinimumLength = 2,
            ErrorMessage = "")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[\u00c0-\u01ffa-zA-Z'-]{2,}$",
            ErrorMessage = "")]
        [StringLength(50, MinimumLength = 2,
            ErrorMessage = "")]
        public string Surname { get; set; }

        [RegularExpression(@"^(19|20)[0-9]{2}(0[1-9]|1[012])(0[1-9]|[12][0-9]|3[01])-[0-9]{4}$",
            ErrorMessage = "Please enter a valid Social Security Number on the form YYYYMMDD-XXXX")]
        [StringLength(13, ErrorMessage = "")]
        [DisplayName("Social Security Number")]
        [Index(IsUnique = true)]
        public string Ssn { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
            ErrorMessage = "")]
        [StringLength(100, MinimumLength = 5)]
        [Index(IsUnique = true)]
        public string Email { get; set; }

        public string Password { get; set; }

        [StringLength(30, MinimumLength = 1,
            ErrorMessage = "Please check that the entered username is between 1 and 30 characters long")]
        [Index(IsUnique = true)]
        public string Username { get; set; }

        public virtual Role Role { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}