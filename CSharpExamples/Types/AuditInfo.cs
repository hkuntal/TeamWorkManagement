// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="AuditInfo.cs">
// Copyright © 2014 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Types
{
    

    public class AuditInfo
    {
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
        /// Gets or sets the patient domain.
        /// </summary>
        public string PatientDomain { get; set; }

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

        /// <summary>
        /// Gets or sets a value indicating whether the user login succeeded or not.
        /// </summary>
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

        public void SetToThreadContext()
        {
            ThreadContext.Properties["clientIp"] = ClientIp;
            ThreadContext.Properties["userId"] = UserName;
            ThreadContext.Properties["patientId"] = PatientIdentifier;
        }
    }
}