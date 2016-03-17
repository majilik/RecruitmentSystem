using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentSystem.Models
{
    /// <summary>
    /// Application entity
    /// </summary>
    public class Application
    {
        /// <summary>
        /// GET/SET
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// GET/SET
        /// </summary>
        [Index(IsUnique = true)]
        public virtual Person Person { get; set; }

        /// <summary>
        /// GET/SET
        /// </summary>
        [Column(TypeName = "DateTime2")]
        public DateTime ApplicationDate { get; set; }

        /// <summary>
        /// GET/SET
        /// </summary>
        [Required]
        public ICollection<CompetenceProfile> CompetenceProfiles { get; set; }


        /// <summary>
        /// GET/SET
        /// </summary>
        [Required]
        public ICollection<Availability> Availabilities { get; set; }

        /// <summary>
        /// GET/SET
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// GET/SET
        /// </summary>
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}