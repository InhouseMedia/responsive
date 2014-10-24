﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ResponsiveDocuments : DbContext
    {
        public ResponsiveDocuments()
            : base("name=ResponsiveDocuments")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<DocumentsView> DocumentsView { get; set; }
    
        public virtual ObjectResult<Documents_Add_Result> Documents_Add(string filename, byte[] filedata)
        {
            var filenameParameter = filename != null ?
                new ObjectParameter("filename", filename) :
                new ObjectParameter("filename", typeof(string));
    
            var filedataParameter = filedata != null ?
                new ObjectParameter("filedata", filedata) :
                new ObjectParameter("filedata", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Documents_Add_Result>("Documents_Add", filenameParameter, filedataParameter);
        }
    
        public virtual int Documents_Del(Nullable<System.Guid> docId)
        {
            var docIdParameter = docId.HasValue ?
                new ObjectParameter("docId", docId) :
                new ObjectParameter("docId", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Documents_Del", docIdParameter);
        }
    }
}
