// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="ZfpPersonName.cs">
// Copyright © 2014 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Types
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Wrapper around DICOM toolkit PersonName.
    /// </summary>
    public class ZfpPersonName : IComparable<ZfpPersonName>, IComparable
    {
        private static readonly IDictionary<string, int> FormatIndex = new Dictionary<string, int> { { "L", 0 }, { "I", 1 }, { "P", 2 } };

        private readonly string personNameFormat;

        private readonly Isip.Dicom.PersonName personName;

        public ZfpPersonName(string value, string nameFormat)
        {
            personName = new Isip.Dicom.PersonName(value);
            personNameFormat = nameFormat;
        }

        public Isip.Dicom.PersonName PersonName
        {
            get
            {
                return personName;
            }
        }

        public string PersonNameToString
        {
            get
            {
                return personName.ToString();
            }
        }

        public static bool operator ==(ZfpPersonName personName1, ZfpPersonName personName2)
        {
            if (ReferenceEquals(null, personName1) || ReferenceEquals(null, personName2))
            {
                return ReferenceEquals(personName1, personName2);
            }

            return personName1.Equals(personName2);
        }

        public static bool operator !=(ZfpPersonName personName1, ZfpPersonName personName2)
        {
            if (ReferenceEquals(null, personName1) || ReferenceEquals(null, personName2))
            {
                return !ReferenceEquals(personName1, personName2);
            }

            return !personName1.Equals(personName2);
        }

        public static bool operator <(ZfpPersonName personName1, ZfpPersonName personName2)
        {
            return personName1.CompareTo(personName2) < 0;
        }

        public static bool operator >(ZfpPersonName personName1, ZfpPersonName personName2)
        {
            return personName1.CompareTo(personName2) > 0;
        }

        public int CompareTo(ZfpPersonName other)
        {
            var specifiedNameFormatArr = new string[3];

            for (int i = 0; i < specifiedNameFormatArr.Length; i++)
            {
                specifiedNameFormatArr[i] = personNameFormat.Substring(i, 1);
            }
            const string Delimiter = " ";
            string originalFormattedName = string.Empty, otherFormattedName = string.Empty;

            ////Name arranged in the default Person Name format , i.e in LIP format
            var originalNameComponents = new[]
                                   {
                                       PersonName.SingleByte.ToString(), 
                                       PersonName.Ideographic.ToString(), 
                                       PersonName.Phonetic.ToString()
                                   };

            var otherNameComponents = new[]
                                   {
                                       other.PersonName.SingleByte.ToString(), 
                                       other.PersonName.Ideographic.ToString(), 
                                       other.PersonName.Phonetic.ToString()
                                   };

            foreach (var nameFormat in specifiedNameFormatArr)
            {
                var formatIndex = FormatIndex[nameFormat];

                var originalNameComponent = originalNameComponents[formatIndex];
                if (!string.IsNullOrWhiteSpace(originalNameComponent))
                {
                    originalFormattedName += originalNameComponent + Delimiter;
                }

                var otherNameComponent = otherNameComponents[formatIndex];
                if (!string.IsNullOrWhiteSpace(otherNameComponent))
                {
                    otherFormattedName += otherNameComponent + Delimiter;
                }
            }

            return string.Compare(originalFormattedName, otherFormattedName, CultureInfo.CurrentCulture, CompareOptions.OrdinalIgnoreCase);
        }

        public int CompareTo(object obj)
        {
            var otherPerson = obj as ZfpPersonName;
            if (otherPerson == null)
                throw new ArgumentException("Could not cast to ZfpPersonName.");
            return CompareTo(otherPerson);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return Equals((ZfpPersonName)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((this.personNameFormat != null ? this.personNameFormat.GetHashCode() : 0) * 397) ^ (this.personName != null ? this.personName.GetHashCode() : 0);
            }
        }

        protected bool Equals(ZfpPersonName other)
        {
            return string.Equals(this.personNameFormat, other.personNameFormat) && Equals(this.personName, other.personName);
        }
    }
}