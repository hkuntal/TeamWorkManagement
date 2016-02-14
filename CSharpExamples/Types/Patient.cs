// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="Patient.cs">
// Copyright © 2014 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Types
{
    using System;
    using System.Collections.Generic;

    public class Patient
    {
        /// <summary>
        /// Gets or sets Patient identifier list.
        /// </summary>
        public IEnumerable<Identifier> PatientIdentifierList { get; set; }

        /// <summary>
        /// Gets or sets Patient Name.
        /// </summary>
        public PersonName Name { get; set; }

        /// <summary>
        /// Gets or sets Date of birth.
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets Patient address.
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// Gets or sets administrative sex.
        /// </summary>
        public Gender AdministrativeSex { get; set; }
    }
}