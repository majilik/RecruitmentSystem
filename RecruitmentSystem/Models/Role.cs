using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentSystem.Models
{
    /// <summary>
    /// This class represents a table in a database.
    /// It is not dependent on any other entities.
    /// The basic CRUD operations are supported via the class
    /// RecruitmentContext.
    /// </summary>
    public class Role
    {
        public long Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1,
            ErrorMessage = "")]
        [Index(IsUnique = true)]
        public string Name { get; set; }
    }
}