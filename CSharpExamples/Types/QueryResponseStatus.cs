//
// Copyright 2012 General Electric Company
//
using System.Globalization;

namespace GEHealthcare.PIV.HL7Resolver.PatientDemographicQuery
{
    public enum QueryResponseStatus
    {
        DataFound,
        NoDataFound,
        ApplicationError,
        ApplicationReject,
        Unknown
    }

    public static class QueryResponseStatusExtension
    {
        public const string DataFound = "OK";
        public const string NoDataFound = "NF";
        public const string ApplicationError = "AE";
        public const string ApplicationReject = "AR";

        public static string ToString(this QueryResponseStatus value)
        {
            string result;
            switch (value)
            {
                case QueryResponseStatus.NoDataFound:
                    result = NoDataFound;
                    break;
                case QueryResponseStatus.ApplicationError:
                    result = ApplicationError;
                    break;
                case QueryResponseStatus.ApplicationReject:
                    result = ApplicationReject;
                    break;
                default:
                    result = DataFound;
                    break;
            }

            return result;
        }

        public static QueryResponseStatus Parse(string value)
        {
            QueryResponseStatus result;
            switch (value.ToUpper(CultureInfo.InvariantCulture))
            {
                case NoDataFound:
                    result = QueryResponseStatus.NoDataFound;
                    break;
                case ApplicationError:
                    result = QueryResponseStatus.ApplicationError;
                    break;
                case ApplicationReject:
                    result = QueryResponseStatus.ApplicationReject;
                    break;
                case DataFound:
                    result = QueryResponseStatus.DataFound;
                    break;
                default:
                    result = QueryResponseStatus.Unknown;
                    break;
            }
            return result;
        }
    }
}
