// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="RemoveAllTagsRequest.cs">
// Copyright 2014 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Request
{
    using GEHealthcare.ZFP.Model.Types;

    public class RemoveAllTagsRequest
    {
        public Identifier PatientIdentifier;

        public string UserId;
    }
}
