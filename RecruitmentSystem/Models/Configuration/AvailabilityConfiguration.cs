using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RecruitmentSystem.Models.Configuration
{
    public class AvailabilityConfiguration : EntityTypeConfiguration<Availability>
    {
        public AvailabilityConfiguration()
        {
            HasKey(a => a.Id);
            HasRequired(a => a.Application);

            Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
            Property(a => a.FromDate).IsRequired();
            Property(a => a.ToDate).IsRequired();
            Property(a => a.Timestamp).IsConcurrencyToken();

            ToTable("Availabilities");
        }
    }
}