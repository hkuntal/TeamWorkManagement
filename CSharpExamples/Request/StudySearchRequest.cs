// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="StudySearchRequest.cs">
// Copyright © 2014 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEHealthcare.ZFP.Model.Types;

namespace GEHealthcare.ZFP.Model.Request
{
    public class StudySearchRequest
    {
        public string User { get; set; }

        public StudySearchQuery SearchQuery { get; set; }

        public AppMode ApplicationMode { get; set; }

        public OpenApiCmd OpenApiCmd { get; set; }

    }
}
