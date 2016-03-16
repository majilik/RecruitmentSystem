using RecruitmentSystem.Resources;
using System.ComponentModel.DataAnnotations;

namespace RecruitmentSystem.Models
{
    /// <summary>
    /// Competence translation enitity
    /// </summary>
    public class CompetenceTranslation
    {
        /// <summary>
        /// GET/SET
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// GET/SET
        /// </summary>
        [Required]
        public Locales Locale { get; set; }
        /// <summary>
        /// GET/SET
        /// </summary>
        [Required]
        public virtual Competence Competence { get; set; }
        /// <summary>
        /// GET/SET
        /// </summary>
        [Required]
        public string Translation { get; set; }
        /// <summary>
        /// GET/SET
        /// </summary>
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}