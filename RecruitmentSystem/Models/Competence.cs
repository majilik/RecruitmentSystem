using RecruitmentSystem.DAL;
using RecruitmentSystem.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace RecruitmentSystem.Models
{
    /// <summary>
    /// Represents a competence area.
    /// </summary>
    public class Competence
    {
        /// <summary>
        /// GET/SET
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// GET/SET
        /// Must be unique.
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "")]
        [Index(IsUnique = true)]
        public string DefaultName { get; set; }

        /// <summary>
        /// GET/SET
        /// </summary>
        [Timestamp]
        public byte[] Timestamp { get; set; }

        /// <summary>
        /// Localization of the competence name.
        /// </summary>
        [NotMapped]
        public string LocalizedName
        {
            get
            {
                Locales currentLocale = LocalesExtension.LocalesFromString(Thread.CurrentThread.CurrentUICulture.Name);
                return new QueryService<CompetenceTranslation>().GetSingle(
                    t => t.Locale == currentLocale && t.Competence.Id == Id,
                    t => t.Competence).Translation ?? DefaultName;
            }
        }

        /// <summary>
        /// GET/SET
        /// </summary>
        public virtual ICollection<CompetenceTranslation> Translations { get; set; }
    }
}