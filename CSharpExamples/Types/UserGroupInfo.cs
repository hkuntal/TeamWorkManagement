// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserGroupInfo.cs" company="General Electric">
//   Copyright 2012 GE Healthcare
// </copyright>
// <summary>
//   Defines the Business object for user related information from the Groups user belongs to
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace GEHealthcare.ZFP.Model.Types
{
    public class UserGroupInfo
    {
        private readonly IList<WorkList> lstWorkList = new List<WorkList>();

        public UserGroupInfo()
        {
        }

        public UserGroupInfo(IList<WorkList> workLists, int timeout)
        {
            lstWorkList = workLists;
            SessionTimeout = timeout;
        }

        public IList<WorkList> WorkLists
        {
            get { return lstWorkList; }
        }

        public int SessionTimeout { get; set; }
    }
}
