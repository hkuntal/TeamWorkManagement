using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceDebug
{
    class Class1
    {
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Config
    {

        private ConfigWorklist[] worklistsField;

        private ConfigEntry[] entriesField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Worklist", IsNullable = false)]
        public ConfigWorklist[] Worklists
        {
            get
            {
                return this.worklistsField;
            }
            set
            {
                this.worklistsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Entry", IsNullable = false)]
        public ConfigEntry[] Entries
        {
            get
            {
                return this.entriesField;
            }
            set
            {
                this.entriesField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ConfigWorklist
    {

        private ConfigWorklistFilter[] filtersField;

        private string idField;

        private string typeField;

        private string nameField;

        private string valueField;

        private bool activeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Filter", IsNullable = false)]
        public ConfigWorklistFilter[] Filters
        {
            get
            {
                return this.filtersField;
            }
            set
            {
                this.filtersField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool Active
        {
            get
            {
                return this.activeField;
            }
            set
            {
                this.activeField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ConfigWorklistFilter
    {

        private string typeField;

        private string valueField;

        private ushort optionalValueField;

        private bool optionalValueFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort OptionalValue
        {
            get
            {
                return this.optionalValueField;
            }
            set
            {
                this.optionalValueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool OptionalValueSpecified
        {
            get
            {
                return this.optionalValueFieldSpecified;
            }
            set
            {
                this.optionalValueFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ConfigEntry
    {

        private byte sessionTimeoutField;

        private bool sessionTimeoutFieldSpecified;

        private string[] worklistField;

        private string typeField;

        private string nameField;

        /// <remarks/>
        public byte SessionTimeout
        {
            get
            {
                return this.sessionTimeoutField;
            }
            set
            {
                this.sessionTimeoutField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SessionTimeoutSpecified
        {
            get
            {
                return this.sessionTimeoutFieldSpecified;
            }
            set
            {
                this.sessionTimeoutFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Worklist")]
        public string[] Worklist
        {
            get
            {
                return this.worklistField;
            }
            set
            {
                this.worklistField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }


}
