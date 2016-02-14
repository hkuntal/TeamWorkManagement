// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="ClientConditionRule.cs">
// Copyright 2014 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Types
{
    using System.Collections.Generic;

    /// <summary>
    /// The Client Condition Rule.
    /// </summary>
    public class ClientConditionRule
    {
        /// <summary>
        /// The values.
        /// </summary>
        private readonly IList<string> values = new List<string>();

        /// <summary>
        /// Gets or sets the field.
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Gets or sets the operator.
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// Gets the values.
        /// </summary>
        public IList<string> Values
        {
            get { return this.values; }
        }

        /// <summary>
        /// The add value.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        public void AddValue(string value)
        {
            this.values.Add(value);
        }
    }
}
