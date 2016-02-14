// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="Study.cs">
// Copyright © 2014 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace GEHealthcare.ZFP.Model.Types.PatientHistory
{
    public class Study
    {
        private static readonly string[] NioModalities = Enum.GetNames(typeof(NioModality));

        public Study()
        {
            SeriesList = new List<Series>();
        }

        public string StudyUid { get; set; }

        public int ExamCkey { get; set; }

        public DateTime StudyDate { get; set; }

        //// Added PatName and PatID for Automation verification.
        public string PatientName { get; set; }

        public string PatientId { get; set; }

        public string AccessionNumber { get; set; }

        public string StudyDescription { get; set; }

        public string ModalitiesInStudy
        {
            get
            {
                var modalities = SeriesList
                            .Select(series => series.Modality)
                            .Where(modality => NioModalities.All(nio => nio != modality))
                            .Distinct()
                            .OrderBy(modality => modality)
                            .ToArray();

                return string.Join("/", modalities);
            }
        }

        public int SeriesCount { get; set; }

        public int StudySopsCount { get; set; }

        public IList<Series> SeriesList { get; private set; }

        public string ExamStsStatus { get; set; }

        public string EaArchiveId { get; set; }
    }
}
