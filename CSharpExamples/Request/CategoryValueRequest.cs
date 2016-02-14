// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="CategoryValueRequest.cs">
// Copyright © 2014 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Request
{
    public class CategoryValueRequest
    {
        public CategoryValueRequest(string categoryName, string value)
        {
            CategoryName = categoryName;
            Value = value;
        }

        public string CategoryName { get; private set; }

        public string Value { get; private set; }
    }
}
