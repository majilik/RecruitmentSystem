using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RecruitmentSystem.Models
{
    public class Person
    {
        /*public Person()
        {
            this.Competencies = new HashSet<Competence>();
        }*/

        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Ssn { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public virtual Role Role { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }

        //public virtual ICollection<Competence> Competencies { get; set; }
    }
}