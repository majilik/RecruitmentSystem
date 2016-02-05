using RecruitmentSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace RecruitmentSystem.DAL
{
    public class RecruitmentContext : DbContext
    {

        public DbSet<Role> Roles { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<CompetenceProfile> CompetenceProfiles { get; set; }
        public DbSet<Competence> Competences { get; set; }
        public DbSet<Availability> Availabilites { get; set; }

        public RecruitmentContext() : base("RecruitmentContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}