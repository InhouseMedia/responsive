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
    
    public partial class Navigation_Content
    {
        public int Id { get; set; }
        public Nullable<int> Navigation_Id { get; set; }
        public string Url { get; set; }
        public string On_Click { get; set; }
    
        public virtual Navigation Navigation { get; set; }
    }
}
