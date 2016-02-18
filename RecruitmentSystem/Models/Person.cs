﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RecruitmentSystem.Models
{
    /// <summary>
    /// Represents a registered user for this service.
    /// </summary>
    public class Person
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Ssn { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        //[DefaultValue()]
        public virtual Role Role { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}