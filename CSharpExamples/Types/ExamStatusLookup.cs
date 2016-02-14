// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExamStatusLookup.cs" company="General Electric">
//   Copyright 2012 GE Healthcare
// </copyright>
// <summary>
//   CPACS ExamStatusLookup entity model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Types
{
    public class ExamStatusLookup
    {
        public string ExamStatusId { get; set; }

        public string ExamStatusDescription { get; set; }

        public string ExamStatusCode { get; set; }
    }
}
