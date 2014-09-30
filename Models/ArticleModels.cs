using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;


namespace Responsive.Models
{
    public class ArticleContext : DbContext
    {
        public ArticleContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Article> ArticleItems { get; set; }
    }

    [Table("Article")]
    public class Article
    {
        /*public Article(){
            this.ContentItems = new HashSet<Article_Content>();
        }
        */
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Article_Id { get; set; }
        public Byte Active { get; set; }
        public int Created_By { get; set; }
        public DateTime Creation_Date { get; set; }

        //public virtual ICollection<Article_Content> ContentItems { get; set; }
    }

    [Table("Article_Content")]
    public class Article_Content 
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Article_Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Meta_Title { get; set; }
        public string Meta_Keywords { get; set; }
        public string Meta_Description { get; set; }
        public Byte Active { get; set; }
        public int Created_By { get; set; }
        public DateTime Creation_Date { get; set; }
    }
}
