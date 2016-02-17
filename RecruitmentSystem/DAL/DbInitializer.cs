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
                {   Name = "Greta",
                    Surname = "Borg",
                    Username = "borg",
                    Password = SecurityManager.HashPassword("w19nk23a"),
                    Role = context.Roles.Where(r => r.Id == 1).Single()
                },
                new Person
                {   Name = "Per",
                    Surname = "Strand",
                    Ssn = "19671212-1211",
                    Email ="per@strand.kth.se",
                    Role = context.Roles.Where(r => r.Id == 2).Single()
                }
            };
            persons.ForEach(p => context.Persons.Add(p));
            context.SaveChanges();

            var availability = new List<Availability>
            {
                new Availability
                {   Person = context.Persons.Where(p => p.Id == 2).Single(),
                    FromDate = DateTime.Parse("2014-02-23"),
                    ToDate = DateTime.Parse("2014-05-25")
                },
                new Availability
                {   Person = context.Persons.Where(p => p.Id == 2).Single(),
                    FromDate = DateTime.Parse("2014-07-10"),
                    ToDate = DateTime.Parse("2014-08-10")
                }
            };
            availability.ForEach(a => context.Availabilites.Add(a));
            context.SaveChanges();

            var competence = new List<Competence>
            {
                new Competence{ Name = "Korvgrillning" },
                new Competence{ Name = "Karuselldrift" }
            };
            competence.ForEach(c => context.Competences.Add(c));
            context.SaveChanges();

            var competenceProfile = new List<CompetenceProfile>
            {
                new CompetenceProfile
                {   Person = context.Persons.Where(p => p.Id == 2).Single(),
                    Competence = context.Competences.Where(c => c.Id == 1).Single(),
                    YearsOfExperience = 3.5M
                },
                new CompetenceProfile
                {   Person=context.Persons.Where(p => p.Id == 2).Single(),
                    Competence = context.Competences.Where(c => c.Id == 2).Single(),
                    YearsOfExperience = 2.0M
                }
            };
            competenceProfile.ForEach(cp => context.CompetenceProfiles.Add(cp));
            context.SaveChanges();
        }
    }
}