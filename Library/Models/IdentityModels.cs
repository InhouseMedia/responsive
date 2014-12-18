namespace Library.Models
{
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Security.Claims;
	using System.Threading.Tasks;
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;


	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	// You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
			: base("Library", throwIfV1Schema: false)
        {
        }
		/*
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			
			modelBuilder.Entity<Article_PublishLogs>()
			.HasKey(e => e.Published_By);
			
			modelBuilder.Entity<Article_PublishLogs>()
						.Property(e => e.Published_By)
						.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
			
			modelBuilder.Entity<Article_PublishLogs>()
						.HasRequired(e => e.User);
			
			base.OnModelCreating(modelBuilder);
		}
		*/
		public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}