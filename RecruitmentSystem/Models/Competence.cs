using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentSystem.Models
{
    /// <summary>
    /// Represents a competence area.
    /// </summary>
    public class Competence
    {
        public long Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "")]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}