namespace Library.Helpers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Reflection.Emit;
	using System.Web;
	using System.Web.Helpers;
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

		public static ImgTag ImgTag(this HtmlHelper htmlHelper, string imagePath, string altText)
		{
			var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

			return new ImgTag(imagePath, altText, urlHelper.Content);
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
			bool isError = (htmlHelper.ValidationErrorFor(expression, "has-error") != null);

			object standardObj = new
			{
				@class = "form-control",
				@placeholder = htmlHelper.WatermarkFor(expression)
			};

			var standardAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(standardObj) as IDictionary<string, object>;
			var pushedAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(attributes) as IDictionary<string, object>;

			var htmlAttributes = HtmlAttributesForBootstrap(standardAttributes, pushedAttributes);

			string label = htmlHelper.LabelFor(expression).ToString();
			string original = htmlHelper.EditorFor(expression, new { htmlAttributes }).ToString();
			string errorLabel = htmlHelper.BootstrapValidationMessageFor(expression).ToString();
			
			// Styling of the elements
			TagBuilder group = new TagBuilder("div");
			group.AddCssClass("form-group has-feedback");
			group.AddCssClass((isError) ? "has-error" : "");

			TagBuilder col = new TagBuilder("div");
			col.AddCssClass("col-md-12");

			col.InnerHtml += (label != "" && htmlAttributes.ContainsKey("Label")) ? label : "";
			
			col.InnerHtml += original + errorLabel;
			group.InnerHtml += col;
			
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

	public class ImgTag : IHtmlString
	{
		private readonly string _imagePath;
		private readonly Func<string, string> _mapVirtualPath;
		private readonly HashSet<int> _pixelDensities;
		private readonly IDictionary<string, string> _htmlAttributes;

		public ImgTag(string imagePath, string altText, Func<string, string> mapVirtualPath)
		{
			_imagePath = imagePath;
			_mapVirtualPath = mapVirtualPath;

			_pixelDensities = new HashSet<int>();
			_htmlAttributes = new Dictionary<string, string>
        {
            { "src", mapVirtualPath(imagePath) },
            { "alt", altText }
        };
		}

		public string ToHtmlString()
		{
			var imgTag = new TagBuilder("img");

			if (_pixelDensities.Any())
			{
				AddSrcsetAttribute(imgTag);
			}

			foreach (KeyValuePair<string, string> attribute in _htmlAttributes)
			{
				imgTag.Attributes[attribute.Key] = attribute.Value;
			}

			return imgTag.ToString(TagRenderMode.SelfClosing);
		}

		private void AddSrcsetAttribute(TagBuilder imgTag)
		{
			int densityIndex = _imagePath.LastIndexOf('.');

			IEnumerable<string> srcsetImagePaths =
				from density in _pixelDensities
				let densityX = density + "x"
				let highResImagePath = _imagePath.Insert(densityIndex, "@" + densityX)
					+ " " + densityX
				select _mapVirtualPath(highResImagePath);

			imgTag.Attributes["srcset"] = string.Join(", ", srcsetImagePaths);
		}

		public ImgTag WithDensities(params int[] densities)
		{
			foreach (int density in densities)
			{
				_pixelDensities.Add(density);
			}

			return this;
		}

		public ImgTag WithSize(int width, int? height = null)
		{
			_htmlAttributes["width"] = width.ToString();
			_htmlAttributes["height"] = (height ?? width).ToString();

			return this;
		}
	}


	//Not yet used
	public static class GridExtensions
	{
		public static WebGridColumn[] RoleBasedColumns(this HtmlHelper htmlHelper,WebGrid grid)
		{
			var user = htmlHelper.ViewContext.HttpContext.User;
			var columns = new List<WebGridColumn>();

			// The Prop1 column would be visible to all users
			columns.Add(grid.Column("Prop1"));

			if (user.IsInRole("Admin"))
			{
				// The Prop2 column would be visible only to users
				// in the foo role
				columns.Add(grid.Column("Admin"));
			}
			return columns.ToArray();
		}
	}	 
}