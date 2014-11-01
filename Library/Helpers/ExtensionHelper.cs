namespace Library.Helpers
{
	using System;
	//using System.Collections.Generic;
	using System.Linq.Expressions;
	using System.Web;
	using System.Web.Mvc;
	using System.Text;
	
	public static class HtmlExtensions
	{
		public static MvcHtmlString Image(this HtmlHelper html, byte[] image)
		{
			var img = String.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(image));
			return new MvcHtmlString("<img src='" + img + "' />");
		}

		public static MvcHtmlString ValidationErrorFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string error)
		{
			if (HasError(htmlHelper, 
						ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData), 
						ExpressionHelper.GetExpressionText(expression)
						)
				)
				return new MvcHtmlString(error);
			else
				return null;
			 
		}

		private static bool HasError(this HtmlHelper htmlHelper, ModelMetadata modelMetadata, string expression)
		{
			string modelName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(expression);
			FormContext formContext = htmlHelper.ViewContext.FormContext;
			if (formContext == null)
				return false;

			if (!htmlHelper.ViewData.ModelState.ContainsKey(modelName))
				return false;

			ModelState modelState = htmlHelper.ViewData.ModelState[modelName];
			if (modelState == null)
				return false;

			ModelErrorCollection modelErrors = modelState.Errors;
			if (modelErrors == null)
				return false;

			return (modelErrors.Count > 0);
		}
	}


	public static class WatermarkExtension
	{
		public static MvcHtmlString WatermarkFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
		{
			var watermark = ModelMetadata.FromLambdaExpression(expression, html.ViewData).Watermark;
			var htmlEncoded = HttpUtility.HtmlEncode(watermark);
			return new MvcHtmlString(htmlEncoded);
		}
	}

	 
}