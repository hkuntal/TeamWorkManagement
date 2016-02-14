// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserInfo.cs" company="Copyright 2012 General Electric Company">
//   Copyright statement. All right reserved
// </copyright> 
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace GEHealthcare.ZFP.Model.Types
{
    public class UserInfo
    {
        public bool IsActive { get; set; }

        public bool IsSuperUser { get; set; }

        public int Id { get; set; }

        public int ClusterId { get; set; }

        public int DepartmentId { get; set; }

        public int FacilityId { get; set; }

        public int AuthorityId { get; set; }

        public DateTime LastLogOn { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string InterfaceCode { get; set; }

        public string Suffix { get; set; }

        public string Prefix { get; set; }

        public string UserName { get; set; }

        public string EmailAddress { get; set; }

        public string Identifier { get; set; }
    }
}
