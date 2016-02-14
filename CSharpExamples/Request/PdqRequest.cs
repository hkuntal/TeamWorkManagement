// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="PdqRequest.cs">
// Copyright © 2014 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Request
{
    using System;
    using System.Collections.Generic;

    using GEHealthcare.ZFP.Model.Types;

    public class PdqRequest
    {
        /// <summary>
        /// Gets or sets patient identifier.
        /// </summary>
        public Identifier PatientIdentifier { get; set; }

        /// <summary>
        /// Gets or sets patient name.
        /// </summary>
        public PersonName PatientName { get; set; }

        /// <summary>
        /// Gets or sets Date of birth.
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets Patient address.
        /// </summary>
        public Address PatientAddress { get; set; }

        /// <summary>
        /// Gets or sets administrative sex.
        /// </summary>
        public Gender AdministrativeSex { get; set; }

        /// <summary>
        /// Gets or sets target domains.
        /// </summary>
        public IEnumerable<Hd> TargetDomains { get; set; }
    }
}