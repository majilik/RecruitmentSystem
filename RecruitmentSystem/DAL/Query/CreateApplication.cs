using RecruitmentSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace RecruitmentSystem.DAL.Query
{
    internal class CreateApplication
    {
        private static IDbContextFactory<RecruitmentContext> _contextFactory = new RecruitmentContextFactory();

        internal static void Invoke(string username, Dictionary<int, decimal> selectedCompetences,
            Dictionary<DateTime, DateTime> selectedAvailabilities)
        {
            using (RecruitmentContext context = _contextFactory.Create())
            {
                IList<Competence> competences = context.Competences.ToList();
                Person applicant = context.Persons.Single(p => p.Username == username);

                Application application =
                    new Application
                    {
                        ApplicationDate = DateTime.Now,
                        Availabilities = selectedAvailabilities
                            .Aggregate(new List<Availability>(), (accumulator, entry) =>
                            {
                                accumulator.Add(new Availability { FromDate = entry.Key, ToDate = entry.Value });
                                return accumulator;
                            }),
                        CompetenceProfiles = selectedCompetences
                            .Aggregate(new List<CompetenceProfile>(), (accumulator, entry) =>
                            {
                                accumulator.Add(new CompetenceProfile
                                {
                                    Competence = competences.Single(c => c.Id == entry.Key),
                                    YearsOfExperience = entry.Value
                                }
                                    );
                                return accumulator;
                            }),
                        Person = applicant
                    };

                context.Applications.Add(application);
                context.SaveChanges();
            }
        }
    }
}