using System;
using System.Collections.Generic;
using System.Linq;
using RecruitmentSystem.Models;
using System.Web;

namespace RecruitmentSystem.DAL.Applications
{
    public class ApplicationManager
    {
        public List<Application> FindApplications()
        {
            List<Application> result = new List<Application>();
            IEnumerable<Application> applications = new QueryService<Application>().GetAll((a => a.Availabilities), (a => a.Person), (a => a.CompetenceProfiles));
            foreach(Application a in applications)
            {
                result.Add(a);
   
            }

            return applications.ToList();
        }
    }

}