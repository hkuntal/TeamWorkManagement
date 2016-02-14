// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="DocumentMetadata.cs">
// Copyright 2012 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Types
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using GEHealthcare.Isip;

    /// <summary>
    /// Document data class.
    /// </summary>
    public class DocumentMetadata : RegistryMetadata
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentMetadata"/> class.
        /// </summary>
        public DocumentMetadata()
        {
            this.Authors = new List<Author>();
            this.ReferenceIdList = new List<ReferenceId>();
            this.LinkEntryUuidList = new List<string>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentMetadata"/> class.
        /// </summary>
        /// <param name="documentData">document data object.</param>
        public DocumentMetadata(DocumentMetadata documentData)
        {
            if (documentData == null) return;
            AvailabilityStatus = documentData.AvailabilityStatus;
            ClassCode = documentData.ClassCode;
            ClassCodeDisplayName = documentData.ClassCodeDisplayName;
            Comments = documentData.Comments;
            ConfidentialityCode = documentData.ConfidentialityCode;
            ContentTypeCode = documentData.ContentTypeCode;
            DocCreationDate = documentData.DocCreationDate;
            DocumentUid = documentData.DocumentUid;
            EntryUuid = documentData.EntryUuid;
            EventCode = documentData.EventCode;
            FormatCode = documentData.FormatCode;
            Hash = documentData.Hash;
            HealthcareFacilityTypeCode = documentData.HealthcareFacilityTypeCode;
            HomeCommunityId = documentData.HomeCommunityId;
            LanguageCode = documentData.LanguageCode;
            MimeType = documentData.MimeType;
            PracticeSettingCode = documentData.PracticeSettingCode;
            RepositoryUid = documentData.RepositoryUid;
            ServiceStartTime = documentData.ServiceStartTime;
            ServiceStopTime = documentData.ServiceStopTime;
            ClassCodeValue = documentData.ClassCodeValue;
            TypeCodeValue = documentData.TypeCodeValue;
            FormatCodeValue = documentData.FormatCodeValue;
            HealthcareFacilityTypeCodeValue = documentData.HealthcareFacilityTypeCodeValue;
            PracticeSettingCodeValue = documentData.HealthcareFacilityTypeCodeValue;
            Title = documentData.Title;
            TypeCode = documentData.TypeCode;
            TypeCodeDisplayName = documentData.TypeCodeDisplayName;
            Authors = new List<Author>();
            documentData.Authors.ForEach(x => Authors.Add(x));
            AuthorInstitution = documentData.AuthorInstitution;
            AuthorPerson = documentData.AuthorPerson;
            EventCodeAnatomy = documentData.EventCodeAnatomy;
            EventCodeModality = documentData.EventCodeModality;
            PatientId = documentData.PatientId;
            DomainId = documentData.DomainId;
            DocumentSize = documentData.DocumentSize;
            SourcePatientInfo = documentData.SourcePatientInfo;
            RetrieveLocationUid = documentData.RetrieveLocationUid;
            AeTitle = documentData.AeTitle;
            ReferenceIdList = new List<ReferenceId>();
            documentData.ReferenceIdList.ForEach(x => ReferenceIdList.Add(x));
            LinkEntryUuidList = new List<string>();
        }

        /// <summary>
        /// Gets or sets Type code display name.
        /// </summary>
        public object TypeCodeDisplayName { get; set; }

        /// <summary>
        /// Gets or sets class code display name.
        /// </summary>
        public object ClassCodeDisplayName { get; set; }

        /// <summary>
        /// Gets or sets the comments associated with the Document.
        /// </summary>
        /// <remarks>
        /// Free form text with an XDS Affinity Domain specified usage.
        /// </remarks>
        /// <value>The comments.</value>
        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets the hash.
        /// </summary>
        /// <remarks>
        /// Hash key of the XDS Document itself. This value is computed
        /// by the Document Repository and used by the Document
        /// Registry for detecting the improper resubmission of XDS
        /// Documents. If present, shall have a single value.
        /// <para/>
        /// If this attribute is received in a Provide And Register Document
        /// Set transaction, it shall be verified by the repository with the
        /// actual hash value of the submitted document; an error shall be
        /// returned on mismatch.
        /// </remarks>
        /// <value>The hash of the document.</value>
        public string Hash { get; set; }

        /// <summary>
        /// Gets or sets the document title.
        /// </summary>
        /// <value>The document title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the document creation date.
        /// </summary>
        /// <value>The creation date.</value>
        public DateTime DocCreationDate { get; set; }

        /// <summary>
        /// Gets or sets the mime type.
        /// </summary>
        /// <value>The mime type.</value>
        public string MimeType { get; set; }

        /// <summary>
        /// Gets or sets the document UID.
        /// </summary>
        public string DocumentUid { get; set; }

        /// <summary>
        /// Gets or sets the content type code.
        /// </summary>
        /// <value>The content type code.</value>
        public string EventCode { get; set; }

        /// <summary>
        /// Gets or sets the class code.
        /// </summary>
        /// <value>The class code.</value>
        public string ClassCode { get; set; }

        /// <summary>
        /// Gets or sets the class code value.
        /// </summary>
        /// <value>The class code.</value>
        public string ClassCodeValue { get; set; }

        /// <summary>
        /// Gets or sets the confidentiality code.
        /// </summary>
        /// <value>The confidentiality code.</value>
        public string ConfidentialityCode { get; set; }

        /// <summary>
        /// Gets or sets the format code.
        /// </summary>
        /// <value>The format code.</value>
        public string FormatCode { get; set; }

        /// <summary>
        /// Gets or sets the format code value.
        /// </summary>
        /// <value>The format code.</value>
        public string FormatCodeValue { get; set; }

        /// <summary>
        /// Gets or sets the healthcare facility type code.
        /// </summary>
        /// <value>The healthcare facility type code.</value>
        public string HealthcareFacilityTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the healthcare facility type code value.
        /// </summary>
        /// <value>The healthcare facility type code.</value>
        public string HealthcareFacilityTypeCodeValue { get; set; }

        /// <summary>
        /// Gets or sets the practice setting code.
        /// </summary>
        /// <value>The practice setting code.</value>
        public string PracticeSettingCode { get; set; }

        /// <summary>
        /// Gets or sets the practice setting code value.
        /// </summary>
        /// <value>The practice setting code.</value>
        public string PracticeSettingCodeValue { get; set; }

        /// <summary>
        /// Gets or sets the type code.
        /// </summary>
        /// <value>The type code.</value>
        public string TypeCode { get; set; }

        /// <summary>
        /// Gets or sets the type code value.
        /// </summary>
        /// <value>The type code.</value>
        public string TypeCodeValue { get; set; }

        /// <summary>
        /// Gets or sets the language code.
        /// </summary>
        /// <value>The language code.</value>
        public string LanguageCode { get; set; }

        /// <summary>
        /// Gets the Author person.
        /// </summary>
        /// <value>The Author person.</value>
        public ICollection<Author> Authors { get; private set; }

        /// <summary>
        /// Gets or sets the ContentType Code.
        /// </summary>
        /// <value>The ContentType Code.</value>
        public string ContentTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the repository Id.
        /// </summary>
        /// <value>The repository Id.</value>
        public string RepositoryUid { get; set; }

        /// <summary>
        /// Gets or sets the ServiceStartTime.
        /// </summary>
        /// <value>The ServiceStartTime.</value>
        public DateTime? ServiceStartTime { get; set; }

        /// <summary>
        /// Gets or sets the ServiceStopTime.
        /// </summary>
        /// <value>The ServiceStopTime.</value>
        public DateTime? ServiceStopTime { get; set; }

        /// <summary>
        /// Gets or sets the name of the author.
        /// </summary>
        /// <value>The name of the author.</value>
        public string AuthorPerson { get; set; }

        /// <summary>
        /// Gets or sets the institution of the author.
        /// </summary>
        /// <value>The name of the author.</value>
        public string AuthorInstitution { get; set; }

        /// <summary>
        /// Gets or sets the modality items in the EventCodeList.
        /// </summary>
        /// <value>The Modality value(s) from the Event code list.</value>
        public string EventCodeModality { get; set; }

        /// <summary>
        /// Gets or sets the institution of the author.
        /// </summary>
        /// <value>The Anatomy value(s) from the Event Code List.</value>
        public string EventCodeAnatomy { get; set; }

        /// <summary>
        /// Gets or sets source patient info.
        /// </summary>
        public SourcePatientInfo SourcePatientInfo { get; set; }

        /// <summary>
        /// Gets or sets global patient id.
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// Gets or sets global domain id.
        /// </summary>
        public string DomainId { get; set; }

        /// <summary>
        /// Gets or sets document size.
        /// </summary>
        public int? DocumentSize { get; set; }

        /// <summary>
        /// Gets or sets retrieve location UID.
        /// </summary>
        public string RetrieveLocationUid { get; set; }

        /// <summary>
        /// Gets or sets AE Title.
        /// </summary>
        public string AeTitle { get; set; }

        /// <summary>
        /// Gets or sets the list of Reference IDs.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly",
            Justification = "Setter needed for deserialization.")]
        public ICollection<ReferenceId> ReferenceIdList { get; set; }

        /// <summary>
        /// Gets or sets the list of Reference Link entry UUID.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly",
            Justification = "Setter needed for deserialization.")]
        public IList<string> LinkEntryUuidList { get; set; }
    }
}