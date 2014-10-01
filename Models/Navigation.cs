//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Responsive.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Navigation
    {
        public Navigation()
        {
            this.Priority = 0.5D;
            this.Navigation_ChangeLogs = new HashSet<Navigation_ChangeLogs>();
            this.Navigation_PublishLogs = new HashSet<Navigation_PublishLogs>();
        }
    
        public int NavigationId { get; set; }
        public int Article_Id { get; set; }
        public string Url { get; set; }
        public string On_Click { get; set; }
        public string Level { get; set; }
        public double Priority { get; set; }
        public byte Active { get; set; }
        public int Created_By { get; set; }
        public System.DateTime Creation_Date { get; set; }
    
        public virtual ICollection<Navigation_ChangeLogs> Navigation_ChangeLogs { get; set; }
        public virtual ICollection<Navigation_PublishLogs> Navigation_PublishLogs { get; set; }
    }
}
