using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RecruitmentSystem.Models.Configuration
{
    public class CompetenceProfileConfiguration : EntityTypeConfiguration<CompetenceProfile>
    {
        public CompetenceProfileConfiguration()
        {
            HasKey(cp => cp.Id);
            HasRequired(cp => cp.Application);
            HasRequired(cp => cp.Competence);

            Property(cp => cp.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
            Property(cp => cp.YearsOfExperience).IsRequired();
            Property(cp => cp.Timestamp).IsConcurrencyToken();

            ToTable("CompetenceProfiles");
        }
    }
}