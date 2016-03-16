using System;

namespace RecruitmentSystem.Extensions
{
    /// <summary>
    /// Represents a set of extensions for validating input parameters.
    /// </summary>
    public static class ArgumentCheck
    {
        /// <summary>
        /// Validates that the argument is not null, empy or whitespace
        /// and returns the arguemnt if it passes validation.
        /// Throws <see cref="ArgumentException"/> if it does not pass.
        /// </summary>
        /// <param name="arg">The argument to validate.</param>
        /// <returns>The argument value.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string ThrowIfNullOrWhiteSpace(this string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                throw new ArgumentException("Argument must not be null, empty or white space.");
            }

            return arg;
        }

        /// <summary>
        /// Validates that the argument is not less than zero
        /// and returns the arguemnt if it passes validation.
        /// Throws <see cref="ArgumentException"/> if it does not pass.
        /// </summary>
        /// <param name="arg">The argument to validate.</param>
        /// <returns>The argument value.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static int ThrowIfLessThanZero(this int arg)
        {
            if (arg < 0)
            {
            throw new ArgumentException("Argument must be greater than 0.");
            }

            return arg;
        }

        /// <summary>
        /// Validates that the argument is not less than one
        /// and returns the arguemnt if it passes validation.
        /// Throws <see cref="ArgumentException"/> if it does not pass.
        /// </summary>
        /// <param name="arg">The argument to validate.</param>
        /// <returns>The argument value.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static int ThrowIfLessThanOne(this int arg)
        {
            if (arg < 1)
            {
                throw new ArgumentException("Argument must be greater than 1.");
            }

            return arg;
        }

        /// <summary>
        /// Validates that the argument is not less than one
        /// and returns the arguemnt if it passes validation.
        /// Throws <see cref="ArgumentException"/> if it does not pass.
        /// </summary>
        /// <param name="arg">The argument to validate.</param>
        /// <returns>The argument value.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static uint ThrowIfLessThanOne(this uint arg)
        {
            if (arg < 1)
            {
                throw new ArgumentException("Argument must be greater than 1.");
            }

            return arg;
        }
    }
}