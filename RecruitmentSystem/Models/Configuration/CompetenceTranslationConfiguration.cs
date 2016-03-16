using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RecruitmentSystem.Models.Configuration
{
    public class CompetenceTranslationConfiguration : EntityTypeConfiguration<CompetenceTranslation>
    {
        public CompetenceTranslationConfiguration()
        {
            HasKey(ct => ct.Id);
            HasRequired(ct => ct.Competence);

            Property(ct => ct.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
            Property(ct => ct.Locale).IsRequired();
            Property(ct => ct.Translation).IsRequired();
            Property(ct => ct.Timestamp).IsConcurrencyToken();

            ToTable("CompetenceTranslations");
        }
    }
}