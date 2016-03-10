using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace RecruitmentSystem.Models.Configuration
{
    public class CompetenceConfiguration : EntityTypeConfiguration<Competence>
    {
        public CompetenceConfiguration()
        {
            HasKey(c => c.Id);
            HasMany(c => c.Translations)
                .WithRequired()
                .WillCascadeOnDelete(true);

            Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
            Property(c => c.DefaultName)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute() { IsUnique = true }))
                .IsRequired();
            Property(c => c.Timestamp).IsConcurrencyToken();

            Ignore(c => c.LocalizedName);

            ToTable("Competences");
        }
    }
}