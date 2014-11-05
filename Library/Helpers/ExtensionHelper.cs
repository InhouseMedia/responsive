namespace Library.Helpers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Reflection.Emit;
	using System.Web;
	using System.Web.Mvc;
	using System.Web.Mvc.Html;
	using System.Text;
	using Newtonsoft.Json;
	
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

		public static MvcHtmlString BootstrapValidationMessageFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object attributes = null)
		{
			string propertyName = ExpressionHelper.GetExpressionText(expression);
			string name = htmlHelper.AttributeEncode(htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldName(propertyName));

			string errorText = "";

			if (htmlHelper.ViewData.ModelState[name] != null &&
				htmlHelper.ViewData.ModelState[name].Errors != null &&
				htmlHelper.ViewData.ModelState[name].Errors.Count > 0)
			{
				//return MvcHtmlString.Empty;
				errorText = htmlHelper.ViewData.ModelState[name].Errors[0].ErrorMessage;
			}

			object standardObj = new
			{
				@class = "text-danger field-validation-error glyphicon glyphicon-remove form-control-feedback",
				@data_toggle = "popover",
				//@title = name,
				@data_content = errorText
			};

			var standardAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(standardObj) as IDictionary<string, object>;
			var pushedAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(attributes) as IDictionary<string, object>;

			var htmlAttributes = HtmlAttributesForBootstrap(standardAttributes, pushedAttributes);

			string htmlEncode = htmlHelper.ValidationMessage(name, htmlAttributes).ToString();
			return new MvcHtmlString(htmlEncode);
		}

		public static MvcHtmlString BootstrapEditorFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object attributes = null)
		{
			bool isError = (htmlHelper.ValidationErrorFor(expression, "has-error") == null)? false: true;

			object standardObj = new
			{
				@class = "form-control",
				@placeholder = htmlHelper.WatermarkFor(expression),
				//@autoFocus = false,
			};

			var standardAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(standardObj) as IDictionary<string, object>;
			var pushedAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(attributes) as IDictionary<string, object>;

			//totalAttributes = standardAttributes.Union(errorAttributes).ToDictionary(k => k.Key, v => v.Value)
			//var htmlAttributes = HtmlAttributesForBootstrap(totalAttributes, pushedAttributes);
			var htmlAttributes = HtmlAttributesForBootstrap(standardAttributes, pushedAttributes);

			string original = htmlHelper.EditorFor(expression, new { htmlAttributes }).ToString();
			string errorLabel = htmlHelper.BootstrapValidationMessageFor(expression).ToString();
			
			// Styling of the elements
			TagBuilder group = new TagBuilder("div");
			group.AddCssClass("form-group has-feedback");
			group.AddCssClass((isError) ? "has-error" : "");

			TagBuilder col = new TagBuilder("div");
			col.AddCssClass("col-md-12");

			col.InnerHtml += original + errorLabel;
			group.InnerHtml+= col;

			return MvcHtmlString.Create(group.ToString());
		}

		private static IDictionary<string, object> HtmlAttributesForBootstrap(IDictionary<string, object> standardAttr, IDictionary<string, object> pushedAttr = null)
		{
			//var htmlAttributes = new Dictionary<string, object>();
			List<string> merge = new List<string>(){"class"};

			if (pushedAttr != null) foreach (var attr in pushedAttr)
				{
					// Replace Attributes
					if (!merge.Contains(attr.Key) && standardAttr.ContainsKey(attr.Key)){
						standardAttr[attr.Key] = attr.Value.ToString();
					}
					// Merge Attributes
					else if (merge.Contains(attr.Key) && standardAttr.ContainsKey(attr.Key)){
						var tmp = standardAttr[attr.Key].ToString();
						standardAttr[attr.Key] = tmp + attr.Value.ToString();
					}
					// Add Attributes
					else {
						standardAttr.Add(attr.Key, attr.Value);
					}
				}

			return standardAttr;
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


		private static MvcHtmlString WatermarkFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
		{
			var watermark = ModelMetadata.FromLambdaExpression(expression, html.ViewData).Watermark;
			var htmlEncoded = HttpUtility.HtmlEncode(watermark);
			return new MvcHtmlString(htmlEncoded);
		}
	}

	 
}