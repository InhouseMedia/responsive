namespace Library.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.ModelConfiguration.Conventions;
	using System.Data.Entity;
	using Microsoft.AspNet.Identity.EntityFramework;

	class ResponsiveForeignKey
	{
	}

	public partial class Article_PublishLogs
	{
		/*
		public Article_PublishLogs()
		{
			this.ApplicationUsers = new HashSet<ApplicationUser>();
			
		}
		//[ForeignKey("Published_By")]
		public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
		
		[ForeignKey("Published_By")]
		public virtual ApplicationUser User { get; set; }
		 */
	}


}
