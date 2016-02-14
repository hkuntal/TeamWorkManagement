// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="TagRequest.cs">
// Copyright 2014 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Request
{
    using GEHealthcare.ZFP.Model.Types;

    public class TagRequest
    {
        public string Owner { get; set; }

        public Identifier PatientIdentifier { get; set; }

        public DocumentIdentifier DocumentIdentifier { get; set; }
    }
}
