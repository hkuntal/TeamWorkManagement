// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="DocumentIdentifier.cs">
// Copyright 2014 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Types
{
    public class DocumentIdentifier
    {
        public string EntryUuid { get; set; }

        public string DocumentUniqueId { get; set; }

        public string RepositoryUniqueId { get; set; }

        public string HomeCommunityId { get; set; }

        public string MimeType { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }

            var curr = (DocumentIdentifier)obj;

            return this.DocumentUniqueId.Equals(curr.DocumentUniqueId) && this.RepositoryUniqueId.Equals(curr.RepositoryUniqueId)
                   && (this.HomeCommunityId ?? string.Empty).Equals(curr.HomeCommunityId ?? string.Empty);
        }

        public override int GetHashCode()
        {
            return this.DocumentUniqueId.GetHashCode() ^ this.RepositoryUniqueId.GetHashCode() ^ (this.HomeCommunityId ?? string.Empty).GetHashCode();
        }

        public override string ToString()
        {
            return this.DocumentUniqueId + ":" + this.RepositoryUniqueId + ":" + (this.HomeCommunityId ?? string.Empty);
        }
    }
}
