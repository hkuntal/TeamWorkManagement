using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
namespace TeamWorkManagement.Models
{
    public class TeamMgmtModel
    {
        public TeamMgmtModel()
        {

        }
        public TeamMgmtDBEntities ObjTeamMgmtDBEntities { get; set; }

        public SelectList RolesSelectList { get; set; }

        public Expression CheckBoxList
        {
            get
            {
                Expression<Func<DbSet>> exp = () => ObjTeamMgmtDBEntities.LkpRoles;
                return exp;
            }
            
        }
        public bool Ruby
        {
            get
            {
                return true;
            }
            
        }
    }
}