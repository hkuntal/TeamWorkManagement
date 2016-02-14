// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="DeleteConditionRequest.cs">
// Copyright © 2014 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Request
{
    public class DeleteConditionRequest
    {
        public DeleteConditionRequest(string conditionName, string userId)
        {
            ConditionName = conditionName;
            UserId = userId;
        }

        public string ConditionName { get; private set; }

        public string UserId { get; private set; }

        public override string ToString()
        {
            return string.Format("ConditionName: {0}; UserId: {1}", this.ConditionName, UserId);
        }
    }
}
