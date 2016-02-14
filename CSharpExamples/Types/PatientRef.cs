// -----------------------------------------------------------------------
// <copyright file="PatientRef.cs" company="Copyright 2012 General Electric Company">
// Copyright statement. All right reserved
// </copyright>
// -----------------------------------------------------------------------
namespace GEHealthcare.ZFP.Model.Types
{
    public class PatientRef
    {
        public string PatientName { get; set; }

        public string PatientId { get; set; }

        public string AuthorityShortCode { get; set; }
    }
}
