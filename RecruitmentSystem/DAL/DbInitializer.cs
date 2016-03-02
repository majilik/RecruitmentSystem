using RecruitmentSystem.Models;
using RecruitmentSystem.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RecruitmentSystem.DAL
{
    /// <summary>
    /// DbInitializer is implicitly an implementation of IDatabaseInitializer
    /// by extending DropCreateDatabaseIfModelChanges<TContext>, where TContext
    /// is RecruitmentContext. This class overrides the Seed method of its
    /// parent and provides the data that should replace the dropped data of
    /// the database specified by RecruitmentContext.
    /// </summary>
    public class DbInitializer : DropCreateDatabaseIfModelChanges<RecruitmentContext>
    {
        protected override void Seed(RecruitmentContext context)
        {
            var roles = new List<Role>
            {
                new Role{ Name = "recruit" },
                new Role{ Name = "applicant" }
            };
            roles.ForEach(r => context.Roles.Add(r));
            context.SaveChanges();

            var persons = new List<Person>
            {
                new Person
                {
                    Name = "Greta",
                    Surname = "Borg",
                    Username = "borg",
                    Password = SecurityManager.HashPassword("w19nk23a"),
                    Role = context.Roles.Where(r => r.Id == 1).Single()
                },
                new Person
                {
                    Name = "Per",
                    Surname = "Strand",
                    Ssn = "19671212-1211",
                    Email ="per@strand.kth.se",
                    Role = context.Roles.Where(r => r.Id == 2).Single()
                }
            };
            persons.ForEach(p => context.Persons.Add(p));
            context.SaveChanges();

            var application = new Application
            {
                ApplicationDate = DateTime.Now,
                Availabilities = new List<Availability>(),
                CompetenceProfiles = new List<CompetenceProfile>(),
                Person = context.Persons.Where(p => p.Id == 2).Single()
            };
            context.Applications.Add(application);
            context.SaveChanges();

            var availability = new List<Availability>
            {
                new Availability
                {
                    Application = context.Applications.Where(a => a.Id == 1).Single(),
                    FromDate = DateTime.Parse("2014-02-23"),
                    ToDate = DateTime.Parse("2014-05-25")
                },
                new Availability
                {
                    Application = context.Applications.Where(a => a.Id == 1).Single(),
                    FromDate = DateTime.Parse("2014-07-10"),
                    ToDate = DateTime.Parse("2014-08-10")
                }
            };
            availability.ForEach(a => context.Availabilites.Add(a));
            context.SaveChanges();
            
            var competence = new List<Competence>
            {
                new Competence{ DefaultName = "Korvgrillning" },
                new Competence{ DefaultName = "Karuselldrift" }
            };
            competence.ForEach(c => context.Competences.Add(c));
            context.SaveChanges();

            var competence1Translations = new List<CompetenceTranslation>
            {
                new CompetenceTranslation {CompetenceRefId = 1, Competence = competence[0], Locale = Resources.Locales.EN_US, Translation = "Sausage BBQ:ing" },
                new CompetenceTranslation {CompetenceRefId = 1, Locale = Resources.Locales.SV_SE, Translation = "Korvgrillning" }
            };
            var competence2Translations = new List<CompetenceTranslation>
            {
                new CompetenceTranslation {CompetenceRefId = 2, Competence = competence[1], Locale = Resources.Locales.EN_US, Translation = "Carousel Operation" },
                new CompetenceTranslation {CompetenceRefId = 2, Competence = competence[1], Locale = Resources.Locales.SV_SE, Translation = "Karuselldrift" },
            };
            competence1Translations.ForEach(a => context.CompetenceTranslations.Add(a));
            competence1Translations.ForEach(a => competence[0].Translations.Add(a));
            competence2Translations.ForEach(a => context.CompetenceTranslations.Add(a));
            competence2Translations.ForEach(a => competence[1].Translations.Add(a));
            context.SaveChanges();

            var competenceProfile = new List<CompetenceProfile>
            {
                new CompetenceProfile
                {
                    Application = context.Applications.Where(a => a.Id == 1).Single(),
                    Competence = context.Competences.Where(c => c.Id == 1).Single(),
                    YearsOfExperience = 3.5M
                },
                new CompetenceProfile
                {
                    Application = context.Applications.Where(a => a.Id == 1).Single(),
                    Competence = context.Competences.Where(c => c.Id == 2).Single(),
                    YearsOfExperience = 2.0M
                }
            };
            competenceProfile.ForEach(cp => context.CompetenceProfiles.Add(cp));
            context.SaveChanges();
        }
    }
}