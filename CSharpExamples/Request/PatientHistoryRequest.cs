// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="PatientHistoryRequest.cs">
// Copyright © 2014 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Request
{
    using System.Collections.Generic;

    using GEHealthcare.ZFP.Model.Types;

    public class PatientHistoryRequest
    {
        public string PrimaryStudyInstanceUid { get; set; }

        public string EaArchiveId { get; set; }

        public PatientRef PatientRef { get; set; }

        public AppMode ApplicationMode { get; set; }

        public OpenApiCmd OpenApiCmd { get; set; }

        public IEnumerable<string> ProxyStudyInstanceUids { get; set; }

        public WorkListType WorkListType { get; set; }

        public string WorkListValue { get; set; }

        public string AccessionNumber { get; set; }
    }
}
