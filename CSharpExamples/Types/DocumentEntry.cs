// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="DocumentEntry.cs">
// Copyright 2012 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Types
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The document entry.
    /// </summary>
    public class DocumentEntry : DocumentMetadata
    {
        /// <summary>
        /// The tags.
        /// </summary>
        private readonly List<Tag> tags = new List<Tag>();

        /// <summary>
        /// The thumbnail URL.
        /// </summary>
        private readonly List<string> thumbnailUrls = new List<string>();

        public DocumentEntry(DocumentMetadata metadata, string displayDateAttribute) : base(metadata)
        {
            this.SetDisplayDate(displayDateAttribute);
        }

        /// <summary>
        /// Gets the list of the tags.
        /// </summary>
        public IList<string> Thumbnails
        {
            get
            {
                return this.thumbnailUrls;
            }
        }

        /// <summary>
        /// Gets the list of the tags.
        /// </summary>
        public IList<Tag> Tags
        {
            get
            {
                return this.tags;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the document mime type is supported.
        /// </summary>
        /// <value>Supported Mime Type indication.</value>
        public bool HasSupportedMimeType { get; set; }

        /// <summary>
        /// Gets or sets the icon which should be shown before the real thumbnail is generated.
        /// </summary>
        /// <value>String pointing to the initially shown icon.</value>
        public string InitialIcon { get; set; }

        // flag that indicates whether thumbnail avialable in cache or not.
        public bool IsThumbnailAvailable { get; set; }

        /// <summary>
        /// Gets or sets study instance Id for DICOM type documents.
        /// </summary>
        public string StudyInstanceUid { get; set; }

        /// <summary>
        /// Gets document display date.
        /// </summary>
        public DateTime DisplayDate { get; private set; }

        /// <summary>
        /// Adds a Thumbnail.
        /// </summary>
        /// <param name="thumbnailUrl">Thumbnail url.</param>
        public void AddThumbnail(string thumbnailUrl)
        {
            this.thumbnailUrls.Add(thumbnailUrl);
        }

        /// <summary>
        /// Adds a tag.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public void AddTag(Tag tag)
        {
            this.tags.Add(tag);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }

            var curr = (DocumentEntry)obj;
            return DocumentUid.Equals(curr.DocumentUid) 
                && RepositoryUid.Equals(curr.RepositoryUid)
                && (HomeCommunityId ?? string.Empty).Equals(curr.HomeCommunityId ?? string.Empty);
        }

        public override int GetHashCode()
        {
            return DocumentUid.GetHashCode() ^ RepositoryUid.GetHashCode() 
                ^ (HomeCommunityId ?? string.Empty).GetHashCode();
        }

        public override string ToString()
        {
            return DocumentUid + ":" + RepositoryUid + ":" 
                + (HomeCommunityId ?? string.Empty);
        }

        private void SetDisplayDate(string attribute)
        {
            switch (attribute)
            {
                case "ServiceStartTime":
                    this.DisplayDate = ServiceStartTime ?? this.DocCreationDate;
                    break;
                case "ServiceStopTime":
                    this.DisplayDate = ServiceStopTime ?? this.DocCreationDate;
                    break;
                default:
                    this.DisplayDate = this.DocCreationDate;
                    break;
            }
        }
    }
}
