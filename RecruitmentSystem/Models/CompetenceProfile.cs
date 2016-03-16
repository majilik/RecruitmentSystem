using System.ComponentModel.DataAnnotations;

namespace RecruitmentSystem.Models
{
    /// <summary>
    /// Represents a persons amount of experience for a specified competence area.
    /// </summary>
    public class CompetenceProfile
    {
        /// <summary>
        /// GET/SET
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// GET/SET
        /// </summary>
        public virtual Application Application { get; set; }
        /// <summary>
        /// GET/SET
        /// </summary>
        public virtual Competence Competence { get; set; }
        /// <summary>
        /// GET/SET
        /// </summary>
        public decimal YearsOfExperience { get; set; }
        /// <summary>
        /// GET/SET
        /// </summary>
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}