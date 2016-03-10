﻿using System;
using System.ComponentModel.DataAnnotations;

namespace RecruitmentSystem.Models
{
    /// <summary>
    /// Represents a time period during which a person is available.
    /// </summary>
    public class Availability
    {
        public long Id { get; set; }
        public virtual Application Application { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}