//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TeamWorkManagement.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class LkpStatu
    {
        public LkpStatu()
        {
            this.WorkHistories = new HashSet<WorkHistory>();
        }
    
        public byte Id { get; set; }
        public string Status { get; set; }
    
        public virtual ICollection<WorkHistory> WorkHistories { get; set; }
    }
}
