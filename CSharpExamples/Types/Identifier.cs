// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="Identifier.cs">
// Copyright 2014 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Types
{
    public class Identifier
    {
        public string Id { get; set; }

        public Hd AssigningAuthority { get; set; }

        public override bool Equals(object other)
        {
            if (other == null || this.GetType() != other.GetType())
            {
                return false;
            }

            var identifier = (Identifier)other;

            return string.Equals(this.Id, identifier.Id) 
                && string.Equals(this.AssigningAuthority.DomainId, identifier.AssigningAuthority.DomainId) 
                && string.Equals(this.AssigningAuthority.DomainType, identifier.AssigningAuthority.DomainType);
        }

        public override string ToString()
        {
            return Id + "^^^" + AssigningAuthority;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = this.Id != null ? this.Id.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (this.AssigningAuthority.DomainId != null ? this.AssigningAuthority.DomainId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.AssigningAuthority.DomainType != null ? this.AssigningAuthority.DomainType.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
