using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentSystem.Models
{
    /// <summary>
    /// Represents a role which gives certain privileges for this service.
    /// </summary>
    public class Role
    {
        /// <summary>
        /// GET/SET
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// GET/SET
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "")]
        [Index(IsUnique = true)]
        public string Name { get; set; }
        /// <summary>
        /// GET/SET
        /// </summary>
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}