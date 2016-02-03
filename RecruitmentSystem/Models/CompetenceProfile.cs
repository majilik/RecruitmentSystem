using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RecruitmentSystem.Models
{
    public class CompetenceProfile
    {
        public long Id { get; set; }
        public virtual Person Person { get; set; }
        public virtual Competence Competence { get; set; }
        public decimal YearsOfExperience { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}