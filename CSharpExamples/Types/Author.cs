// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="Author.cs">
// Copyright 2012 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Types
{
    using System.Collections.Generic;

    using GEHealthcare.Isip;

    /// <summary>
    /// Author entity.
    /// </summary>
    public class Author
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Author"/> class.
        /// </summary>
        public Author()
        {
            this.Institutions = new List<string>();
            this.Roles = new List<string>();
            this.Specialties = new List<string>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Author"/> class.
        /// </summary>
        /// <param name="author">Author entity.</param>
        public Author(Isip.Xds.Types.Author author) : this()
        {
            if (author == null) return;
            this.Name = author.Name;
            author.Institutions.ForEach(x => this.Institutions.Add(x));
            author.Roles.ForEach(x => this.Roles.Add(x));
            author.Specialties.ForEach(x => this.Specialties.Add(x));
        }

        /// <summary>
        /// Gets or sets Author Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets Institutions. 
        /// </summary>
        public ICollection<string> Institutions { get; private set; }

        /// <summary>
        /// Gets Roles.
        /// </summary>
        public ICollection<string> Roles { get; private set; }

        /// <summary>
        /// Gets Specialties.
        /// </summary>
        public ICollection<string> Specialties { get; private set; }
    }
}
