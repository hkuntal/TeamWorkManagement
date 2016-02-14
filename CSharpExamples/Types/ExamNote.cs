// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="ExamNote.cs">
// Copyright © 2014 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Types
{
    using System;
    
    public class ExamNote
    {
        public string Creator { get; set; }

        public DateTime? CreationDateTime { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public bool Active { get; set; }

        public bool HasAudio { get; set; }
    }
}