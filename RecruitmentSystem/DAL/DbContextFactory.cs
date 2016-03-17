using System.Data.Entity.Infrastructure;

namespace RecruitmentSystem.DAL
{
    /// <summary>
    /// Represents an implementation of the factory pattern specified by <see cref="IDbContextFactory{T}"/>.
    /// </summary>
    public class RecruitmentContextFactory : IDbContextFactory<RecruitmentContext>
    {
        /// <summary>
        /// Creates a new instance of <see cref="RecruitmentContext"/>.
        /// </summary>
        /// <returns>The newly instantiated <see cref="RecruitmentContext"/>.</returns>
        public RecruitmentContext Create()
        {
            return new RecruitmentContext();
        }
    }
}