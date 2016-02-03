using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RecruitmentSystem.Models
{
    public class Availability
    {
        public long Id { get; set; }
        public virtual Person Person { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}