using RecruitmentSystem.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;

namespace RecruitmentSystem.Models
{
    /// <summary>
    /// Represents a competence area.
    /// </summary>
    public class Competence
    {
        public long Id { get; set; }
        
        public string Name { get
            {
                switch(LocalesExtension.LocalesFromString(Thread.CurrentThread.CurrentUICulture.Name))
                {
                    case Locales.SV_SE:
                        return SwedishName;
                    case Locales.EN_US:
                    default:
                        return EnglishName;
                }
            }
        }

        public string SwedishName { get; set; }

        public string EnglishName { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}