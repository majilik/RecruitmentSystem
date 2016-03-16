using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Infrastructure.Annotations;

namespace RecruitmentSystem.Models.Configuration
{
    public class ApplicationConfiguration : EntityTypeConfiguration<Application>
    {
        public ApplicationConfiguration()
        {
            HasKey(a => a.Id);
            HasMany(a => a.Availabilities)
                .WithRequired()
                .WillCascadeOnDelete(true);
            HasMany(a => a.CompetenceProfiles)
                .WithRequired()
                .WillCascadeOnDelete(true);
            HasRequired(a => a.Person).WithRequiredDependent().Map(m => m.MapKey("PersonId")).WillCascadeOnDelete(true);

            Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
            Property(a => a.ApplicationDate).IsRequired();
            Property(a => a.Person.Id)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute() { IsUnique = true}))
                .IsRequired();
            Property(a => a.Status).IsRequired();
            Property(a => a.Timestamp).IsConcurrencyToken();

            ToTable("Applications");
        }
    }
}