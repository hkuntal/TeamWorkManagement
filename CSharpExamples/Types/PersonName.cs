// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="PersonName.cs">
// Copyright © 2014 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Types
{
    public class PersonName
    {
        public string FamilyName { get; set; }

        public string FamilyNamePrefix { get; set; }

        public string GivenName { get; set; }

        public string MiddleName { get; set; }

        public string Suffix { get; set; }

        public string Prefix { get; set; }

        public string Degree { get; set; }
    }
}