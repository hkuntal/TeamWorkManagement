// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="ReferenceId.cs">
// Copyright 2014 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Types
{
    public class ReferenceId
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceId"/> class.
        /// </summary>
        public ReferenceId()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceId"/> class.
        /// </summary>
        /// <param name="referenceId">ReferenceId entity.</param>
        public ReferenceId(ReferenceId referenceId)
            : this()
        {
            if (referenceId == null) return;
            this.Identifier = referenceId.Identifier;
            this.DomainId = referenceId.DomainId;
            this.TypeCode = referenceId.TypeCode;
            this.HomeCommunityId = referenceId.HomeCommunityId;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Identifier { get; set; }

        /// <summary>
        /// Gets or sets the domain ID.
        /// </summary>
        /// <value>The domain ID.</value>
        public string DomainId { get; set; }

        /// <summary>
        /// Gets or sets the type code.
        /// </summary>
        /// <value>The type code.</value>
        public string TypeCode { get; set; }

        /// <summary>
        /// Gets or sets the Home Community ID.
        /// </summary>
        /// <value>The Home Community ID.</value>
        public string HomeCommunityId { get; set; }

        /// <summary>
        /// Make Reference ID String.
        /// </summary>
        /// <returns>Reference Id String.</returns>
        public string MakeReferenceId()
        {
            return this.Identifier + "^^^&" + this.DomainId + "&ISO^" + this.TypeCode;
        }
    }
}
