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
    
    public partial class Article
    {
        public Article()
        {
            this.Article_ChangeLogs = new HashSet<Article_ChangeLogs>();
            this.Article_Content = new HashSet<Article_Content>();
            this.Article_Metadata = new HashSet<Article_Metadata>();
            this.Article_PublishLogs = new HashSet<Article_PublishLogs>();
        }
    
        public int Article_Id { get; set; }
        public string Template { get; set; }
        public byte Active { get; set; }
        public string Created_By { get; set; }
        public System.DateTime Creation_Date { get; set; }
    
        public virtual ICollection<Article_ChangeLogs> Article_ChangeLogs { get; set; }
        public virtual ICollection<Article_Content> Article_Content { get; set; }
        public virtual ICollection<Article_Metadata> Article_Metadata { get; set; }
        public virtual ICollection<Article_PublishLogs> Article_PublishLogs { get; set; }
        public virtual AspNetUsers AspNetUsers { get; set; }
    }
}
