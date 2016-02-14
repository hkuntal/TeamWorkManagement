//
// Copyright 2012 General Electric Company
//

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using GEHealthcare.ZFP.Model.Types;
using JetBrains.Annotations;

namespace GEHealthcare.ZFP.Common
{
    /// <summary>
    /// Audit Data Model.
    /// </summary>
    public sealed class AuditDataModel
    {
        public const string AuditUserName = "UserName";
        public const string AuditActionType = "ActionType";
        public const string AuditStudyInstanceUid = "StudyInstanceUID";
        public const string AuditAccessionNumber = "AccessionNumber";
        public const string AuditPatientIdentifier = "PatientIdentifier";
        public const string AuditPatientName = "PatientName";
        public const string AuditStudyDescription = "StudyDescription";
        public const string AuditSeriesNumber = "SeriesNumber";
        public const string AuditImageNumber = "ImageNumber";
        public const string AuditDateTimestamp = "DateTimeStamp";
        public const string AuditDisplayMultiplePatientMessage = "DisplayMultiplePtientMessgae";
        public const string AuditClientIp = "ClientIp";
        public const string HostNameTag = "HostName";
        public const string AuditStartTag = "<Audit>";
        public const string AuditEndTag = "</Audit>";

        /// <summary>
        /// Serialized Object for Audit Data Model.
        /// </summary>
        [NotNull] 
        private StringBuilder serializedObject;

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the study info.
        /// </summary>
        /// <value>The study info.</value>
        public string StudyInstanceUid { get; set; }

        /// <summary>
        /// Gets or sets the accession number.
        /// </summary>
        /// <value>The accession number.</value>
        public string AccessionNumber { get; set; }

        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>The patient identifier.</value>
        public string PatientIdentifier { get; set; }

        /// <summary>
        /// Gets or sets patient name.
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// Gets or sets study description.
        /// </summary>
        public string StudyDescription { get; set; }

        /// <summary>
        /// Gets or sets series number.
        /// </summary>
        public string SeriesNumber { get; set; }

        /// <summary>
        /// Gets or sets image number.
        /// </summary>
        public string ImageNumber { get; set; }

        /// <summary>
        /// Gets or sets date time stamp.
        /// </summary>
        public string DateTimestamp { get; set; }

        /// <summary>
        /// Gets or sets multiple patient message display value. 
        /// </summary>
        public string DisplayMultiplePatientMessage { get; set; }
        
        /// <summary>
        /// Gets or sets the audit action.
        /// </summary>
        /// <value>The audit action.</value>
        public AuditActionType AuditAction { get; set; }

        /// <summary>
        /// Gets or sets the audit source ID for ATNA logging.
        /// Since we do not need to log this property value, 
        /// this property is not added to AuditDataModel string.
        /// </summary>
        /// <value> Audit source ID for ATNA logging. </value>
        public string AtnaAuditSourceId { get; set; }

        public bool UserLogOnSucceeded { get; set; }

        /// <summary>
        /// Gets or sets client IP for logging.
        /// </summary>
        /// <value> Client IP for user Login audit. </value>
        public string ClientIp { get; set; }

        /// <summary>
        /// Gets or sets host name for logging.
        /// </summary>
        /// <value> Host name for user Login audit. </value>
        public string HostName { get; set; }
        
        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            serializedObject = new StringBuilder();
            serializedObject.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0}", AuditStartTag));
            SerializeParameter(AuditUserName, UserName);
            SerializeParameter(AuditActionType, AuditAction.ToString());
            SerializeParameter(AuditStudyInstanceUid, StudyInstanceUid);
            SerializeParameter(AuditAccessionNumber, AccessionNumber);
            SerializeParameter(AuditPatientIdentifier, PatientIdentifier);
            SerializeParameter(AuditPatientName, PatientName);
            SerializeParameter(AuditStudyDescription, StudyDescription);
            SerializeParameter(AuditSeriesNumber, SeriesNumber);
            SerializeParameter(AuditImageNumber, ImageNumber);
            SerializeParameter(AuditDateTimestamp, DateTimestamp);
            SerializeParameter(AuditDisplayMultiplePatientMessage, DisplayMultiplePatientMessage);
            SerializeParameter(AuditClientIp, ClientIp);
            SerializeParameter(HostNameTag, HostName);
            serializedObject.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0}", AuditEndTag));
            return serializedObject.ToString();
        }

        /// <summary>
        /// Serializes the parameter.
        /// </summary>
        /// <param name="name">The name of the Parameter.</param>
        /// <param name="value">The value of the Parameter.</param>
        private void SerializeParameter(string name, string value)
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value))
            {
                serializedObject.AppendLine(string.Format(CultureInfo.InvariantCulture, "<{0}>{1}</{0}>", name, value));
            }
        }
    }
}