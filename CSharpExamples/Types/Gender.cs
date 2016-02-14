//
// Copyright © 2014 General Electric Company  
//

namespace GEHealthcare.ZFP.Model.Types
{
    using System.Globalization;

    /// <summary>
    /// Represent Gender field in HL7 standard.
    /// </summary>
    public enum Gender
    {
        Unknown,
        Ambiguous,
        Female,
        Male,
        NotApplicable,
        Other
    }

    public static class GenderExtension
    {
        public const char Ambiguous = 'A';

        public const char Female = 'F';

        public const char Male = 'M';

        public const char NotApplicable = 'N';

        public const char Other = 'O';

        public const char Unknown = 'U';

        public static char ToChar(this Gender value)
        {
            char result;
            switch (value)
            {
                case Gender.Ambiguous:
                    result = Ambiguous;
                    break;
                case Gender.Female:
                    result = Female;
                    break;
                case Gender.Male:
                    result = Male;
                    break;
                case Gender.NotApplicable:
                    result = NotApplicable;
                    break;
                case Gender.Other:
                    result = Other;
                    break;
                default:
                    result = Unknown;
                    break;
            }

            return result;
        }

        public static string ToHL7AbbreviationString(this Gender value)
        {
            return ToChar(value).ToString(CultureInfo.InvariantCulture);
        }

        public static Gender Parse(char value)
        {
            Gender result;
            switch (value)
            {
                case Ambiguous:
                    result = Gender.Ambiguous;
                    break;
                case Female:
                    result = Gender.Female;
                    break;
                case Male:
                    result = Gender.Male;
                    break;
                case NotApplicable:
                    result = Gender.NotApplicable;
                    break;
                case Other:
                    result = Gender.Other;
                    break;
                default:
                    result = Gender.Unknown;
                    break;
            }
            return result;
        }
    }
}