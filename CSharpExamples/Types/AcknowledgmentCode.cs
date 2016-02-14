//
// Copyright 2012 General Electric Company
//

namespace GEHealthcare.ZFP.Model.Types
{
    using System.Globalization;

    public enum AcknowledgmentCode
    {
        ApplicationAccept,
        ApplicationError, 
        ApplicationReject
    }

    public static class AcknowledgmentCodeExtension
    {
        public const string ApplicationAccept = "AA";
        public const string ApplicationError = "AE";
        public const string ApplicationReject = "AR";

        public static string ToString(this AcknowledgmentCode value)
        {
            string result;
            switch (value)
            {
                case AcknowledgmentCode.ApplicationAccept:
                    result = ApplicationAccept;
                    break;
                case AcknowledgmentCode.ApplicationReject:
                    result = ApplicationReject;
                    break;
                default:
                    result = ApplicationError;
                    break;
            }

            return result;
        }

        public static AcknowledgmentCode Parse(string value)
        {
            AcknowledgmentCode result;
            switch (value.ToUpper(CultureInfo.InvariantCulture))
            {
                case ApplicationAccept:
                    result = AcknowledgmentCode.ApplicationAccept;
                    break;
                case ApplicationReject:
                    result = AcknowledgmentCode.ApplicationReject;
                    break;
                default:
                    result = AcknowledgmentCode.ApplicationError;
                    break;
            }
            return result;
        }
    }
}
