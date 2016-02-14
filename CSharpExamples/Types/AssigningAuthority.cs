// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="AssigningAuthority.cs">
// Copyright 2014 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Types
{
    public class AssigningAuthority
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ShortCode { get; set; }

        public string Description { get; set; }

        public bool ActiveStatus { get; set; }
    }
}
