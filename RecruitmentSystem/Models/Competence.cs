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
        public long Id { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

        [NotMapped]
        public string LocalizedName
        {
            get
            {
                QueryService<CompetenceTranslation> queryTranslations = new QueryService<CompetenceTranslation>();
                Locales currentLocale = LocalesExtension.LocalesFromString(Thread.CurrentThread.CurrentUICulture.Name);
                return queryTranslations.GetSingle(t => t.Locale == currentLocale && t.CompetenceRefId == Id).Translation ?? DefaultName;
            }
        }

        public string DefaultName { get; set; }
        
        public virtual ICollection<CompetenceTranslation> Translations { get; set; }
    }

    public class CompetenceTranslation
    {
        [Key, Column(Order = 0), ForeignKey("Competence")]
        public long CompetenceRefId { get; set; }

        [Key, Column(Order = 1)]
        public Locales Locale { get; set; }

        [Required]
        public virtual Competence Competence { get; set; }

        [Required]
        public string Translation { get; set; }
    }
}