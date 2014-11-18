namespace Library.Helpers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	class AccountHelper
	{

	}

	public class UserRoles
	{
		public string Id { get; set; }
		public string UserName { get; set; }
		public bool Admin { get; set; }
		public bool Author { get; set; }
		public bool Contributors { get; set; }
		public bool Editor { get; set; }
		public bool Manager { get; set; }
		public bool Moderator { get; set; }
		public bool Viewer { get; set; }

		/*		
		admin		= Manage everything
		manager		= Manage most aspects of the site
		editor		= Scheduling and managing content
		author		= Write important content
		contributors= Authors with limited rights
		moderator	= Moderate user content
		member		= Special user access
		subscriber	= Paying Average Joe
		user		= Average Joe
		*/

	}
}
