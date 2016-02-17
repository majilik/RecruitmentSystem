using System.ComponentModel.DataAnnotations;

namespace RecruitmentSystem.Models
{
    /// <summary>
    /// Represents a persons amount of experience for a specified competence area.
    /// </summary>
    public class CompetenceProfile
    {
        public long Id { get; set; }
        public virtual Person Person { get; set; }
        public virtual Competence Competence { get; set; }
        public decimal YearsOfExperience { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}