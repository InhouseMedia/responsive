﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Library.Helpers
{
	class HtmlPrefixHelper
	{
	}

	public static class HtmlPrefixScopeExtensions
	{
		public static IDisposable BeginHtmlFieldPrefixScope(this HtmlHelper html, string htmlFieldPrefix)
		{
			return new HtmlFieldPrefixScope(html.ViewData.TemplateInfo, htmlFieldPrefix);
		}

		private class HtmlFieldPrefixScope : IDisposable
		{
			private readonly TemplateInfo templateInfo;
			private readonly string previousHtmlFieldPrefix;

			public HtmlFieldPrefixScope(TemplateInfo templateInfo, string htmlFieldPrefix)
			{
				this.templateInfo = templateInfo;

				previousHtmlFieldPrefix = templateInfo.HtmlFieldPrefix;
				templateInfo.HtmlFieldPrefix = htmlFieldPrefix;
			}

			public void Dispose()
			{
				templateInfo.HtmlFieldPrefix = previousHtmlFieldPrefix;
			}
		}
	}
}
