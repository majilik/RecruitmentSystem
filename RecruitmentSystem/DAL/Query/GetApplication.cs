using RecruitmentSystem.Models;
using System.Linq;

namespace RecruitmentSystem.DAL.Query
{
    public class GetApplication
    {
        public static Application Invoke(int? id)
        {
            Application application;

            using (RecruitmentContext context = new RecruitmentContext())
            {
                application = context.Applications.Single(a => a.Id == id);
            }

            return application;
        }
    }
}