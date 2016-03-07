using RecruitmentSystem.Models;
using System.Data.Entity;
using System.Linq;

namespace RecruitmentSystem.DAL.Query
{
    public class GetApplication
    {
        public static Application Invoke(int id)
        {
            Application application;

            using (RecruitmentContext context = new RecruitmentContext())
            {
                application = context.Applications
                    .Where(a => a.Id == id)
                    .Include(a => a.Availabilities)
                    .Include(a => a.CompetenceProfiles.Select(c => c.Competence))
                    .Include(a => a.Person)
                    .Single();
            }

            return application;
        }
    }
}