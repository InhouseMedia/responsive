namespace Cms.Controllers
{
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	using Microsoft.AspNet.Identity.Owin;
	using Microsoft.Owin.Security;
	using System;
	using System.Collections.Generic;
	using System.Dynamic;
	using System.Linq;
	using System.Threading.Tasks;
	using System.Web;
	using System.Web.Helpers;
	using System.Web.Mvc;

	using Library.Models;
	using Library.Helpers;

	[Authorize]
    public class RolesController : Controller
    {
		ApplicationDbContext context = new ApplicationDbContext();


		private ApplicationUserManager _userManager;

        public RolesController()
        {
        }

		public RolesController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
           // SignInManager = signInManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


		// GET: Users
		[Authorize(Roles = "Admin")]
		public ActionResult Users()
		{
			var roles = context.Roles;
			var users = context.Users;

			IdentityRole AdminRole = roles.FirstOrDefault(r => r.Name == "Admin");
			IdentityRole AuthorRole = roles.FirstOrDefault(r => r.Name == "Author");
			IdentityRole ContributorsRole = roles.FirstOrDefault(r => r.Name == "Contributors");
			IdentityRole EditorRole = roles.FirstOrDefault(r => r.Name == "Editor");
			IdentityRole ManagerRole = roles.FirstOrDefault(r => r.Name == "Manager");
			IdentityRole ModeratorRole = roles.FirstOrDefault(r => r.Name == "Moderator");
			IdentityRole ViewerRole = roles.FirstOrDefault(r => r.Name == "Viewer");

			List<UserRoles> data = new List<UserRoles>();
			
			foreach (ApplicationUser user in users)
			{
				List<IdentityUserRole> userRoles = user.Roles.ToList();
				

				data.Add(
					new UserRoles(){
						Id = user.Id,
						UserName = user.UserName,
						Admin = (userRoles.Any(r => r.RoleId == AdminRole.Id)),
						Author = (userRoles.Any(r => r.RoleId == AuthorRole.Id)),
						Contributors = (userRoles.Any(r => r.RoleId == ContributorsRole.Id)),
						Editor = (userRoles.Any(r => r.RoleId == EditorRole.Id)),
						Manager = (userRoles.Any(r => r.RoleId == ManagerRole.Id)),
						Moderator = (userRoles.Any(r => r.RoleId == ModeratorRole.Id)),
						Viewer = (userRoles.Any(r => r.RoleId == ViewerRole.Id))
					}
				);
			}


			List<WebGridColumn> columns = new List<WebGridColumn>();
			//columns.Add(new WebGridColumn() { ColumnName = "Id", Header = "Id", CanSort = true });
			columns.Add(new WebGridColumn() { ColumnName = "UserName", Header = "Name", CanSort = true });
			foreach (var role in roles) {
				columns.Add(new WebGridColumn()
				{
					ColumnName = role.Name,
					Header = role.Name,
					CanSort = true,
					Style = "text-center",
					Format = (item) =>
					{
						return new HtmlString("<input type='checkbox' value=''"+(item[role.Name]? "checked" : "") +"/>");
					}
				});
			}
			ViewBag.Columns = columns;

			return View(data);
		}

        // GET: Role
		//[RoleAuthorization(Roles = "Admin")]
		public ActionResult Index()
		{
			var roles = context.Roles.ToList();
			return View(roles);
		}

		// GET: /Roles/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: /Roles/Create
		[HttpPost]
		public ActionResult Create(FormCollection collection)
		{
			try
			{
				context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
				{
					Name = collection["RoleName"]
				});
				context.SaveChanges();
				ViewBag.ResultMessage = "Role created successfully !";
				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

        //
		// GET: /Roles/Edit/5
		public ActionResult Edit(string roleName)
		{
			var thisRole = context.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

			return View(thisRole);
		}

		// POST: /Roles/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Microsoft.AspNet.Identity.EntityFramework.IdentityRole role)
		{
			try
			{
				context.Entry(role).State = System.Data.Entity.EntityState.Modified;
				context.SaveChanges();

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

        //
        // GET: /Roles/Delete/5
        public ActionResult Delete(string RoleName)
        {
            var thisRole = context.Roles.Where(r => r.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            context.Roles.Remove(thisRole);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ManageUserRoles()
        {
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAddToUser(string UserName, string RoleName)
        {
            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            //var account = new AccountController();
            UserManager.AddToRole(user.Id, RoleName);
            
            ViewBag.ResultMessage = "Role created successfully !";
            
            // prepopulat roles for the view dropdown
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;   

            return View("ManageUserRoles");
        }
		
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetRoles(string UserName)
        {            
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
               // var account = new AccountController();

                ViewBag.RolesForThisUser = UserManager.GetRoles(user.Id);

                // prepopulat roles for the view dropdown
                var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = list;            
            }

            return View("ManageUserRoles");
        }
		
		/*
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> GetRoles(string UserName)
		{

			if (!string.IsNullOrWhiteSpace(UserName))
			{
				ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

				var roles = await UserManager.GetRoles(user.Id);
				ViewBag.RolesForThisUser = roles ;
			}
			return View("ManageUserRoles");
		}
		*/


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleForUser(string UserName, string RoleName)
        {
            var account = new AccountController();
            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            if (account.UserManager.IsInRole(user.Id, RoleName))  
            {
                account.UserManager.RemoveFromRole(user.Id, RoleName);
                ViewBag.ResultMessage = "Role removed from this user successfully !";
            }
            else
            {
                ViewBag.ResultMessage = "This user doesn't belong to selected role.";
            }
            // prepopulat roles for the view dropdown
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            return View("ManageUserRoles");
        }
    }
}