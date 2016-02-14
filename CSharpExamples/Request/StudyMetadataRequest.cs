// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StudyMetadataRequest.cs" company="General Electric">
//   Copyright 2012 GE Healthcare
// </copyright>
// <summary>
//   Defines the StudyRequest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Request
{
    using GEHealthcare.ZFP.Model.Types;

    public class StudyMetadataRequest
    {
        public string PrimaryStudyInstanceUid { get; set; }

        public int AssigningAuthorityId { get; set; }

        public AppMode ApplicationMode { get; set; }

        public string EaArchiveId { get; set; }

        public bool FetchOfflineExam { get; set; }

        public AuditInfo AuditInfo { get; set; }
    }
}
