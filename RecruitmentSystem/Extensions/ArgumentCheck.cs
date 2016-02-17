using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentSystem.Extensions
{
    public static class ArgumentCheck
    {
        public static string ThrowIfNullOrWhiteSpace(this String arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
                throw new ArgumentException("Argument must not be null, empty or white space.");

            return arg;
        }
    }
}