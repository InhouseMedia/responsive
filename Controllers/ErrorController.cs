﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Responsive.Models;

namespace Responsive.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error404
		public ActionResult E404(Article_Content content)
		{

			return View(content);
		}
    }
}