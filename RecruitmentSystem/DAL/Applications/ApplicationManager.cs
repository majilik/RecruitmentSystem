using System;
using System.Collections.Generic;
using System.Linq;
using RecruitmentSystem.Models;
using System.Web;

namespace RecruitmentSystem.DAL.Applications
{
    public class ApplicationManager
    {
        public List<Application> FindApplications(Competence competenece)
        {
            List<Application> result = new List<Application>();
            IEnumerable<Application> applications = new QueryService<Application>().GetAll();
            foreach(Application a in applications)
            {
                foreach(CompetenceProfile cp in a.CompetenceProfiles)
                {
                    if (cp.Competence == competenece) result.Add(a);
                }
            }

            return applications.ToList();
        }
    }

}