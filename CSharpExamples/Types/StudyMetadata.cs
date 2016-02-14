// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="StudyMetadata.cs">
// Copyright © 2014 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Types
{
    using System;

    public class StudyMetadata
    {
        public string StudyInstanceUid { get; set; }

        public string AccessionNumber { get; set; }

        public string AssigningAuthorityId { get; set; }

        public DateTime StudyDate { get; set; }

        public string StudyTime { get; set; }

        public string StudyDescription { get; set; }

        public string PatientId { get; set; }

        public string PatientIdInstitutionShortCode { get; set; }

        public ZfpPersonName PatientName { get; set; }

        public string PatientSex { get; set; }

        public DateTime? PatientBirthDate { get; set; }

        public string Modality { get; set; }

        public string ReferringPhysicianName { get; set; }

        public string EncryptedStudyLaunchPayload { get; set; }

        public TimeSpan? PatientAge { get; set; }

        public string ExamStatusDescription { get; set; }

        public string ExamStsStatus { get; set; }

        public int ImageCount { get; set; }

        public int SeriesCount { get; set; }

        public bool HasReports { get; set; }

        public string NationalHealthServiceIdentifier { get; set; }

        public string AeTitle { get; set; }

        public string InstanceAvailability { get; set; }

        public string EaArchiveId { get; set; }

        public string UniquePatientKey { get; set; }

        public bool IsAccessionNumberIgnored { get; set; }
    }
}