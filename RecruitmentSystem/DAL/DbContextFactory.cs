using System.Data.Entity.Infrastructure;

namespace RecruitmentSystem.DAL
{
    public class RecruitmentContextFactory : IDbContextFactory<RecruitmentContext>
    {
        public RecruitmentContext Create()
        {
            return new RecruitmentContext();
        }
    }
}