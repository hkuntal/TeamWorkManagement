// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="RegistryMetadata.cs">
// Copyright 2012 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Types
{
    /// <summary>
    /// Object that represents registry object type and holds its metadata.
    /// </summary>
    public class RegistryMetadata
    {
        public string EntryUuid { get; set; }

        /// <summary>
        /// Gets or sets the Home Community Id.
        /// </summary>
        public string HomeCommunityId { get; set; }

        /// <summary>
        /// Gets or sets the availability status.
        /// </summary>
        /// <value>The availability status.</value>
        /// <remarks>
        /// An XDS Document shall have one of two availability statuses:
        /// Approved    available for patient care
        /// Deprecated  obsolete
        /// <para/>
        /// This attribute is always set to Approved as part of the
        /// submission of new XDS Documents. It may be changed to
        /// Deprecated under the primary responsibility of the Document
        /// Source with possible patient supervision.
        /// <para/>
        /// Although XDS supports the ability to delete documents, there
        /// is no such state as "the Document Entry is removed" (only an
        /// audit trail is kept if such a deletion is allowed).
        /// <para/>
        /// This list may be extended in the future. See section 4.1.3.3
        /// Atomicity Requirements for XDS Submission Requests for
        /// additional details.
        /// <para/>
        /// If present, shall have a single value.
        /// </remarks>
        public string AvailabilityStatus { get; set; }

        public override bool Equals(object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || this.GetType() != obj.GetType())
                return false;

            var registryMetadata = (RegistryMetadata)obj;
            return this.EntryUuid.Equals(registryMetadata.EntryUuid);
        }

        public override int GetHashCode()
        {
            return this.EntryUuid.GetHashCode();
        }
    }
}
