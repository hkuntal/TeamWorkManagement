// ----------------------------------------------------------------------------------
// <copyright file="WorkList.cs" company="Copyright 2012 General Electric Company">
//     Copyright statement. All right reserved
// </copyright>
//
// ------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Types
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Xml.Serialization;

    /// <summary>
    /// Work List types.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [Serializable]
    [XmlType(AnonymousType = true)]
    public enum WorkListType
    {
        All,

        ReferringPhysician,

        ReferringService,

        AETitle,
    }

    /// <summary>
    /// Config Class.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [Serializable]
    [System.Diagnostics.DebuggerStepThroughAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "Config")]
    public partial class WorkListConfig
    {
        /// <summary>
        /// Gets or sets the WorkList Object.
        /// </summary>
        [XmlArrayItem("Worklist", IsNullable = false)]
        public WorkList[] Worklists { get; set; }

        /// <summary>
        /// Gets or sets the Config Entries.
        /// </summary>
        [XmlArrayItem("Entry", IsNullable = false)]
        public Entry[] Entries { get; set; }
    }

    /// <summary>
    /// WorkList Properties.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Reviewed. Suppression is OK here.")]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [Serializable]
    [System.Diagnostics.DebuggerStepThroughAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType = true)]
    public partial class WorkList
    {
        /// <summary>
        /// Gets or sets the WorkList Filters.
        /// </summary>
        [XmlArrayItem("Filter", IsNullable = false)]
        public WorkListFilter[] Filters { get; set; }

        /// <summary>
        /// Gets or sets the Referring Physician.
        /// </summary>
        [XmlArrayItem("Physician", IsNullable = false)]
        public ReferringPhysician[] ReferringPhysicians { get; set; }

        [XmlArrayItem("AeTitle", IsNullable = false)]
        public AeTitle[] AeTitles { get; set; }

        /// <summary>
        /// Gets or sets the WorkList Id.
        /// </summary>
        [XmlAttribute(DataType = "ID")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the WorkList Type.
        /// </summary>
        [XmlAttribute]
        public WorkListType Type { get; set; }

        public string WorklistType { get { return this.Type.ToString(); } }

        /// <summary>
        /// Gets or sets the WorkList Name.
        /// </summary>
        [XmlAttribute]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the WorkList Value.
        /// </summary>
        [XmlAttribute]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the work list is default.
        /// </summary>
        [XmlAttribute]
        public bool IsDefault { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the work list is active.
        /// </summary>
        [XmlAttribute("Active")]
        public bool IsActive { get; set; }

        public static bool operator ==(WorkList left, WorkList right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(WorkList left, WorkList right)
        {
            return !Equals(left, right);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return this.Equals((WorkList)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = this.Id != null ? this.Id.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (int)this.Type;
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Value != null ? this.Value.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ this.IsDefault.GetHashCode();
                hashCode = (hashCode * 397) ^ this.IsActive.GetHashCode();
                return hashCode;
            }
        }

        protected bool Equals(WorkList other)
        {
            return string.Equals(this.Id, other.Id) && this.Type == other.Type && string.Equals(this.Name, other.Name)
                && string.Equals(this.Value, other.Value) && this.IsDefault.Equals(other.IsDefault)
                && this.IsActive.Equals(other.IsActive);
        }
    }

    /// <summary>
    /// Gets or sets a value for WorkList Filters.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [Serializable]
    [System.Diagnostics.DebuggerStepThroughAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType = true)]
    public partial class WorkListFilter
    {
        /// <summary>
        /// Gets or sets a value for WorkList Filter Type.
        /// </summary>
        [XmlAttribute]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the WorkList Filter Value.
        /// </summary>
        [XmlAttribute]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the period value.
        /// </summary>
        [XmlAttribute]
        public string OptionalValue { get; set; }
    }

    /// <summary>
    /// Gets or sets a value forReferring Physicians.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [Serializable]
    [System.Diagnostics.DebuggerStepThroughAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType = true)]
    public partial class ReferringPhysician
    {
        /// <summary>
        /// Gets or sets the Physician Value.
        /// </summary>
        [XmlAttribute]
        public string Value { get; set; }
    }

    /// <summary>
    /// Config Entry.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [Serializable]
    [System.Diagnostics.DebuggerStepThroughAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType = true)]
    public partial class Entry
    {
        /// <summary>
        /// Gets or sets the WorkList Collection.
        /// </summary>
        [XmlElement("Worklist", DataType = "IDREF")]
        public string[] Worklist { get; set; }

        /// <summary>
        /// Gets or sets the Session timeout value based on UserGroup.
        /// </summary>
        [XmlAttribute]
        public int SessionTimeout { get; set; }

        /// <summary>
        /// Gets or sets the WorkList Type.
        /// </summary>
        [XmlAttribute]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the WorkList Name.
        /// </summary>
        [XmlAttribute]
        public string Name { get; set; }
    }

    /// <summary>
    /// The AE title.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [Serializable]
    [System.Diagnostics.DebuggerStepThroughAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType = true)]
    public partial class AeTitle
    {
        /// <summary>
        /// Gets or sets the Archive Value.
        /// </summary>
        [XmlAttribute]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the Archive Value.
        /// </summary>
        [XmlAttribute]
        public string EaServer { get; set; }
    }
}