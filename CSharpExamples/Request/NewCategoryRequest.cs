// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="NewCategoryRequest.cs">
// Copyright © 2014 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Request
{
    public class NewCategoryRequest
    {
        public NewCategoryRequest(string name, string displayName)
        {
            Name = name;
            DisplayName = displayName;
        }

        public string Name { get; private set; }

        public string DisplayName { get; private set; }
    }
}
