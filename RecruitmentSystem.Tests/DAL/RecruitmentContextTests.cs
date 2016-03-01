using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecruitmentSystem.Models;
using RecruitmentSystem.DAL;
using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System;

namespace RecruitmentSystem.Tests.DAL
{
    [TestClass]
    public class RecruitmentContextTests
    {
        private RecruitmentContext context;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<RecruitmentContext>());
        }

        [TestInitialize]
        public void TestInitialize()
        {
            context = new RecruitmentContext("TestRecruitmentContext");
        }

        [TestCleanup]
        public void TestCleanp()
        {
            context.Dispose();
        }

        public void CanInsertValidAvailabilityTest()
        {
            Availability availability = new Availability
            {
                FromDate = DateTime.Now,
                ToDate = DateTime.Now
            };

            context.Entry(availability).State = EntityState.Added;

            NUnit.Framework.Assert.DoesNotThrow(() => context.SaveChanges());
        }

        public void InsertingInvalidAvailabilityThrowsExceptionTest()
        {
            /*Availability availabilityNullFrom = new Availability
            {
                ToDate = DateTime.Now
            };

            context.Entry(availabilityNullFrom).State = EntityState.Added;

            NUnit.Framework.Assert.Throws<DbEntityValidationException>(() => context.SaveChanges());

            Availability availabilityNullTo = new Availability
            {
                FromDate = DateTime.Now
            };

            context.Entry(availabilityNullFrom).State = EntityState.Detached;
            context.Entry(availabilityNullTo).State = EntityState.Added;

            NUnit.Framework.Assert.Throws<DbEntityValidationException>(() => context.SaveChanges());*/
        }

        [TestMethod]
        public void CanInsertValidRoleTest()
        {
            Role role = new Role { Name = "test_role" };

            context.Roles.Add(role);

            NUnit.Framework.Assert.DoesNotThrow(() => context.SaveChanges());
            NUnit.Framework.Assert.DoesNotThrow(() => context.Roles.Single(r => r.Name.Equals(role.Name)));
        }

        [TestMethod]
        public void InsertingEmptyRoleThrowsExceptionTest()
        {
            Role role = new Role {};

            context.Entry(role).State = EntityState.Added;

            NUnit.Framework.Assert.Throws<DbEntityValidationException>(() => context.SaveChanges());
        }

        [TestMethod]
        public void CanInsertValidPersonTest()
        {
        }

        [TestMethod]
        public void InsertingEmptyPersonThrowsExceptionTest()
        {
        }

        [TestMethod]
        public void CanInsertValidCompetenceTest()
        {
            Competence competence = new Competence { Name = "test_competence" };

            context.Entry(competence).State = EntityState.Added;

            NUnit.Framework.Assert.DoesNotThrow(() => context.SaveChanges());
        }

        [TestMethod]
        public void InsertingEmptyCompetenceThrowsExceptionTest()
        {
            Competence competence = new Competence { };

            context.Entry(competence).State = EntityState.Added;

            NUnit.Framework.Assert.Throws<DbEntityValidationException>(() => context.SaveChanges());
        }

        [TestMethod]
        public void InsertingNonUniqueCompetenceThrowsExceptionTest()
        {
            Competence competence = new Competence { Name = "test_competence" };
            Competence competenceShallowCopy = new Competence { Name = competence.Name };

            context.Entry(competence).State = EntityState.Added;
            context.Entry(competenceShallowCopy).State = EntityState.Added;

            NUnit.Framework.Assert.Throws<DbUpdateException>(() => context.SaveChanges());
        }
    }
}
