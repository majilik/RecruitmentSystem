using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentSystem.Models
{
    public class Competence
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}