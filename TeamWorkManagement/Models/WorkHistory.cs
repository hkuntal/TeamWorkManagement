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
    
    public partial class WorkHistory
    {
        public int Id { get; set; }
        public byte MemberId { get; set; }
        public int ObjectId { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> FinishDate { get; set; }
        public Nullable<byte> Status { get; set; }
    
        public virtual LkpStatu LkpStatu { get; set; }
        public virtual TeamMember TeamMember { get; set; }
    }
}
