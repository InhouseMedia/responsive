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
    
    public partial class AspNetUsers
    {
        public AspNetUsers()
        {
            this.AspNetUserClaims = new HashSet<AspNetUserClaims>();
            this.AspNetUserLogins = new HashSet<AspNetUserLogins>();
            this.AspNetRoles = new HashSet<AspNetRoles>();
            this.Article_PublishLogs = new HashSet<Article_PublishLogs>();
            this.Article_ChangeLogs = new HashSet<Article_ChangeLogs>();
            this.Article_Content = new HashSet<Article_Content>();
            this.Article = new HashSet<Article>();
            this.Navigation = new HashSet<Navigation>();
            this.Navigation_ChangeLogs = new HashSet<Navigation_ChangeLogs>();
            this.Navigation_PublishLogs = new HashSet<Navigation_PublishLogs>();
        }
    
        public string Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
    
        public virtual ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual ICollection<AspNetRoles> AspNetRoles { get; set; }
        public virtual ICollection<Article_PublishLogs> Article_PublishLogs { get; set; }
        public virtual ICollection<Article_ChangeLogs> Article_ChangeLogs { get; set; }
        public virtual ICollection<Article_Content> Article_Content { get; set; }
        public virtual ICollection<Article> Article { get; set; }
        public virtual ICollection<Navigation> Navigation { get; set; }
        public virtual ICollection<Navigation_ChangeLogs> Navigation_ChangeLogs { get; set; }
        public virtual ICollection<Navigation_PublishLogs> Navigation_PublishLogs { get; set; }
    }
}
