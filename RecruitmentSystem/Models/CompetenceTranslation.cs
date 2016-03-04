using RecruitmentSystem.Resources;
using System.ComponentModel.DataAnnotations;

namespace RecruitmentSystem.Models
{
    public class CompetenceTranslation
    {
        public long Id { get; set; }

        [Required]
        public Locales Locale { get; set; }

        [Required]
        public virtual Competence Competence { get; set; }

        [Required]
        public string Translation { get; set; }
    }
}