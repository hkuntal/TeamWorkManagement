// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="AddConditionRequest.cs">
// Copyright © 2014 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Request
{
    using GEHealthcare.ZFP.Model.Types;

    public class AddConditionRequest
    {
        public AddConditionRequest(ClientCondition clientCondition, string userId)
        {
            ClientCondition = clientCondition;
            UserId = userId;
        }

        public ClientCondition ClientCondition { get; private set; }

        public string UserId { get; private set; }

        public override string ToString()
        {
           return string.Format("ConditionName: {0}, UserId: {1}", this.ClientCondition != null ? this.ClientCondition.Name : string.Empty, UserId);
        }
    }
}
