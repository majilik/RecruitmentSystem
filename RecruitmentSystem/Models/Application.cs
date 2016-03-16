using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentSystem.Models
{
    public class Application
    {
        public long Id { get; set; }

        [Index(IsUnique = true)]
        public virtual Person Person { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime ApplicationDate { get; set; }

        [Required]
        public ICollection<CompetenceProfile> CompetenceProfiles { get; set; }

        [Required]
        public ICollection<Availability> Availabilities { get; set; }

        public bool Status { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}