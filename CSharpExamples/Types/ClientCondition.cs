// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="ClientCondition.cs">
// Copyright 2014 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Types
{
    using System.Collections.Generic;

    /// <summary>
    /// The client condition.
    /// </summary>
    public class ClientCondition
    {
          /// <summary>
        /// The values.
        /// </summary>
        private readonly IDictionary<string, ClientConditionRule> values = new Dictionary<string, ClientConditionRule>();

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether private.
        /// </summary>
        public bool Private { get; set; }

        /// <summary>
        /// Gets the rules.
        /// </summary>
        public IDictionary<string, ClientConditionRule> Rules
        {
            get
            {
                return this.values;
            }
        }

        /// <summary>
        /// The add rule.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public void AddRule(string key, ClientConditionRule value)
        {
            this.values.Add(key, value);
        }
    }
 }