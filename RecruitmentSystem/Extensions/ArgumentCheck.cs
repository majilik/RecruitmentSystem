using System;

namespace RecruitmentSystem.Extensions
{
    public static class ArgumentCheck
    {
        public static string ThrowIfNullOrWhiteSpace(this string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
                throw new ArgumentException("Argument must not be null, empty or white space.");

            return arg;
        }

        public static int ThrowIfLessThanZero(this int arg)
        {
            if (arg < 0)
                throw new ArgumentException("Argument must be greater than 0.");

            return arg;
        }

        public static int ThrowIfLessThanOne(this int arg)
        {
            if (arg < 1)
                throw new ArgumentException("Argument must be greater than 1.");

            return arg;
        }

        public static uint ThrowIfLessThanOne(this uint arg)
        {
            if (arg < 1)
                throw new ArgumentException("Argument must be greater than 1.");

            return arg;
        }
    }
}