using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
//using Responsive.Helpers;

namespace Responsive.Controllers
{
    public class BaseController : Controller
	{
		//public ConfigClass.ConfigObject Config { get; set; }

		protected override void Initialize(RequestContext requestContext)
		{
			//base.Initialize(requestContext);
			//ViewBag.Config = ConfigClass.Settings;
		}
	}
}