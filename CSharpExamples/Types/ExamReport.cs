// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="ExamReport.cs">
// Copyright © 2014 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Types
{
    public class ExamReport
    {
        public byte[] ReportContent { get; set; }

        public string ExamCKey { get; set; }
    }
}