// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="PatientDataRequest.cs">
// Copyright © 2014 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Request
{
    using GEHealthcare.ZFP.Model.Types;

    public class PatientDataRequest
    {
        /// <summary>
        /// Gets or sets patient identifier.
        /// </summary>
        public Identifier PatientIdentifier { get; set; }

        /// <summary>
        /// Gets or sets CCOW context identifier.
        /// </summary>
        public Identifier ContextIdentifier { get; set; }

        /// <summary>
        /// Gets or sets user type.
        /// </summary>
        public string UserType { get; set; }

        /// <summary>
        /// Gets or sets user specialty.
        /// </summary>
        public string Specialty { get; set; }

        /// <summary>
        /// Gets or sets XDS query run count.
        /// </summary>
        public int XdsQueryRunCount { get; set; }
    }
}