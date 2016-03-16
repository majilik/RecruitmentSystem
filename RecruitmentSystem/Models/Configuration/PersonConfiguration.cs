using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace RecruitmentSystem.Models.Configuration
{
    public class PersonConfiguration : EntityTypeConfiguration<Person>
    {
        public PersonConfiguration()
        {
            HasKey(p => p.Id);
            HasRequired(p => p.Role).WithRequiredDependent().WillCascadeOnDelete(true);

            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
            Property(p => p.Name);
            Property(p => p.Surname);
            Property(p => p.Ssn);
            Property(p => p.Password);
            Property(p => p.Username)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute() { IsUnique = true }))
                .IsRequired();
            Property(p => p.Timestamp).IsConcurrencyToken();

            ToTable("Persons");
        }
    }
}