//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Library.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Article_PublishLogs
    {
        public int Id { get; set; }
        public Nullable<int> Article_Id { get; set; }
        public string Published_By { get; set; }
        public System.DateTime Published_Date { get; set; }
    
        public virtual Article Article { get; set; }
        public virtual AspNetUsers AspNetUsers { get; set; }
    }
}
