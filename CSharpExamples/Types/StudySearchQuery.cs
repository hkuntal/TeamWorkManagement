// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="StudySearchQuery.cs">
// Copyright © 2014 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Types
{
    public class StudySearchQuery
    {
        public string AccessionNumber { get; set; }

        public string AeTitle { get; set; }

        public string AssigningAuthorityId { get; set; }

        public string AuthorityNameShortCode { get; set; }

        public int ImageCount { get; set; }

        public string PatientId { get; set; }

        public string PatientName { get; set; }

        public string PatientBirthDate { get; set; }

        public string PatientSex { get; set; }

        public string StudyId { get; set; }

        public int ExamId { get; set; }

        public string StudyInstanceUid { get; set; }

        public string StudyDescription { get; set; }

        public string StudyTime { get; set; }

        public string StudyStatus { get; set; }

        public string ModalityTypes { get; set; }

        public string ExamStatusId { get; set; }

        public string ReferringPhysicianName { get; set; }

        public string StudyCreatedStartDate { get; set; }

        public string StudyCreatedEndDate { get; set; }

        public string ExamStatusDescription { get; set; }

        public string ExamStsStatus { get; set; }

        public string LastFourPatId { get; set; }

        public int NoOfRecords { get; set; }

        public int PageNo { get; set; }

        public bool IsSearch { get; set; }

        public string SortField { get; set; }

        public string SortOrder { get; set; }

        public string PersonNameFormat { get; set; }

        public string OtherRequestParameters { get; set; }

        public WorkListType WorkListType { get; set; }

        public string WorkListValue { get; set; }

        public string DisplayMultiplePatientMessage { get; set; }

        public string NationalHealthServiceDomainId { get; set; }

        public string PrimaryStudyInstanceUid { get; set; }

        public bool IsSearchByAccessionOnly { get; set; }

        public bool IsExactMatchForPatientId { get; set; }

        public string UserId { get; set; }
    }
}