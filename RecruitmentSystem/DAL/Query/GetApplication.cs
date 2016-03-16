using RecruitmentSystem.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace RecruitmentSystem.DAL.Query
{
    internal class GetApplication
    {
        private static IDbContextFactory<RecruitmentContext> _contextFactory = new RecruitmentContextFactory();

        internal static Application Invoke(int id)
        {
            Application application;

            using (RecruitmentContext context = _contextFactory.Create())
            {
                application = context.Applications
                    .Where(a => a.Id == id)
                    .Include(a => a.Availabilities)
                    .Include(a => a.CompetenceProfiles.Select(c => c.Competence))
                    .Include(a => a.Person)
                    .AsNoTracking()
                    .Single();
            }

            return application;
        }
    }
}