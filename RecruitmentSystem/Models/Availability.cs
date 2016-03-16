using System;
using System.ComponentModel.DataAnnotations;

namespace RecruitmentSystem.Models
{
    /// <summary>
    /// Represents a time period during which a person is available.
    /// </summary>
    public class Availability
    {
        /// <summary>
        /// GET/SET
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// GET/SET
        /// </summary>
        public virtual Application Application { get; set; }
        /// <summary>
        /// GET/SET
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }
        /// <summary>
        /// GET/SET
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }
        /// <summary>
        /// GET/SET
        /// </summary>
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}