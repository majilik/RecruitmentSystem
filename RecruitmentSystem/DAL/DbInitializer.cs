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
                new Role { Name = "recruit" },
                new Role { Name = "applicant" }
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
                    Role = context.Roles.Single(r => r.Id == 1)
                },
                new Person
                {
                    Name = "Per",
                    Surname = "Strand",
                    Ssn = "19671212-1211",
                    Email ="per@strand.kth.se",
                    Role = context.Roles.Single(r => r.Id == 2)
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
                    Application = context.Applications.Single(a => a.Id == 1),
                    FromDate = DateTime.Parse("2014-02-23"),
                    ToDate = DateTime.Parse("2014-05-25")
                },
                new Availability
                {
                    Application = context.Applications.Single(a => a.Id == 1),
                    FromDate = DateTime.Parse("2014-07-10"),
                    ToDate = DateTime.Parse("2014-08-10")
                }
            };
            availability.ForEach(a => context.Availabilites.Add(a));
            context.SaveChanges();
            
            var competence = new List<Competence>
            {
                new Competence
                {
                    DefaultName = "Sausage BBQ:ing",
                    Translations = new List<CompetenceTranslation>
                    {
                        new CompetenceTranslation
                        {
                            Locale = Resources.Locales.EN_US,
                            Translation = "Sausage BBQ:ing"
                        },
                        new CompetenceTranslation
                        {
                            Locale = Resources.Locales.SV_SE,
                            Translation = "Korvgrillning"
                        }
                    }
                },
                new Competence
                {
                    DefaultName = "Carousel Operation",
                    Translations = new List<CompetenceTranslation>
                    {
                        new CompetenceTranslation
                        {
                            Locale = Resources.Locales.EN_US,
                            Translation = "Carousel Operation"
                        },
                        new CompetenceTranslation
                        {
                            Locale = Resources.Locales.SV_SE,
                            Translation = "Karuselldrift"
                        }
                    }
                }
            };
            competence.ForEach(c => context.Competences.Add(c));
            context.SaveChanges();

            var competenceProfile = new List<CompetenceProfile>
            {
                new CompetenceProfile
                {
                    Application = context.Applications.Single(a => a.Id == 1),
                    Competence = context.Competences.Single(c => c.Id == 1),
                    YearsOfExperience = 3.5M
                },
                new CompetenceProfile
                {
                    Application = context.Applications.Single(a => a.Id == 1),
                    Competence = context.Competences.Single(c => c.Id == 2),
                    YearsOfExperience = 2.0M
                }
            };
            competenceProfile.ForEach(cp => context.CompetenceProfiles.Add(cp));
            context.SaveChanges();
        }
    }
}