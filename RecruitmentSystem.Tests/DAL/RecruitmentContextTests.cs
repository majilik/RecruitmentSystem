using RecruitmentSystem.Models;
using RecruitmentSystem.DAL;
using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System;
using NUnit.Framework;

namespace RecruitmentSystem.Tests.DAL
{
    [TestFixture]
    public class RecruitmentContextTests
    {
        private RecruitmentContext context;

        [OneTimeSetUp]
        public static void ClassInitialize()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<RecruitmentContext>());
        }

        [SetUp]
        public void TestInitialize()
        {
            context = new RecruitmentContext("TestRecruitmentContext");
        }

        [TearDown]
        public void TestCleanp()
        {
            context.Dispose();
        }

        [Test]
        public void CanInsertValidAvailabilityTest()
        {
            Availability availability = new Availability
            {
                FromDate = DateTime.Now,
                ToDate = DateTime.Now
            };

            context.Entry(availability).State = EntityState.Added;

            Assert.DoesNotThrow(() => context.SaveChanges());
        }

        [Test]
        public void InsertingInvalidAvailabilityThrowsExceptionTest()
        {
            Availability availabilityNullFrom = new Availability
            {
                ToDate = DateTime.Now
            };

            context.Entry(availabilityNullFrom).State = EntityState.Added;

            Assert.Throws<DbEntityValidationException>(() => context.SaveChanges());

            Availability availabilityNullTo = new Availability
            {
                FromDate = DateTime.Now
            };

            context.Entry(availabilityNullFrom).State = EntityState.Detached;
            context.Entry(availabilityNullTo).State = EntityState.Added;

            Assert.Throws<DbEntityValidationException>(() => context.SaveChanges());
        }

        [Test]
        public void CanInsertValidRoleTest()
        {
            Role role = new Role { Name = "test_role" };

            context.Roles.Add(role);

            Assert.DoesNotThrow(() => context.SaveChanges());
            Assert.DoesNotThrow(() => context.Roles.Single(r => r.Name.Equals(role.Name)));
        }

        [Test]
        public void InsertingEmptyRoleThrowsExceptionTest()
        {
            Role role = new Role {};

            context.Entry(role).State = EntityState.Added;

            Assert.Throws<DbEntityValidationException>(() => context.SaveChanges());
        }

        [Test]
        public void CanInsertValidPersonTest()
        {
        }

        [Test]
        public void InsertingEmptyPersonThrowsExceptionTest()
        {
        }

        [Test]
        public void CanInsertValidCompetenceTest()
        {
            Competence competence = new Competence { DefaultName = "test_competence" };

            context.Entry(competence).State = EntityState.Added;

            Assert.DoesNotThrow(() => context.SaveChanges());
        }

        [Test]
        public void InsertingEmptyCompetenceThrowsExceptionTest()
        {
            Competence competence = new Competence { };

            context.Entry(competence).State = EntityState.Added;

            Assert.Throws<DbEntityValidationException>(() => context.SaveChanges());
        }

        [Test]
        public void InsertingNonUniqueCompetenceThrowsExceptionTest()
        {
            Competence competence = new Competence { DefaultName = "test_competence" };
            Competence competenceShallowCopy = new Competence { DefaultName = competence.DefaultName };

            context.Entry(competence).State = EntityState.Added;
            context.Entry(competenceShallowCopy).State = EntityState.Added;

            Assert.Throws<DbUpdateException>(() => context.SaveChanges());
        }
    }
}
