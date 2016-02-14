// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="UserConditionRequest.cs">
// Copyright © 2014 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Request
{
    using System.Collections.Generic;

    public class UserConditionRequest
    {
        public UserConditionRequest(IList<string> conditionNames, string userId)
        {
            ConditionNames = conditionNames;
            UserId = userId;
        }

        public string UserId { get; private set; }

        public IList<string> ConditionNames { get; private set; }

        public override string ToString()
        {
            return string.Format("ConditionNames: {0}; UserId: {1}", string.Join(",", this.ConditionNames), UserId);
        }
    }
}
