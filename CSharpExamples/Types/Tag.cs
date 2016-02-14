//
// Copyright 2011 General Electric Company
//

namespace GEHealthcare.ZFP.Model.Types
{
    using System;

    public class Tag
    {
        public Tag() { }
        
        public Tag(int tagId, string owner, string documentUid, string repositoryUid)
        {
            this.TagId = tagId;
            this.Owner = owner;
            this.RepositoryUid = repositoryUid;
            this.DocumentUid = documentUid;
            this.CreationDate = DateTime.Now;
        }

        /// <summary>
        /// Gets or sets tag id.
        /// </summary>
        public int TagId { get; set; }

        /// <summary>
        /// Gets or sets the id of the user who created the tag.
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// Gets or sets tagged document id.
        /// </summary>
        public string DocumentUid { get; set; }

        /// <summary>
        /// Gets or sets the repository id of the document.
        /// </summary>
        public string RepositoryUid { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the tag.
        /// </summary>
        public DateTime CreationDate { get; set; }
    }
}
