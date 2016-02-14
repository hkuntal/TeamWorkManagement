// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="InstitutionName.cs">
// Copyright © 2014 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Types
{
    /// <summary>
    /// Describes an institution name and patient ID for that institution
    /// The ID is not guaranteed to be 
    /// unique outside of the domain.
    /// </summary>
    public class InstitutionName
    {
        public string Id { get; set; }

        public string ShortCode { get; set; }
    }
}