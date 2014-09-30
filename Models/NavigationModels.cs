using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;


namespace Responsive.Models
{
    public class NavigationContext : DbContext
    {
        public NavigationContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Navigation> NavigationItems { get; set; }
    }

    [Table("Navigation")]
    public class Navigation
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Article_Id { get; set; }
        public string Url { get; set; }
        public string Level { get; set; }
        public Byte Active { get; set; }
        public int Created_By { get; set; }
        public DateTime Creation_Date { get; set; }
    }

}
