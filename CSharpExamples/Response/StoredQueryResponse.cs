// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="StoredQueryResponse.cs">
// Copyright 2012 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Response
{
    using System.Collections.Generic;

    using GEHealthcare.ZFP.Model.Types;

    public class StoredQueryResponse
    {
        public StoredQueryResponse()
        {
            this.RegistryEntries = new HashSet<RegistryMetadata>();
        }

        public ISet<RegistryMetadata> RegistryEntries { get; private set; }
    }
}
