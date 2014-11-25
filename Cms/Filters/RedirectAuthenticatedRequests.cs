namespace Cms.Filters
{
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Infrastructure;
	using System.Threading;
	using System.Web.Routing;
	using System.Web.Mvc;

	public class RedirectAuthenticatedRequests : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (filterContext.HttpContext.Request.IsAuthenticated)
			{
				filterContext.Result = new RedirectToRouteResult(
					new RouteValueDictionary(new
					{
						controller = "Home",
						action = "Index"
					}
				));
			}

			base.OnActionExecuting(filterContext);
		}
	}
}