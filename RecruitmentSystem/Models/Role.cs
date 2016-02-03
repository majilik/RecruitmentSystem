using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentSystem.Models
{
    /// <summary>
    /// This class represents a table in a database.
    /// It is not dependent on any other entities.
    /// The basic CRUD operations are supported via the class RoleContext.
    /// </summary>
    public class Role
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}