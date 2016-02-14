// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="PixQueryRequest.cs">
// Copyright © 2014 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Request
{
    using GEHealthcare.ZFP.Model.Types;

    public class PixQueryRequest
    {
        public Identifier PatientIdentifier { get; set; }
        
        public string TargetDomain { get; set; }
        
        public bool QueryById { get; set; }

        public AuditInfo AuditInfo { get; set; }
    }
}
