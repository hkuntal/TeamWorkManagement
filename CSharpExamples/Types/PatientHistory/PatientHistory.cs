// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="PatientHistory.cs">
// Copyright © 2014 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

namespace GEHealthcare.ZFP.Model.Types.PatientHistory
{
    public class PatientHistory
    {
        public PatientHistory()
        {
            StudyList = new List<Study>();
        }

        public IList<Study> StudyList { get; private set; }

        public IList<string> Modalities
        {
            get
            {
                return StudyList
                        .SelectMany(study => study.SeriesList.Select(series => series.Modality))
                        .Distinct()
                        .OrderBy(mod => mod)
                        .ToArray();
            }
        }
    }
}
