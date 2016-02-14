// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="Address.cs">
// Copyright © 2014 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Types
{
    public class Address
    {
        public string StreetAddress { get; set; }

        public string OtherDesignation { get; set; }

        public string City { get; set; }

        public string StateOrProvince { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public string AddressType { get; set; }

        public string OtherGeographicDesignation { get; set; }
    }
}