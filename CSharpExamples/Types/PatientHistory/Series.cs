// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="Series.cs">
// Copyright © 2014 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace GEHealthcare.ZFP.Model.Types.PatientHistory
{
    using GEHealthcare.ZFP.Model.Types.Dicom;

    using Newtonsoft.Json;

    public class Series
    {
        public Series(string seriesUid)
        {
            SeriesUid = seriesUid;
        }

        public string SeriesUid { get; private set; }

        [JsonIgnore]
        public double SeriesNumberSort { get; set; }

        public string SeriesNumber { get; set; }

        public string Modality { get; set; }

        public string SeriesDescription { get; set; }

        public string SeriesTime { get; set; }

        public int SeriesSopsCount { get; set; }

        public DicomSop FirstSop { get; set; }

        public IList<DicomSop> Sop { get; set; }
    }
}
