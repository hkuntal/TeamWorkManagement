// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExamCkeyStudyUid.cs" company="General Electric">
//   Copyright 2012 GE Healthcare
// </copyright>
// <summary>
//   Defines the StudyRequest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Types
{
    public class ExamCkeyStudyUid
    {
        public ExamCkeyStudyUid(int examCkey, string studyInstanceUid)
        {
            this.StudyInstanceUid = studyInstanceUid;
            this.ExamCkey = examCkey;
        }

        public int ExamCkey { get; private set; }

        public string StudyInstanceUid { get; private set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj.GetType() != typeof(ExamCkeyStudyUid))
                return false;
            var examObj = obj as ExamCkeyStudyUid;
            if (examObj == null)
                return false;
            return examObj.ExamCkey.Equals(this.ExamCkey) && examObj.StudyInstanceUid.Equals(this.StudyInstanceUid);
        }

        public override int GetHashCode()
        {
            return this.StudyInstanceUid.GetHashCode() ^ this.ExamCkey.GetHashCode();
        }
    }
}
