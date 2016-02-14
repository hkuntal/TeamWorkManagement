// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="HD.cs">
// Copyright © 2014 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Types
{
    public class Hd
    {
        public string DomainNameSpace;
        public string DomainId;
        public string DomainType;

        private const string DefaultDomainType = "ISO";

        public Hd(string domain)
        {
            DomainType = string.Empty;
            this.DomainId = string.Empty;
            DomainNameSpace = string.Empty;
            if (!string.IsNullOrEmpty(domain))
            {
                // TODO: move delimiter & to constants
                var targetDomain = domain.Split('&');
                DomainNameSpace = targetDomain.Length > 0 ? targetDomain[0] : string.Empty;
                this.DomainId = targetDomain.Length > 1 ? targetDomain[1] : string.Empty;
                DomainType = targetDomain.Length > 2 ? targetDomain[2] : string.Empty;
            }
        }

        public Hd() : this(null)
        {
        }

        public override string ToString()
        {
            return string.Format("{0}&{1}&{2}", DomainNameSpace ?? string.Empty, DomainId, DomainType ?? DefaultDomainType);
        }
    }
}
