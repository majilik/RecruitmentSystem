using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace RecruitmentSystem.Resources
{
    /// <summary>
    /// Enumerates all available Locales that are covered with language resources.
    /// </summary>
    public enum Locales
    {
        EN_US, SV_SE
    }

    /// <summary>
    /// Provides extensions to the Locales enum and helper methods for handling locales.
    /// </summary>
    public class LocalesExtension
    {
        public static Locales LocalesFromString(string locale)
        {
            if (locale == null)
            {
                return Locales.EN_US;
            }

            locale = locale.ToLower();
            switch (locale)
            {
                case "sv-se":
                    return Locales.SV_SE;
                case "en-us":
                default:
                    return Locales.EN_US;
            }
        }

        /// <summary>
        /// Parses a locales enum and returns corresponding CultureInfo.
        /// </summary>
        /// <param name="locale">Locales enum</param>
        /// <returns>Parsed CultureInfo, defaults to 'en-US' if locale not found.</returns>
        public static CultureInfo ParseCultureInfo(Locales? locale)
        {
            switch (locale)
            {
                case Locales.SV_SE:
                    return new CultureInfo("sv-SE");
                case Locales.EN_US:
                default:
                    return new CultureInfo("en-US");
            }
        }

        /// <summary>
        /// Parses a locale string and returns corresponding CultureInfo.
        /// </summary>
        /// <param name="locale">Locale string on the format languagecode2-country/regioncode2.</param>
        /// <returns>Parsed CultureInfo, defaults to 'en-US' if locale not found.</returns>
        public static CultureInfo ParseCultureInfo(string locale)
        {
            if (locale == null)
            {
                return new CultureInfo("en-US");
            }

            locale = locale.ToLower();
            switch (locale)
            {
                case "sv-se":
                    return new CultureInfo("sv-SE");
                case "en-us":
                default:
                    return new CultureInfo("en-US");
            }
        }

        /// <summary>
        /// Parses a locale string and checks if it is implemented.
        /// </summary>
        /// <param name="locale">Locale string on the format languagecode2-country/regioncode2.</param>
        /// <returns>false if locale not found, true otherwise.</returns>
        public static bool IsLocaleImplemented(string locale)
        {
            locale = locale.ToLower();
            switch (locale)
            {
                case "sv-se":
                case "en-us":
                    return true;
                default:
                    return false;
            }
        }
    }
}