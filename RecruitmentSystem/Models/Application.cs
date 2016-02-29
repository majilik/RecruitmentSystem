using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentSystem.Models
{
    public class Application
    {
        [Index(IsUnique = true)]
        public virtual Person Person { get; set; }
        public long Id { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime ApplicationDate { get; set; }
        public List<CompetenceProfile> CompetenceProfiles { get; set; }
        public List<Availability> Availabilities { get; set; }
        public bool Status { get; set; }
    }
}