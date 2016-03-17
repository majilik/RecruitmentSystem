using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentSystem.Models
{
    /// <summary>
    /// Represents a registered user for this service.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// GET/SET
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// GET/SET
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// GET/SET
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// GET/SET
        /// </summary>
        public string Ssn { get; set; }
        /// <summary>
        /// GET/SET
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// GET/SET
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// GET/SET
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// GET/SET
        /// </summary>
        public virtual Role Role { get; set; }
        /// <summary>
        /// GET/SET
        /// </summary>
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}