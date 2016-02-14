// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="SourcePatientInfo.cs">
// Copyright 2012 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Types
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// Source patient info.
    /// </summary>
    public class SourcePatientInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SourcePatientInfo"/> class.
        /// </summary>
        public SourcePatientInfo()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SourcePatientInfo"/> class.
        /// </summary>
        /// <param name="sourcePatientInfo">source patient info.</param>
        public SourcePatientInfo(IEnumerable<string> sourcePatientInfo)
        {
            this.ParseSourcePatientInfoField(sourcePatientInfo);
        }

        /// <summary>
        /// Gets or sets first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets Middle name.
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        ///  Gets or sets last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets date of birth.
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets Gender.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Gets a value indicating whether last name valid.
        /// </summary>
        public bool IsValid
        {
            get { return !string.IsNullOrEmpty(this.LastName); }
        }

        /// <summary>
        /// Parse source patient info field.
        /// </summary>
        /// <param name="sourcePatientInfo">source patient info.</param>
        private void ParseSourcePatientInfoField(IEnumerable<string> sourcePatientInfo)
        {
            foreach (var patientInfo in sourcePatientInfo)
            {
                var eachField = patientInfo;

                // TODO: this needs to be fixed in XDS core library, getting unnecessary pipe "|" character if any PID field value is empty.
                if (eachField.StartsWith("|", StringComparison.Ordinal))
                {
                    eachField = eachField.Substring(1);
                }

                if (eachField.StartsWith("PID-5", StringComparison.Ordinal))
                {
                    this.ParsePatientName(eachField);
                }
                else if (eachField.StartsWith("PID-7", StringComparison.Ordinal))
                {
                    this.ParseDateOfBirth(eachField);
                }
                else if (eachField.StartsWith("PID-8", StringComparison.Ordinal))
                {
                    var components = StringSplitter(eachField);
                    if (components.Count() > 1)
                    {
                        this.Gender = components[1];
                    }
                }
            }
        }

        /// <summary>
        /// Parse date of birth.
        /// </summary>
        /// <param name="patientInfo">patient info.</param>
        private void ParseDateOfBirth(string patientInfo)
        {
            var components = StringSplitter(patientInfo);
            if (components.Count() < 2)
            {
                return;
            }

            var dateAsString = components[1];
            try
            {
                while (dateAsString.Length < 8)
                {
                    dateAsString = "0" + dateAsString;
                }

                // if DOB carries time, ignore it as we don't have to display TIME.
                if (dateAsString.Length > 8)
                {
                    dateAsString = dateAsString.Substring(0, 8);
                }

                this.DateOfBirth = DateTime.ParseExact(dateAsString, "yyyyMMdd", CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                // ignore the exception
            }
        }

        /// <summary>
        /// parse patient name.
        /// </summary>
        /// <param name="patientInfo">patient info.</param>
        private void ParsePatientName(string patientInfo)
        {
            var components = StringSplitter(patientInfo).ToArray();
            if (components.Count() > 3)
            {
                this.MiddleName = components[3];
            }
            if (components.Count() > 2)
            {
                this.FirstName = components[2];
            }
            if (components.Count() > 1)
            {
                this.LastName = components[1];
            }
        }

        /// <summary>
        /// String splitter.
        /// </summary>
        /// <param name="input">string to split.</param>
        /// <returns>array of string.</returns>
        private string[] StringSplitter(string input)
        {
            char[] delimitChars = { '|', '^' };
            var words = input.Split(delimitChars);
            return words;
        }
    }
}