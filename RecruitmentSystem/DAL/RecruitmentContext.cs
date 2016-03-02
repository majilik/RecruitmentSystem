using RecruitmentSystem.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace RecruitmentSystem.DAL
{
    /// <summary>
    /// A RecritmentContext is responsible for managing entity objects, also
    /// known as Common Language Runtime (CLR) objects, during run time. This
    /// includes populating objects with data from a database
    /// (materializing data), change tracking, concurrency handling,
    /// propagating object changes back to the database and binding objects to
    /// controls.
    /// 
    /// If this context is used in a Web application, a separate instance
    /// should be used per request. Dispose of the context instance when
    /// it is no longer needed to avoid wasting resources. The preferred way
    /// should be the following:
    /// Use using if you want all the resources that the context controls to be
    /// disposed at the end of the block. When you use using, the compiler
    /// automatically creates a try/finally block and calls dispose in the
    /// finally block.
    /// using (var context = new RecruitmentContext())
    /// {
    ///     Perform data access using the context
    /// }
    /// </summary>
    public class RecruitmentContext : DbContext
    {
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<CompetenceProfile> CompetenceProfiles { get; set; }
        public virtual DbSet<Competence> Competences { get; set; }
        public virtual DbSet<Availability> Availabilites { get; set; }
        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<CompetenceTranslation> CompetenceTranslations { get; set; }

        /// <summary>
        /// Takes the connection string defined in web.config and instantiates
        /// a RecruitmentContext. This is less flexible since we can only
        /// define one data source at a time, however it is much easier to
        /// change the data source at a later point without having to refactor
        /// the code dependent on RecruitmentContext. If flexibility is
        /// crucial, simply define an interface IRecruitmentContext and have
        /// the inheriting classes define different data sources.
        /// </summary>
        public RecruitmentContext() : base("RecruitmentContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}