namespace Cms.Controllers
{
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	using Microsoft.AspNet.Identity.Owin;
	using System;
	using System.Collections.Generic;
	using System.Linq;
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
		[Authorize(Roles = "Admin,Manager")]
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

			var currentAdminRole = User.IsInRole(AdminRole.Name);
			var currentManagerRole = User.IsInRole(ManagerRole.Name);

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
			columns.Add(new WebGridColumn() { ColumnName = "UserName", Header = "Name", CanSort = true });
			foreach (var role in roles) {
				var disableCheckBox1 = (role.Name == AdminRole.Name && !currentAdminRole);

				columns.Add(new WebGridColumn()
				{
					ColumnName = role.Name,
					Header = role.Name,
					CanSort = true,
					Style = "text-center",
					Format = (item) =>
					{
						//Managers should not be able to add or remove Admin rights
						bool disableCheckBox = (!currentAdminRole && item["Admin"] == true)? true :disableCheckBox1;
						return new HtmlString("<input type='checkbox' name='" + item.Id + "' value='" + role.Name + "' " + (item[role.Name] ? "checked" : "") + (disableCheckBox ? " disabled": "") + "/>");
					}
				});
			}
			columns.Add(new WebGridColumn() 
			{ 
				ColumnName = "Delete", 
				Header = "Delete", 
				Style = "text-center", 
				Format = (item) => 
				{
					//Managers should not be able to add or remove Admin rights (or remove their own account)
					bool disableCheckBox = ((!currentAdminRole && !currentManagerRole) || (!currentAdminRole && item["Admin"] == true) || item.Id == User.Identity.GetUserId());
					return new HtmlString("<a href='/Account/Delete/" + item.Id + "' class='btn btn-default btn-xs" + (disableCheckBox ? " disabled" : "") + "' data-toggle='modal' data-target='#deleteModalSmall'><i class='glyphicon glyphicon-trash'></i></a>"); 
				} 
			});
			ViewBag.Columns = columns;

			return View(data);
		}

		[HttpPost]
		[Authorize(Roles = "Admin,Manager")]
		[ValidateAntiForgeryToken]
		public JsonResult Change(FormCollection model)
		{
			IValueProvider valueProvider = model.ToValueProvider();
			List<IdentityRole> roles = getRoles();
			
			foreach (string key in model.Keys)
			{
				ValueProviderResult result = valueProvider.GetValue(key);
				string[] selectedRoles = result.AttemptedValue.Split(',');

				ApplicationUser user = context.Users.Where(u => u.Id.Equals(key)).FirstOrDefault();

				if (user != null)
				{
					var currentRoles = roles.Where(r => user.Roles.Any(x => x.RoleId.Contains(r.Id))).Select(s => s.Name).ToList();
					var changedRoles = roles.Where(r => selectedRoles.Any(x => x.Contains(r.Name))).Select(s => s.Name).ToList();

					string[] removeRoles = currentRoles.Except(changedRoles).ToArray();
					string[] addRoles = changedRoles.Except(currentRoles).ToArray();

					// Filter admin roles when user isn't authorized for it.
					// (they could enable/disable Admin checkboxes by using developer tools)
					if (!User.IsInRole("Admin"))
					{
						int foundRemoveId = Array.IndexOf(removeRoles, "Admin");
						int foundAddId = Array.IndexOf(addRoles, "Admin");

						if (foundRemoveId > -1 || foundAddId > -1)
						{
							ModelState.AddModelError("", "You don't have rights to add or remove Admin roles to a specific user.");
							Response.StatusCode = 400;
							Response.TrySkipIisCustomErrors = true;
						}

						//removeRoles = removeRoles.Where((val, idx) => idx != foundRemoveId).ToArray();
						//addRoles = addRoles.Where((val, idx) => idx != foundAddId).ToArray();						
					}

					if (ModelState.IsValid) {
						if (removeRoles.Length > 0)
							UserManager.RemoveFromRoles(user.Id, removeRoles);

						if (addRoles.Length > 0)
							UserManager.AddToRoles(user.Id, addRoles);
					}
					
				}				
			}

			return Json(ModelState.Values.SelectMany(x => x.Errors));
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

		private List<IdentityRole> getRoles() {
			
			return context.Roles.ToList();
		}
    }
}